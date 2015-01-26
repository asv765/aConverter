using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class LshetLengthMismatchError: ErrorClass
    {
        private int lshetlength;

        public LshetLengthMismatchError(int Alshet_digits_count, int AlshetLength)
        {
            this.ErrorName = String.Format("Длина строки лицевого счета в таблице ABONENT.DBF ({0}) не совпадает со значением системной переменное LSHET_DIGITS_COUNT ({1})",
                AlshetLength, Alshet_digits_count);
            this.lshetlength = AlshetLength;
            this.IsTerminating = false;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            LshetLengthMismatchCorrectionCase llmcc = new LshetLengthMismatchCorrectionCase(lshetlength);
            llmcc.ParentError = this;
            CorrectionCases.Add(llmcc);
        }
    }
}
