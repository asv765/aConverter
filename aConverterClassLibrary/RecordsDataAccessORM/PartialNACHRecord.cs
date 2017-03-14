using System;
using System.Globalization;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_NACH
    {
        public string InsertSQL
        {
            get
            {
                // INSERT INTO CNV$NACH (ID, LSHET, DOCUMENTCD, MONTH_, YEAR_, MONTH2, YEAR2, 
                // FNATH, PROCHL, PROCHLVOLUME, VOLUME, REGIMCD, REGIMNAME, SERVICECD, SERVICENAME, 
                // DATE_VV, TYPE_, DOCNAME, DOCNUMBER, DOCDATE) 
                // VALUES (NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

                var insertNachTemplate =
                    "INSERT INTO CNV$NACH (ID, LSHET, DOCUMENTCD, MONTH_, YEAR_, MONTH2, YEAR2, FNATH, PROCHL, PROCHLVOLUME, VOLUME, REGIMCD, REGIMNAME, SERVICECD, SERVICENAME, DATE_VV, TYPE_, DOCNAME, DOCNUMBER, DOCDATE, VTYPE_) " +
                    "VALUES (NULL, '{0}', '{1}', {2}, {3}, {4}, {5}, " +
                    "{6}, {7}, {8}, {9}, {10}, '{11}', {12}, '{13}', " +
                    "'{14}', {15}, {16}, {17}, {18}, {19});";

                var sql = String.Format(insertNachTemplate,
                    LSHET, DOCUMENTCD, MONTH_, YEAR_, MONTH2, YEAR2,
                    FNATH.ToString(CultureInfo.InvariantCulture).Replace(',', '.'),
                    PROCHL.ToString(CultureInfo.InvariantCulture).Replace(',', '.'),
                    PROCHLVOLUME.ToString(CultureInfo.InvariantCulture).Replace(',', '.'),
                    VOLUME.ToString(CultureInfo.InvariantCulture).Replace(',', '.'),
                    REGIMCD, REGIMNAME, SERVICECD, SERVICENAME,
                    DATE_VV.ToString("d.MM.yyyy HH:mm:ss"), TYPE_,
                    String.IsNullOrEmpty(DOCNAME) ? "NULL" : "'" + DOCNAME + "'",
                    String.IsNullOrEmpty(DOCNUMBER) ? "NULL" : "'" + DOCNUMBER + "'",
                    DOCDATE.HasValue ?  DOCDATE.Value.ToString("d.MM.yyyy HH:mm:ss") : "NULL", 
                    VTYPE_.HasValue ? VTYPE_.Value.ToString() : "NULL"
                    );
                return sql;
            }
        }
    }
}
