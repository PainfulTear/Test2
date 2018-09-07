using System;
using System.Collections.Generic;
using System.Text;
using ElisDB;
using LangStuffs;

namespace Elis.ImportExport
{
    public static class ExportArchiveRecordToHTML
    {
        //число ручных программ в строке автоматической программы
        const int progsPerRow = 6;

        public static string ToHTML(ArchiveRecord rec, string comments)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"
<style type=""text/css"">
table{border-collapse:collapse}
th{padding: 5px}
td{padding: 5px}
</style>");
            /*
            sb.AppendFormat("<h4>{0} {1}</h4>\r\n", rec.Date, rec.Name);
             */
            sb.AppendFormat("<h2>{0}</h2>\r\n", Lang.Phrase("Ручные программы"));
            //нужно ли показывать время?
            if (rec.HandPrograms.Count > 0 && !DeviceStuffs.DeviceInfo.HasTimePerFreq(rec.HandPrograms[0].SourceProgramId.device))
                sb.AppendFormat("<div>{0}</div>\r\n", Lang.Phrase("Время на частоту: {0}", TimeSpan.FromSeconds(rec.TimePerFrequency)));
            
            //шапка таблицы ручных программ
            sb.AppendFormat(
@"<table cellspacing=0 cellpadding=0 bordercolor=""#000000"" border=""1px"">
<tr><th>#</th><th>{0}</th><th>{1}</th><th>{2}</th></tr>", 
            Lang.Phrase("Название программы"),
            Lang.Phrase("Имя в приборе"),
            Lang.Phrase("Время"));
            //выводим ручные программы
            foreach (var prog in rec.HandPrograms)
                sb.AppendFormat(
                    "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>\r\n",
                    prog.Number, 
                    prog.Name,
                    prog.ShortName,
                    prog.TotalTime.ToString()
                );
            sb.AppendLine("</table>\r\n");

            if (rec.AutoPrograms.Count > 0)
            {
                sb.AppendFormat("<h2>{0}</h2>\r\n", Lang.Phrase("Автоматические программы"));
                //шапка таблицы авт программ
                sb.AppendFormat(
    @"<table cellspacing=0 cellpadding=0 bordercolor=""#000000"" border=""1px"">
<tr><th>{0}</th><th colspan={3}>{1}</th><th>{2}</th></tr>",
                Lang.Phrase("Программа"),
                Lang.Phrase("Ручные программы и паузы"),
                Lang.Phrase("Общее время"),
                progsPerRow * 2);
                //выводим авт программы
                foreach (var prog in rec.AutoPrograms)
                {
                    int rowCount = (1 + (prog.HandProgs.Count - 1) / progsPerRow) * 2;
                    for (int iRow = 0; iRow < rowCount; iRow++)
                    {
                        sb.AppendFormat("<tr>\r\n");
                        if (iRow == 0) sb.AppendFormat("<td rowspan={1} valign=middle>{0}</td>", prog.Name, rowCount);

                        for (int iCol = 0; iCol < progsPerRow; iCol++)
                        {
                            int i = iCol + (iRow/2) * progsPerRow ;

                            if (iRow % 2 == 0)
                                sb.AppendFormat("<td>{0}</td><td>{1}</td>", Lang.Phrase("Пр.{0}", i + 1), Lang.Phrase("Пауза"));
                            else
                            {
                                if (i < prog.HandProgs.Count)
                                    sb.AppendFormat("<td>{0}</td><td>{1}</td>", prog.HandProgs[i].Number, i < prog.HandProgs.Count - 1 ? (prog.Pauses[i]/60).ToString() : "&nbsp;");
                                else
                                    sb.AppendFormat("<td>&nbsp;</td><td>&nbsp;</td>");
                            }
                        }
                        if (iRow == 0) sb.AppendFormat("<td rowspan={1} valign=middle>{0}</td>", prog.TotalTime, rowCount);
                        sb.AppendFormat("</tr>\r\n");
                    }
                }
                sb.AppendLine("</table>\r\n");
            }
            if (!string.IsNullOrEmpty(comments))
            {
                sb.AppendLine("<p>" + comments.Replace("\n", "<br>") + "</p>");
            }
            return sb.ToString();
        }
    }
}
