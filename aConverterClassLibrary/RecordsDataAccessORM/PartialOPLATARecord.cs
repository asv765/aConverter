using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_OPLATA: ISQLInsertable
    {
        public string InsertSQL
        {
            get
            {
                // (1, '0100090417', '90417_1', 5, 2015, 170.77, '21-MAY-2015 00:00:00', '21-MAY-2015 00:00:00', 4, 'Почта', 3, 'Отопление', NULL);
                var insertOplataTemplate =
                    "INSERT INTO CNV$OPLATA (ID, LSHET, DOCUMENTCD, MONTH_, YEAR_, SUMMA, DATE_, DATE_VV, SOURCECD, SOURCENAME, SERVICECD, SERVICENAME, PRIM_) " +
                    "VALUES (NULL, '{0}', '{1}', {2}, {3}, {4}, '{5}', '{6}', {7}, '{8}', {9}, '{10}', {11});";

                var sql = String.Format(insertOplataTemplate,
                    LSHET, DOCUMENTCD, MONTH_, YEAR_, SUMMA.ToString(CultureInfo.InvariantCulture).Replace(',','.'),
                    DATE_.ToString("d.MM.yyyy HH:mm:ss"),
                    DATE_VV.ToString("d.MM.yyyy HH:mm:ss"),
                    SOURCECD,
                    SOURCENAME,
                    SERVICECD,
                    SERVICENAME,
                    String.IsNullOrEmpty(PRIM_) ? "NULL" : "'" + PRIM_ + "'"
                    );
                return sql;
            }
        }
    }
}
