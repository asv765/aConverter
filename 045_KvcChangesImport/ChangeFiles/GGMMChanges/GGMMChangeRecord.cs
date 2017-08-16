using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges
{
    public class GGMMChangeRecord : ChangeGGMMRecord, IChangeRecord
    {
        public string FileName;

        public GGMMChangeRecord() { }

        public GGMMChangeRecord(byte[] bytes, ChangeFileInfo fileInfo) : base(bytes, fileInfo)
        {
            FileName = fileInfo.FileName;
        }

        public static GGMMChangeRecord CreateFromIzmVodo(DataRow dr, ChangeFileInfo fileInfo)
        {
            int nachType, graph;
            switch (Convert.ToInt32(dr[3]))
            {
                case 14:
                //case 4:
                    nachType = 4;
                    graph = 24;
                    break;
                default:
                    return null;
            }

            return new GGMMChangeRecord
            {
                FileName = fileInfo.FileName,
                РасчетныйГод = fileInfo.РасчетныйГод,
                РасчетныйМесяц = fileInfo.РасчетныйМесяц,
                FileDate = new DateTime(fileInfo.РасчетныйГод, fileInfo.РасчетныйМесяц, 1),
                LsKvc = new LsKvc((uint)Convert.ToDouble(dr[0]), (uint)Convert.ToDouble(dr[1])),
                Значение = Convert.ToInt32(dr[4]),
                КодСчетчика = dr.IsNull(2) ? (byte)0 : Convert.ToByte(dr[2]),
                ХозяинИзменения = 0,
                ХозяинЛс = 0,
                ТипНачисления = (byte)nachType,
                Графа = (byte)graph
            };
        }

        public GraphInfo GetGraphInfo()
        {
            var info = ChangesConsts.AllGraphInfo.FirstOrDefault(gi => gi.Nach == ТипНачисления && gi.GraphKod == Графа);
            if (info == null) throw new Exception($"Не найдена графа Начисление = {ТипНачисления} Графа = {Графа}\r\nЛС={LsKvc.Ls}");
            return info;
        }
    }
}
