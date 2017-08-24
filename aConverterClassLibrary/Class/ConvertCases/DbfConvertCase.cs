namespace aConverterClassLibrary
{
    public abstract class DbfConvertCase : ConvertCase
    {
        public abstract void DoDbfConvert();


        public sealed override void DoConvert()
        {
            InitializeManager(aConverter_RootSettings.SourceDbfFilePath);
            try
            {
                DoDbfConvert();
            }
            finally
            {
                Dispose();
            }
        }
    }
}
