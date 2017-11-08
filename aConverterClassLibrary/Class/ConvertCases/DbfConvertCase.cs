using aConverterClassLibrary.Class.Utils;

namespace aConverterClassLibrary
{
    public abstract class DbfConvertCase : ConvertCase
    {
        protected DbfManager DbfManager;

        public abstract void DoDbfConvert();

        public sealed override void DoConvert()
        {
            DbfManager = new DbfManager(aConverter_RootSettings.SourceDbfFilePath);
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
