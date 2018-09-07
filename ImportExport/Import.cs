using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ElisDB;
using FileDB;
using LangStuffs;
using System.Globalization;
using DeviceStuffs;
using System.IO;

namespace Elis.ImportExport
{
    /// <summary>
    /// Импорт внешних данных в БД
    /// </summary>
    public class Import
    {
        public Import()
        {
        }

        Regex regex1 = new Regex(@"^\$(?<category>\$?)(?<device>\d+)$");
        Regex regex2 = new Regex(@"^#(?<langName>\w\w)(?<name>.+)$");
        Regex regex3 = new Regex(@"^[\d\.,\+\-]+$");
        Regex regex4 = new Regex(@"^/\$\d+$");

        Regex regex5 = new Regex(@"\[(\d+)\]");
        Regex regex6 = new Regex(@"\[([a-zA-Z,_]+)\]");

        /// <summary>
        /// Импорт CSV с автоопределением разделителей
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="fileName"></param>
        /// <param name="enc"></param>
        public void ImportFromCSV(DBType dbType, CategoryType catType, string fileName, Encoding enc)
        {
            ImportFromCSV(dbType, catType, fileName, enc, CsvParser.AutoDetectSeparator(fileName, enc));
        }

        /// <summary>
        /// Импорт CSV
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="fileName"></param>
        /// <param name="enc"></param>
        /// <param name="separator"></param>
        public void ImportFromCSV(DBType defaultDBType, CategoryType defaultCatType, string fileName, Encoding enc, char separator)
        {
            //
            Category currentCategory = null;
            DeviceType curentCategoryDevice = DeviceType.None;
            StringBuilder warnings = new StringBuilder();
            Dictionary<ProgramId, ProgramId> sourceProgIdToProgId = new Dictionary<ProgramId, ProgramId>();
            //
            var parser = new CsvParser();
            parser.separator = separator;
            int iLineCounter = 0;
            //
            var catType = defaultCatType;
            var dbType = defaultDBType;

            foreach (var line in parser.Parse(fileName, enc))
            {
                iLineCounter++;

                if (line.Count < 2 || line[0].Trim() == "")
                {
                    warnings.AppendLine(LangStuffs.Lang.Phrase("Пустая строка: {0}",iLineCounter));
                    continue;
                }
                //проверяем первое поле ($тип прибора)
                var m = regex1.Match(line[0]);
                if (m == null || m.Groups.Count < 2)//неизвестная строка
                {
                    warnings.AppendLine(LangStuffs.Lang.Phrase("Строка {0} в неизвестном формате", iLineCounter));
                    continue;
                }
                //получаем тип прибора
                DeviceType device = ParseDeviceType(m.Groups["device"].Value);
                if(device == DeviceType.Any || device == DeviceType.None)
                {
                    warnings.AppendLine(LangStuffs.Lang.Phrase("Неизвестный тип прибора в строке {0}", iLineCounter));
                    continue;
                }
                //новая категория?
                if (m.Groups["category"].Value == "$")
                {
                    try
                    {
                        catType = defaultCatType;
                        dbType = defaultDBType;

                        currentCategory = ParseCategory(line, device, ref catType, ref dbType, warnings);
                        curentCategoryDevice = device;
                        var isNewCategory = true;
                        //ищем категорию с таким же именем
                        var key = new DBKey(dbType, ItemType.Category, device);
                        foreach(var cat in  (DB.FileDict[key] as Categories).Values)
                            if (cat.Type == currentCategory.Type)
                            if (cat.GetName()[Lang.RuLCID] == currentCategory.GetName()[Lang.RuLCID])//уже существующая категория
                            {
                                currentCategory = cat;
                                //обновляем имена
                                cat.SetName(currentCategory.GetName());
                                isNewCategory = false;
                                break;
                            }
                        //если новая категория - добавляем
                        if (isNewCategory)
                            DB.AddCategory(dbType, device, currentCategory);
                    }
                    catch (Exception ex)
                    {
                        currentCategory = null;
                        curentCategoryDevice = DeviceType.None;
                        warnings.AppendLine(LangStuffs.Lang.Phrase("Строка {0}: {1}", iLineCounter, ex.Message));
                        continue;
                    }
                }
                else
                {
                    //новая программа
                    if (currentCategory == null || curentCategoryDevice != device)
                    {
                        warnings.AppendLine(LangStuffs.Lang.Phrase("В строке {0} найдена программа, но неизвестно, к какой категории она относится", iLineCounter));
                        continue;
                    }
                    try
                    {
                        ProgramData data;
                        Dictionary<int, ProgramName> names;
                        int sourceProgIndex;
                        string key;
                        ProgramId progId;
                        //парсим
                        ParseProgram(line, device, out data, out names, out key, out sourceProgIndex);
                        ProgramId sourceProgId = new ProgramId(dbType, device, sourceProgIndex);
                        ProgramKeyName keyName = new ProgramKeyName(sourceProgId, key);
                        //
                        if (data != null)//если это не ссылка
                        {
                            //если экспорт идет в служебную БД, необходимо шифровать данные
                            if (DB.EncodeNeeded(dbType, device))
                                data.Encode(device);
                            //ищем полное русское название программы
                            //var fullName = names[Lang.RuLCID].FullName;
                            var names2 = DB.GetProgramNames(dbType, device, sourceProgIndex);
        				    if(names2 != null)
        				    if (device == DeviceType.DETA_AP_4 || device == DeviceType.DETA_Ritm_4) {
        				    foreach (var LCID in Lang.GetAccessibleLanguages())
        				    	foreach (var name2 in names[Lang.ISONameToLCID(LCID)].FullName)
        				    		if (name2.ToString().Length > 30 || name2.ToString().Length == 0)
        				    		{
        				    		warnings.AppendLine(LangStuffs.Lang.Phrase("В строке {0} не указано название программы", iLineCounter));
                 				  	continue;                    				
        				    		}
        				    }
        				    else {
							foreach (var LCID in Lang.GetAccessibleLanguages())
								if (LCID == "EN" || LCID == "DE" || LCID == "FR" || LCID == "ES" || LCID == "RU" )
        				    	foreach (var name2 in names[Lang.ISONameToLCID(LCID)].FullName)
        				    		if (name2.ToString().Length > 30 || name2.ToString().Length == 0)
        				    		{
        				    		warnings.AppendLine(LangStuffs.Lang.Phrase("В строке {0} не указано название программы", iLineCounter));
                 				  	continue;                    				
        				    		}
        				    }
                            //добавляем программу в БД
                            progId = DB.AddOrUpdateProgram(dbType, device, data, keyName, sourceProgIndex, Lang.RuLCID);
                            //запоминаем перекодировку
                            if (sourceProgId.id >= 0)
                                sourceProgIdToProgId.Add(sourceProgId, progId);
                            //добавляем имена
                            foreach (var p in names)
                            {
                                var oldName = DB.GetProgramName(progId, p.Key);
                                if(oldName == null)
                                    DB.SetProgramName(p.Value, progId, p.Key);
                                else
                                {
                                    //сохраняем старый объект имени, потому что там могут быть описания и рекомендации
                                    oldName.FullName = p.Value.FullName;
                                    oldName.ShortName = p.Value.ShortName;
                                    DB.SetProgramName(oldName, progId, p.Key);
                                }
                            }
                        }
                        else
                        {
                            //это была ссылка на другую программу
                            if (!sourceProgIdToProgId.ContainsKey(sourceProgId))
                                throw new Exception(Lang.Phrase("Найдена ссылка на программу, но программы с таким кодом нет.") + string.Format(" Device={0} DBType={1} ProgId={2}", sourceProgId.device, sourceProgId.dbType, sourceProgId.id));
                            progId = sourceProgIdToProgId[sourceProgId];
                        }
                        //добавляем в категорию
                        var exists = false;
                        foreach(var pi in currentCategory.ProgIds)
                            if (pi.Equals(progId))
                            {
                                exists = true;
                                break;
                            }
                        if(!exists)//если программы нет в этой категории, добавляем
                            currentCategory.AddProgId(progId);
                    }
                    catch (Exception ex)
                    {
                        warnings.AppendLine(LangStuffs.Lang.Phrase("Строка {0}: {1}", iLineCounter, ex.Message));
                        continue;
                    }
                }
            }

            if (warnings.Length > 0)
                throw new NonCriticalException(Lang.Phrase("Импорт выполнен, со следующими замечаниями:\r\n{0}", warnings.ToString()));
        }

        private Category ParseCategory(List<string> line, DeviceType device, ref CategoryType catType, ref DBType dbType, StringBuilder warnings)
        {
            if(line.Count<3 || line[3].Trim() == "")
                throw new Exception(Lang.Phrase("Имя категории не задано"));


            //парсим тип БД и тип категории
            string s = line[3].Trim();
            var m = regex6.Match(s);
            if (m != null && m.Groups.Count > 1)
            {
                var parts = m.Groups[1].Value.Split(',');
                dbType = (DBType)Enum.Parse(typeof(DBType), parts[0]);
                catType = (CategoryType)Enum.Parse(typeof(CategoryType), parts[1]);
            }


            //имя на русском
            LocalizableString name = new LocalizableString();
            name.Set(line[3].Trim(), Lang.RuLCID);

            //создаем категорию
            Category cat = new Category(name);

            for (int i = 5; i < line.Count; i++)
            {
                int LCID;
                string nameString;
                if (ParseName(line[i], out LCID, out nameString))
                {
                    //заносим имя
                    name.Set(nameString, LCID);
                }
            }

            cat.Type = catType;

            return cat;
        }

        bool ParseName(string lineItem, out int LCID, out string nameString)
        {
            LCID = 127;
            nameString = "";

            var m = regex2.Match(lineItem);
            if (m == null || m.Groups.Count < 3)
                return false;
            //GB -> EN
            var langISOname = m.Groups["langName"].Value.ToLower();
            if (langISOname == "gb")
                langISOname = "en";
            //язык
            LCID = Lang.ISONameToLCID(langISOname);
            if (LCID == 127)
                throw new Exception(Lang.Phrase("Неизвестный язык '{0}'. Язык должен задаваться двухбуквенным идентификатором (ISO 639-1).", m.Groups["langName"].Value));

            nameString = m.Groups["name"].Value.Trim();

            return true;
        }
        
        

        private void ParseProgram(List<string> line, DeviceType device, out ProgramData data, out Dictionary<int, ProgramName> names, out string key, out int sourceProgId)
        {
            ///пытаемся читать номер программы
            sourceProgId = -1;
            key = "";
            
            if (line.Count >= 3)
            {
                //var m = regex5.Match(line[1]);
                //if (m != null && m.Groups.Count > 1) {
                	//string keyString;
                
                	//sourceProgId = int.Parse(m.Groups[1].Value);
                	sourceProgId = int.Parse(line[1].Trim());
                	key = line[2].Trim();
                //}
            }

            if (line.Count < 4 || line[3].Trim() == "" || line[4].Trim() == "")
            {
                if (sourceProgId >= 0)
                {
                    //это ссылка на программу
                    data = null;
                    names = null;
                    return;
                }
                throw new Exception(Lang.Phrase("Имя программы не задано"));
            }
            
            data= null;
            names = new Dictionary<int,ProgramName>();

            
            //имя на русском
            names.Add(Lang.RuLCID, ParseName2(line[3].Trim(), line[4].Trim()));

            for (int i = 4; i < line.Count; i++)
            {
                int LCID;
                string nameString;
                if (ParseName(line[i], out LCID, out nameString))
                {
                    names.Add(LCID, ParseName2(nameString, line[i + 1].Trim()));
                    i++;
                }
                else
                    if (ParseProgramData(line, i, device, out data))
                        break;
            }
            if(data==null)
                throw new Exception(Lang.Phrase("Частоты программы не найдены."));
        }

        private ProgramName ParseName2(string fullName, string shortName)
        {
            string desc = "";

            if (fullName.Contains("\\n"))
            {
                var parts = fullName.Split(new string[] { "\\n" }, 2, StringSplitOptions.None);
                fullName = parts[0].Trim();
                desc = parts[1].Replace("\\n", "\r\n");
            }

            return new ProgramName(fullName, shortName, desc);
        }
        
        //private ProgramKey ParseKey(string key)

        private bool ParseProgramData(List<string> line, int startIndex, DeviceType device, out ProgramData data)
        {
            data = null;
            if (regex3.IsMatch(line[startIndex].Trim()))
            {
                data = new ProgramData();
                //начинаем считывание частот и времен
                for (int i = startIndex; i < line.Count; i++)
                {
                    string freq = line[i].Trim();
                    if (regex3.IsMatch(freq))
                    {
                        float f = float.Parse(freq.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                        string time = line[i + 1].Trim();
                        if (!regex3.IsMatch(time))
                            throw new Exception(Lang.Phrase("Частота найдена, но не найдено время для нее"));
                        if(f<=0 || f > DeviceInfo.GetMaxFreq(device))
                            throw new Exception(Lang.Phrase("Частота '{0}' выходит за допустимый диапазон", f));
                        UInt16 t = UInt16.Parse(time);
                        FreqTime ft = new FreqTime(f, t);
                        data.Add(ft);
                        i++;
                    }
                    else
                    {
                        if (!regex4.IsMatch(freq))
                            throw new Exception(Lang.Phrase("Неизвестный завершающий символ '{0}'", freq));
                        break;
                    }
                }

                if(data.Count > DeviceInfo.GetMaxFreqCount(device))
                    throw new Exception(Lang.Phrase("Число частот программы превышает допустимое значение для данного типа устройства"));

                return true;
            }

            return false;
        }

        DeviceType ParseDeviceType(string s)
        {
            switch (s)
            {
                case "1": return DeviceType.DETA_Ritm_Old;
                case "2": return DeviceType.DETA_AP_Old;
                case "4": return DeviceType.DETA_AP;
                case "5": return DeviceType.DETA_Ritm;
                case "6": return DeviceType.DETA_AP_Third;
                case "7": return DeviceType.DETA_Ritm_Third;
                case "8": return DeviceType.DETA_AP_4;
                case "9": return DeviceType.DETA_Ritm_4;
                case "": return DeviceType.DETA_Wave;//?????? !!!!!!!
            }

            return DeviceType.Any;
        }

        /// <summary>
        /// Импорт описаний программ.
        /// Формат:
        /// $Краткое_Имя_Программы1
        /// Описание....
        /// $Краткое_Имя_Программы2
        /// Описание.... 
        /// </summary>
        public void ImportDescriptions(DBType dbType, DeviceType device, string fileName, int LCID, Encoding encoding)
        {
            StringBuilder warnings = new StringBuilder();
            Dictionary<string, ProgramId> nameToProgId = new Dictionary<string, ProgramId>();

            //создаем словарь имен
            var names = DB.GetProgramNames(dbType, device, LCID);
            foreach (var progId in names.Keys)
                nameToProgId[names[progId].ShortName] = new ProgramId(dbType, device, progId);
            //читаем файл  
            using (StreamReader sr = new StreamReader(fileName, encoding))
            {
                ProgramId currentProgramId = null;
                string currentDesc = "";
                int iLineCounter = 0;
                while (sr.Peek() >= 0)
                {
                    iLineCounter++;
                    string line = sr.ReadLine();
                    if (line.StartsWith("$"))//имя программы
                    {
                        if (currentProgramId != null)
                            FlushProgramDesc(LCID, names, currentProgramId, currentDesc, nameToProgId, warnings);
                        currentDesc = "";
                        string progName = line.Trim().Substring(1);
                        if (!nameToProgId.ContainsKey(progName))
                        {
                            currentProgramId = null;
                            warnings.AppendLine(Lang.Phrase("Строка {0}: {1}", iLineCounter, Lang.Phrase("Имя программы не найдено: {0}", progName)));
                            continue;
                        }

                        currentProgramId = nameToProgId[progName];
                    }
                    else
                        currentDesc += line + Environment.NewLine;
                }
                if (currentProgramId != null)
                    FlushProgramDesc(LCID, names, currentProgramId, currentDesc, nameToProgId, warnings);
            }
            //
            if (warnings.Length > 0)
                throw new NonCriticalException(Lang.Phrase("Импорт выполнен, со следующими замечаниями:\r\n{0}", warnings.ToString()));
        }


        private void FlushProgramDesc(int LCID, ProgramNames names, ProgramId currentProgramId, string currentDesc, Dictionary<string, ProgramId> nameToProgId, StringBuilder warnings)
        {
            currentDesc = currentDesc.Trim();
            var m = Regex.Match(currentDesc, @"%%DESCRIPTION_SMALL%%(.*?)(%%\w+%%|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if(m.Success)
                names[currentProgramId.id].Description = m.Groups[1].Value.Trim();

            m = Regex.Match(currentDesc, @"%%DESCRIPTION_FULL%%(.*?)(%%\w+%%|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (m.Success)
                names[currentProgramId.id].FullDescription = m.Groups[1].Value.Trim();

            m = Regex.Match(currentDesc, @"%%RECOMENDED%%(.*?)(%%\w+%%|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (m.Success)
            if (LCID == Lang.RuLCID)//только для русского языка
            {
                var lines = m.Groups[1].Value.Trim().Split('\r', '\n');
                var list = new List<int>();
                foreach(var line in lines)
                    if(line!="")
                    {
                        ProgramId id;
                        if(nameToProgId.TryGetValue(line.Trim(), out id))
                            list.Add(id.id);
                    }
                names[currentProgramId.id].SetRecommendedProgIds(list);
            }
        }
    }

    public class NonCriticalException : Exception
    {
        public NonCriticalException(string message)
            : base(message)
        {
        }
    }
}
