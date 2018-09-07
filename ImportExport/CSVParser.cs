using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Elis.ImportExport
{
    public class CsvParser
    {
        public char separator = ',';
        public char quote = '"';

        public IEnumerable<string> ReadLines(string fileName, Encoding enc)
        {
            using (StreamReader sr = new StreamReader(fileName, enc))
                while (sr.Peek() >= 0)
                    yield return sr.ReadLine();
        }

        public static char AutoDetectSeparator(string fileName, Encoding enc)
        {
            using (StreamReader sr = new StreamReader(fileName, enc))
                while (sr.Peek() >= 0)
                {
                    var s = sr.ReadLine();
                    //если есть табуляции - скорее всего это и есть разделитель
                    if (s.Contains("\t")) return '\t';
                    //считаем число запятых и точек с запятыми
                    int semicolonCount = 0;
                    int commaCount = 0;
                    foreach (char c in s)
                        if (c == ';') semicolonCount++;
                        else
                            if (c == ',') commaCount++;
                    //точек с запятыми больше чем запятых
                    if (semicolonCount > commaCount) return ';';
                    return ',';
                }

            return ',';
        }

        public IEnumerable<List<string>> Parse(string fileName, Encoding enc)
        {
            foreach (var line in Parse(ReadLines(fileName, enc)))
                yield return line;
        }

        public IEnumerable<List<string>> Parse(IEnumerable<string> lines)
        {
            var e = lines.GetEnumerator();
            while (e.MoveNext())
                yield return ParseLine(e);
        }

        private List<string> ParseLine(IEnumerator<string> e)
        {
            var items = new List<string>();
            foreach (string token in GetToken(e))
                items.Add(token);
            return items;
        }

        private IEnumerable<string> GetToken(IEnumerator<string> e)
        {
            string token = "";
            State state = State.outQuote;

        again:
            foreach (char c in e.Current)
                switch (state)
                {
                    case State.outQuote:
                        if (c == separator)
                        {
                            yield return token;
                            token = "";
                        }
                        else
                            if (c == quote)
                                state = State.inQuote;
                            else
                                token += c;
                        break;
                    case State.inQuote:
                        if (c == quote)
                            state = State.mayBeOutQuote;
                        else
                            token += c;
                        break;
                    case State.mayBeOutQuote:
                        if (c == quote)
                        {
                            //кавычки внутри кавычек
                            state = State.inQuote;
                            token += c;
                        }
                        else
                        {
                            state = State.outQuote;
                            goto case State.outQuote;
                        }
                        break;
                }

            //разрыв строки внутри кавычек
            if (state == State.inQuote && e.MoveNext())
            {
                token += Environment.NewLine;
                goto again;
            }

            yield return token;
        }

        enum State { outQuote, inQuote, mayBeOutQuote }
    }
}
