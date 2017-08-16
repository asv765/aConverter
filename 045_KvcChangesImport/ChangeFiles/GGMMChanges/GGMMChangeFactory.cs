using System.IO;
using System.Text.RegularExpressions;
using _045_KvcChangesImport.ChangeFiles.Abstract;
using _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges
{
    public class GGMMChangeFactory : IChangeFileFactory
    {
        public static Regex VodoIzmRegex = new Regex(@"IZM(\d{4})\.xlsx");


        public IChangeFile Create(string fileName)
        {
            string onlyName = Path.GetFileName(fileName);
            var match = VodoIzmRegex.Match(onlyName);
            if (match.Success)
                return new GGMMChangeFileVodoIzm(fileName);

            return new GGMMChangeFile(fileName);
        }
    }
}
