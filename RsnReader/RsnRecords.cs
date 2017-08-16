using System;
using System.Linq;

namespace RsnReader
{
    #region Общие

    #region Счетчик

    public class RsnCounterKod
    {
        public byte Код;
        public decimal Значение;

        public RsnCounterKod() { }

        public RsnCounterKod(uint info, byte vid)
        {
            string sinfo = $"{info:D10}";
            Код = Byte.Parse(sinfo.Substring(0, 2));
            Значение = Int32.Parse(sinfo.Substring(2, 8)) / RsnHelper.GetDigitsCount(vid, true);
        }
    }

    public class RsnCounterKodRaz : RsnCounterKod
    {
        public byte Разрядность;

        public RsnCounterKodRaz() { }

        public RsnCounterKodRaz(uint info, byte vid) : base(info, vid)
        {
            string sinfo = $"{info:D10}";
            Разрядность = Byte.Parse(sinfo.Substring(2, 1));
            Значение = UInt32.Parse(sinfo.Substring(3, 7)) / RsnHelper.GetDigitsCount(vid, true);
        }
    }

    #endregion

    public class Record_BaseSum
    {
        public byte Вид;
        //public ushort Год;
        //public ushort Месяц;
        public decimal Сумма;
        public byte? КодСчетчика;

        public Record_BaseSum() { }

        public Record_BaseSum(byte[] octet, bool isCounter = false)
        {
            Constructor(octet, isCounter);
            //octet.ParseShortDate(out Год, out Месяц);
        }

        public Record_BaseSum(byte[] octet, ushort year, ushort month, bool isCounter = false)
        {
            Constructor(octet, isCounter);
            //Год = year;
            //Месяц = month;
        }

        private void Constructor(byte[] octet, bool isCounter)
        {
            Вид = octet[1];
            if (isCounter)
            {
                string info = $"{octet.ToUInt32():D10}";
                КодСчетчика = byte.Parse(info.Substring(0, 2));
                Сумма = Int32.Parse(info.Substring(2, 8)) / 100m;
            }
            else
                Сумма = octet.ToInt32() / 100m;
        }
    }

    public class Record_BaseValue
    {
        public byte Вид;
        public decimal Значение;

        public Record_BaseValue() { }

        public Record_BaseValue(byte[] octet, int? startIndex = null, decimal divider = 100m, bool int32 = true)
        {
            Вид = octet[1];
            var value = startIndex != null
                ? int32 ? octet.ToInt32((int)startIndex) : octet.ToUInt16((int)startIndex)
                : int32 ? octet.ToInt32() : octet.ToUInt16();
            Значение = value / divider;
        }
    }

    public class Record_BaseDateValue
    {
        public byte Вид;
        public ushort Год;
        public ushort Месяц;
        public int Значение;

        public Record_BaseDateValue(byte[] octet)
        {
            Вид = octet[1];
            octet.ParseShortDate(out Год, out Месяц);
            Значение = octet.ToInt32();
        }
    }

    public class Record_ПотреблениеПоНормативам : Record_BaseValue
    {
        public ushort? КодПараметра;

        public Record_ПотреблениеПоНормативам() { }

        public Record_ПотреблениеПоНормативам(byte[] octet, bool withParam = false)
        {
            Вид = octet[1];
            Значение = octet.ToInt32()/RsnHelper.GetDigitsCount(Вид, false);
            if (withParam) КодПараметра = octet.ToUInt16();
        }
    }

    #endregion

    /// <summary>
    /// Алгоритмы
    /// </summary>
    public class Record_014
    {
        public byte Вид;
        public ushort ХозяинВида;
        public byte Алгоритм;

        public Record_014() { }

        public Record_014(byte[] octet)
        {
            Вид = octet[1];
            ХозяинВида = octet.ToUInt16();
            Алгоритм = octet[5];
        }
    }

    /// <summary>
    /// Площадь или норматив для начислений
    /// </summary>
    public class Record_016
    {
        public byte Вид;
        public decimal Значение1;
        public decimal Значение2;

        public Record_016() { }

        public Record_016(byte[] octet)
        {
            Вид = octet[1];
            switch (Вид)
            {
                case 2:
                    Значение1 = octet[2];
                    Значение2 = octet.ToInt32()/100m;
                    break;
                case 3:
                    Значение1 = octet.ToUInt16()/100m;
                    Значение2 = octet[4];
                    break;
                case 5:
                    Значение1 = octet.ToInt32()/100m;
                    break;
                case 6:
                    Значение1 = octet.ToInt32()/100000m;
                    break;
                case 29:
                    Значение1 = octet.ToInt32()/10m;
                    break;
                default:
                    Значение1 = octet.ToInt32();
                    break;
            }
        }
    }

    /// <summary>
    /// Потребление по счетчикам
    /// </summary>
    public class Record_037
    {
        public byte Вид;
        //public ushort Год;
        //public ushort Месяц;
        public decimal Потребление;
        public byte? КодСчетчика;

        public Record_037() { }

        public Record_037(byte[] octet, bool isCounter = false)
        {
            Вид = octet[1];
            //octet.ParseShortDate(out Год, out Месяц);
            if (isCounter)
            {
                var counter = new RsnCounterKod(octet.ToUInt32(), Вид);
                КодСчетчика = counter.Код;
                Потребление = counter.Значение;
            }
            else
                Потребление = octet.ToInt32()/RsnHelper.GetDigitsCount(Вид, true);
        }

        public Record_037(byte[] octet, ushort year, ushort month, bool isCounter = false)
        {
            Вид = octet[1];
            //Год = year;
            //Месяц = month;
            if (isCounter)
            {
                var counter = new RsnCounterKod(octet.ToUInt32(), Вид);
                КодСчетчика = counter.Код;
                Потребление = counter.Значение;
            }
            else
                Потребление = octet.ToInt32() / RsnHelper.GetDigitsCount(Вид, true);
        }
    }

    /// <summary>
    /// Показания на конец месяц (непонятная запись)
    /// </summary>
    public class Record_017
    {
        /// <summary>
        /// 03,05,06,07
        /// </summary>
        public byte Вид;
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public RsnCounterKodRaz Счетчик;

        public Record_017() { }

        public Record_017(byte[] octet)
        {
            Вид = octet[1];
            octet.ParseShortDate(out РасчетныйГод, out РасчетныйМесяц);
            Счетчик = new RsnCounterKodRaz(octet.ToUInt32(), Вид);
        }
    }

    /// <summary>
    /// Счетчик зам. УЗС на начало месяцо (непонятная запись)
    /// </summary>
    public class Record_019
    {
        /// <summary>
        /// 03,05,07
        /// </summary>
        public byte Вид;
        public ushort Год;
        public ushort Месяц;
        public RsnCounterKodRaz Счетчик;

        public Record_019() { }

        public Record_019(byte[] octet)
        {
            Вид = octet[1];
            octet.ParseShortDate(out Год, out Месяц);
            Счетчик = new RsnCounterKodRaz(octet.ToUInt32(), Вид);
        }
    }

    /// <summary>
    /// Признак не начисления среднего
    /// </summary>
    public class Record_020
    {
        /// <summary>
        /// 03,05,07
        /// </summary>
        public byte Вид;
        /// <summary>
        /// 1 - Водоканал, 2 - нет ОДН, нет ОДН по ОПУ
        /// </summary>
        public byte ПризнакНеНачислСреднее;

        public Record_020() { }

        public Record_020(byte[] octet)
        {
            Вид = octet[1];
            ПризнакНеНачислСреднее = octet[4];
        }
    }

    /// <summary>
    /// Признак снятия повышающего коэффецианта
    /// </summary>
    public class Record_025
    {
        public byte Вид;
        public byte ПризнакСнятияПовышКоэффициента;

        public Record_025() { }

        public Record_025(byte[] octet)
        {
            Вид = octet[1];
            ПризнакСнятияПовышКоэффициента = octet[4];
        }
    }

    /// <summary>
    /// Долг по счетчику в Квт/ч
    /// </summary>
    public class Record_028
    {
        /// <summary>
        /// 03
        /// </summary>
        public byte Вид;
        public ushort ГодУстановкиДолга;
        public ushort МесяцУстановкиДолга;
        public RsnCounterKod Счетчик;

        public Record_028() { }

        public Record_028(byte[] octet)
        {
            Вид = octet[1];
            octet.ParseShortDate(out ГодУстановкиДолга, out МесяцУстановкиДолга);
            Счетчик = new RsnCounterKod(octet.ToUInt32(), Вид);
        }
    }

    /// <summary>
    /// УЗС на начало месяца
    /// </summary>
    public class Record_030
    {
        /// <summary>
        /// 03,05,06,07
        /// </summary>
        public byte Вид;
        //public ushort ПоследнийОплачиваемыйГод;
        //public ushort ПоследнийОплачиваемыйМесяц;
        public RsnCounterKodRaz Счетчик;

        public Record_030() { }

        public Record_030(byte[] octet)
        {
            Вид = octet[1];
            //octet.ParseShortDate(out ПоследнийОплачиваемыйГод, out ПоследнийОплачиваемыйМесяц);
            Счетчик = new RsnCounterKodRaz(octet.ToUInt32(), Вид);
        }
    }

    /// <summary>
    /// УЗС на конец месяца
    /// </summary>
    public class Record_031
    {
        /// <summary>
        /// 03,05,06,07
        /// </summary>
        public byte Вид;
        //public ushort ПоследнийОплачиваемыйГод;
        //public ushort ПоследнийОплачиваемыйМесяц;
        public RsnCounterKodRaz Счетчик;

        public Record_031() { }

        public Record_031(byte[] octet)
        {
            Вид = octet[1];
            //octet.ParseShortDate(out ПоследнийОплачиваемыйГод, out ПоследнийОплачиваемыйМесяц);
            Счетчик = new RsnCounterKodRaz(octet.ToUInt32(), Вид);
        }
    }

    /// <summary>
    /// Тариф по счетчику
    /// </summary>
    public class Record_034 : Record_BaseValue
    {
        //public ushort Год;
        //public ushort Месяц;
        public byte КодСчетчика;

        public Record_034() { }

        public Record_034(byte[] octet)
        {
            Вид = octet[1];
            //octet.ParseShortDate(out Год, out Месяц);
            string data = $"{octet.ToUInt32():D10}";
            Значение = Int32.Parse(data.Substring(2, 8))/100m;
            КодСчетчика = Byte.Parse(data.Substring(0, 2));
        }
    }

    /// <summary>
    /// Ист.инф по счетчику
    /// </summary>
    public class Record_038
    {
        /// <summary>
        /// 03,05,06,07
        /// </summary>
        public byte Вид;
        //public ushort Год;
        //public ushort Месяц;
        public byte КодСчетчика;
        public byte ИстИнф;

        public Record_038() { }

        public Record_038(byte[] octet)
        {
            Вид = octet[1];
            //octet.ParseShortDate(out Год, out Месяц);
            string data = $"{octet.ToUInt32():D10}";
            КодСчетчика = Byte.Parse(data.Substring(0, 2));
            ИстИнф = Byte.Parse(data.Substring(2, 8));
        }
    }

    /// <summary>
    /// Нормативы
    /// </summary>
    public class Record_134
    {
        public byte Вид;
        /// <summary>
        /// 02 - Норматив(плита+колонка)(2зн) 06 - Норматив(4зн) 17 - Норматив на водоотвед. х/в(2зн) 03,05,07,83,85,87 - Норматив (2зн). Если есть код параметра, то норматив
        /// </summary>
        public decimal Значение1;
        /// <summary>
        /// 02 - Норматив отопление(2зн) 06 - Потребление(4зн) 17 - Норматив на водоотвед. г/в(2зн) 03,05,07,83,85,87 - Норматив на полив(3зн)
        /// </summary>
        public decimal Значение2;
        /// <summary>
        /// животные, надвор.постройки
        /// </summary>
        public ushort? КодПараметра;

        public Record_134() { }

        public Record_134(byte[] octet, bool withParam = false)
        {
            Вид = octet[1];
            switch (Вид)
            {
                case 02:
                    Значение1 = octet.ToUInt16(4)/100m;
                    Значение2 = octet.ToUInt16(6)/100m;
                    break;
                case 06:
                    Значение1 = octet.ToUInt16() / 100000m;
                    Значение2 = octet.ToInt32() / 100000m;
                    break;
                case 17:
                    Значение1 = octet.ToUInt16() / 100m;
                    Значение2 = octet.ToUInt16(4) / 100m;
                    break;
                case 03:
                case 07:
                case 83:
                case 85:
                case 87:
                    Значение1 = octet.ToUInt16() / 100m;
                    Значение2 = octet.ToInt32() / 1000m;
                    break;
                case 05:
                    if (withParam)
                    {
                        КодПараметра = octet.ToUInt16();
                        Значение1 = octet.ToUInt32()/100m;
                    }
                    else
                    {
                        Значение1 = octet.ToUInt16()/100m;
                        Значение2 = octet.ToInt32()/1000m;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Начисление по нормативу
    /// </summary>
    public class Record_160 : Record_BaseValue
    {
        public ushort Коэффециент;

        public Record_160() { }

        public Record_160(byte[] octet)
        {
            Вид = octet[1];
            Коэффециент = octet.ToUInt16();
            Значение = octet.ToUInt32()/100m;
        }
    }

    public class Record_171
    {
        public byte НомерЖителя;
        public byte ПризнакНанимателяИлиСобственника;
        public byte Статус;

        public Record_171() { }

        public Record_171(byte[] octet)
        {
            НомерЖителя = octet[1];
            ПризнакНанимателяИлиСобственника = octet[2];
            Статус = octet[3];
        }
    }

    /// <summary>
    /// Тарифы для компонентов в содержении жилья
    /// </summary>
    public class Record_026
    {
        public byte Вид;
        public decimal Тариф;
        public decimal Начисление;

        public Record_026() { }

        public Record_026(byte[] octet)
        {
            Вид = octet[1];
            Тариф = octet.ToUInt16() / 100m;
            Начисление = octet.ToUInt16(4) / 100m;
        }
    }

    /// <summary>
    /// Параметры дома
    /// </summary>
    public class Record_200
    {
        public byte КодРеквезита;
        public decimal Значение;

        public Record_200() { }

        public Record_200(byte[] octet)
        {
            КодРеквезита = octet[1];
            switch (КодРеквезита)
            {
                case 1:
                case 3:
                case 4:
                case 6:
                case 13:
                case 14:
                case 15:
                case 16:
                case 20:
                case 22:
                case 23:
                case 26:
                case 28:
                    Значение = octet.ToUInt16(4);
                    break;

                case 7:
                case 8:
                case 9:
                case 10:
                case 17:
                case 25:
                case 27:
                case 29:
                case 30:
                    Значение = octet.ToUInt32() / 100m;
                    break;

                case 21:
                case 250:
                case 251:
                    break;

                case 2:
                case 5:
                case 11:
                case 12:
                case 18:
                case 19:
                    break;

                default:
                    throw new Exception($"Необработанй код реквезита {КодРеквезита} для записи типа 200");
            }
        }
    }

    /// <summary>
    /// Значаениен параметров придомового участка (животные, надворные постройки)
    /// </summary>
    public class Record_029
    {
        public ushort КодПараметра;
        public uint Значение;

        public Record_029() { }

        public Record_029(byte[] octet)
        {
            КодПараметра = octet.ToUInt16();
            Значение = octet.ToUInt32();
        }
    }

    /// <summary>
    /// Информация об ОПУ
    /// </summary>
    public class Record_201
    {
        public byte Вид;
        public byte НомерСчетчика;
        public byte Разрядность;
        public byte Коэффициент;
        /// <summary>
        /// 0-рабочий, 1-резервный, 2-неисправен
        /// </summary>
        public byte СтатусСчетчика;
        public byte НаличиеСчетчика;

        public Record_201() { }

        public Record_201(byte[] octet)
        {
            Вид = octet[2];
            НомерСчетчика = octet[3];
            Разрядность = octet[4];
            Коэффициент = octet[5];
            СтатусСчетчика = octet[6];
            НаличиеСчетчика = octet[7];
        }
    }

    /// <summary>
    /// Показания и потребление по ОПУ
    /// </summary>
    public class Record_202
    {
        public byte Вид;
        public byte НомерСчетчика;
        public decimal Значение;

        public Record_202(byte[] octet)
        {
            Вид = octet[1];
            НомерСчетчика = octet[2];
            Значение = octet.ToUInt32() / RsnHelper.GetDigitsCount(Вид, true);
        }
    }
}
