using System;
using System.Data;
using _048_Rgmek.Records;

namespace _048_Rgmek.NachImport
{
    public class CommonNachImport : INachImport
    {
        public void Import(DataRow dr, Action<NachExcelRecord> importAction)
        {
            importAction(new NachExcelRecord(dr));
        }
    }
}
