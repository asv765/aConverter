using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Xml.Serialization;
using System.IO;

namespace aConverterClassLibrary
{
    [Serializable]
    [XmlInclude(typeof(DbfStatistic))]
    [XmlInclude(typeof(FdbStatistic))]
    public abstract class Statistic
    {
        private string sql;
        /// <summary>
        /// SQL-запрос
        /// </summary>
        public string Sql
        {
            get { return sql; }
            set { sql = value; }
        }

        private string statisticName;
        /// <summary>
        /// Наименование статистики
        /// </summary>
        public string StatisticName
        {
            get { return statisticName; }
            set { statisticName = value; }
        }

        private List<string> fieldRecodeList = new List<string>();
        /// <summary>
        /// Каждая строка это два значения, разделенных ;. Первое значение - имя в наборе, второе - на что изменить
        /// </summary>
        public List<string> FieldRecodeList
        {
            get { return fieldRecodeList; }
            set { fieldRecodeList = value; }
        }

        private StatisticType statisticType;
        /// <summary>
        /// Тип статистики
        /// </summary>
        [XmlIgnore]
        public StatisticType StatisticType
        {
            get { return statisticType; }
            set { statisticType = value; }
        }

        /// <summary>
        /// Значение типа статистики для сериализации
        /// </summary>
        public int StatisticTypeId
        {
            get { return (int)statisticType; }
            set { statisticType = (StatisticType)value; }
        }

        /// <summary>
        /// Наименование типа статистики (только чтение)
        /// </summary>
        public string StatisticTypeName
        {
            get { return StatisticType.ToString().Replace('_', ' '); }
        }

        public Statistic() { }

        public Statistic(string AName, string ASql, List<string> AFieldRecodeList)
        {
            sql = ASql;
            fieldRecodeList = AFieldRecodeList;
            statisticName = AName;
        }

        public abstract DataTable GenerateStatistic();

        public static void ExportToCsv(DataTable dt, string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251));
            foreach (DataColumn dc in dt.Columns)
            {
                sw.Write(dc.ColumnName + ";");
            }
            sw.WriteLine();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dr[i].ToString() + ";");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        /// <summary>
        /// Отображает форму со статистикой (для таблицы), расчитывает значение (для одиночного значения)
        /// </summary>
        //public string ShowStatistic(out DataTable RetStatistic)
        //{
        //    string rv = "";
        //    RetStatistic = null;
        //    try
        //    {
        //        if (StatisticType == StatisticType.Таблица)
        //        {
        //            RetStatistic = GenerateStatistic();
        //        }
        //        else if (StatisticType == StatisticType.Одиночное_значение)
        //        {
        //            RetStatistic = GenerateStatistic();
        //            rv = Convert.ToString(RetStatistic.Rows[0][0]);
        //            this.value = rv;
        //        }
        //        else if (StatisticType == StatisticType.Не_возвращает_значений)
        //        {
        //            GenerateStatistic();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rv = "В результате выполнения возникла ошибка:\r\n" + ex.ToString();
        //    }
        //    return rv;

            //decimal rv = -1;
            //try
            //{
            //    if (StatisticType == StatisticType.Таблица)
            //    {
            //        DataTable dt = GenerateStatistic();
            //        FormStatisticResult fss = new FormStatisticResult(StatisticName, dt, FieldRecodeList);
            //        fss.Show();
            //    }
            //    else if (StatisticType == StatisticType.Одиночное_значение)
            //    {
            //        DataTable dt = GenerateStatistic();
            //        rv = Convert.ToDecimal(dt.Rows[0][0]);
            //        this.value = rv;
            //    }
            //    else if (StatisticType == StatisticType.Не_возвращает_значений)
            //    {
            //        GenerateStatistic();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("В результате выполнения возникла ошибка:\r\n" +
            //        ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return -2;
            //}
            //return rv;
        //}

        private string value = null;
        /// <summary>
        /// Значения для одиночного запроса
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string StatisticText()
        {
            DataTable dt = this.GenerateStatistic();
            string rs = "";

            rs += String.Format(this.StatisticName) + "\r\n";
            if (this.StatisticType == aConverterClassLibrary.StatisticType.Одиночное_значение)
                rs += value.ToString();
            else if (this.StatisticType == aConverterClassLibrary.StatisticType.Таблица)
            {
                if (dt.Rows.Count > 0)
                {
                    List<int> columnLength = new List<int>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        object o = dt.Rows[0][dc.ColumnName];
                        int vl = o.ToString().Length;
                        int cl = dc.ColumnName.Length;
                        int rcl = Math.Max(vl, cl) + 3;
                        columnLength.Add(rcl);
                        rs += dc.ColumnName.PadRight(rcl);
                    }
                    rs += "\r\n";
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = 0;
                        foreach (object o in dr.ItemArray)
                        {
                            rs += o.ToString().PadRight(columnLength[i]);
                            i++;
                        }
                        rs += "\r\n";
                    }
                }
            }
            return rs;
        }
    }

    public enum StatisticType
    {
        Таблица,
        Одиночное_значение,
        Не_возвращает_значений
    }
}
