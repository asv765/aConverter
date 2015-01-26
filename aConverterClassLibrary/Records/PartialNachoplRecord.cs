using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    public partial class NachoplRecord
    {
        public decimal CalculatedEdebet
        {
            get
            {
                return this.Bdebet + this.Fnath + this.Prochl - this.Oplata;
            }
        }

        public decimal CalculatedBdebet
        {
            get
            {
                return this.Edebet - this.Fnath - this.Prochl + this.Oplata;
            }
        }

    }
}
