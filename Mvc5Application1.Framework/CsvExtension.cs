using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Mvc5Application1.Framework
{
    public static class CsvExtension
    {
        public static string CreateCsv<T>(this List<T> list,
            params string[] fieldHeaderMappers)
        {
            return CreateCsv(list, false, fieldHeaderMappers);
        }

        public static string CreateCsv<T>(this List<T> list, bool includeAllField, params string[] fieldHeaderMappers)
        {
            if (list == null || list.Count == 0) return string.Empty;

            var headerMapper = BuildHeaderMapper(fieldHeaderMappers);

            using (var sw = new StringWriter())
            {
                //get properties from 0th member
                PropertyInfo[] props = list[0].GetType().GetProperties();

                //build header line
                var headerLine = new StringBuilder();
                foreach (PropertyInfo pi in props)
                {
                    if (includeAllField || headerMapper.ContainsKey(pi.Name.ToLower()))
                    {
                        var header = pi.Name;
                        if (headerMapper.ContainsKey(header.ToLower()))
                        {
                            header = headerMapper[header.ToLower()];
                        }
                        headerLine.Append(header + ",");
                    }
                }
                sw.WriteLine(headerLine.ToString().Trim(','));

                foreach (var item in list)
                {
                    //build data line
                    var valueLine = new StringBuilder();
                    foreach (PropertyInfo pi in props)
                    {
                        if (includeAllField || headerMapper.ContainsKey(pi.Name.ToLower()))
                        {
                            string stringValue = string.Format("\"{0}\",",
                                item.GetType().GetProperty(pi.Name).GetValue(item, null));
                            valueLine.Append(stringValue);
                        }
                    }
                    sw.WriteLine(valueLine.ToString().Trim(','));
                }
                return sw.GetStringBuilder().ToString();
            }
        }

        private static IDictionary<string, string> BuildHeaderMapper(string[] fieldHeaderMappers)
        {
            var dict = new Dictionary<string, string>();
            foreach (var fieldHeaderMapper in fieldHeaderMappers)
            {
                var filed = fieldHeaderMapper.Split('|', ',', ';');
                dict[filed[0].ToLower()] = filed[1];
            }
            return dict;
        }
    }
}