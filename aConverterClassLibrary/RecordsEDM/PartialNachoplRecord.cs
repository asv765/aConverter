namespace aConverterClassLibrary.RecordsEDM
{
    public partial class nachopl
    {
        public decimal CalculatedEdebet
        {
            get
            {
                return this._BDEBET + this._FNATH + this._PROCHL - this._OPLATA;
            }
        }

        public decimal CalculatedBdebet
        {
            get
            {
                return this._EDEBET - this._FNATH - this._PROCHL + this._OPLATA;
            }
        }

    }
}
