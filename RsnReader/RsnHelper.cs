using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace RsnReader
{
    public static class RsnHelper
    {
        public readonly static byte[] PeniResources = new byte[] { 41, 42, 43, 45, 46, 47, 50, 55, 57, 65, 73, 74, 75, 77 };

        public static readonly byte[] NotImportingHouseChars = {2, 5, 11, 12, 18, 19, 20, 21, 250, 251};

        public static readonly Dictionary<byte, int> HouseCcharRecode = new Dictionary<byte, int>
        {
            {3, -2},
            {4, -3},
            {7, 101039},
            {8, 32009},
            {9, 101040},
            {13, 31009},
            {14, 31008},
            {15, 31010},
            {16, 31014},
            {17, 31013},
            {22, 31011},
            {23, 31007},
            {25, 32010},
            {27, 31012},
            {29, 32001},
            {30, 32002},
        };

        public static readonly Dictionary<byte, int> HouseLcharRecode = new Dictionary<byte, int>
        {
            {26, 62663002},
            {28, 62990001},
        };

        public static decimal GetDigitsCount(byte vid, bool isCounter)
        {
            switch (vid)
            {
                case 02: return isCounter ? 100m : 100m;
                case 03: return isCounter ? 1m : 1m;
                case 05: return isCounter ? 10m : 100m;
                case 06: return isCounter ? 1000m : 100000m;
                case 07: return isCounter ? 10m : 100m;
                case 15: return isCounter ? 100m : 100m;
                case 17: return isCounter ? 10m : 100m;
                case 35: return isCounter ? 100m : 100m;
                //case 83: return isCounter ? 100m : 100m;
                //case 85: return isCounter ? 1000m : 1000m;
                //case 87: return isCounter ? 1000m : 1000m;
                case 83: return isCounter ? 1m : 100m;
                case 85: return isCounter ? 10m : 1000m;
                case 87: return isCounter ? 10m : 1000m;
                default: throw new Exception($"Необработанный вид ({vid}) для определения количества знаков после запятой в счетчиках");
            }
        }

        public static void ParseShortDate(this byte[] bytes, out ushort year, out ushort month, int startIndex = 2)
        {
            ParseShortDate(bytes.ToUInt16(startIndex), out year, out month);
        }

        public static void ParseShortDate(ushort shortDate, out ushort year, out ushort month)
        {
            string sdate = $"{shortDate:D4}";
            year = UInt16.Parse("20" + sdate.Substring(0, 2));
            month = UInt16.Parse(sdate.Substring(2, 2));
        }

        public static void ParseShortDate(this DataRow row, out ushort year, out ushort month, int index)
        {
            ParseShortDate(Convert.ToUInt16(row[index]), out year, out month);
        }

        public static DateTime ToDateTime(this byte[] bytes, int startIndex = 4, bool yearFirst = false)
        {
            return ToDateTime(bytes.ToUInt32(startIndex), yearFirst);
        }

        public static DateTime ToDateTime(this DataRow row, int index, bool yearFirst = false)
        {
            return ToDateTime(Convert.ToUInt32(row[index]), yearFirst);
        }

        private static DateTime ToDateTime(uint date, bool yearFirst = false)
        {
            string sdate = $"{date:D8}";
            string firstSymbols = sdate.Substring(0, 2);

            int year, month, day;
            if (yearFirst)
            {
                if (firstSymbols == "00")
                {
                    year = Int32.Parse(sdate.Substring(2, 2)) + 2000;
                    month = Int32.Parse(sdate.Substring(4, 2));
                    day = Int32.Parse(sdate.Substring(6, 2));
                }
                else
                {
                    year = Int32.Parse(sdate.Substring(0, 4));
                    month = Int32.Parse(sdate.Substring(4, 2));
                    day = Int32.Parse(sdate.Substring(6, 2));
                }
            }
            else
            {
                if (firstSymbols == "00")
                {
                    year = Int32.Parse(sdate.Substring(6, 2)) + 2000;
                    month = Int32.Parse(sdate.Substring(4, 2));
                    day = Int32.Parse(sdate.Substring(2, 2));
                }
                else
                {
                    year = Int32.Parse(sdate.Substring(4, 4));
                    month = Int32.Parse(sdate.Substring(2, 2));
                    day = Int32.Parse(sdate.Substring(0, 2));
                }
            }

            return new DateTime(year, month, day);
        }

        public readonly static DateTime StartDate1999 = new DateTime(1999, 12, 31);

        public static DateTime GetDateFromStart(this byte[] bytes, DateTime startDate, int startIndex = 2, bool int32 = false)
        {
            return int32
                ? startDate.AddDays(bytes.ToUInt32(startIndex))
                : startDate.AddDays(bytes.ToUInt16(startIndex));
        }

        public static string GetShortDate(DateTime date)
        {
            return GetShortDate(date.Year, date.Month);
        }

        public static string GetShortDate(int year, int month)
        {
            return $"{year.ToString().Substring(2,2):D2}{month:D2}";
        }

        public static bool IsEmpty(this byte[] octet, int startIndex = 0, int endIndex = 7)
        {
            int result = 0;
            for(int i = startIndex; i <= endIndex; i++)
            {
                result += octet[i];
            }
            return result == 0;
        }

        #region BitConverter

        public static UInt16 ToUInt16(this byte[] bytes, int startIndex = 2)
        {
            return BitConverter.ToUInt16(bytes, startIndex);
        }

        public static UInt32 ToUInt32(this byte[] bytes, int startIndex = 4)
        {
            return BitConverter.ToUInt32(bytes, startIndex);
        }

        public static Int32 ToInt32(this byte[] bytes, int startIndex = 4)
        {
            return BitConverter.ToInt32(bytes, startIndex);
        }

        #endregion
    }
}
