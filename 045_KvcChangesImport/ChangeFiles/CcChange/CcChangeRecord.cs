using System.Collections.Generic;
using System.Data;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public class CcChangeRecord : CcAbonent, IChangeRecord
    {
        public readonly string FileName;

        public CcChangeRecord() { }

        public CcChangeRecord(List<byte[]> bytes, CcFileInfo ccFileInfo) : base(bytes, ccFileInfo)
        {
            FileName = ccFileInfo.FileName;
        }

        public CcChangeRecord(List<DataRow> rows, ushort fileYear, ushort fileMonth)
            : base(rows, fileYear, fileMonth)
        {
            
        }

        public static CcChangeRecord CreateFromOdantXls(List<DataRow> rows, ushort fileYear, ushort fileMonth)
        {
            var changeRecord = new CcChangeRecord
            {
                СостояниеЛс = 0,
                ХозяинЛс = 0,
                FileYear = fileYear,
                FileMonth = fileMonth,
            };
            if (rows == null || rows.Count == 0) return changeRecord;
            changeRecord.LsKvc = new LsKvc(rows[0][1].ToString(), true);
            for (int i = 0; i < rows.Count; i++)
            {
                changeRecord.Жители.Add(Citizen.CreateFromOdatXls(rows[i], changeRecord.LsKvc));
            }
            return changeRecord;
        }
    }
}
