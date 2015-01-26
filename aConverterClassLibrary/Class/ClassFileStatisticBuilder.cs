using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class FileStatisticBuilder
    {
        public FileStatisticBuilder(TableManager ATableManager)
        {
            TableManager = ATableManager;
        }

        public TableManager TableManager { get; set; }

        public string FileName { get; set; }

        public string ShortFileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FileName);
            }
        }

        public List<FieldStatistic> FieldList = new List<FieldStatistic>();

        public int RowCount { get; set; }

        public bool HasStatistic { get; set; }

        public int FieldCount { get; set; }

        public void CalcStatistic()
        {
            RowCount = Convert.ToInt32(TableManager.ExecuteScalar(String.Format("SELECT COUNT(*) FROM {0}", ShortFileName)));
            // Заполняем список полей
            List<DbfField> ldfFact = CoverRecordGeneratorClass.GetFieldList(ShortFileName, TableManager.DataSourceString);
            FieldList.Clear();
            foreach (DbfField df in ldfFact)
            {
                FieldStatistic fs = new FieldStatistic(this);
                fs.Field = df;
                FieldList.Add(fs);
            }
            FieldCount = FieldList.Count;
            if (FieldCount > 0)
            {
                CalcDetailStatistic();
            }
            HasStatistic = true;
        }

        /// <summary>
        /// Расчет детальной статистики (по каждому полю)
        /// </summary>
        public void CalcDetailStatistic()
        {
            foreach (FieldStatistic fs in this.FieldList)
            {
                fs.CalcStatistic();

            }
        }

        public static int CompareByRowCount(FileStatisticBuilder x, FileStatisticBuilder y)
        {
            return x.RowCount.CompareTo(y.RowCount);
        }

        public List<string> GetHtmlReport()
        {
            List<string> ls = new List<string>();
            ls.Add("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">");
            ls.Add("<html>");
            ls.Add(" <head>");
            ls.Add("  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1251\">");
            ls.Add(String.Format("  <title>Статистика по таблице {0}</title>", this.ShortFileName));
            ls.Add(" </head>");
            ls.Add(" <body>");
            ls.Add(String.Format("<h1 align=\"center\">Статистика по таблице {0}</h1>", this.ShortFileName));
            ls.Add("<table border=\"1\" align=\"center\">");
            ls.Add("<tbody>");
            ls.Add("<thead><tr>");
            ls.Add(String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>", 
                "Поле", "Все значения одинаковы", "Все значения уникальны", "Количество уникальных значений", "Легенда"));
            ls.Add("</tr></thead>");

            //ls.Add("<tr><td colspan=\"3\">" + ciwTwin.FullName + "</td></tr>");

            foreach (FieldStatistic fs in this.FieldList)
            {
                int i = 0;
                string s = "";
                foreach (FieldValue fv in fs.CountStatistic)
                {
                    s += fv.Value + ";" + fv.Count.ToString() + "<br/>";
                    i++;
                    if (i > 15 && fs.CountStatistic.Count > i)
                    {
                        s += "...<br/>";
                        break;
                    }
                }
                ls.Add("<tr>");
                ls.Add(String.Format("<td>{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td><td align=\"center\">{3}</td><td>{4}</td>", 
                    fs.Field.ToString(),
                    fs.OnlyTheSameValues ? "Да" : "",
                    fs.OnlyUniqueValues ? "Да" : "",
                    fs.CountUniqueValues,
                    s));
                ls.Add("</tr>");
            }


            ls.Add("</tbody>");
            ls.Add("</table>");
            ls.Add("</body>");

            return ls;
        }

    }

    public class FieldStatistic
    {
        private FileStatisticBuilder parentFSB;

        public FieldStatistic(FileStatisticBuilder AParentFSB)
        {
            parentFSB = AParentFSB;
        }

        public DbfField Field { get; set; }

        public bool OnlyTheSameValues { get; set; }

        public bool OnlyUniqueValues { get; set; }

        public bool NoValues { get; set; }

        public bool HasStatistic { get; set; }

        public int CountUniqueValues { get; set; }

        public List<FieldValue> CountStatistic = new List<FieldValue>();

        /// <summary>
        /// Расчет статистики по полю:
        ///     - выполняем запрос с группировкой по полю и подсчетов количества уникальных значений
        /// </summary>
        public void CalcStatistic()
        {
            string query = String.Format("SELECT {0} AS FIELDVALUE, COUNT(*) AS CNT FROM {1} GROUP BY {0} ORDER BY CNT DESCENDING, FIELDVALUE", Field.FieldName, parentFSB.ShortFileName);
            DataTable dt = parentFSB.TableManager.ExecuteQuery(query);

            CountUniqueValues = dt.Rows.Count;

            OnlyTheSameValues = false;
            OnlyUniqueValues = false;
            NoValues = true;

            if (dt.Rows.Count > 0) NoValues = false;
            if (dt.Rows.Count == 1) OnlyTheSameValues = true;
            if (dt.Rows.Count == parentFSB.RowCount) OnlyUniqueValues = true;
            if (dt.Rows.Count > 0)
            {
                CountStatistic.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    CountStatistic.Add(new FieldValue() 
                                    { 
                                        Value = dr["FIELDVALUE"].ToString(), 
                                        Count = Convert.ToInt32(dr["CNT"]) 
                                    });
                }
            }
            else
                CountStatistic.Clear();

            HasStatistic = true;
        }
    }

    public class FieldValue
    {
        public string Value { get; set; }
        public int Count { get; set; }
    }
    
}
