using System;
using System.Data;
using aConverterClassLibrary;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges
{
    public class GraphInfo
    {
        public int Nach;
        public int GraphKod;
        public byte Vid;
        public int MaxValue;
        public string Name;

        public GraphInfo(DataRow dr)
        {
            Nach = Convert.ToInt32(dr[1]);
            GraphKod = Convert.ToInt32(dr[2]);
            Vid = Convert.ToByte(dr[3]);
            MaxValue = Convert.ToInt32(dr[4]);
            Name = dr[6].ToString();
        }

        public static GraphInfo[] GetFullGraphInfo(string excelFileName, string listName)
        {
            var dt = Utils.ReadExcelFile(excelFileName, listName);
            var allGraphs = new GraphInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                allGraphs[i] = new GraphInfo(dt.Rows[i]);
            }
            return allGraphs;
        }
    }
}
