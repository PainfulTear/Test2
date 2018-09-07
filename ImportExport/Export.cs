using System;
using System.Collections.Generic;
using System.Text;
using ElisDB;
using LangStuffs;
using System.IO;
using DeviceStuffs;
using System.Globalization;
using System.Security.Cryptography;

namespace Elis.ImportExport
{
    /// <summary>
    /// Экспорт БД
    /// </summary>
    public class Export
    {
        public Export()
        {
        }


        /// <summary>
        /// Экспорт в CSV
        /// </summary>
        /// <param name="dbType">Укажите DBType.Any для экспорта всех типов приборов</param>
        /// <param name="catType">Укажите CategoryType.Any для экспорта всех типов категорий</param>
        /// <param name="fileName"></param> 
        /// <param name="enc"></param>
        /// <param name="separator"></param>
        public void ExportToCSV(DBType dbType, CategoryType catType, string fileName, Encoding enc, char separator)
        {
            StringBuilder warnings = new StringBuilder();
            Dictionary<ProgramId, ProgramId> exportedProgramIds = new Dictionary<ProgramId, ProgramId>();
            using (StreamWriter sw = new StreamWriter(fileName, false, enc))
            {
                foreach (var db in DB.dbTypes)
                if (dbType == db || dbType == DBType.Any)
                {
                    foreach (var cat in DB.catTypes)        
                        if (catType == cat || catType == CategoryType.Any)
                            ExportToCSV(db, cat, sw, separator, warnings, exportedProgramIds);

                    //экспортируем категории без типа
                    if(catType == CategoryType.Any)
                        ExportToCSV(db, CategoryType.None, sw, separator, warnings, exportedProgramIds);
                }
            }

            if (warnings.Length > 0)
                throw new NonCriticalException(Lang.Phrase("Экспорт выполнен, со следующими замечаниями:\r\n{0}", warnings.ToString()));
        }

        private void ExportToCSV(DBType dbType, CategoryType catType, StreamWriter sw, char separator, StringBuilder warnings, Dictionary<ProgramId, ProgramId> exportedProgramIds)
        {
            //перебираем типы приборов
            foreach(var device in DB.devices)
            {
                //перебираем все категории для данного типа приброра и для данного типа категорий
                Categories cats = (Categories)DB.FileDict[dbType, ItemType.Category, device];

                if(cats!=null)
                foreach (var cat in cats.Values)
                    if (cat.Type == catType)
                        ExportCategoryToCSV(cat, device, dbType, catType, sw, separator, warnings, exportedProgramIds);
            }
        }

        private void ExportCategoryToCSV(Category cat, DeviceType device, DBType dbType, CategoryType catType, StreamWriter sw, char separator, StringBuilder warnings, Dictionary<ProgramId, ProgramId> exportedProgramIds)
        {
            string deviceId = DeviceTypeToString(device);
            int enLCID = Lang.ISONameToLCID("en");
            int ruLCID = Lang.ISONameToLCID("ru");

            //пишем имена на разных языках
            StringBuilder sb = new StringBuilder();
            var name = cat.GetName();
            
            //пишем имя на русском
            {
                var ruName = name.Get(ruLCID);
                if (name != null)
                    sb.AppendFormat("{1}{0}{0}", separator, PrepareString(ruName, separator));
                else
                    sb.AppendFormat("{0}{0}");
            }
            //ищем все языки
            foreach(var locName in name.Items)
            if(locName.LCID != ruLCID)//русски уже записали
            if(!string.IsNullOrEmpty(locName.value))
                sb.AppendFormat("{1}{0}{0}", separator, PrepareString("#"+Lang.LCIDtoISOName(locName.LCID).ToUpper() +" " +locName.value,separator));
            //пишем хидер категории
            sw.Write(string.Format("$${1}{0}", separator, deviceId));
            sw.Write(PrepareString(string.Format("{0} [{1},{2}]", name.Get(enLCID), dbType, catType), separator)+separator);
            sw.Write(string.Format("{2}/$${1}" + Environment.NewLine, separator, deviceId, sb.ToString()));
            //экспортируем программы
            foreach(var prog in cat.ProgIds)
                ExportProgramToCSV(prog, sw, separator, warnings, exportedProgramIds);
        }

        private void ExportProgramToCSV(ProgramId progId, StreamWriter sw, char separator, StringBuilder warnings, Dictionary<ProgramId, ProgramId> exportedProgramIds)
        {
            string deviceId = DeviceTypeToString(progId.device);
            int enLCID = Lang.ISONameToLCID("en");
            int ruLCID = Lang.ISONameToLCID("ru");
            StringBuilder sb = new StringBuilder();

            var enName = DB.GetProgramName(progId, enLCID);
            var enNameStr = "";
            if(enName!=null)
                enNameStr = enName.FullName;
            sb.Append(string.Format("${1}{0}{2}{0}", separator, deviceId, PrepareString(string.Format("{0} [{1}]", enNameStr, progId.id), separator)));
            //если программа еще не экспортировалась, пишем ее имена и частоты
            if(!exportedProgramIds.ContainsKey(progId))
            {
                //пишем имена на русском
                {
                    var name = DB.GetProgramName(progId, ruLCID);
                    if (name != null)
                        sb.AppendFormat("{1}{0}{2}{0}", separator, PrepareString(name.FullName + (string.IsNullOrEmpty(name.Description) ? "" : Environment.NewLine + name.Description), separator), PrepareString(name.ShortName, separator));
                    else
                        sb.AppendFormat("{0}{0}");
                }
                //пишем имена на разных языках
                //ищем все языки
                foreach (var cult in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
                {
                    if (cult.LCID == ruLCID)
                        continue;//русский уже записали
                    var name = DB.GetProgramName(progId, cult.LCID);
                    if (name != null)
                        sb.AppendFormat("{1}{0}{2}{0}", separator, PrepareString("#" + Lang.LCIDtoISOName(cult.LCID).ToUpper() + " " + name.FullName + (string.IsNullOrEmpty(name.Description) ? "" : Environment.NewLine + name.Description), separator), PrepareString(name.ShortName, separator));
                }

                //пишем частоты
                var data = DB.GetProgramData(progId);
                if (data != null)
                {
                    //если данные зашифрованы и есть ключ - расшифровываем
                    var aesKey = AES.GetKeyByDevice(progId.device);
                    if (data.HasEncodedFreq && aesKey != null && data.Count == 0)
                        try
                        {
                            data.Decode(progId.device);
                        }
                        catch (CryptographicException)
                        {
                            warnings.AppendLine(
                                Lang.Phrase("Не удалось расшифровать частоты. Возможно, задан неверный ключ шифрования.") +
                                string.Format("DeviceType={0} DbType={1} ProgId={2} Name={3}", progId.device, progId.dbType, progId.id, progId.ToString()));
                            return;
                        }
                    //пишем частоты и времена
                    for (int i = 0; i < data.CountOfFrequencies; i++)
                        sb.AppendFormat("{1:0.##}{0}{2}{0}", separator, data[i].frequency, data[i].time);
                }
                //запоминаем, что эту программу уже экспортировали
                exportedProgramIds.Add(progId, progId);
            }
            //завершение строки
            sb.Append(string.Format("/${0}"+Environment.NewLine, deviceId));
            //сбрасываем в поток
            sw.Write(sb.ToString());
        }

        string PrepareString(string item, char separator)
        {
            item = item.Replace("\"", "\"\"").Replace("\r","").Replace("\n","\\n");
            if (item.Contains(separator.ToString()) || item.Contains("\""))
                item = "\"" + item + "\"";

            return item;
        }

        string DeviceTypeToString(DeviceType device)
        {
            switch (device)
            {
                case DeviceType.DETA_Ritm_Old: return "1";
                case DeviceType.DETA_AP_Old : return  "2";
                case DeviceType.DETA_AP : return  "4";
                case DeviceType.DETA_Ritm: return "5";
                case DeviceType.DETA_AP_Third: return "6";
                case DeviceType.DETA_Ritm_Third: return "7";
                case DeviceType.DETA_AP_4: return "8";
                case DeviceType.DETA_Ritm_4: return "9";
                case DeviceType.DETA_QUANTUM: return "10";
                case DeviceType.DETA_Wave: return "";//???????? !!!!!!!!
            }

            throw new UnknownDeviceException();
        }
    }
}
