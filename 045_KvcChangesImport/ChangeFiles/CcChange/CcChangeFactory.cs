using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public class CcChangeFactory : IChangeFileFactory
    {
        public IChangeFile Create(string fileName)
        {
            string fn = fileName.ToLower();
            if (fn.Contains(".dbf.xlsx") || fn.Contains(".dbf.xls")) return new CcChangeFileDbf(fileName);
            if (fn.Contains(".xls") || fn.Contains(".xlsx")) return new CcChangeOdantExcel(fileName);
            return new CcChangeFile(fileName);
        }

        public enum FactoryType
        {
            RmbBin,
            DbfRso,
            ExcelOdant
        }
    }
}
