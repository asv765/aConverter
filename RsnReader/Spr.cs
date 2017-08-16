using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace RsnReader
{
    public class Spr
    {
        public byte Ns { get; set; }
        public byte Rg { get; set; }

        public Int32 R1 { get; set; }
        public Int32 R2 { get; set; }
        public Int32 R3 { get; set; }
        public Int64 R4 { get; set; }

        public string Sr40 { get; set; }
        public string Sr20 { get; set; }
    }

    public class FullSpr
    {
        public static string SPRFilePath = @"D:\Organizations\KVC\Spr\Spr";

        public Spr[] Data;

        public FullSpr(int sprNumber, int year, int month)
        {
            Data = JsonConvert.DeserializeObject<Spr[]>(File.ReadAllText($"{SPRFilePath}{sprNumber}{RsnHelper.GetShortDate(year, month)}.json"));
        }

        public Spr[] GetSpecificDictionary(byte dictionaryId)
        {
            return Data.Where(s => s.Ns == dictionaryId).ToArray();
        }
    }

    public class Spr0 : FullSpr
    {
        public Spr0(int year, int month) : base(0, year, month)
        {

        }

        public Spr[] GetSubSpr(SubSpr ns)
        {
            return GetSpecificDictionary((byte)ns);
        }

        public enum SubSpr
        {
            РодственныеСвязи = 66,
        }
    }

    public class Spr1 : FullSpr
    {
        public Spr1(int year, int month) : base(1, year, month)
        {

        }

        public Spr[] GetSubSpr(SubSpr ns)
        {
            return GetSpecificDictionary((byte)ns);
        }

        public enum SubSpr
        {
            Улицы = 1,
            Хозяева = 2,
            ВходимостьХозяев = 6,
            ПродолжениеХозяев = 7,
            Округа = 32,
            ВидыУслуг = 44,
            ОтклоненияДомовИКвартир = 48,
            АдресаИТелефоныХозяев = 73,
            СчетаДляБезналичных = 122,
            ПродолжениеУлиц = 124,
            ВидыДляПени = 125
        }
    }

    public class Spr2 : FullSpr
    {
        public Spr2(int year, int month) : base(2, year, month)
        {

        }

        public Spr[] GetSubSpr(SubSpr ns)
        {
            return GetSpecificDictionary((byte)ns);
        }

        public enum SubSpr
        {
            Банки = 26,
        }
    }

    public class Spr4 : FullSpr
    {
        public Spr4(int year, int month) : base(4, year, month)
        {

        }

        public Spr[] GetSubSpr(SubSpr ns)
        {
            return GetSpecificDictionary((byte)ns);
        }

        public enum SubSpr
        {
            Алгоритмы = 21,
            КодСчетчика = 77
        }
    }
}
