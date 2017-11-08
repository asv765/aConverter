using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using _045_KvcChangesImport.ChangeFiles.Abstract;
using Utils = aConverterClassLibrary.Utils;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public class CcChangeOdantExcel : IChangeFile
    {
        private readonly string _fileName;

        public CcChangeOdantExcel(string fileName)
        {
            _fileName = fileName;
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            using (var dt = Utils.ReadExcelFile(_fileName, "Sheet"))
            {
                StepCounter.StepStart(dt.Rows.Count);
                string lastLs = null;
                var rows = new List<DataRow>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];
                    string currentLs = row[1].ToString();
                    if (currentLs == lastLs)
                    {
                        rows.Add(row);
                    }
                    else
                    {
                        lastLs = currentLs;
                        if (rows.Count > 0)
                        {
                            CcChangeRecord abonent;
                            try
                            {
                                abonent = CcChangeRecord.CreateFromOdantXls(rows, (ushort) ChangesConsts.ImportYear,
                                    (ushort) ChangesConsts.ImportMonth);
                            }
                            catch (Exception ex)
                            {
                                string message = $"Ошибка парсинга исходного файла. {ex} {_fileName}";
                                CitizenChangesImport.ErrorLog.Add(message);
                                rows = new List<DataRow> { row };
                                Task.Factory.StartNew(() => MessageBox.Show(message));
                                continue;
                            }
                            convertAction(abonent);
                        }
                        rows = new List<DataRow> { row };
                    }
                    StepCounter.Iterate();
                }
                var lastAbonent = CcChangeRecord.CreateFromOdantXls(rows, (ushort) ChangesConsts.ImportYear,
                    (ushort) ChangesConsts.ImportMonth);
                convertAction(lastAbonent);
                StepCounter.StepFinish();
            }
        }
    }
}
