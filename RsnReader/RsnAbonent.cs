using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsnReader
{
    public class RsnAbonent
    {
        public static RsnAbonent[] Abonents;

        //010
        public byte Регион;
        public byte Округ;
        public byte ПаспортныйСтол;
        private uint Адр1;
        //011
        public byte СостояниеЛС;
        public ushort ХозяинЛС;
        private uint Адр2;
        //012
        public byte НеПечататьАдр2;
        public byte ХарактеристикаКвартиры;
        //013
        private byte _количествоСобственников;
        public byte КоличествоСобственников => FileYear == 2017 ? _количествоСобственников : (byte)ПризнакиЖителей.Count(c => $"{c.Статус:D2}".Substring(0, 1) == "1");
        public byte КолвоДнейСОтоплением;
        public byte КоличествоПМЖ;
        public byte КоличествоВо;
        public byte КоличествоПМП;
        public decimal ОбщаяПлощадь;
        public int ЧислоПроживающих => КоличествоПМЖ /*- КоличествоВо*/ + КоличествоПМП;
        //public int ЧислоСобственников => ПризнакиЖителей.Count(c => $"{c.Статус:D2}".Substring(0, 1) == "1");
        public int ГазовоеОборудование;
        //024
        public decimal ЖилаяПлощадь;
        public decimal ПлощадьКомнат;
        //060,061,062
        public string FIO;
        //100
        public byte ВидЖилогоПомещения;
        public byte ФормаСобственностиЖилогоПомещения;
        public byte ЦельИспользованияЖилищногоФонда;
        public byte СпециализированныйЖилищныйФонд;
        public byte СпособУправления;
        public byte ПризнакТсжЖск;
        public byte ТипПомещения;
        //135
        public int КоличествоКаналов;
        //216
        public decimal ОтапливаемаяПлощадьДома;

        public List<Record_014> Алгоритмы = new List<Record_014>(); //014
        public List<Record_016> ПлощадиНормативы = new List<Record_016>(); //016
        //public List<Record_017> ПокНаКонецМесяца = new List<Record_017>(); //017
        //public List<Record_019> СчЗамУЗСнаНачМес = new List<Record_019>(); //019
        public List<Record_020> ПризнакиНеНачислСреднее = new List<Record_020>(); //020
        public List<Record_BaseSum> СписанныеСальдо = new List<Record_BaseSum>(); //022
        public List<Record_BaseSum> Суммы_ручныхПрошлых = new List<Record_BaseSum>(); //022
        public List<Record_BaseSum> Суммы_автопрошлых = new List<Record_BaseSum>(); //023
        //public List<Record_025> ПризнакиСнятияПовышКоэффициента = new List<Record_025>(); //025
        //public List<Record_028> ДолгиПоСчетчикам = new List<Record_028>(); //028
        public List<Record_030> УЗСнаНачМес = new List<Record_030>(); //030
        public List<Record_031> УЗСнаКонМес = new List<Record_031>(); //031
        public List<Record_BaseSum> ОплатыЗаПериод = new List<Record_BaseSum>(); //032, 132
        public List<Record_BaseSum> КорректировкиОплат = new List<Record_BaseSum>(); //032
        public List<Record_034> ТарифыПоСчетчикам = new List<Record_034>(); //034
        public List<Record_037> ПотребленияПоСчетчикам = new List<Record_037>(); //037
        public List<Record_037> КорректПотребПоСчетчикам = new List<Record_037>(); //037
        public List<Record_038> ИстИнфПоСчетчикам = new List<Record_038>(); //038
        public List<Record_BaseSum> НачисленияПоСчетчикам = new List<Record_BaseSum>(); //039
        public List<Record_BaseSum> КорректНачислПоСчетчикам = new List<Record_BaseSum>(); //039
        //public List<Record_BaseSum> ИтогСуммыПрошлИзменений = new List<Record_BaseSum>(); //119
        public List<Record_BaseSum> СальдоНаНачало = new List<Record_BaseSum>(); //130
        public List<Record_BaseSum> СальдоНаКонец = new List<Record_BaseSum>(); //131
        public List<Record_134> Нормативы = new List<Record_134>(); //134,137
        public List<Record_ПотреблениеПоНормативам> ПотреблениеПоНормативам = new List<Record_ПотреблениеПоНормативам>(); //134,135,136,138
        //public List<Record_BaseValue> ТарифыПоНормативам = new List<Record_BaseValue>(); //135
        public List<Record_160> НачисленияПоНормативам = new List<Record_160>(); //160
        public List<Record_171> ПризнакиЖителей = new List<Record_171>(); //171
        //public List<string> Жители = new List<string>(); //175
        public List<Record_BaseValue> Тарифы = new List<Record_BaseValue>(); //034,135
        public List<Record_026> ТарифыСодЖилКомп = new List<Record_026>(); //026
        public List<Record_BaseValue> РучныеНачисления = new List<Record_BaseValue>();
        public List<Record_200> ПараметрыДома = new List<Record_200>();
        public List<Record_029> ПараметрыУчастка = new List<Record_029>();
        public List<Record_201> ОПУ = new List<Record_201>();
        public List<Record_202> ПоказанияОПУ = new List<Record_202>();
        public List<Record_202> ПотреблениеОПУ = new List<Record_202>();
        public List<Record_BaseValue> НецОтоплТарифЭнергия = new List<Record_BaseValue>();
        public List<Record_BaseValue> НецОтоплТарифТеплонос = new List<Record_BaseValue>();
        public List<Record_BaseValue> НецОтоплПотреблЭнергияДом = new List<Record_BaseValue>();
        public List<Record_BaseValue> НецОтоплПотреблТеплоносДом = new List<Record_BaseValue>();

        //public List<dynamic> o206 = new List<dynamic>();

        public LsKvc LsKvc;
        //public string LsAbonent;

        public DateTime FileDate;
        public int FileYear;
        public int FileMonth;

        private static readonly byte[] IgnoredTypes = {};
        private static readonly byte[] UnknownTypes = {};

        public RsnAbonent() { }

        public RsnAbonent(List<byte[]> bytes, RsnFile rsnFileInfo)
        {
            FIO = "";
            FileDate = rsnFileInfo.FileDate;
            FileYear = FileDate.Year;
            FileMonth = FileDate.Month;
            int octecCount = bytes.Count;
            for (int i = 0; i < octecCount; i++)
            {
                var octet = bytes[i];
                var type = octet[0];
                var vid = octet[1];

                switch (type)
                {
                    case 010:
                        Регион = vid;
                        Округ = octet[2];
                        ПаспортныйСтол = octet[3];
                        Адр1 = octet.ToUInt32();
                        break;
                    case 011:
                        СостояниеЛС = vid;
                        ХозяинЛС = octet.ToUInt16();
                        Адр2 = octet.ToUInt32();
                        break;
                    case 012:
                        НеПечататьАдр2 = octet[2];
                        ХарактеристикаКвартиры = octet[3];
                        break;
                    case 013:
                        _количествоСобственников = octet[1];
                        КолвоДнейСОтоплением = octet[2];
                        КоличествоПМЖ = octet[3];
                        КоличествоВо = octet[4];
                        КоличествоПМП = octet[5];
                        ОбщаяПлощадь = octet.ToUInt16(6) / 10m;
                        break;
                    case 014:
                        Алгоритмы.Add(new Record_014(octet));
                        if (vid == 2) ГазовоеОборудование = octet[7];
                        break;
                    case 015:
                        РучныеНачисления.Add(new Record_BaseValue(octet));
                        break;
                    case 016:
                        if (vid == 0) break;
                        //Record_016 record016;
                        //switch (vid)
                        //{
                        //    case 02:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 03:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 05:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 06:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 29:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 16:
                        //    case 17:
                        //        record016 = new Record_016(octet);
                        //        break;
                        //    case 0:
                        //        continue;
                        //    default:
                        //        throw new Exception($"Необработанный вид ({vid}) для записи типа 016");
                        //}
                        ПлощадиНормативы.Add(new Record_016(octet));
                        break;
                    case 017:
                        //if (new[] {1,2,9,16,17}.Contains(vid)) continue; //TODO для старых файлов
                        //if (!new[] {03,05,06,07}.Contains(vid))
                        //    throw new Exception($"Необработанный вид ({vid}) для записи типа 017");
                        //ПокНаКонецМесяца.Add(new Record_017(octet));
                        break;
                    case 019:
                        //if (!new[] { 03, 05, 07 }.Contains(vid))
                        //    throw new Exception($"Необработанный вид ({vid}) для записи типа 019");
                        //СчЗамУЗСнаНачМес.Add(new Record_019(octet));
                        break;
                    case 020:
                        ПризнакиНеНачислСреднее.Add(new Record_020(octet));
                        break;
                    case 022:
                        if (octet.ToUInt16() == 9999)
                            СписанныеСальдо.Add(new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth));
                        else
                            Суммы_ручныхПрошлых.Add(new Record_BaseSum(octet));
                        break;
                    case 023:
                        Суммы_автопрошлых.Add(new Record_BaseSum(octet));
                        break;
                    case 024:
                        ЖилаяПлощадь = octet.ToUInt16() / 10m;
                        ПлощадьКомнат = octet.ToUInt16(4) / 10m;
                        break;
                    case 025:
                        //break; //TODO вроде не надо
                        //ПризнакиСнятияПовышКоэффициента.Add(new Record_025(octet));
                        break;
                    case 026:
                        if (FileYear < 2017) break;
                        //if (!new[] { 103, 105, 107 }.Contains(vid)) throw new Exception($"Необработанный вид ({vid}) для записи типа 026");
                        ТарифыСодЖилКомп.Add(new Record_026(octet));
                        break;
                    case 028:
                        //if (vid != 03)
                        //    throw new Exception($"Необработанный вид ({vid}) для записи типа 028");
                        //ДолгиПоСчетчикам.Add(new Record_028(octet));
                        break;
                    case 029:
                        ПараметрыУчастка.Add(new Record_029(octet));
                        break;
                    case 030:
                        //if (!new[] { 03, 05, 06, 07 }.Contains(vid)) throw new Exception($"Необработанный вид ({vid}) для записи типа 030");
                        УЗСнаНачМес.Add(new Record_030(octet));
                        break;
                    case 031:
                        //if (!new[] { 03, 05, 06, 07 }.Contains(vid)) throw new Exception($"Необработанный вид ({vid}) для записи типа 031");
                        УЗСнаКонМес.Add(new Record_031(octet));
                        break;
                    case 032:
                        if (new[] { 03,05,07 }.Contains(vid) && octet.ToUInt16() == 0)
                            КорректировкиОплат.Add(new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth, true));
                        else
                            ОплатыЗаПериод.Add(new Record_BaseSum(octet, true));
                        break;
                    case 034:
                        //if (ТарифыПоСчетчикам.Any(t => t.Вид == vid)) break; //TODO косячные абоненты
                        ТарифыПоСчетчикам.Add(new Record_034(octet));
                        if (Тарифы.Any(t => t.Вид == vid)) break;
                        Тарифы.Add(new Record_034(octet));
                        break;
                    case 037:
                        var indMonth = octet.ToUInt16();
                        if (indMonth == 0)
                        {
                            if (vid == 17)
                            {
                                var kor = new Record_037(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth);
                                kor.Потребление *= -1;
                                КорректПотребПоСчетчикам.Add(kor);
                            }
                            else /*if (new[] { 03, 05, 06, 07 }.Contains(vid))*/
                            {
                                var kor = new Record_037(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth, true);
                                kor.Потребление *= -1;
                                КорректПотребПоСчетчикам.Add(kor);
                            }
                            //else throw new Exception($"Необработанный вид ({vid}) для записи типа 037");
                        }
                        else
                        {
                            if (new[] { 03, 05, 06, 07 }.Contains(vid))
                            {
                                if (indMonth == 9999)
                                {
                                    var ind = new Record_037(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth, true);
                                    ind.Потребление *= -1;
                                    ПотребленияПоСчетчикам.Add(ind);
                                }
                                else
                                    ПотребленияПоСчетчикам.Add(new Record_037(octet, true));
                            }
                            else /*if (new[] { 17, 83, 85, 87 }.Contains(vid))*/
                            {
                                ПотребленияПоСчетчикам.Add(new Record_037(octet));
                            }
                            //else throw new Exception($"Необработанный вид ({vid}) для записи типа 037");
                        }
                        break;
                    case 038:
                        //if (new[] { 03,05,06,07}.Contains(vid))
                        ИстИнфПоСчетчикам.Add(new Record_038(octet));
                        break;
                    case 039:
                        var cntNachMonth = octet.ToUInt16();
                        if (cntNachMonth == 0)
                        {
                            if (vid == 17)
                            {
                                var kor = new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth);
                                kor.Сумма *= -1;
                                КорректНачислПоСчетчикам.Add(kor);
                            }
                            else /*if (new[] { 03, 05, 06, 07 }.Contains(vid))*/
                            {
                                var kor = new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth, true);
                                kor.Сумма *= -1;
                                КорректНачислПоСчетчикам.Add(kor);
                            }
                            //else throw new Exception($"Необработанный вид ({vid}) для записи типа 039");
                        }
                        else
                        {
                            if (new[] { 03, 05, 06, 07 }.Contains(vid))
                            {
                                if (cntNachMonth == 9999)
                                {
                                    var nach = new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth);
                                    nach.Сумма *= -1;
                                    НачисленияПоСчетчикам.Add(nach);
                                }
                                else
                                    НачисленияПоСчетчикам.Add(new Record_BaseSum(octet, true));
                            }
                            else /*if (new[] { 17, 83, 85, 87 }.Contains(vid))*/
                                НачисленияПоСчетчикам.Add(new Record_BaseSum(octet));
                            //else throw new Exception($"Необработанный вид ({vid}) для записи типа 039");
                        }
                        break;
                    case 041:
                        break; //TODO дублирует показания по одн, причем код услуги + 80
                    case 60:
                    case 61:
                    case 62:
                        FIO += rsnFileInfo.Encoding.GetString(octet, 1, 7);
                        break;
                    case 100:
                        ВидЖилогоПомещения = octet[1];
                        ФормаСобственностиЖилогоПомещения = octet[2];
                        ЦельИспользованияЖилищногоФонда = octet[3];
                        СпециализированныйЖилищныйФонд = octet[4];
                        СпособУправления = octet[5];
                        ПризнакТсжЖск = octet[6];
                        ТипПомещения = octet[7];
                        break;
                    case 119:
                        //ИтогСуммыПрошлИзменений.Add(new Record_BaseSum(octet));
                        break;
                    case 130:
                        СальдоНаНачало.Add(new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth));
                        break;
                    case 131:
                        СальдоНаКонец.Add(new Record_BaseSum(octet, rsnFileInfo.FileYear, rsnFileInfo.FileMonth));
                        break;
                    case 132:
                        if (octet.ToUInt16() == 0)
                            КорректировкиОплат.Add(new Record_BaseSum(octet));
                        else
                            ОплатыЗаПериод.Add(new Record_BaseSum(octet));
                        break;
                    case 134:
                        //if (vid == 02) break;
                        Нормативы.Add(new Record_134(octet));
                        if (vid == 06)
                        {
                            ПотреблениеПоНормативам.Add(new Record_ПотреблениеПоНормативам(octet));
                        }
                        break;
                    case 137:
                        Нормативы.Add(new Record_134(octet, true));
                        break;
                    case 135:
                        if (RsnHelper.PeniResources.Contains(vid)) break; //TODO пени
                        //if (new[] {01, 03, 05, 07, 17, 83, 85, 87,  15, 16, 20, 02, 06, 09, 08, 34, 28, 10, 29, 30, 11, 18, 35 }.Contains(vid))
                        //{
                        //ТарифыПоНормативам.Add(new Record_BaseValue(octet, 2, 100f, false));
                        if (!Тарифы.Any(t => t.Вид == vid))
                        {
                            if (vid == 06) 
                            {
                                Тарифы.Add(new Record_BaseValue(octet, 4));
                            }
                            else if (vid == 02)
                            {
                                var gazTarif = new[]
                                    {
                                        new Record_BaseValue(octet, 2, 1000, false),
                                        new Record_BaseValue(octet, 4, 1000, false),
                                        new Record_BaseValue(octet, 6, 1000, false)
                                    }.FirstOrDefault(t => t.Значение != 0);
                                if (gazTarif != null) Тарифы.Add(gazTarif);
                            }
                            else
                            {
                                Тарифы.Add(new Record_BaseValue(octet, 2, 100m, false));
                            }
                        }
                        if (vid == 09)
                        {
                            КоличествоКаналов = octet.ToUInt16(4);
                            break;
                        } 
                        if (octet.ToUInt32() != 0 && vid != 1 && vid != 2) ПотреблениеПоНормативам.Add(new Record_ПотреблениеПоНормативам(octet));
                        //}
                        //else throw new Exception($"Необработанный вид ({vid}) для записи типа 135");
                        break;
                    case 136:
                        //if (vid == 02) break;
                        //if (new[] {05, 83, 85, 87, 02}.Contains(vid))
                            ПотреблениеПоНормативам.Add(new Record_ПотреблениеПоНормативам(octet));
                        //else throw new Exception($"Необработанный вид ({vid}) для записи типа 136");
                        break;
                    case 138:
                        ПотреблениеПоНормативам.Add(new Record_ПотреблениеПоНормативам(octet, true));
                        break;
                    case 141:
                        //TODO непонятно. Дублирует потребление по счетчику, причем не у всех. Более того у абонентов со счетчикам
                        break;
                    case 160:
                        НачисленияПоНормативам.Add(new Record_160(octet));
                        break;
                    case 161: //TODO не понятно, что это
                        break;
                    case 171:
                        ПризнакиЖителей.Add(new Record_171(octet));
                        break;
                    case 175:
                        //if (FileYear < 2017 && vid == 1) КоличествоСобственников++;
                        //ParceCitizensFio(octet, rsnFileInfo.Encoding);
                        break;
                    case 200:
                        ПараметрыДома.Add(new Record_200(octet));
                        break;
                    case 201:
                        if (vid == 20) ОПУ.Add(new Record_201(octet));
                        else if (vid == 19)
                        {
                            var necTarif = new Record_BaseValue
                            {
                                Вид = octet[3],
                                Значение = octet.ToUInt32() / 100m
                            };
                            if (octet[2] == 1) НецОтоплТарифЭнергия.Add(necTarif);
                            else НецОтоплТарифТеплонос.Add(necTarif);
                        }
                        break;
                    case 202:
                        var ns = octet[3];
                        if (ns == 1) ПоказанияОПУ.Add(new Record_202(octet));
                        else if (ns == 2) ПотреблениеОПУ.Add(new Record_202(octet));
                        else if (ns == 3) break; //TODO
                        else throw new Exception($"Неизвестный номер подстроки {ns} для записи типа 202");
                        break;
                    case 203:
                        break;
                    case 205:
                        var nacPort = new Record_BaseValue
                        {
                            Вид = octet[2],
                            Значение = octet.ToInt32() / 1000m //TODO количество знаков
                        };
                        if (vid == 1) НецОтоплПотреблЭнергияДом.Add(nacPort);
                        else НецОтоплПотреблТеплоносДом.Add(nacPort);
                        break;
                    case 206:
                        //o206.Add(new { Vid = octet[1], Val = octet.ToInt32() });
                        break;
                    case 207:
                        break;
                    case 216:
                        ОтапливаемаяПлощадьДома = octet.ToUInt32() / 10m;
                        break;
                    default:
                        //if (IgnoredTypes.Contains(type)) break;
                        //if (UnknownTypes.Contains(type)) break;
                        //if (type == 101 || (type >= 103 && type <= 117) || (type >= 120 && type <= 127))
                        //{
                        //    break; //TODO Расчет пени
                        //}
                        //throw new Exception($"Необработанный тип ({type}) записи");
                        break;
                }
            }
            LsKvc = new LsKvc(Адр1, Адр2);
            if (!String.IsNullOrWhiteSpace(FIO))
            {
                FIO = FIO.Replace("\0", "").Trim();
                //FIO = Regex.Replace(FIO, @"\s+", " ");
            }
            else FIO = null;
            //Жители = Жители.Select(c => c.Replace("\0", "").Trim()).ToList();
            if (!Нормативы.Any(n => n.Вид == 6) && ПлощадиНормативы.Any(n => n.Вид == 6))
            {
                var pn = ПлощадиНормативы.First(n => n.Вид == 6);
                Нормативы.Add(new Record_134()
                {
                    Вид = pn.Вид,
                    Значение1 = pn.Значение1
                });
            }
        }

        public static List<int> КолвоКал = new List<int>();

        //private void ParceCitizensFio(byte[] octet, Encoding encoding)
        //{
        //    byte strNumber = octet[1];
        //    if (strNumber == 0) return;
        //    else if (strNumber == 1)
        //        Жители.Add(encoding.GetString(octet, 2, 6));
        //    else
        //        Жители[Жители.Count - 1] += encoding.GetString(octet, 2, 6);
        //}

        #region ShouldSerialize

        public bool ShouldSerializeАлгоритмы()
        {
            return (Алгоритмы != null && Алгоритмы.Count > 0);
        }

        public bool ShouldSerializeПлощадиНормативы()
        {
            return (ПлощадиНормативы != null && ПлощадиНормативы.Count > 0);
        }

        //public bool ShouldSerializeПокНаКонецМесяца()
        //{
        //    return (ПокНаКонецМесяца != null && ПокНаКонецМесяца.Count > 0);
        //}

        //public bool ShouldSerializeСчЗамУЗСнаНачМес()
        //{
        //    return (СчЗамУЗСнаНачМес != null && СчЗамУЗСнаНачМес.Count > 0);
        //}

        //public bool ShouldSerializeПризнакиНеНачислСреднее()
        //{
        //    return (ПризнакиНеНачислСреднее != null && ПризнакиНеНачислСреднее.Count > 0);
        //}

        public bool ShouldSerializeСписанныеСальдо()
        {
            return (СписанныеСальдо != null && СписанныеСальдо.Count > 0);
        }

        public bool ShouldSerializeСуммы_ручныхПрошлых()
        {
            return (Суммы_ручныхПрошлых != null && Суммы_ручныхПрошлых.Count > 0);
        }

        public bool ShouldSerializeСуммы_автопрошлых()
        {
            return (Суммы_автопрошлых != null && Суммы_автопрошлых.Count > 0);
        }

        //public bool ShouldSerializeПризнакиСнятияПовышКоэффициента()
        //{
        //    return (ПризнакиСнятияПовышКоэффициента != null && ПризнакиСнятияПовышКоэффициента.Count > 0);
        //}

        //public bool ShouldSerializeДолгиПоСчетчикам()
        //{
        //    return (ДолгиПоСчетчикам != null && ДолгиПоСчетчикам.Count > 0);
        //}

        public bool ShouldSerializeУЗСнаНачМес()
        {
            return (УЗСнаНачМес != null && УЗСнаНачМес.Count > 0);
        }

        public bool ShouldSerializeУЗСнаКонМес()
        {
            return (УЗСнаКонМес != null && УЗСнаКонМес.Count > 0);
        }

        public bool ShouldSerializeОплатыЗаПериод()
        {
            return (ОплатыЗаПериод != null && ОплатыЗаПериод.Count > 0);
        }

        public bool ShouldSerializeКорректировкиОплат()
        {
            return (КорректировкиОплат != null && КорректировкиОплат.Count > 0);
        }

        //public bool ShouldSerializeТарифыПоСчетчикам()
        //{
        //    return (ТарифыПоСчетчикам != null && ТарифыПоСчетчикам.Count > 0);
        //}

        public bool ShouldSerializeПотребленияПоСчетчикам()
        {
            return (ПотребленияПоСчетчикам != null && ПотребленияПоСчетчикам.Count > 0);
        }

        public bool ShouldSerializeКорректПотребПоСчетчикам()
        {
            return (КорректПотребПоСчетчикам != null && КорректПотребПоСчетчикам.Count > 0);
        }

        public bool ShouldSerializeИстИнфПоСчетчикам()
        {
            return (ИстИнфПоСчетчикам != null && ИстИнфПоСчетчикам.Count > 0);
        }

        public bool ShouldSerializeНачисленияПоСчетчикам()
        {
            return (НачисленияПоСчетчикам != null && НачисленияПоСчетчикам.Count > 0);
        }

        public bool ShouldSerializeКорректНачислПоСчетчикам()
        {
            return (КорректНачислПоСчетчикам != null && КорректНачислПоСчетчикам.Count > 0);
        }

        //public bool ShouldSerializeИтогСуммыПрошлИзменений()
        //{
        //    return (ИтогСуммыПрошлИзменений != null && ИтогСуммыПрошлИзменений.Count > 0);
        //}

        public bool ShouldSerializeСальдоНаНачало()
        {
            return (СальдоНаНачало != null && СальдоНаНачало.Count > 0);
        }

        public bool ShouldSerializeСальдоНаКонец()
        {
            return (СальдоНаКонец != null && СальдоНаКонец.Count > 0);
        }

        public bool ShouldSerializeНормативы()
        {
            return (Нормативы != null && Нормативы.Count > 0);
        }

        public bool ShouldSerializeПотреблениеПоНормативам()
        {
            return (ПотреблениеПоНормативам != null && ПотреблениеПоНормативам.Count > 0);
        }

        //public bool ShouldSerializeТарифыПоНормативам()
        //{
        //    return (ТарифыПоНормативам != null && ТарифыПоНормативам.Count > 0);
        //}

        public bool ShouldSerializeТарифы()
        {
            return (Тарифы != null && Тарифы.Count > 0);
        }

        public bool ShouldSerializeНачисленияПоНормативам()
        {
            return (НачисленияПоНормативам != null && НачисленияПоНормативам.Count > 0);
        }

        public bool ShouldSerializeПризнакиЖителей()
        {
            return (ПризнакиЖителей != null && ПризнакиЖителей.Count > 0);
        }

        //public bool ShouldSerializeЖители()
        //{
        //    return (Жители != null && Жители.Count > 0);
        //}

        public bool ShouldSerializeТарифыСодЖилКомп()
        {
            return (ТарифыСодЖилКомп != null && ТарифыСодЖилКомп.Count > 0);
        }

        public bool ShouldSerializeПараметрыДома()
        {
            return (ПараметрыДома != null && ПараметрыДома.Count > 0);
        }

        public bool ShouldSerializeРучныеНачисления()
        {
            return (РучныеНачисления != null && РучныеНачисления.Count > 0);
        }

        public bool ShouldSerializeПараметрыУчастка()
        {
            return (ПараметрыУчастка != null && ПараметрыУчастка.Count > 0);
        }

        #endregion
    }
} 