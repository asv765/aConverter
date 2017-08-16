using System;

namespace RsnReader
{
    public class SutRecord
    {
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public byte ДеньКвц;
        public DateTime ДеньОплаты;
        public byte БанкПП;
        public byte ПодразделениеПП;
        public byte ОператорПП;
        private uint Адр1;
        private uint Адр2;
        public ushort ЗаГод;
        public ushort ЗаМесяц;
        public byte Вид;
        public decimal Сумма;
        public ushort ХозяинВида;

        public LsKvc LsKvc;
        public DateTime DateVv => new DateTime(РасчетныйГод, РасчетныйМесяц, ДеньКвц);

        public SutRecord (byte[] bytes)
        {
            bytes.ParseShortDate(out РасчетныйГод, out РасчетныйМесяц, 0);
            ДеньКвц = bytes[2];
            ДеньОплаты = bytes.ToDateTime(4, true);
            БанкПП = bytes[8];
            ПодразделениеПП = bytes[9];
            ОператорПП = bytes[10];
            Адр1 = bytes.ToUInt32(14);
            Адр2 = bytes.ToUInt32(18);
            LsKvc = new LsKvc(Адр1, Адр2);
            bytes.ParseShortDate(out ЗаГод, out ЗаМесяц, 22);
            Вид = bytes[24];
            Сумма = bytes.ToUInt32(25) / 100m;
            ХозяинВида = bytes.ToUInt16(37);
        }
    }
}
