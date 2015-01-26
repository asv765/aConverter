using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data.Common;

namespace aConverterClassLibrary
{
    public class PatternUtils
    {
        // public static string Parse(string s, DataRow dr, bool RegexPresent)
        public static string Parse(string s, DbDataReader dr, bool RegexPresent, bool ConditionStringsPresent, MatchCollection BodyMatchCollection)
        {
            string[] lbodya = s.Replace("\r","").Split('\n');
            string rbody = "";
            foreach (string fbody in lbodya)
            {
                string lbody = fbody;
                if (ConditionStringsPresent)
                {
                    Match m = Regex.Match(lbody, @"^{\$c\s*\w+}");
                    if (m.Success)
                    {
                        string fieldName = Regex.Match(m.Value, @"(?<={\$c\s*)\w+(?=\s*})").Value;
                        bool checkcondition = Convert.ToBoolean(dr[fieldName]);
                        if (!checkcondition)
                            continue;
                        else
                            lbody = lbody.Replace(m.Value, "");
                    }
                }

                MatchCollection mc;
                if (BodyMatchCollection != null)
                    mc = BodyMatchCollection;
                else
                    mc = Regex.Matches(s, Pattern.FieldPattern);

                foreach (Match m in mc)
                {
                    string fieldName = m.Value;
                    string format = "";
                    if (m.Value.Contains(':'))
                    {
                        fieldName = m.Value.Split(':')[0];
                        format = m.Value.Split(':')[1];
                    }
                    string newValue = "";
                    if (!String.IsNullOrEmpty(format))
                        newValue = string.Format("{0:" + format + "}", dr[fieldName]);
                    else
                        newValue = dr[m.Value].ToString().Trim();

                    if (dr[fieldName] is Decimal) newValue = newValue.Replace(',', '.');

                    // lbody = Regex.Replace(lbody, "%" + m.Value + "%", newValue);
                    lbody = lbody.Replace("%" + m.Value + "%", newValue);
                }

                if (RegexPresent)
                {
                    mc = Regex.Matches(lbody, @"(?i)(?<=Regex\(~)(.+?,.+?)(?=~\))");
                    foreach (Match m in mc)
                    {
                        string argument = Regex.Match(m.Value, @"^.+?(?=~\,~)").Value;
                        string regex = Regex.Match(m.Value, @"(?<=~\,~).+$").Value;
                        string searchs = "Regex(~" + argument + "~,~" + regex + "~)";
                        string replaces = Regex.Match(argument, regex).Value;
                        lbody = lbody.Replace(searchs, replaces);
                    }
                }

                if (!String.IsNullOrEmpty(lbody)) rbody += (!String.IsNullOrEmpty(rbody) ? "\r\n" : "") +  lbody;
            }
            if (!String.IsNullOrEmpty(rbody)) rbody += "\r\n";
            return rbody;
        }

        public static string Parse(string s, DbDataReader dr, bool RegexPresent, bool ConditionStringsPresent, Dictionary<string, PatternDictionary> DictionaryCollection, MatchCollection BodyMatchCollection)
        {
            string lbody = Parse(s, dr, RegexPresent, ConditionStringsPresent, BodyMatchCollection);

            // Парсим по словарям
            if (DictionaryCollection.Count > 0)
            {
                MatchCollection mc = Regex.Matches(lbody, @"(?i)(?<={\$d ).+?(?=})");
                foreach (Match m in mc)
                {
                    string dictionaryName = Regex.Match(m.Value, @"(?i)\S+(?=\()").Value;
                    string keyValue = Regex.Match(m.Value, @"(?i)(?<=\()\S+(?=\))").Value;

                    PatternDictionary currentDictionary;
                    if (DictionaryCollection.ContainsKey(dictionaryName))
                    {
                        currentDictionary = DictionaryCollection[dictionaryName];
                        string currentValue = currentDictionary.Dictionary[keyValue];
                        string searchSubstring = @"{\$d " + dictionaryName + @"\(.*?\)}";
                        lbody = Regex.Replace(lbody, searchSubstring, currentValue);
                    }
                }
            }

            return lbody;
        }
    }
}
