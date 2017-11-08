using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class LcharsRecordUtils
    {
        /// <summary>
        /// Прореживает список качественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CNV_LCHAR> ThinOutList(List<CNV_LCHAR> lrl, bool useIntLs = false)// -----------------------------
        {
            // Сортируем список
            var rlrl = new List<CNV_LCHAR>();
            if (useIntLs) lrl.Sort(CompareLcharsUsingIntLs);
            else lrl.Sort(CompareLchars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldlcharcd = -1; long oldlcharvalue = -1;
            for (int i = 0; i < lrl.Count; i++)
            {
                var t = lrl[i];
                if (t.LSHET != oldlshet || t.LCHARCD != oldlcharcd || t.VALUE_ != oldlcharvalue)
                {
                    rlrl.Add(t);
                    oldlshet = t.LSHET;
                    oldlcharcd = (int)t.LCHARCD;
                    oldlcharvalue = (int)t.VALUE_;
                }
            }
            lrl.Clear();
            lrl.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return rlrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LcharsRecord----------------------------------------------- н в 4
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareLcharsUsingIntLs(CNV_LCHAR lr1, CNV_LCHAR lr2)
        {
            if (lr1.SortLshet < lr2.SortLshet)
                return -1;
            if (lr1.SortLshet > lr2.SortLshet)
                return 1;
            if (lr1.LCHARCD < lr2.LCHARCD)
                return -1;
            if (lr1.LCHARCD > lr2.LCHARCD)
                return 1;
            if (lr1.DATE_ < lr2.DATE_)
                return -1;
            if (lr1.DATE_ > lr2.DATE_)
                return 1;
            return 0;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LcharsRecord----------------------------------------------- н в 4
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareLchars(CNV_LCHAR lr1, CNV_LCHAR lr2)
        {
            if (long.Parse(lr1.LSHET) < long.Parse(lr2.LSHET))
                return -1;
            if (long.Parse(lr1.LSHET) > long.Parse(lr2.LSHET))
                return 1;
            if (lr1.LCHARCD < lr2.LCHARCD)
                return -1;
            if (lr1.LCHARCD > lr2.LCHARCD)
                return 1;
            if (lr1.DATE_ < lr2.DATE_)
                return -1;
            if (lr1.DATE_ > lr2.DATE_)
                return 1;
            return 0;
        }

        /// <summary>
        /// Считывает таблицу перекодировки
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<RlCharsRecord> LoadRlCharsRecord(string fileName)
        {
            var rl = new List<RlCharsRecord>();
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
                    var rlc = new RlCharsRecord(field, range, lcharcd, lcharname, lcharvalue, valuedesc, addparam);
                    rl.Add(rlc);
                }
            }
            return rl;
        }

        /// <summary>
        /// Делает уникальный набор характеристик с ключом "лс,код характерстки, дата"
        /// </summary>        
        public static List<CNV_LCHAR> CreateUniqueLchars(List<CNV_LCHAR> llc)
        {
            return llc
                .GroupBy(lc => new {lc.LSHET, lc.LCHARCD, lc.DATE_})
                .Select(glc => glc.Last())
                .ToList();
        }
    }

    public class ClassLCharsRecode
    {
        // private DateTime startDateTime = DateTime.MinValue;

        private List<RlCharsRecord> recodeList = new List<RlCharsRecord>();

        public ClassLCharsRecode(List<RlCharsRecord> RecodeList)
        {
            if (RecodeList == null) throw new ArgumentNullException("RecodeList");
            recodeList = RecodeList;
        }

        public List<CNV_LCHAR> GenerateLChars(DataRow row, string lshet, DateTime startDateTime)
        {
            var lcrl = new List<CNV_LCHAR>();
            foreach (RlCharsRecord rlcr in recodeList)
            {
                if (CheckExpression(row, rlcr))
                {
                    CNV_LCHAR lcr = rlcr.GetLChars(lshet, startDateTime);
                    lcrl.Add(lcr);
                }
            }
            return lcrl;
        }

        public List<CNV_LCHAR> GenerateLChars(string fieldName, int fieldValue, string lshet, DateTime startDateTime, bool createObligatoryLchars)
        {
            List<CNV_LCHAR> lcrl = new List<CNV_LCHAR>();
            foreach (RlCharsRecord rlcr in recodeList)
            {
                if (CheckExpression(fieldName, fieldValue, rlcr))
                {
                    CNV_LCHAR lcr = rlcr.GetLChars(lshet, startDateTime);
                    lcrl.Add(lcr);
                }
            }
            return lcrl;
        }

        protected virtual bool CheckExpression(DataRow dr, RlCharsRecord rlcr)
        {
            string fieldName; object fieldValue;
            if (!dr.Table.Columns.Contains(rlcr.Type))
                return false;
            fieldName = rlcr.Type;
            fieldValue = dr[fieldName];
            bool rv = CheckExpression(fieldName, fieldValue, rlcr);
            return rv;
        }

        protected virtual bool CheckExpression(string fieldName, object fieldValue, RlCharsRecord rlcr)
        {
            if (fieldName == "ALL") return true;
            bool succeded = false;
            if (fieldValue is string || fieldValue is bool)
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

    public class RlCharsRecord
    {
        // 
        public string Type { get; set; }

        public string Range { get; set; }

        public int Lcharcd { get; set; }

        public string Lcharname { get; set; }

        public int Valuecd { get; set; }

        public string Valuename { get; set; }

        public string Addparam { get; set; }


        public RlCharsRecord(string aType, string aRange, int alCharcd, string alCharName,
            int aValuecd, string aValueName, string aAddParam)
        {
            Type = aType;
            Range = aRange;
            Lcharcd = alCharcd;
            Lcharname = alCharName;
            Valuecd = aValuecd;
            Valuename = aValueName;
            Addparam = aAddParam;
        }

        public CNV_LCHAR GetLChars(string lshet, DateTime date)
        {
            var lcr = new CNV_LCHAR
            {
                LSHET = lshet,
                LCHARCD = Lcharcd,
                LCHARNAME = Lcharname,
                VALUE_ = Valuecd,
                VALUEDESC = Valuename,
                DATE_ = date
            };

            return lcr;
        }
    }
}
