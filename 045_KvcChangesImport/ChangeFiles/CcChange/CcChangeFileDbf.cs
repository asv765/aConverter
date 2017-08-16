using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using aConverterClassLibrary;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public class CcChangeFileDbf : IChangeFile
    {
        private readonly string _fileName;

        public CcChangeFileDbf(string fileName)
        {
            _fileName = fileName;
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            DataTable dt = Utils.ReadExcelFile(_fileName, "izm_sp");
            StepCounter.StepStart(dt.Rows.Count);
            string lastLs = null;
            var rows = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];
                string currentLs = row[0].ToString();
                if (currentLs == lastLs)
                {
                    rows.Add(row);
                }
                else
                {
                    lastLs = currentLs;
                    if (rows.Count > 0)
                    { 
                        var abonent = new CcChangeRecord(rows, (ushort)ChangesConsts.ImportYear, (ushort)ChangesConsts.ImportMonth);
                        convertAction(abonent);
                    }
                    rows = new List<DataRow> {row};
                }
                StepCounter.Iterate();
            }
            StepCounter.StepFinish();
        }
    }
}
