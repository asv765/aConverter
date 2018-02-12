using System;
using System.Data;
using _048_Rgmek.Records;

namespace _048_Rgmek.NachImport
{
    public interface INachImport
    {
        void Import(DataRow dr, Action<NachExcelRecord> importAction);
    }
}
