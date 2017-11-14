﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.Class.ConvertCases;
using aConverterClassLibrary.Class.Utils;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using _048_Rgmek.Records;
using static _048_Rgmek.Consts;

namespace _048_Rgmek
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public static readonly string LsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\lsrecode.csv";
        public static readonly string DuRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\durecode.csv";
        public static readonly string HouseRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\houserecode.csv";
        public static readonly string CnttypeRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\cnttyperecode.csv";
        public static readonly string CounterIdRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\counteridrecode.csv";
        public static readonly string GroupCounterIdRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\groupcounteridrecode.csv";
        public static readonly string IndtypeRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\indtyperecode.csv";
        public static readonly string PlaceToLshetRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\placetolshetrecode.csv";
        public static readonly string HouseToLshetRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\housetolshetrecode.csv";
        public static readonly string HouseStreetNameRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\housestreetnamerecode.csv";

        public static readonly string NachFilesDirectory = aConverter_RootSettings.SourceDbfFilePath;
        public static readonly string AddCharsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\AddCharsRecode.xlsx";

        public static readonly Dictionary<long, int> CcharRecode = new Dictionary<long, int>
        {
            {1, 2}, // Общая площадь
            {2, 4}, // Полезная площадь
            {3, 23}, // Число комнат
            {4, 5}, // Площадь нежилых помещений
            {5, 1}, // Число зарегистрированных
            {6, 3}, // Число проживающих
        };

        public static readonly Dictionary<KeyValuePair<long, long>, KeyValuePair<int, int>> LcharRecode =
            new Dictionary<KeyValuePair<long, long>, KeyValuePair<int, int>>
            {
                {new KeyValuePair<long, long>(1, 0), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(1, 1), new KeyValuePair<int, int>(2, 1)},
                {new KeyValuePair<long, long>(1, 2), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(2, 0), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(2, 1), new KeyValuePair<int, int>(2, 2)},
                {new KeyValuePair<long, long>(2, 2), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(3, 0), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(3, 1), new KeyValuePair<int, int>(2, 3)},
                {new KeyValuePair<long, long>(3, 2), new KeyValuePair<int, int>(2, 0)},
                {new KeyValuePair<long, long>(4, 0), new KeyValuePair<int, int>(13, 0)},
                {new KeyValuePair<long, long>(4, 1), new KeyValuePair<int, int>(13, 1)},
                {new KeyValuePair<long, long>(4, 2), new KeyValuePair<int, int>(13, 0)},
                {new KeyValuePair<long, long>(5, 1), new KeyValuePair<int, int>(5, 1)},
                {new KeyValuePair<long, long>(5, 2), new KeyValuePair<int, int>(5, 2)},
                {new KeyValuePair<long, long>(5, 3), new KeyValuePair<int, int>(5, 3)},
                {new KeyValuePair<long, long>(5, 4), new KeyValuePair<int, int>(5, 4)},
                {new KeyValuePair<long, long>(5, 5), new KeyValuePair<int, int>(5, 5)},
                {new KeyValuePair<long, long>(5, 6), new KeyValuePair<int, int>(5, 6)},
                {new KeyValuePair<long, long>(6, 1), new KeyValuePair<int, int>(6, 1)},
                {new KeyValuePair<long, long>(6, 4), new KeyValuePair<int, int>(6, 2)},
                {new KeyValuePair<long, long>(6, 7), new KeyValuePair<int, int>(6, 3)},
                {new KeyValuePair<long, long>(6, 8), new KeyValuePair<int, int>(6, 4)},
                {new KeyValuePair<long, long>(6, 9), new KeyValuePair<int, int>(6, 5)},
                {new KeyValuePair<long, long>(6, 10), new KeyValuePair<int, int>(6, 6)},
                {new KeyValuePair<long, long>(6, 11), new KeyValuePair<int, int>(6, 7)},
            };

        public static readonly Dictionary<int, int> TarifRecode = new Dictionary<int, int>
        {
            { 1, 5 }, //Газ. плиты двухставочный
            { 4, 1 }, //Городской газ. плиты
            { 5, 2 }, //Городской эл. плиты
            { 6, 4 }, //Гос. сектор
            { 7, 3 }, //Сельский
            { 8, 6 }, //Электроплиты двуставочный
        };

        public static readonly int CurrentMonth = 10;
        public static readonly int CurrentYear = 2017;
        public static readonly DateTime MinConvertDate = new DateTime(2017, 1, 1);
        public static readonly DateTime NullDate = new DateTime(1899, 12, 30);

        public const int UnknownTownId = 1;
        public const string UnknownTownName = "Неизвестен";
        public const int UnknownStreetId = 1;
        public const string UnknownStreetName = "Неизвестна";
        public const int DefaultDigitCount = 5;
    }

    public static class Utils
    {
        public static long GetValue(string key, Dictionary<string, long> dic, ref long lastvalue)
        {
            long value;
            if (!dic.TryGetValue(key, out value))
            {
                value = ++lastvalue;
                dic.Add(key, value);
            }
            return value;
        }

        public static void SaveDictionary(Dictionary<string, long> recodedic, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (var kvp in recodedic) sw.WriteLine(kvp.Key + ";" + kvp.Value);
            }
        }

        public static Dictionary<string, long> ReadDictionary(string filename)
        {
            var dic = new Dictionary<string, long>();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    dic.Add(line.Split(';')[0], Convert.ToInt64(line.Split(';')[1]));
                }
            }
            return dic;
        }
    }

    #region Конвертация во временные таблицы

    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();
            BufferEntitiesManager.CreateAllEntities();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    public class ConvertAbonent : DbfConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENTS - конвертация информации об абонентах";
            Position = 20;
            IsChecked = false;
        }

        Regex fioRegex = new Regex(@"(?<F>[А-Яа-я\w]+)( +(?<I>[А-Яа-я\w]{1})\.)?( +(?<O>[А-Яа-я\w]{1})\.)?");

        public override void DoDbfConvert()
        {
            SetStepsCount(2);
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

            var lsrecode = new Dictionary<string, long>();
            long lastls = 1010000000;
            var durecode = new Dictionary<string, long>();
            long lastducd = 0;
            var houserecode = new Dictionary<string, long>();
            long lasthousecd = 0;
            var placerecode = new Dictionary<string, List<string>>();

            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);

                var a = new CNV_ABONENT
                {
                    LSHET = Utils.GetValue(abonent.Lshet, lsrecode, ref lastls).ToString(),
                    EXTLSHET = abonent.Lshet.Trim().Replace("-",""),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    RAYONKOD = (int)abonent.Distkod,
                    RAYONNAME = abonent.Distname.Trim(),
                    PRIM_ = abonent.Address.Trim(),
                    POSTINDEX = abonent.Postindex.Trim(),
                };

                if (!String.IsNullOrWhiteSpace(abonent.Fio))
                {
                    string fio = abonent.Fio.Trim();
                    if (fioRegex.IsMatch(fio))
                    {
                        var match = fioRegex.Match(fio);
                        a.F = match.Groups["F"].Value;
                        a.I = match.Groups["I"].Value;
                        a.O = match.Groups["O"].Value;
                    }
                }

                a.TOWNSKOD = (int)abonent.Townskod;
                a.TOWNSNAME = abonent.Townsname.Trim();
                if (a.TOWNSKOD == 0)
                {
                    a.TOWNSKOD = UnknownTownId;
                    a.TOWNSNAME = UnknownTownName;
                }

                a.ULICAKOD = (int)abonent.Ulicakod;
                a.ULICANAME = abonent.Ulicaname.Trim();
                if (a.ULICAKOD == 0)
                {
                    a.ULICAKOD = UnknownStreetId;
                    a.ULICANAME = UnknownStreetName;
                }

                a.DUCD = (int)Utils.GetValue(abonent.Ducd, durecode, ref lastducd);
                a.DUNAME = abonent.Duname.Trim();

                a.HOUSECD = (int)Utils.GetValue(abonent.Housecd, houserecode, ref lasthousecd);
                a.HOUSENO = abonent.Ndoma.Trim();

                int korpusno;
                if (!String.IsNullOrEmpty(abonent.Korpustip.Trim()))
                {
                    a.HOUSEPOSTFIX = abonent.Korpustip.Substring(0, 3).Trim() + " " + abonent.Korpus.Trim();
                }
                else if (Int32.TryParse(abonent.Korpus, out korpusno))
                {
                    a.KORPUSNO = korpusno;
                }
                else
                {
                    a.HOUSEPOSTFIX = abonent.Korpus.Trim();
                }

                if (!String.IsNullOrWhiteSpace(abonent.Kvartira))
                {
                    int kvartira;
                    if (Int32.TryParse(abonent.Kvartira, out kvartira)) a.FLATNO = kvartira;
                    else a.FLATPOSTFIX = abonent.Kvartira;
                }

                if(a.ROOMNO != 0) a.ROOMNO = (short)abonent.Komnata;

                List<string> lshetsForPlace;
                if (!placerecode.TryGetValue(abonent.Placecd, out lshetsForPlace))
                    placerecode.Add(abonent.Placecd, new List<string> {a.LSHET});
                else 
                    lshetsForPlace.Add(a.LSHET);

                lca.Add(a);
                Iterate();
            }
            StepFinish();

            Utils.SaveDictionary(lsrecode, LsRecodeFileName);
            Utils.SaveDictionary(durecode, DuRecodeFileName);
            Utils.SaveDictionary(houserecode, HouseRecodeFileName);
            File.WriteAllLines(PlaceToLshetRecodeFileName, placerecode.SelectMany(p => p.Value.Select(v => $"{p.Key};{v}")));
            File.WriteAllLines(HouseToLshetRecodeFileName, lca.Select(a => $"{a.HOUSECD};{a.LSHET}"));
            File.WriteAllLines(HouseStreetNameRecodeFileName, lca.Select(a => $"{a.HOUSECD};ул. {a.ULICANAME} д.{a.HOUSENO}{a.HOUSEPOSTFIX}").Distinct());

            StepStart(1);
            SaveListInsertSQL(lca, InsertRecordCount);
            Iterate();

            StepFinish();
        }
    }

    public class ConvertChars : DbfConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - конвертация информации об количественных характеристиках";
            Position = 30;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(3);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var cold = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                cold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(cold.Lshet, out lshet))
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = lshet.ToString(),
                        SortLshet = lshet,
                        CHARCD = CcharRecode[cold.Charcd],
                        CHARNAME = cold.Charname.Trim(),
                        DATE_ = cold.Date,
                        VALUE_ = cold.Value_
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc = CharsRecordUtils.ThinOutList(lcc, true);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lcc);
            StepFinish();
        }
    }

    public class ConvertLChars : DbfConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - конвертация информации о качественных характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            BufferEntitiesManager.DropTableData("CNV$LCHARS");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lcc = new List<CNV_LCHAR>();

            using (var dt = Tmsource.GetDataTable("LCHARS"))
            {
                StepStart(dt.Rows.Count);
                var lcold = new LcharsRecord();
                foreach (DataRow dataRow in dt.Rows)
                {
                    lcold.ReadDataRow(dataRow);

                    long lshet;
                    if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                    {
                        var recodeValue = LcharRecode[new KeyValuePair<long, long>(lcold.Lcharcd, lcold.Value_)];
                        var c = new CNV_LCHAR
                        {
                            LSHET = lshet.ToString(),
                            SortLshet = lshet,
                            LCHARCD = recodeValue.Key,
                            LCHARNAME = lcold.Lcharname.Trim(),
                            DATE_ = lcold.Date,
                            VALUE_ = recodeValue.Value,
                            VALUEDESC = lcold.Valuedesc
                        };
                        lcc.Add(c);
                    }
                    Iterate();
                }
                StepFinish();
            }

            StepStart(1);
            lcc = LcharsRecordUtils.CreateUniqueLchars(lcc);
            StepFinish();

            StepStart(1);
            lcc = LcharsRecordUtils.ThinOutList(lcc, true);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lcc);
            StepFinish();
        }
    }

    public class ConvertLCharsTarifs : DbfConvertCase
    {
        public ConvertLCharsTarifs()
        {
            ConvertCaseName = "LCHARS Tarifs - конвертация информации о качественных характеристиках тарифов";
            Position = 41;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lcc = new List<CNV_LCHAR>();

            var nachFiles = ConvertNach.GetNachFiles();
            StepStart(nachFiles.Length);
            foreach (var file in nachFiles)
            {
                var fileDate = ConvertNach.GetNachFileDate(file);
                aConverterClassLibrary.Utils.ReadExcelFileByRow(file, null, dr =>
                {
                    var nachInfo = new NachExcelRecord(dr);

                    long lshet;
                    if (lsrecode.TryGetValue(nachInfo.LsKvc, out lshet))
                    {
                        int tarifValue;
                        switch (nachInfo.Tarif)
                        {
                            case NachExcelRecord.TarifType.Gp1:
                                tarifValue = 1;
                                break;
                            case NachExcelRecord.TarifType.Gp2:
                                tarifValue = 5;
                                break;
                            case NachExcelRecord.TarifType.Ep1:
                                tarifValue = 2;
                                break;
                            case NachExcelRecord.TarifType.Ep2:
                                tarifValue = 6;
                                break;
                            case NachExcelRecord.TarifType.Village:
                                tarifValue = 3;
                                break;
                            default:
                                return;
                        }
                        lcc.Add(new CNV_LCHAR
                        {
                            LSHET = lshet.ToString(),
                            SortLshet = lshet,
                            LCHARCD = 12,
                            LCHARNAME = "Тариф электроэнергии",
                            DATE_ = fileDate,
                            VALUE_ = tarifValue,
                            VALUEDESC = nachInfo.Nach.ToString()
                        });

                        if (nachInfo.Nach != NachExcelRecord.NachType.AddNach)
                            lcc.Add(new CNV_LCHAR
                            {
                                LSHET = lshet.ToString(),
                                SortLshet = lshet,
                                LCHARCD = 21,
                                LCHARNAME = "Сч. электроэнергии",
                                DATE_ = fileDate,
                                VALUE_ = nachInfo.Nach == NachExcelRecord.NachType.WithDevice ? 1 : 0,
                                VALUEDESC = nachInfo.Nach.ToString()
                            });
                    }
                });
                lcc = LcharsRecordUtils.CreateUniqueLchars(lcc);
                lcc = LcharsRecordUtils.ThinOutList(lcc, true);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc = LcharsRecordUtils.CreateUniqueLchars(lcc);
            StepFinish();

            StepStart(1);
            lcc = LcharsRecordUtils.ThinOutList(lcc, true);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lcc);
            StepFinish();
        }
    }

    public class ConvertAddChars : DbfConvertCase
    {
        public ConvertAddChars()
        {
            ConvertCaseName = "ADDCHARS - дополнительные характеристики";
            Position = 45;
            IsChecked = false;
        }

        private static List<CNV_CHAR> lacc;
        private static List<CNV_LCHAR> lalc;
        private static List<CNV_AADDCHAR> laac;
        private static List<CNV_CHARSHOUSES> lhcc;
        private static List<CNV_LCHARHOUSES> lhlc;
        private static List<CNV_HADDCHAR> lhac;

        private static Dictionary<string, int> valueListRecode;
        private static Dictionary<string, long> lsrecode;

        private static string[] IgnoreCharsList =
        {
            "58ee8b6a-e74a-48e2-b215-7f5156c51d07",
            "8db2b0be-a293-48b1-b025-d3123c9f7b29"
        };

        public override void DoDbfConvert()
        {
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$AADDCHAR");

            lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var houserecode = Utils.ReadDictionary(HouseRecodeFileName);
            var placeToLshetRecode = File.ReadAllLines(PlaceToLshetRecodeFileName)
                .Select(s =>
                {
                    string[] info = s.Split(';');
                    return new {Place = info[0], Lshet = long.Parse(info[1])};
                })
                .GroupBy(p => p.Place, p => p.Lshet)
                .ToDictionary(gp => gp.Key, gp => gp.ToList());
            var houseToLshetRecode = File.ReadAllLines(HouseToLshetRecodeFileName)
                .Select(s =>
                {
                    string[] info = s.Split(';');
                    return new { House = Convert.ToInt64(info[0]), Lshet = long.Parse(info[1]) };
                })
                .GroupBy(p => p.House, p => p.Lshet)
                .ToDictionary(gp => gp.Key, gp => gp.ToList());
            lacc = new List<CNV_CHAR>();
            lalc = new List<CNV_LCHAR>();
            laac = new List<CNV_AADDCHAR>();
            lhcc = new List<CNV_CHARSHOUSES>();
            lhlc = new List<CNV_LCHARHOUSES>();
            lhac = new List<CNV_HADDCHAR>();

            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            long housecd;
            using (var dt = aConverterClassLibrary.Utils.ReadExcelFile(AddCharsRecodeFileName, null))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    if (String.IsNullOrWhiteSpace(dr[3].ToString())) continue;
                    var recode = new AddCharRecodeRecord(dr);

                    if (IgnoreCharsList.Contains(recode.RgmekCode)) continue;

                    bool isEnumChar = recode.AType == AddCharRecodeRecord.AbonentType.LChar || 
                        (recode.AType == AddCharRecodeRecord.AbonentType.AChar && 
                            (Convert.ToInt32(fbManager.ExecuteScalar($"select ad.additionalchartype from additionalchars ad where ad.additionalcharcd = {recode.AbonentCode}")) == 7)); //проверка на тип перечисления в базе

                    valueListRecode = new Dictionary<string, int>();
                    if (isEnumChar)
                    {
                        string sql;
                        switch (recode.AType)
                        {
                            case AddCharRecodeRecord.AbonentType.LChar:
                                sql = $"select lv.logicsignificance, lv.significance from logicvalues lv where lv.kod = {recode.AbonentCode}";
                                break;
                            case AddCharRecodeRecord.AbonentType.AChar:
                                sql = $"select adv.avaliablevalue, adv.valuecd from additionalcharsvalues adv where adv.additionalcharcd = {recode.AbonentCode}";
                                break;
                            default:
                                throw new Exception($"Тип абонента {recode.AType} не поддерживает перечисление");
                        }
                        using (var dtCharValues = fbManager.ExecuteQuery(sql))
                        {
                            foreach (DataRow drValue in dtCharValues.Rows)
                            {
                                valueListRecode.Add(drValue[0].ToString().Trim().ToLower(), Convert.ToInt32(drValue[1]));
                            }
                        }
                    }


                    using (var dtChars = Tmsource.ExecuteQuery($"SELECT * FROM {recode.RType.GetDescription()} WHERE CHARCD = '{recode.RgmekCode}'"))
                    {
                        foreach (DataRow drChar in dtChars.Rows)
                        {
                            var charRecord = new CommonAddCharRecord(drChar);

                            if (CustomCharHandler(recode, charRecord)) continue;

                            switch (recode.RType)
                            {
                                case AddCharRecodeRecord.RgmekType.LsChar:
                                    switch (recode.BelongTo)
                                    {
                                        case AddCharRecodeRecord.BelongType.Abonent:
                                            long lshet;
                                            if (!lsrecode.TryGetValue(charRecord.Owner, out lshet)) continue;
                                            switch (recode.AType)
                                            {
                                                case AddCharRecodeRecord.AbonentType.LChar:
                                                    AddLChar(recode, charRecord, lshet);
                                                    break;
                                                case AddCharRecodeRecord.AbonentType.AChar:
                                                    AddAChar(recode, charRecord, lshet, isEnumChar);
                                                    break;
                                                default:
                                                    throw new Exception($"Необработанный тип абонента {recode.AType} для типа ргмэк {recode.RType} и принадлежности {recode.BelongTo}");
                                            }
                                            break;
                                        default:
                                            throw new Exception($"Необработанный тип принадлежности {recode.BelongTo} для типа ргмэк {recode.RType}");
                                    }
                                    break;
                                case AddCharRecodeRecord.RgmekType.HsChar:
                                    switch (recode.BelongTo)
                                    {
                                        case AddCharRecodeRecord.BelongType.House:
                                            if (!houserecode.TryGetValue(charRecord.Owner, out housecd)) continue;
                                            switch (recode.AType)
                                            {
                                                case AddCharRecodeRecord.AbonentType.AChar:
                                                    AddACharHouse(recode, charRecord, housecd, isEnumChar);
                                                    break;
                                                case AddCharRecodeRecord.AbonentType.LChar:
                                                    AddLcharHouse(recode, charRecord, housecd);
                                                    break;
                                                case AddCharRecodeRecord.AbonentType.CChar:
                                                    AddCcharHouse(recode, charRecord, housecd);
                                                    break;
                                                default:
                                                    throw new Exception($"Необработанный тип абонента {recode.AType} для типа ргмэк {recode.RType} и принадлежности {recode.BelongTo}");
                                            }
                                            break;
                                        case AddCharRecodeRecord.BelongType.Abonent:
                                            if (!houserecode.TryGetValue(charRecord.Owner, out housecd)) continue;
                                            List<long> lshets;
                                            if (!houseToLshetRecode.TryGetValue(housecd, out lshets)) continue;
                                            switch (recode.AType)
                                            {
                                                case AddCharRecodeRecord.AbonentType.CChar:
                                                    lshets.ForEach(ls => AddCChar(recode, charRecord, ls));
                                                    break;
                                                default:
                                                    throw new Exception($"Необработанный тип абонента {recode.AType} для типа ргмэк {recode.RType} и принадлежности {recode.BelongTo}");
                                            }
                                            break;
                                        default:
                                            throw new Exception($"Необработанный тип принадлежности {recode.BelongTo} для типа ргмэк {recode.RType}");
                                    }
                                    break;
                                case AddCharRecodeRecord.RgmekType.FlChar:
                                    switch (recode.BelongTo)
                                    {
                                        case AddCharRecodeRecord.BelongType.Abonent:
                                            List<long> lshets;
                                            if (!placeToLshetRecode.TryGetValue(charRecord.Owner, out lshets)) continue;
                                            switch (recode.AType)
                                            {
                                                case AddCharRecodeRecord.AbonentType.LChar:
                                                    lshets.ForEach(ls => AddLChar(recode, charRecord, ls));
                                                    break;
                                                default:
                                                    throw new Exception($"Необработанный тип абонента {recode.AType} для типа ргмэк {recode.RType} и принадлежности {recode.BelongTo}");
                                            }
                                            break;
                                        default:
                                            throw new Exception($"Необработанный тип принадлежности {recode.BelongTo} для типа ргмэк {recode.RType}");
                                    }
                                    break;
                                default:
                                    throw new Exception($"Необработанный тип принадлежности в ргмэк {recode.RType}");
                            }
                        }
                    }
                    Iterate();
                }
                StepFinish();
            }

            StepStart(6);
            lacc = CharsRecordUtils.ThinOutList(lacc, true);
            Iterate();
            lalc = LcharsRecordUtils.ThinOutList(lalc, true);
            Iterate();
            laac = AddCharsRecordUtils.ThinOutList(laac);
            Iterate();
            lhcc = HcharsRecordUtils.ThinOutList(lhcc);
            Iterate();
            lhlc = HcharsRecordUtils.ThinOutList(lhlc);
            Iterate();
            lhac = HcharsRecordUtils.ThinOutList(lhac);
            StepFinish();

            StepStart(6);
            BufferEntitiesManager.SaveDataToBufferIBScript(lacc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lalc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(laac);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lhcc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lhlc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lhac);
            StepFinish();
        }

        private void AddLChar(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long lshet)
        {
            lalc.Add(new CNV_LCHAR
            {
                LSHET = lshet.ToString(),
                SortLshet = lshet,
                LCHARCD = recode.AbonentCode,
                LCHARNAME = recode.CharName,
                VALUE_ = GetLcharRecode(recode, charRecord),
                VALUEDESC = charRecord.Value,
                DATE_ = charRecord.Date
            });
        }

        private void AddCChar(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long lshet)
        {
            lacc.Add(new CNV_CHAR
            {
                LSHET = lshet.ToString(),
                SortLshet = lshet,
                CHARCD = recode.AbonentCode,
                CHARNAME = recode.CharName,
                VALUE_ = Decimal.Parse(charRecord.Value),
                DATE_ = charRecord.Date
            });
        }

        private void AddAChar(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long lshet, bool isEnumChar)
        {
            string value;
            if (isEnumChar)
            {
                int recodeValue = GetLcharRecode(recode, charRecord);
                value = recodeValue.ToString();
            }
            else 
                value = charRecord.Value;
            laac.Add(new CNV_AADDCHAR
            {
                LSHET = lshet.ToString(),
                ADDCHARCD = recode.AbonentCode,
                VALUE = value
            });
        }

        private void AddACharHouse(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long housecd, bool isEnumChar)
        {
            string value;
            if (isEnumChar)
            {
                int recodeValue = GetLcharRecode(recode, charRecord);
                value = recodeValue.ToString();
            }
            else
                value = charRecord.Value;
            lhac.Add(new CNV_HADDCHAR
            {
                HOUSECD = (int?)housecd,
                ADDCHARCD = recode.AbonentCode,
                VALUE_ = value
            });
        }

        private void AddLcharHouse(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long housecd)
        {
            lhlc.Add(new CNV_LCHARHOUSES
            {
                HOUSECD = (int?)housecd,
                LCHARCD = recode.AbonentCode,
                LCHARNAME = recode.CharName,
                VALUE_ = GetLcharRecode(recode, charRecord),
                VALUEDESC = charRecord.Value,
                DATE_ = charRecord.Date
            });
        }

        private void AddCcharHouse(AddCharRecodeRecord recode, CommonAddCharRecord charRecord, long housecd)
        {
            lhcc.Add(new CNV_CHARSHOUSES
            {
                HOUSECD = (int)housecd,
                CHARCD = recode.AbonentCode,
                CHARNAME = recode.CharName,
                VALUE_ = Convert.ToDecimal(charRecord.Value),
                DATE_ = charRecord.Date
            });
        }

        private int GetLcharRecode(AddCharRecodeRecord recode, CommonAddCharRecord charRecord)
        {
            int recodeValue;
            if (!valueListRecode.TryGetValue(charRecord.Value.ToLower(), out recodeValue))
            {
                if (Int32.TryParse(charRecord.Value, out recodeValue) && valueListRecode.Values.Contains(recodeValue))
                    return recodeValue;
                else
                {
                    Task.Factory.StartNew(() => MessageBox.Show($"Не нашлась перекодировка характеристики {recode.CharName} со значением {charRecord.Value}"));
                    return 0;
                }
            }
            else return recodeValue;
        }

        private bool CustomCharHandler(AddCharRecodeRecord recode, CommonAddCharRecord charRecord)
        {
            long lshet;
            switch (recode.RgmekCode)
            {
                case "1a083d4e-8798-48dd-98f1-568029c5ce2c":
                    if (!lsrecode.TryGetValue(charRecord.Owner, out lshet)) break;
                    AddLChar(recode, charRecord, lshet);
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

    public class ConvertCounters : DbfConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - конвертация информации о счетчиках";
            Position = 50;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(4);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$COUNTERTYPES");
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            
            var lc = new List<CNV_COUNTER>();

            var cnttyperecode = new Dictionary<string, CNV_COUNTERTYPE>();

            var counteridrecode = new Dictionary<string, long>();
            long counterid = 0;

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            StepStart(1);
            var digitCounterDic = GetCounterDigits(Tmsource);
            StepFinish();
            int lastcnttypeid = 0;
            var lcold = new CountersRecord();
            StepStart(1);

            DbfManager.ExecuteQueryByRow(@"select c.*, d.enddate 
                from counters c
                left join (
            	    select oldcntid, max(date) as enddate 
            	    from cntrs_ac
            	    where opertype in ('Замена', 'Снятие')
            	    group by oldcntid
                ) d on d.oldcntid = c.counterid
                where c.counterid is not null", dataRow =>
            { 
                    lcold.ReadDataRow(dataRow);

                    CNV_COUNTERTYPE cnttype;
                    if (!cnttyperecode.TryGetValue(lcold.Cnttype, out cnttype))
                    {
                        int digitcount;
                        if (!digitCounterDic.TryGetValue(lcold.Cnttype, out digitcount))
                            digitcount = DefaultDigitCount;
                        cnttype = new CNV_COUNTERTYPE
                        {
                            ID = ++lastcnttypeid,
                            EQUIPMENTGROUPID = 36,
                            EQUIPMENTTYPEID = 18,
                            PERIODKOD = (int?) lcold.Periodpov,
                            NAME = lcold.Cntname.Trim(),
                            ACCURACY = lcold.Precision,
                            COEFFICIENT = 1,
                            DIGITCOUNT = digitcount
                        };
                        cnttyperecode.Add(lcold.Cnttype, cnttype);
                    }

                    long lshet;
                    if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                    {
                        var enddate = dataRow.IsNull("enddate")
                            ? (DateTime?) null
                            : Convert.ToDateTime(dataRow["enddate"]);
                        if (enddate == DateTime.MinValue) enddate = null;
                        var c = new CNV_COUNTER()
                        {
                            LSHET = lshet.ToString(),
                            NAME = lcold.Name.Trim(),
                            SERIALNUM = lcold.Serialnum.Trim(),
                            CNTNAME = lcold.Cntname,
                            SETUPDATE = lcold.Setupdate,
                            DEACTDATE = enddate == null || enddate == NullDate ? null : enddate,
                            GUID_ = lcold.Counterid.Trim(),
                            CNTTYPE = cnttype.ID,
                            SETUPPLACE = (int?) lcold.Instplid
                        };
                        c.COUNTERID = Utils.GetValue(lcold.Counterid, counteridrecode, ref counterid).ToString();
                        if (lcold.Lastpov.Year > 1950)
                        {
                            c.LASTPOV = lcold.Lastpov;
                            if (lcold.Periodpov > 0)
                                c.NEXTPOV = lcold.Lastpov.AddMonths((int) lcold.Periodpov);
                        }
                        string prim = "";
                        if (lcold.Rgresid > 0)
                            prim += (prim == "" ? "" : ", ") + "RGRESID=" + lcold.Rgresid.ToString();
                        if (lcold.Precision > 0)
                            prim += (prim == "" ? "" : ", ") + "PRECISION=" +
                                    lcold.Precision.ToString().Replace(',', '.');
                        if (!String.IsNullOrEmpty(lcold.Amperage))
                            prim += (prim == "" ? "" : ", ") + "AMPERAG=" + lcold.Amperage.Trim();
                        if (!String.IsNullOrEmpty(lcold.Instplace))
                            prim += (prim == "" ? "" : ", ") + "INSTPLACE=" + lcold.Instplace.Trim();
                        c.PRIM_ = prim;

                    lc.Add(c);
                }
            });
            Utils.SaveDictionary(cnttyperecode.ToDictionary(ct => ct.Key, ct => (long)ct.Value.ID), CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, CounterIdRecodeFileName);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(cnttyperecode.Values);
            StepFinish();

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }

        public static Dictionary<string, int> GetCounterDigits(TableManager Tmsource)
        {
            var digitCounterDic = new Dictionary<string, int>();
            using (var dt = Tmsource.ExecuteQuery("select cnttype, max(razryad) as razryad from scales_t group by cnttype"))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int digitcount = Convert.ToInt32(dr["RAZRYAD"]);
                    digitCounterDic.Add(dr["CNTTYPE"].ToString(), digitcount == 0 ? DefaultDigitCount : digitcount);
                }
            }
            return digitCounterDic;
        }
    }

    public class ConvertGroupCounters : DbfConvertCase
    {
        public ConvertGroupCounters()
        {
            ConvertCaseName = "GROUP COUNTERS - групповые счетчики";
            Position = 55;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(4);

            var lc = new List<CNV_COUNTER>();
            var cnttyperecode = new Dictionary<string, CNV_COUNTERTYPE>();

            var counteridrecode = new Dictionary<string, long>();
            long counterid = 0;

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var houserecode = Utils.ReadDictionary(HouseRecodeFileName);

            var housestreetnamerecode = File.ReadAllLines(HouseStreetNameRecodeFileName)
               .Select(s =>
               {
                   string[] info = s.Split(';');
                   return new { House = long.Parse(info[0]), Fullname = info[1] };
               })
               .ToDictionary(gp => gp.House, gp => gp.Fullname);

            StepStart(2);
            var digitCounterDic = ConvertCounters.GetCounterDigits(Tmsource);
            Iterate();
            var abnsInHouses = GetDicAbnsInHouses(Tmsource);
            StepFinish();

            using (var dt = Tmsource.ExecuteQuery(@"select c.*, d.enddate 
from cntrs_gr c
left join (
	select oldcntid, max(date) as enddate 
	from cntrs_ac
	where opertype in ('Замена', 'Снятие')
	group by oldcntid
) d on d.oldcntid = c.counterid"))
            {
                int lastcnttypeid = (int)Utils.ReadDictionary((CnttypeRecodeFileName)).Select(cr => cr.Value).Max();
                StepStart(dt.Rows.Count);
                var cntRecord = new Cntrs_grRecord();
                foreach (DataRow dr in dt.Rows)
                {
                    cntRecord.ReadDataRow(dr);

                    CNV_COUNTERTYPE cnttype;
                    if (!cnttyperecode.TryGetValue(cntRecord.Cnttype, out cnttype))
                    {
                        int digitcount;
                        if (!digitCounterDic.TryGetValue(cntRecord.Cnttype, out digitcount))
                            digitcount = DefaultDigitCount;
                        cnttype = new CNV_COUNTERTYPE
                        {
                            ID = ++lastcnttypeid,
                            EQUIPMENTGROUPID = 36,
                            EQUIPMENTTYPEID = 18,
                            PERIODKOD = (int?)cntRecord.Periodpov,
                            NAME = cntRecord.Cntname.Trim(),
                            ACCURACY = cntRecord.Precision,
                            COEFFICIENT = 1,
                            DIGITCOUNT = digitcount,
                        };
                        cnttyperecode.Add(cntRecord.Cnttype, cnttype);
                    }

                    long houseid;
                    if (houserecode.TryGetValue(cntRecord.Housecd, out houseid))
                    {
                        string[] abnList;
                        if (abnsInHouses.TryGetValue(cntRecord.Housecd, out abnList))
                        {
                            foreach (var lsKvc in abnList)
                            {
                                long lshet;
                                if (!lsrecode.TryGetValue(lsKvc, out lshet)) continue;
                                var enddate = dr.IsNull("enddate") ? (DateTime?)null : Convert.ToDateTime(dr["enddate"]);
                                if (enddate == DateTime.MinValue) enddate = null;
                                var c = new CNV_COUNTER()
                                {
                                    LSHET = lshet.ToString(),
                                    NAME = cntRecord.Name.Trim() + " " + housestreetnamerecode[houseid],
                                    SERIALNUM = cntRecord.Serialnum.Trim(),
                                    CNTNAME = cntRecord.Cntname,
                                    SETUPDATE = cntRecord.Setupdate,
                                    DEACTDATE = enddate == null || enddate == NullDate ? null : enddate,
                                    GUID_ = cntRecord.Counterid.Trim(),
                                    CNTTYPE = cnttype.ID,
                                    SETUPPLACE = (int?)cntRecord.Instplid,
                                    COUNTER_LEVEL = 1,
                                    GROUPCOUNTERMODULEID = 23,
                                    TARGETBALANCE_KOD = 29,
                                    TARGETNEGATIVEBALANCE_KOD = 29
                                };
                                c.COUNTERID = Utils.GetValue(cntRecord.Counterid, counteridrecode, ref counterid).ToString();
                                if (cntRecord.Lastpov.Year > 1950)
                                {
                                    c.LASTPOV = cntRecord.Lastpov;
                                    if (cntRecord.Periodpov > 0)
                                        c.NEXTPOV = cntRecord.Lastpov.AddMonths((int)cntRecord.Periodpov);
                                }
                                string prim = "";
                                if (cntRecord.Rgresid > 0)
                                    prim += (prim == "" ? "" : ", ") + "RGRESID=" + cntRecord.Rgresid.ToString();
                                if (cntRecord.Precision > 0)
                                    prim += (prim == "" ? "" : ", ") + "PRECISION=" +
                                            cntRecord.Precision.ToString().Replace(',', '.');
                                if (!String.IsNullOrEmpty(cntRecord.Amperage))
                                    prim += (prim == "" ? "" : ", ") + "AMPERAG=" + cntRecord.Amperage.Trim();
                                if (!String.IsNullOrEmpty(cntRecord.Instplace))
                                    prim += (prim == "" ? "" : ", ") + "INSTPLACE=" + cntRecord.Instplace.Trim();
                                c.PRIM_ = prim;

                                lc.Add(c);
                            }
                        }
                    }
                }
            }

            Utils.SaveDictionary(counteridrecode, GroupCounterIdRecodeFileName);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(cnttyperecode.Values);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }

        public static Dictionary<string, string[]> GetDicAbnsInHouses(TableManager tms)
        {
            using (var dt = tms.ExecuteQuery("select lshet, housecd from abonent "))
            {
                var tempList = new List<CNV_COUNTER>();
                foreach (DataRow dr in dt.Rows)
                {
                    tempList.Add(new CNV_COUNTER { LSHET = dr["lshet"].ToString().Trim(), COUNTERID = dr["housecd"].ToString().Trim() });
                }
                return tempList.GroupBy(tl => tl.COUNTERID, tl => tl.LSHET)
                    .ToDictionary(gtl => gtl.Key, gtl => gtl.ToArray());
            }
        }
    }

    public class ConvertCntrsind : DbfConvertCase
    {
        public ConvertCntrsind()
        {
            ConvertCaseName = "CNTRSIND - конвертация информации о показаниях счетчиков";
            Position = 60;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            var lc = new List<CNV_CNTRSIND>();

            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            var groupcounteridrecode = Utils.ReadDictionary(GroupCounterIdRecodeFileName);
            long counterid = 0;


            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM CNTRSIND")));
            DbfManager.ExecuteQueryByRow("SELECT * FROM CNTRSIND", dataRow =>
            {
                var cr = new CntrsindRecord();
                cr.ReadDataRow(dataRow);

                if (counteridrecode.TryGetValue(cr.Counterid, out counterid) ||
                    groupcounteridrecode.TryGetValue(cr.Counterid, out counterid))
                {
                    var c = new CNV_CNTRSIND()
                    {
                        COUNTERID = counterid.ToString(),
                        DOCUMENTCD = GetDocumentCd(cr.Doc),
                        INDDATE = cr.Date,
                        INDICATION = cr.Indication,
                        INDTYPE = 1,
                        CASETYPE = cr.Indtype.Trim() == "Расчетное" ? 3 : (int?) null
                    };
                    lc.Add(c);
                }
                Iterate();
            });
            StepFinish();

//            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(@"select top 1
//	(select count(0) from IND_2017 where indtype = 'От абонента (по квитанции)') +
//	(select count(0) from IND_2016 where indtype = 'От абонента (по квитанции)') +
//	(select count(0) from IND_2015 where indtype = 'От абонента (по квитанции)') 
//from TARIFS
//order by TARIFCD")));
//            DbfManager.ExecuteQueryByRow(@"select * from IND_2017
//                                        where indtype = 'От абонента (по квитанции)'
//                                        union all
//                                        select * from IND_2016
//                                        where indtype = 'От абонента (по квитанции)'
//                                        union all
//                                        select * from IND_2015
//                                        where indtype = 'От абонента (по квитанции)'",
            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(@"select top 1
	(select count(0) from CNTRSKVC where indtype = 'От абонента (по квитанции)')
from TARIFS
order by TARIFCD")));
            DbfManager.ExecuteQueryByRow(@"select * from CNTRSKVC where indtype = 'От абонента (по квитанции)'",
                dataRow =>
                {
                    var cr = new CntrsindRecord();
                    cr.Counterid = dataRow["CounterID"].ToString().Trim();
                    cr.Doc = dataRow["Doc"].ToString().Trim();
                    cr.Date = Convert.ToDateTime(dataRow["DocDate"].ToString());
                    cr.Indication = Convert.ToDecimal(dataRow["Indication"]);

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid) ||
                        groupcounteridrecode.TryGetValue(cr.Counterid, out counterid))
                    {
                        var c = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid.ToString(),
                            DOCUMENTCD = GetDocumentCd(cr.Doc),
                            INDDATE = cr.Date,
                            INDICATION = cr.Indication,
                            INDTYPE = 0,
                        };
                        lc.Add(c);
                    }
                    Iterate();
                });
            StepFinish();

            StepStart(1);
            var lc2 = CntrsindRecordUtils.ThinOutList(lc);
            StepFinish();

            StepStart(1);
            CntrsindRecordUtils.RestoreHistory(ref lc2, RestoreHistoryType.С_конца_по_конечным_показаниям);
            StepFinish();

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }

        private string GetDocumentCd(string doc)
        {
            if (doc.StartsWith("Акт на установку"))
            {
                return "АУ " + doc.Substring(49, 10);
            }
            else if (doc.StartsWith("Акт приемки"))
            {
                return "АП " + doc.Substring(32, 10);
            }
            else if (doc.StartsWith("Акт снятия"))
                return "АС " + doc.Substring(21, 10);
            else
                throw new Exception("Неожиданное наименование документа о снятии показаний:\r\n" +
                    doc);
        }
    }

    public class ConvertPayment : DbfConvertCase
    {
        public ConvertPayment()
        {
            ConvertCaseName = "PAYMENT - оплата";
            Position = 80;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            BufferEntitiesManager.DropTableData("CNV$OPLATA");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var sourcedocrecode = new Dictionary<string, long>();
            long lastsourcedoc = 10;

            var lp = new List<CNV_OPLATA>();
            int paydoc = 0;

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM PAYMENT")) + 1);
            using (var reader = Tmsource.ExecuteQueryToReader("SELECT * FROM PAYMENT"))
            {
                var pr = new PaymentRecord();
                while (reader.Read())
                {
                    Iterate();
                    pr.Date = Convert.ToDateTime(reader["DATE"]);
                    pr.Doc = reader["DOC"].ToString().Trim();
                    pr.Lshet = reader["lSHET"].ToString().Trim();
                    pr.Activcd = reader["ACTIVCD"].ToString().Trim();
                    pr.Activnm = reader["ACTIVNM"].ToString().Trim();
                    pr.Paypostcd = reader["PAYPOSTCD"].ToString().Trim();
                    pr.Paypostnm = reader["PAYPOSTNM"].ToString().Trim();
                    pr.Methodcd = Convert.ToInt64(reader["METHODCD"]);
                    pr.Methodnm = reader["METHODNM"].ToString().Trim();
                    pr.Resourcd = Convert.ToInt64(reader["RESOURCD"]);
                    pr.Resournm = reader["RESOURNM"].ToString().Trim();
                    pr.Summa = Convert.ToDecimal(reader["SUMMA"]);
                    pr.Servicecd = reader["SERVICECD"].ToString().Trim();
                    pr.Servicenm = reader["SERVICENM"].ToString().Trim();
                    pr.Opertype = reader["OPERTYPE"].ToString().Trim();

                    if (pr.Date < MinConvertDate) continue;
                    if (pr.Activcd != "01" || pr.Resourcd != 4 || String.IsNullOrWhiteSpace(pr.Servicenm)) continue;

                    long lshet;
                    if (lsrecode.TryGetValue(pr.Lshet, out lshet))
                    {
                        lp.Add(new CNV_OPLATA
                        {
                            LSHET = lshet.ToString(),
                            DATE_ = pr.Date,
                            DATE_VV = pr.Date,
                            MONTH_ = pr.Date.Month,
                            YEAR_ = pr.Date.Year,
                            SERVICECD = GetServicecd(pr),
                            SERVICENAME = $"{pr.Servicenm}",
                            SUMMA = pr.Summa,
                            DOCUMENTCD = $"P{++paydoc}",
                            SOURCENAME = pr.Paypostnm,
                            SOURCECD = (int)Utils.GetValue(pr.Paypostcd, sourcedocrecode, ref lastsourcedoc)
                        });
                    }
                }
            }
            StepFinish();
            
            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lp);
            StepFinish();
        }

        public static int GetServicecd(PaymentRecord pr)
        {
            string servicename = pr.Servicenm.ToLower();
            if (servicename.Contains("электроэнергия")) return 9;
            if (servicename.Contains("общедовые нужды") || servicename.Contains("общедомовые нужды")) return 29;
            throw new Exception($"Необработанная услуга {pr.Servicenm}");
        }
    }

    public class ConvertNach : ConvertCase
    {
        public ConvertNach()
        {
            ConvertCaseName = "NACH - начисления";
            Position = 90;
            IsChecked = false;
        }

        private static readonly Regex FileNameRegex = new Regex(@"(?<month>январь|февраль|март|апрель|май|июнь|июль|август|сентябрь|октябрь|ноябрь|декабрь)(?<year>\d{4})\.xlsx");
        private static readonly Dictionary<string, int> Months = new Dictionary<string, int>
        {
            {"январь", 1},
            {"февраль", 2},
            {"март", 3},
            {"апрель", 4},
            {"май", 5},
            {"июнь", 6},
            {"июль", 7},
            {"август", 8},
            {"сентябрь", 9},
            {"октябрь", 10},
            {"ноябрь", 11},
            {"декабрь", 12}
        };

        public static string[] GetNachFiles()
        {
            return Directory.GetFiles(NachFilesDirectory)
                .Where(fileName => FileNameRegex.IsMatch(new FileInfo(fileName).Name.ToLower()))
                .ToArray();
        }

        public static DateTime GetNachFileDate(string fullFileName)
        {
            var match = FileNameRegex.Match(new FileInfo(fullFileName).Name.ToLower());
            return new DateTime(Int32.Parse(match.Groups["year"].Value), Months[match.Groups["month"].Value], 1);
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$NACH");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var ln = new List<CNV_NACH>();

            int nachdoc = 0;
            var files = GetNachFiles();
            StepStart(files.Length);
            foreach (var file in files)
            {
                var fileDate = GetNachFileDate(file);
                aConverterClassLibrary.Utils.ReadExcelFileByRow(file, null, dr =>
                {
                    var nachInfo = new NachExcelRecord(dr);
                    nachdoc++;

                    long lshet;
                    if (!lsrecode.TryGetValue(nachInfo.LsKvc, out lshet)) return;

                    var nach = new CNV_NACH
                    {
                        LSHET = lshet.ToString(),

                        MONTH_ = fileDate.Month,
                        MONTH2 = fileDate.Month,
                        YEAR_ = fileDate.Year,
                        YEAR2 = fileDate.Year,
                        DATE_VV = fileDate,

                        DOCUMENTCD = $"N{nachdoc}",

                        AUTOUSE = 0,
                        CASETYPE = null,
                        TYPE_ = (int) nachInfo.Nach,
                        VTYPE_ = (int) nachInfo.Nach + 1,

                        SERVICECD = (int) nachInfo.Service,
                        SERVICENAME = nachInfo.Service.ToString(),
                    };

                    if (nachInfo.Nach == NachExcelRecord.NachType.AddNach)
                    {
                        if (nachInfo.Sum != 0)
                        {
                            nach.FNATH = 0;
                            nach.VOLUME = 0;
                            nach.PROCHL = nachInfo.Sum;
                            nach.PROCHLVOLUME = 0;
                            nach.REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + (int) nachInfo.Zone;
                            nach.REGIMNAME = nachInfo.Nach.ToString();
                            ln.Add(nach);
                        }
                    }
                    else
                    {
                        if (nachInfo.Sum != 0 || nachInfo.Volume != 0)
                        {
                            nach.FNATH = nachInfo.Sum;
                            nach.VOLUME = nachInfo.Volume;
                            nach.PROCHL = 0;
                            nach.PROCHLVOLUME = 0;
                            nach.REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + (int) nachInfo.Zone;
                            nach.REGIMNAME = nachInfo.Nach.ToString();
                            ln.Add(nach);
                        }
                        if (nachInfo.SumCoef != 0)
                        {
                            nach.FNATH = nachInfo.SumCoef;
                            nach.VOLUME = 0;
                            nach.PROCHL = 0;
                            nach.PROCHLVOLUME = 0;
                            nach.REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + 100 + (int) nachInfo.Zone;
                            nach.REGIMNAME = nachInfo.Nach.ToString();
                            ln.Add(nach);
                        }
                    }
                });
                Iterate();
            }
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(ln);
            StepFinish();
        }
    }

    public class ConvertSaldo : DbfConvertCase
    {
        public ConvertSaldo()
        {
            ConvertCaseName = "SALDO - сальдо";
            Position = 100;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            var lnop = new List<CNV_NACHOPL>();

            var convertDate = new DateTime(CurrentYear, CurrentMonth, 1).AddDays(-1);

            using (var dt = Tmsource.ExecuteQuery("select * from lschars where charcd = 'd673ef5d-90b1-11df-ae5f-001e8c71f1cc'"))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var charRecord = new CommonAddCharRecord(dr);
                    long lshet;
                    if (lsrecode.TryGetValue(charRecord.Owner, out lshet))
                    {
                        lnop.Add(new CNV_NACHOPL
                        {
                            LSHET = lshet.ToString(),
                            SERVICENAME = "Электроэнергия",
                            SERVICECD = 9,
                            PROCHL = 0,
                            FNATH = 0,
                            OPLATA = 0,
                            BDEBET = 0,
                            EDEBET = Convert.ToDecimal(charRecord.Value),
                            MONTH2 = convertDate.Month,
                            YEAR2 = convertDate.Year,
                            MONTH_ = convertDate.Month,
                            YEAR_ = convertDate.Year
                        });
                    }
                    Iterate();
                }
                StepFinish();

                StepStart(1);
                BufferEntitiesManager.SaveDataToBufferIBScript(lnop);
                StepFinish();
            }
        }
    }

    #endregion

    #region Перенос данных в целевые таблицы

    public class SplitterTransfer : ConvertCase
    {
        public SplitterTransfer()
        {
            ConvertCaseName = "";;
            Position = 998;
            IsChecked = false;
        }

        public override void DoConvert()
        {

        }
    }

    public class TransferAddressObjects : ConvertCase
    {
        public TransferAddressObjects()
        {
            ConvertCaseName = "Перенос данных об адресных объектах";
            Position = 1000;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(7);

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00150_SETTLEMENT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00200_PUNKT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00300_STREET");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00400_DISTRICT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00500_INFORMATIONOWNERS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00600_HOUSES");
            StepFinish();
        }
    }

    public class TransferAbonents : ConvertCase
    {
        public TransferAbonents()
        {
            ConvertCaseName = "Перенос данных об абонентах";
            Position = 1010;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
            Iterate();
            StepFinish();
        }
    }


    public class TransferExtlshet : ConvertCase
    {
        public TransferExtlshet()
        {
            ConvertCaseName = "Перенос данных о внешних лицевых счетах";
            Position = 1015;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "2", "0" });
            Iterate();
        }
    }

    public class TransferChars : ConvertCase
    {
        public TransferChars()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках";
            Position = 1020;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferLchars : ConvertCase
    {
        public TransferLchars()
        {
            ConvertCaseName = "Перенос данных о качественных характеристиках";
            Position = 1030;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferAddchars : ConvertCase
    {
        public TransferAddchars()
        {
            ConvertCaseName = "Перенос данных о дополнительных характеристиках";
            Position = 1031;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteQuery(@"merge into abonentadditionalchars ad
using cnv$aaddchar ca on ca.lshet = ad.lshet and ca.addcharcd = ad.additionalcharcd
when matched then
    update set ad.significance = ca.""VALUE""
when not matched then
    insert(additionalcharcd, lshet, significance)
    values(ca.addcharcd, ca.lshet, ca.""VALUE"")");
            StepFinish();
        }
    }

    public class TransferCounterTypes : ConvertCase
    {
        public TransferCounterTypes()
        {
            ConvertCaseName = "Перенос данных о типах счетчиков";
            Position = 1039;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01100_COUNTERTYPES");
            StepFinish();
        }
    }

    public class TransferCounters : ConvertCase
    {
        public TransferCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках и показаниях";
            Position = 1040;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0", "1", "0", "1" });
            StepFinish();
        }
    }

    public class TransferGroupCounters : KvcConvertCase
    {
        public TransferGroupCounters()
        {
            ConvertCaseName = "Перенос данных о групповых счетчиках и показаниях";
            Position = 1045;
            IsChecked = false;

        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01050_GROUPCOUNTERS", new[] { "0", "1", "0" });
            Iterate();
        }
    }

    public class TransferPayment : ConvertCase
    {
        public TransferPayment()
        {
            ConvertCaseName = "Перенос данных о истории оплат";
            Position = 1070;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(2);
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            StepFinish();
        }
    }

    public class TransferNach : ConvertCase
    {
        public TransferNach()
        {
            ConvertCaseName = "Перенос данных о истории начислений";
            Position = 1080;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(2);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            StepFinish();
        }
    }

    public class TransferSaldo : ConvertCase
    {
        public TransferSaldo()
        {
            ConvertCaseName = "Перенос данных о сальдо";
            Position = 1090;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(1);
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { CurrentYear.ToString(CultureInfo.InvariantCulture),
                CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            StepFinish();
        }
    }

    public class TransferHchars : KvcConvertCase
    {
        public TransferHchars()
        {
            ConvertCaseName = "Перенос характеристик домов";
            Position = 1100;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(3);
            fbm.ExecuteProcedure("CNV$CNV_00850_CHARSHOUSES", new[] { "0", "0" });
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00855_LCHARSHOUSES", new[] { "0", "0" });
            Iterate();
            fbm.ExecuteQuery(@"merge into housesadditionchars ad
using cnv$haddchar ca on ca.housecd = ad.housecd and ca.addcharcd = ad.additionalcharcd
when matched then
    update set ad.significance = ca.VALUE_
when not matched then
    insert(additionalcharcd, housecd, significance)
    values(ca.addcharcd, ca.housecd, ca.VALUE_)");
            StepFinish();
        }
    }

    #endregion
}
