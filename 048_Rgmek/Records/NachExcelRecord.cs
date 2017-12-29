using System;
using System.Data;

namespace _048_Rgmek.Records
{
    public class NachExcelRecord
    {
        public string LsKvc;
        //public SourceType Source;
        public ServiceType Service;
        public NachType Nach;
        public TarifType Tarif;
        public ZoneType Zone;
        public decimal Price;
        public int People;
        public decimal Volume;
        public decimal Sum;
        public decimal SumCoef;

        public NachExcelRecord(DataRow dr)
        {
            LsKvc = dr[0].ToString().Trim();
            /*string value = dr[1].ToString().Trim().ToLower();
            switch (value)
            {
                case "квц":
                    Source = SourceType.Kvc;
                    break;
                case "ргмэк":
                    Source = SourceType.Rgmek;
                    break;
                default:
                    throw new Exception($"Неизвестный источник {dr[1]}");
            }*/
            string value = dr[2].ToString().Trim().ToLower();
            switch (value)
            {
                case "жилые":
                    Service = ServiceType.Living;
                    break;
                case "одн":
                case "однмкд":
                    Service = ServiceType.Odn;
                    break;
                default:
                    throw new Exception($"Неизвестный вид услуги {dr[2]}");
            }
            value = dr[3].ToString().Trim().ToLower();
            switch (value)
            {
                case "поприбору":
                    Nach = NachType.WithDevice;
                    break;
                case "безприбора":
                    Nach = NachType.WithoutDevice;
                    break;
                case "допначисление":
                    Nach = NachType.AddNach;
                    break;
                default:
                    throw new Exception($"Неизвестный вид расчета {dr[3]}");
            }
            value = dr[4].ToString().Trim().ToLower();
            switch (value)
            {
                case "гп1":
                    Tarif = TarifType.Gp1;
                    break;
                case "гп2":
                    Tarif = TarifType.Gp2;
                    break;
                case "эп1":
                    Tarif = TarifType.Ep1;
                    break;
                case "эп2":
                    Tarif = TarifType.Ep2;
                    break;
                case "сельский":
                    Tarif = TarifType.Village;
                    break;
                case "?":
                    Tarif = TarifType.Unknown;
                    break;
                default:
                    throw new Exception($"Неизвестный тариф {dr[4]}");
            }
            value = dr[5].ToString().Trim().ToLower();
            switch (value)
            {
                case "-":
                case "?":
                    Zone = ZoneType.None;
                    break;
                case "день":
                    Zone = ZoneType.Day;
                    break;
                case "ночь":
                    Zone = ZoneType.Night;
                    break;
                default:
                    throw new Exception($"Неизвестная зона {dr[5]}");
            }
            value = dr[6].ToString();
            Price = String.IsNullOrWhiteSpace(value) ? 0 : Convert.ToDecimal(value);
            value = dr[7].ToString();
            People = String.IsNullOrWhiteSpace(value) ? 0 : Convert.ToInt32(value);
            value = dr[8].ToString();
            Volume = String.IsNullOrWhiteSpace(value) ? 0 : Convert.ToDecimal(value);
            value = dr[9].ToString();
            Sum = String.IsNullOrWhiteSpace(value) ? 0 : Convert.ToDecimal(value);
            value = dr[10].ToString();
            SumCoef = String.IsNullOrWhiteSpace(value) ? 0 : Convert.ToDecimal(value);
        }


        public enum SourceType
        {
            Kvc,
            Rgmek
        }

        public enum ServiceType
        {
            Living = 9,
            Odn = 29
        }

        public enum NachType
        {
            WithoutDevice = 0,
            WithDevice = 1,
            AddNach
        }

        public enum TarifType
        {
            Unknown = 10,
            Gp1 = 9001,
            Gp2 = 9004, // плюс зона будет зонный режим
            Ep1 = 9002,
            Ep2 = 9006, // плюс зона будет зонный режим
            Village = 9003
        }

        public enum ZoneType
        {
            None = 0,
            Day = 1,
            Night = 2
        }
    }
}
