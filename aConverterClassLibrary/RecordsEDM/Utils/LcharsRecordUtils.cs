using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary.RecordsEDM
{
    public class LcharsRecordUtils
    {
        /// <summary>
        /// Прореживает список качественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<lchar> ThinOutList(List<lchar> lrl)// -----------------------------
        {
            // Сортируем список
            List<lchar> rlrl = new List<lchar>();
            lrl.Sort(CompareLchars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldlcharcd = -1; decimal oldlcharvalue = -1;
            for (int i = 0; i < lrl.Count; i++)
            {
                if (lrl[i].LSHET != oldlshet || lrl[i].LCHARCD != oldlcharcd || lrl[i].VALUE != oldlcharvalue)
                {
                    rlrl.Add(lrl[i]);
                    oldlshet = lrl[i].LSHET;
                    oldlcharcd = (Int32)lrl[i].LCHARCD;
                    oldlcharvalue = (Int32)lrl[i].VALUE;
                }
            }
            return rlrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LcharsRecord----------------------------------------------- н в 4
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareLchars(lchar lr1, lchar lr2)
        {
            if (Convert.ToUInt64(lr1.LSHET) < Convert.ToUInt64(lr2.LSHET))
                return -1;
            else if (Convert.ToUInt64(lr1.LSHET) > Convert.ToUInt64(lr2.LSHET))
                return 1;
            else
            {
                if (lr1.LCHARCD < lr2.LCHARCD)
                    return -1;
                else if (lr1.LCHARCD > lr2.LCHARCD)
                    return 1;
                else
                {
                    if (lr1.DATE < lr2.DATE)
                        return -1;
                    else if (lr1.DATE > lr2.DATE)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        /// <summary>
        /// Считывает таблицу перекодировки
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<RLCharsRecord> LoadRLCharsRecord(string fileName)
        {
            List<RLCharsRecord> rl = new List<RLCharsRecord>();
            DataTable dt = aConverterClassLibrary.Utils.ReadExcelFile(fileName, "Лист1");
            foreach (DataRow odr in dt.Rows)
            {
                if (!(odr["FIELD"] is DBNull))
                {
                    string field = odr["FIELD"].ToString();
                    string range = odr["FIELDVALUE"].ToString();
                    int lcharcd = Convert.ToInt32(odr["LCHARCD"]);
                    string lcharname = odr["LCHARNAME"].ToString();
                    int lcharvalue = Convert.ToInt32(odr["LCHARVALUE"]);
                    string valuedesc = odr["LCHARVALUEDESC"].ToString();
                    string addparam = odr["ADDPARAM"].ToString();
                    RLCharsRecord rlc = new RLCharsRecord(field, range, lcharcd, lcharname, lcharvalue, valuedesc, addparam);
                    rl.Add(rlc);
                }
            }
            return rl;
        }

    }

    public class ClassLCharsRecode
    {
        // private DateTime startDateTime = DateTime.MinValue;

        private List<RLCharsRecord> recodeList = new List<RLCharsRecord>();

        public ClassLCharsRecode(List<RLCharsRecord> RecodeList)
        {
            recodeList = RecodeList;
        }

        public List<lchar> GenerateLChars(DataRow row, string lshet, DateTime startDateTime)
        {
            List<lchar> lcrl = new List<lchar>();
            foreach (RLCharsRecord rlcr in recodeList)
            {
                if (checkExpression(row, rlcr))
                {
                    lchar lcr = rlcr.GetLChars(lshet, startDateTime);
                    lcrl.Add(lcr);
                }
            }
            return lcrl;
        }

        public List<lchar> GenerateLChars(string fieldName, int fieldValue, string lshet, DateTime startDateTime, bool createObligatoryLchars)
        {
            List<lchar> lcrl = new List<lchar>();
            foreach (RLCharsRecord rlcr in recodeList)
            {
                if (checkExpression(fieldName, fieldValue, rlcr))
                {
                    lchar lcr = rlcr.GetLChars(lshet, startDateTime);
                    lcrl.Add(lcr);
                }
            }
            return lcrl;
        }

        protected virtual bool checkExpression(DataRow dr, RLCharsRecord rlcr)
        {
            string fieldName; object fieldValue;
            if (!dr.Table.Columns.Contains(rlcr.Type))
                return false;
            else
            {
                fieldName = rlcr.Type;
                fieldValue = dr[fieldName];
            }
            bool rv = checkExpression(fieldName, fieldValue, rlcr);
            return rv;
        }

        protected virtual bool checkExpression(string fieldName, object fieldValue, RLCharsRecord rlcr)
        {
            if (fieldName == "ALL") return true;
            bool succeded = false;
            if (fieldValue.GetType() == typeof(string) || fieldValue.GetType() == typeof(bool))
            {
                if (fieldValue.ToString().ToUpper() == rlcr.Range.ToUpper()) return true;
            }
            else
            {
                int value = Convert.ToInt32(fieldValue);
                string[] values = rlcr.Range.Split(',');
                foreach (string s in values)
                {
                    string[] range = s.Replace("..", "~").Split('~');
                    if (range.Length == 1)
                    {
                        if (Convert.ToInt32(s) == value)
                        {
                            succeded = true;
                            break;
                        }
                    }
                    else if (range.Length == 2)
                    {
                        int minvalue = Convert.ToInt32(range[0]);
                        int maxvalue = Convert.ToInt32(range[1]);
                        if (value >= minvalue && value <= maxvalue)
                        {
                            succeded = true;
                            break;
                        }
                    }
                    else
                        throw new Exception("Неверная конструкция " + s + " в поле FieldValue");
                }
                return succeded;
            }
            return false;

        }
    }

    public class RLCharsRecord
    {
        private string type;
        // 
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string range;

        public string Range
        {
            get { return range; }
            set { range = value; }
        }

        private int lcharcd;

        public int Lcharcd
        {
            get { return lcharcd; }
            set { lcharcd = value; }
        }

        private string lcharname;

        public string Lcharname
        {
            get { return lcharname; }
            set { lcharname = value; }
        }

        private int valuecd;

        public int Valuecd
        {
            get { return valuecd; }
            set { valuecd = value; }
        }

        private string valuename;

        public string Valuename
        {
            get { return valuename; }
            set { valuename = value; }
        }

        private string addparam;

        public string Addparam
        {
            get { return addparam; }
            set { addparam = value; }
        }


        public RLCharsRecord(string AType, string ARange, int ALCharcd, string ALCharName,
            int AValuecd, string AValueName, string AAddParam)
        {
            type = AType;
            range = ARange;
            lcharcd = ALCharcd;
            lcharname = ALCharName;
            valuecd = AValuecd;
            valuename = AValueName;
            addparam = AAddParam;
        }

        public lchar GetLChars(string lshet, DateTime date)
        {
            lchar lcr = new lchar();

            lcr.LSHET = lshet;
            lcr.LCHARCD = this.Lcharcd;
            lcr.LCHARNAME = this.lcharname;
            lcr.VALUE = this.Valuecd;
            lcr.VALUEDESC = this.Valuename;
            lcr.DATE = date;

            return lcr;
        }
    }
}
