namespace _048_Rgmek.NachImport
{
    public class CommonNachFactory : INachImportFactory
    {
        public INachImport Create(string filePath)
        {
            return new CommonNachImport();
        }
    }
}
