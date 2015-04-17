using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary.Records
{
    public class LcharsRecordUtils
    {
        /// <summary>
        /// Прореживает список качественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<LcharsRecord> ThinOutList(List<LcharsRecord> lrl)// -----------------------------неведомая вещь 3
        {
            // Сортируем список
            List<LcharsRecord> rlrl = new List<LcharsRecord>();
            lrl.Sort(CompareLchars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldlcharcd = -1; decimal oldlcharvalue = -1;
            for (int i = 0; i < lrl.Count; i++)
            {
                if (lrl[i].Lshet != oldlshet || lrl[i].Lcharcd != oldlcharcd || lrl[i].Value_ != oldlcharvalue)
                {
                    rlrl.Add(lrl[i]);
                    oldlshet = lrl[i].Lshet;
                    oldlcharcd = lrl[i].Lcharcd;
                    oldlcharvalue = lrl[i].Value_;
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
        public static int CompareLchars(LcharsRecord lr1, LcharsRecord lr2)
        {
            if (Convert.ToUInt64(lr1.Lshet) < Convert.ToUInt64(lr2.Lshet))
                return -1;
            else if (Convert.ToUInt64(lr1.Lshet) > Convert.ToUInt64(lr2.Lshet))
                return 1;
            else
            {
                if (lr1.Lcharcd < lr2.Lcharcd)
                    return -1;
                else if (lr1.Lcharcd > lr2.Lcharcd)
                    return 1;
                else
                {
                    if (lr1.Date < lr2.Date)
                        return -1;
                    else if (lr1.Date > lr2.Date)
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

        public List<LcharsRecord> GenerateLChars(DataRow row, string lshet, DateTime startDateTime)
        {
            List<LcharsRecord> lcrl = new List<LcharsRecord>();
            foreach (RLCharsRecord rlcr in recodeList)
            {
                if (checkExpression(row, rlcr))
                {
                    LcharsRecord lcr = rlcr.GetLChars(lshet, startDateTime);
                    lcrl.Add(lcr);
                }
            }
            return lcrl;
        }

        public List<LcharsRecord> GenerateLChars(string fieldName, int fieldValue, string lshet, DateTime startDateTime, bool createObligatoryLchars)
        {
            List<LcharsRecord> lcrl = new List<LcharsRecord>();
            foreach (RLCharsRecord rlcr in recodeList)
            {
                if (checkExpression(fieldName, fieldValue, rlcr))
                {
                    LcharsRecord lcr = rlcr.GetLChars(lshet, startDateTime);
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

        public LcharsRecord GetLChars(string lshet, DateTime date)
        {
            LcharsRecord lcr = new LcharsRecord();

            lcr.Lshet = lshet;
            lcr.Lcharcd = this.Lcharcd;
            lcr.Lcharname = this.lcharname;
            lcr.Value_ = this.Valuecd;
            lcr.Valuedesc = this.Valuename;
            lcr.Date = date;

            return lcr;
        }
    }
}
