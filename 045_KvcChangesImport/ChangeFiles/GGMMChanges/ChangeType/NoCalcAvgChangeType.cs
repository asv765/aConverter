using System;
using System.Collections.Generic;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class NoCalcAvgChangeType : IGGMMChangeType
    {
        private List<NoCalcAvgCharVid> _noCalcChars;

        public NoCalcAvgChangeType(List<NoCalcAvgCharVid> noCalcChars)
        {
            _noCalcChars = noCalcChars;
        }

        public void Convert(GGMMChangeRecord record)
        {
            string lshet = Consts.LsDic[record.LsKvc.Ls];
            foreach (var noCalcChar in _noCalcChars)
            {
                DigitChangesImport.CcharsList.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    CHARCD = noCalcChar.CcharId,
                    VALUE_ = 0,
                    DATE_ = record.FileDate
                });
                DigitChangesImport.LcharsList.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    LCHARCD = noCalcChar.LcharId,
                    VALUE_ = 1,
                    DATE_ = record.FileDate
                });
                DigitChangesImport.LcharsList.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    LCHARCD = noCalcChar.LcharId,
                    VALUE_ = 0,
                    DATE_ = noCalcChar.Date
                });
            }
        }

        public class NoCalcAvgCharVid
        {
            public int CcharId;
            public int LcharId;
            public DateTime Date;
        }
    }

    public class NoCalcAvgChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 3)
            {
                if (record.Графа >= 23 && record.Графа <= 27)
                {
                    ushort year, month;
                    RsnHelper.ParseShortDate(Convert.ToUInt16(record.Значение), out year, out month);
                    var date = new DateTime(year, month, 1).AddMonths(1);
                    var noCalcChars = new List<NoCalcAvgChangeType.NoCalcAvgCharVid>
                    {
                        new NoCalcAvgChangeType.NoCalcAvgCharVid
                        {
                            CcharId = 10009,
                            LcharId = 10009,
                            Date = date
                        },
                        new NoCalcAvgChangeType.NoCalcAvgCharVid
                        {
                            CcharId = 10004,
                            LcharId = 10004,
                            Date = date
                        },
                         new NoCalcAvgChangeType.NoCalcAvgCharVid
                        {
                            CcharId = 10005,
                            LcharId = 10005,
                            Date = date
                        },
                    };
                    return new NoCalcAvgChangeType(noCalcChars);
                }
                else
                    return null;
            }
            else 
                return null;
        }
    }
}
