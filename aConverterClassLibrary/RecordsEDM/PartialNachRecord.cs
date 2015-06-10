using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace aConverterClassLibrary.RecordsEDM
{
    public partial class nach
    {
        public nach Clone()
        {
            var n = new nach()
            {
                LSHET = this.LSHET,
                DATE_VV = this.DATE_VV,
                DOCDATE = this.DOCDATE,
                DOCNAME = this.DOCNAME,
                DOCNUMBER = this.DOCNUMBER,
                DOCUMENTCD = this.DOCUMENTCD,
                FNATH = this.FNATH,
                MONTH = this.MONTH,
                MONTH2 = this.MONTH2,
                PROCHL = this.PROCHL,
                REGIMCD = this.REGIMCD,
                REGIMNAME = this.REGIMNAME,
                SERVICECD = this.SERVICECD,
                SERVICENAM = this.SERVICENAM,
                TYPE = this.TYPE,
                VOLUME = this.VOLUME,
                YEAR = this.YEAR,
                YEAR2 = this.YEAR2
            };
            return n;
        }

        public string InsertCommand
        {
            get
            {
                string s =
                    String.Format(
                        "INSERT INTO `nach` (`ID`, `LSHET`, `DOCUMENTCD`, `MONTH`, `YEAR`, `MONTH2`, `YEAR2`," +
                        " `FNATH`, `PROCHL`, `VOLUME`, `REGIMCD`, `REGIMNAME`, `SERVICECD`, `SERVICENAM`, " +
                        "`DATE_VV`, `TYPE`, `DOCNAME`, `DOCNUMBER`, `DOCDATE`) " +
                        "VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, '{11}', {12}, '{13}', '{14}', {15}, {16}, {17}, {18})",
                        this.ID,
                        this.LSHET,
                        this.DOCUMENTCD,
                        this.MONTH,
                        this.YEAR,
                        this.MONTH2,
                        this.YEAR2,
                        this.FNATH,
                        this.PROCHL,
                        this.VOLUME,
                        this.REGIMCD,
                        this.REGIMNAME,
                        this.SERVICECD,
                        this.SERVICENAM,
                        getDataForSql(this.DATE_VV),
                        this.TYPE,
                        String.IsNullOrEmpty(this.DOCNAME) ? "NULL" : "'"+this.DOCNAME+"'",
                        String.IsNullOrEmpty(this.DOCNUMBER) ? "NULL" : "'"+this.DOCNUMBER+"'", 
                        this.DOCDATE.HasValue ? "'"+getDataForSql(this.DOCDATE.Value) : "NULL"
                        );
                return s;
            }
        }

        private string getDataForSql(DateTime dt)
        {
            return String.Format("{0:D4}-{1:D2}-{2:D2}", dt.Year, dt.Month, dt.Day);
        }
    }
}
