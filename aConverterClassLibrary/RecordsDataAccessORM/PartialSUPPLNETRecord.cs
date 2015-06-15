using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class SUPPLNET
    {
        public SUPPLNET CloneRecord()
        {
            var sr = new SUPPLNET
            {
                LSHET = this.LSHET,
                SERVICECD = this.SERVICECD,
                KNOTL1CD = this.KNOTL1CD,
                KNOTL1NAME = this.KNOTL1NAME,
                KNOTL2CD = this.KNOTL2CD,
                KNOTL2NAME = this.KNOTL2NAME,
                CONNECTED = this.CONNECTED,
                SUPPDATE = this.SUPPDATE
            };

            return sr;
        }
    }
}
