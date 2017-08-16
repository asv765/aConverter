using System;
using System.Data;
using System.IO;
using aConverterClassLibrary;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class GGMMChangeFileVodoIzm : ChangeFileInfo, IChangeFile
    {
        public GGMMChangeFileVodoIzm(string fileName)
        {
            string sDate = GGMMChangeFactory.VodoIzmRegex.Match(Path.GetFileName(fileName)).Groups[1].Value;
            FileName = fileName;
            РасчетныйГод = (ushort) (int.Parse(sDate.Substring(0, 2)) + 2000);
            РасчетныйМесяц = (ushort) int.Parse(sDate.Substring(2, 2));
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            using (var dt =  Utils.ReadExcelFile(FileName, "IZM"))
            {
                StepCounter.StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    StepCounter.Iterate();
                    var change = GGMMChangeRecord.CreateFromIzmVodo(dr, this);
                    if (change == null) continue;
                    convertAction(change);
                }
            }
            StepCounter.StepFinish();
        }
    }
}
