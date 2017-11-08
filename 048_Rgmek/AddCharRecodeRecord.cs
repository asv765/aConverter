using System;
using System.ComponentModel;
using System.Data;

namespace _048_Rgmek
{
    public class AddCharRecodeRecord
    {
        public RgmekType RType;
        public string RgmekCode;
        public string CharName;
        public int AbonentCode;
        public AbonentType AType;
        public BelongType BelongTo;

        public AddCharRecodeRecord(DataRow dr)
        {
            string value = dr[0].ToString().Trim().ToLower();
            switch (value)
            {
                case "lschar":
                    RType = RgmekType.LsChar;
                    break;
                case "hschar":
                    RType = RgmekType.HsChar;
                    break;
                case "flchar":
                    RType = RgmekType.FlChar;
                    break;
                default:
                    throw new Exception($"Неизвестный тип харакетеристики РГМЭК {dr[0]}");
            }
            RgmekCode = dr[1].ToString().Trim();
            CharName = dr[2].ToString().Trim();
            AbonentCode = Convert.ToInt32(dr[3]);
            value = dr[4].ToString().Trim().ToLower();
            switch (value)
            {
                case "ccharslist":
                    AType = AbonentType.CChar;
                    break;
                case "lcharslist":
                    AType = AbonentType.LChar;
                    break;
                case "additionalchars":
                    AType = AbonentType.AChar;
                    break;
                default:
                    throw new Exception($"Неизвестный тип Абонент {dr[4]}");
            }
            value = dr[5].ToString().Trim().ToLower();
            switch (value)
            {
                case "абонент":
                    BelongTo = BelongType.Abonent;
                    break;
                case "дом":
                    BelongTo = BelongType.House;
                    break;
                default:
                    throw new Exception($"Неизвестная принадлежность {dr[5]}");
            }
        }

        public enum RgmekType
        {
            [Description("LsChars")]
            LsChar,
            [Description("HsChars")]
            HsChar,
            [Description("FlChars")]
            FlChar
        }

        public enum AbonentType
        {
            CChar,
            LChar,
            AChar
        }

        public enum BelongType
        {
            Abonent,
            House
        }
    }
}
