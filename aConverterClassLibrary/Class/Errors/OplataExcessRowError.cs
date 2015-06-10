using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class OplataExcessRowError: ErrorClass
    {
        public OplataExcessRowError()
        {
            this.ErrorName = "В файле OPLATA.DBF есть записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в NACHOPL.DBF";
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи OPLATA.DBF с ненулевой суммой, для которых отсутствует соответствующая запись в NACHOPL.DBF",
                "select * " +
                        "from oplata o " +
                        "where o.Summa <> 0  AND " + 
                            "o.lshet + str(o.ServiceCD,5) + str(Year(o.Date_Vv),4) + str(month(o.Date_vv),2) not in " +
                            "(select n.lshet + str(n.ServiceCD,5) + str(n.Year,4) + str(n.month,2) from nachopl n)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            DeleteOplataExcessRowCorrectionCase cc = new DeleteOplataExcessRowCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }}
