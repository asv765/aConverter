using System;

namespace aConverterClassLibrary.Class.Utils.KVC
{
    public class LsKvc
    {
        public UInt32 Adr1 { get; private set; }
        public UInt32 Adr2 { get; private set; }

        public UInt16 StreetId { get; private set; }
        public UInt16 HouseId { get; private set; }
        public UInt16 KorpusId { get; private set; }
        public UInt16 FlatId { get; private set; }
        public UInt16 RoomNumber { get; private set; }
        public UInt16 AddressHash { get; private set; }

        public string Ls { get; private set; }
        public string CombinedLs => $"{Adr1:D8}{Adr2:D6}";

        public LsKvc(UInt32 adr1, UInt32 adr2)
        {
            Constructor(adr1, adr2);
        }

        public LsKvc(UInt32 adr1)
        {
            Adr1 = adr1;
            Adr2 = 0;
            string sadr1 = $"{adr1:D8}";
            StreetId = UInt16.Parse(sadr1.Substring(0, 3));
            HouseId = UInt16.Parse(sadr1.Substring(3, 3));
            KorpusId = UInt16.Parse(sadr1.Substring(6, 2));

            Ls = $"{StreetId:D3}-{HouseId:D3}-{KorpusId:D2}";
        }

        public LsKvc(ulong combinedLs)
        {
            string ls = $"{combinedLs:D14}";
            Constructor(UInt32.Parse(ls.Substring(0, 8)), UInt32.Parse(ls.Substring(8, 6)));
        }

        public LsKvc(string lsKvc, bool withDashes)
        {
            if (withDashes) lsKvc = lsKvc.Replace("-", "");
            string ls = $"{UInt64.Parse(lsKvc):D14}";
            Constructor(UInt32.Parse(ls.Substring(0, 8)), UInt32.Parse(ls.Substring(8, 6)));
        }

        private void Constructor(UInt32 adr1, UInt32 adr2)
        {
            Adr1 = adr1;
            Adr2 = adr2;
            string sadr1 = $"{adr1:D8}";
            string sadr2 = $"{adr2:D6}";

            StreetId = UInt16.Parse(sadr1.Substring(0, 3));
            HouseId = UInt16.Parse(sadr1.Substring(3, 3));
            KorpusId = UInt16.Parse(sadr1.Substring(6, 2));
            FlatId = UInt16.Parse(sadr2.Substring(0, 3));
            RoomNumber = UInt16.Parse(sadr2.Substring(3, 1));
            AddressHash = UInt16.Parse(sadr2.Substring(4, 2));

            Ls = $"{StreetId:D3}-{HouseId:D3}-{KorpusId:D2}-{FlatId:D3}-{RoomNumber:D1}-{AddressHash:D2}";
        }

        public override string ToString()
        {
            return Ls;
        }
    }
}
