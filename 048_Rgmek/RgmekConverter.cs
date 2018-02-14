﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.Class.ConvertCases;
using aConverterClassLibrary.Class.Utils;
using aConverterClassLibrary.Class.Utils.KVC;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using _048_Rgmek.Forms;
using _048_Rgmek.NachImport;
using _048_Rgmek.Records;
using static _048_Rgmek.Consts;
using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;


namespace _048_Rgmek
{
    public static class Consts
    {
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
        public static readonly string AbonenstNotFromKvcFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\abonentsnotfromkvc.txt";
        public static readonly string CounterIdToLsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\counteridtolsrecode.csv";
        public static readonly string NachFilesDirectory = aConverter_RootSettings.SourceDbfFilePath + @"\Nach";
        public static readonly string AddCharsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\AddCharsRecode.xlsx";

        public static readonly string AbonentsNotFromKvcSaldoDbfTableName = "saldo";

        public static readonly Regex lsKvcRegex = new Regex(@"\d{3}-\d{3}-\d{2}-\d{3}-\d{1}-\d{2}");

        public static readonly Dictionary<long, int> CcharRecode = new Dictionary<long, int>
        {
            {1, 2}, // Общая площадь
            {2, 4}, // Полезная площадь
            {3, 23}, // Число комнат
            {4, 5}, // Площадь нежилых помещений
            {5, 1}, // Число зарегистрированных
            {6, 3}, // Число проживающих
        };

        public static readonly Dictionary<KeyValuePair<long, long>, KeyValuePair<int, int>[]> LcharRecode = new Dictionary<KeyValuePair<long, long>, KeyValuePair<int, int>[]>
        {
            { new KeyValuePair<long, long>(1, 0), new[] {new KeyValuePair<int, int>(2, 0), new KeyValuePair<int, int>(11, 0)} },
            { new KeyValuePair<long, long>(1, 1), new[] {new KeyValuePair<int, int>(2, 1), new KeyValuePair<int, int>(11, 1)} },
            { new KeyValuePair<long, long>(1, 2), new[] {new KeyValuePair<int, int>(2, 0), new KeyValuePair<int, int>(11, 0)} },
            { new KeyValuePair<long, long>(2, 0), new[] {new KeyValuePair<int, int>(2, 0) /*, new KeyValuePair<int, int>(11, 0)*/} },
            { new KeyValuePair<long, long>(2, 1), new[] {new KeyValuePair<int, int>(2, 2) /*, new KeyValuePair<int, int>(11, 1)*/} },
            { new KeyValuePair<long, long>(2, 2), new[] {new KeyValuePair<int, int>(2, 0) /*, new KeyValuePair<int, int>(11, 0)*/} },
            { new KeyValuePair<long, long>(3, 0), new[] {new KeyValuePair<int, int>(2, 0) /*, new KeyValuePair<int, int>(11, 0)*/} },
            { new KeyValuePair<long, long>(3, 1), new[] {new KeyValuePair<int, int>(2, 3) /*, new KeyValuePair<int, int>(11, 1)*/} },
            { new KeyValuePair<long, long>(3, 2), new[] {new KeyValuePair<int, int>(2, 0) /*, new KeyValuePair<int, int>(11, 0)*/} },
            { new KeyValuePair<long, long>(4, 0), new KeyValuePair<int, int>[0] /* new[] {new KeyValuePair<int, int>(13, 0)}*/ },
            { new KeyValuePair<long, long>(4, 1), new KeyValuePair<int, int>[0] /* new[] {new KeyValuePair<int, int>(13, 1)}*/ },
            { new KeyValuePair<long, long>(4, 2), new KeyValuePair<int, int>[0] /* new[] {new KeyValuePair<int, int>(13, 0)}*/ },
            { new KeyValuePair<long, long>(5, 1), new[] {new KeyValuePair<int, int>(5, 1)} },
            { new KeyValuePair<long, long>(5, 2), new[] {new KeyValuePair<int, int>(5, 2)} },
            { new KeyValuePair<long, long>(5, 3), new[] {new KeyValuePair<int, int>(5, 3)} },
            { new KeyValuePair<long, long>(5, 4), new[] {new KeyValuePair<int, int>(5, 4)} },
            { new KeyValuePair<long, long>(5, 5), new[] {new KeyValuePair<int, int>(5, 5)} },
            { new KeyValuePair<long, long>(5, 6), new[] {new KeyValuePair<int, int>(5, 6)} },
            { new KeyValuePair<long, long>(55, 0), new[] {new KeyValuePair<int, int>(2, 0), new KeyValuePair<int, int>(11, 0)} },
            { new KeyValuePair<long, long>(55, 1), new[] {new KeyValuePair<int, int>(2, 1), new KeyValuePair<int, int>(11, 1)} },
            { new KeyValuePair<long, long>(55, 2), new[] {new KeyValuePair<int, int>(2, 0), new KeyValuePair<int, int>(11, 0)} },
            { new KeyValuePair<long, long>(6, 1), new[] {new KeyValuePair<int, int>(6, 1)} },
            { new KeyValuePair<long, long>(6, 4), new[] {new KeyValuePair<int, int>(6, 2)} },
            { new KeyValuePair<long, long>(6, 7), new[] {new KeyValuePair<int, int>(6, 3)} },
            { new KeyValuePair<long, long>(6, 8), new[] {new KeyValuePair<int, int>(6, 4)} },
            { new KeyValuePair<long, long>(6, 9), new[] {new KeyValuePair<int, int>(6, 5)} },
            { new KeyValuePair<long, long>(6, 10), new[] {new KeyValuePair<int, int>(6, 6)} },
            { new KeyValuePair<long, long>(6, 11), new[] {new KeyValuePair<int, int>(6, 7)} },
        };

        public static readonly Dictionary<int, int> TarifRecode = new Dictionary<int, int>
        {
            {1, 5}, //Газ. плиты двухставочный
            {4, 1}, //Городской газ. плиты
            {5, 2}, //Городской эл. плиты
            {6, 4}, //Гос. сектор
            {7, 3}, //Сельский
            {8, 6}, //Электроплиты двуставочный
        };

        public static readonly int CurrentMonth = 02; // должен быть следующий месяц после последнего закрытого
        public static readonly int CurrentYear = 2018;
        public static readonly DateTime MinConvertDate = new DateTime(2015, 1, 1);
        public static readonly DateTime NullDate = new DateTime(1899, 12, 30);

        public const int UnknownTownId = 1;
        public const string UnknownTownName = "Неизвестен";
        public const int UnknownStreetId = 1;
        public const string UnknownStreetName = "Неизвестна";
        public const int DefaultDigitCount = 5;

        public static readonly Dictionary<int, string[]> HouseTypeRecode = new Dictionary<int, string[]>()
        {
            {1, new[] {"баня"}},
            {2, new[] {"гараж"}},
            {3, new[] {"дача"}},
            {4, new[] {"многоквартирныйдом"}},
            {5, new[] {"общежитие"}},
            {6, new[] {"подземнаяавтостоянка"}},
            {7, new[] {"садовоетоварищество"}},
            {8, new[] {"частныйдом"}},
        };

        public static readonly Dictionary<int, string[]> FlatTypeRecode = new Dictionary<int, string[]>()
        {
            {1, new[] {"баня"}},
            {2, new[] {"бокс"}},
            {3, new[] {"бытовка"}},
            {4, new[] {"гараж"}},
            {5, new[] {"дачныйдом"}},
            {6, new[] {"дачныйдом/гараж"}},
            {7, new[] {"дом"}},
            {8, new[] {"ж"}},
            {9, new[] {"жилойдом/гараж"}},
            {10, new[] {"квартира"}},
            {11, new[] {"квартира/гараж"}},
            {12, new[] {"кладовка"}},
            {13, new[] {"коммунальнаяквартира"}},
            {14, new[] {"комната"}},
            {15, new[] {"местаобщегопользования"}},
            {16, new[] {"нежилоепомещение"}},
            {17, new[] {"общеквартирныйучет"}},
            {18, new[] {"общийучет"}},
            {19, new[] {"объединеннаяквартира"}},
            {20, new[] {"паркинг/гараж"}},
            {21, new[] {"подземнаяавтостоянка"}},
            {22, new[] {"подсобное"}},
            {23, new[] {"сарай"}},
            {24, new[] {"уч.", "участок"}},
        };

        public static readonly Dictionary<int, string[]> CoutnerPlaceRecode = new Dictionary<int, string[]>
        {
            {0, new[] {"неизвестно"}},
            {1, new[] {"баня"}},
            {2, new[] {"ввыноснойшитовой"}},
            {3, new[] {"вдоме"}},
            {4, new[] {"вком.учетатп-799"}},
            {5, new[] {"вкоридоредома"}},
            {6, new[] {"впанелесверху"}},
            {7, new[] {"вн.ст.бани", "внешняястенабани"}},
            {8, new[] {"вн.ст.гаража", "вн.стенагаража"}},
            {9, new[] {"вн.стенаж/д", "внешняястенажилогодома"}},
            {10, new[] {"вн.стенахоз/п"}},
            {11, new[] {"вовруж/длифты", "воврулифтов"}},
            {12, new[] {"вовруобъекта"}},
            {13, new[] {"вру"}},
            {14, new[] {"вруавр", "врусавр"}},
            {15, new[] {"врувкоридоре2эт."}},
            {16, new[] {"врувп.№4"}},
            {17, new[] {"врувподвале3под."}},
            {18, new[] {"врувэлектрощитовой"}},
            {19, new[] {"вруж/д", "вружилогодома"}},
            {20, new[] {"вруна1этаже"}},
            {21, new[] {"вруобщежития"}},
            {22, new[] {"вруобщежитиянам.о.п."}},
            {23, new[] {"вруподвал"}},
            {24, new[] {"вруподвалж/д"}},
            {25, new[] {"вруподъезд№1", "вру1подъезда"}},
            {26, new[] {"вруподъезд№2", "вру2подъезда"}},
            {27, new[] {"вруподъезд№3", "вру3подъезд"}},
            {28, new[] {"вруподъезд№4", "вру4подъезд"}},
            {29, new[] {"вруподъезд№5", "вру5подъезда"}},
            {30, new[] {"вруподъезд№6", "вру6подъезд"}},
            {31, new[] {"вруподъезд№7", "вру7подъезд"}},
            {32, new[] {"вруподъезд№8", "вру8подъезда"}},
            {33, new[] {"вруподъезд№9", "вруподъезд9"}},
            {34, new[] {"врупомещения"}},
            {35, new[] {"врутоварищества", "врутсж"}},
            {36, new[] {"гараж"}},
            {37, new[] {"дача"}},
            {38, new[] {"доп.двери", "доп.дверь", "доп.дверь"}},
            {39, new[] {"доп.трубостойка"}},
            {40, new[] {"кам.забор"}},
            {41, new[] {"кварт.", "квартира"}},
            {42, new[] {"кладовка"}},
            {43, new[] {"комната"}},
            {44, new[] {"комнатаучетатп276"}},
            {45, new[] {"коридор"}},
            {46, new[] {"коридорна1этаже"}},
            {47, new[] {"крышнаякотельная"}},
            {48, new[] {"лест.кл.", "лест.клетка", "лест.клетка", "лестничнаяклетка"}},
            {49, new[] {"местаобщегопользования"}},
            {50, new[] {"мет.стойканаул"}},
            {51, new[] {"навнешнейстенетп-22"}},
            {52, new[] {"нанаружнойстенетп-502"}},
            {53, new[] {"настенегаража"}},
            {54, new[] {"настенедачи"}},
            {55, new[] {"настенедома"}},
            {56, new[] {"настенежилогодома"}},
            {57, new[] {"настенетп"}},
            {58, new[] {"настенехоз.постройки"}},
            {59, new[] {"натерриториистройки"}},
            {60, new[] {"натер.участка"}},
            {61, new[] {"навводе"}},
            {62, new[] {"общеквартирныйучет"}},
            {63, new[] {"наопоре", "опора"}},
            {64, new[] {"вподвале", "подвал"}},
            {65, new[] {"подъезд"}},
            {66, new[] {"подъезд№1(этаж2)"}},
            {67, new[] {"привходеналево"}},
            {68, new[] {"пристройка"}},
            {69, new[] {"рп-1п.11р.2"}},
            {70, new[] {"ру-0.4квтп-758,п.3,р.3"}},
            {71, new[] {"ру-0.4квтп-758,п.8,р.4"}},
            {72, new[] {"сарай"}},
            {73, new[] {"стойказабора"}},
            {74, new[] {"строит.вагончик", "строительныйвагончик"}},
            {75, new[] {"втп-1052"}},
            {76, new[] {"тп110"}},
            {77, new[] {"тп118,тп-118"}},
            {78, new[] {"тп-172"}},
            {79, new[] {"тп-176"}},
            {80, new[] {"тп-229"}},
            {81, new[] {"тп-240"}},
            {82, new[] {"тп-276"}},
            {83, new[] {"тп-307"}},
            {84, new[] {"тп311"}},
            {85, new[] {"тп-35"}},
            {86, new[] {"тп-385"}},
            {87, new[] {"тп-401"}},
            {88, new[] {"тп-454"}},
            {89, new[] {"тп-53"}},
            {90, new[] {"тп-539"}},
            {91, new[] {"втп-543"}},
            {92, new[] {"тп-543"}},
            {93, new[] {"тп-745"}},
            {94, new[] {"тп-746"}},
            {95, new[] {"тп-758"}},
            {96, new[] {"тп-758ру-0.4кв"}},
            {97, new[] {"тп-759,тп-759"}},
            {98, new[] {"тп-898"}},
            {99, new[] {"тп-903"}},
            {100, new[] {"тп-92"}},
            {101, new[] {"тп-940настене"}},
            {102, new[] {"фасадздания"}},
            {103, new[] {"цокольныйэтаж"}},
            {104, new[] {"шу1-этажа"}},
            {105, new[] {"шунавнешнейстенетп-128"}},
            {106, new[] {"шунастенетп865"}},
            {107, new[] {"шунастенетп866"}},
            {108, new[] {"щитучета"}},
            {109, new[] {"щунанарстенетп488"}},
            {110, new[] {"щунастенерп-1"}},
            {111, new[] {"щунастенетп-423"}},
            {112, new[] {"щунатп-456"}},
            {113, new[] {"вэл.щитовой", "эл.щитовая"}},
            {114, new[] {"эл.щитовая1этаж"}},
            {115, new[] {"эл.щитоваяв1подъезде"}},
            {116, new[] {"эл.щитоваяв2подъезде"}},
            {117, new[] {"эл.щитоваявподвале"}},
            {118, new[] {"эл.щитоваятп-344"}},
            {119, new[] {"эл.щитоваятп-345"}},
            {120, new[] {"эл.щитоваятп-463"}},
            {121, new[] {"эл.щитоваятп-763"}},
            {122, new[] {"эл.щитоваяувхода"}},
        };

        public static string LsKvcWithoutKr(string lsKvc)
        {
            if (lsKvc.Length != 19) return lsKvc;
            return lsKvc.Substring(0, 16);
        }

        public static long FindLsRecode(string lsKvc, Dictionary<string, long> lsRecode)
        {
            long recodedLs;
            lsRecode.TryGetValue(LsKvcWithoutKr(lsKvc), out recodedLs);
            return recodedLs;
        }

        public static string StringRecode(string origin)
        {
            return origin.Trim().Replace(" ", "").ToLower();
        }
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

        public static void SaveDictionary(Dictionary<string, string> recodedic, string filename)
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

        public static HashSet<string> GetLsNotFromKvc()
        {
            return new HashSet<string>(File.ReadAllLines(AbonenstNotFromKvcFileName));
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

    public class FindNotExistedAbonentInNach : ConvertCase
    {
        public FindNotExistedAbonentInNach()
        {
            ConvertCaseName = "Поиск абонентов в файле с начислениями, которые отсутствуют в БД";
            Position = 11;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var nachFile = ConvertNach.GetNachFiles().First(n => ConvertNach.GetNachFileDate(n) == new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1));
            var notExistedNach = new List<NachExcelRecord>();
            var addNach = new List<NachExcelRecord>();
            aConverterClassLibrary.Utils.ReadExcelFileByRow(nachFile, null, dr =>
            {
                var nachInfo = new NachExcelRecord(dr);
                if (lsrecode.ContainsKey(LsKvcWithoutKr(nachInfo.LsKvc)))
                {
                    if (nachInfo.Nach == NachExcelRecord.NachType.AddNach)
                        addNach.Add(nachInfo);
                }
                else
                {
                    notExistedNach.Add(nachInfo);
                }
            });
            var notExitedLs = notExistedNach.Select(n => n.LsKvc).Distinct().ToList();
            var notExistedSum = notExistedNach.Sum(n => n.Sum);
            var notExistedVol = notExistedNach.Sum(n => n.Volume);
            var notExistedLsList = String.Join("\r\n", notExitedLs);
            var addNachSum = addNach.Sum(n => n.Sum);
            var addNachVol = addNach.Sum(n => n.Volume);
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
            SetStepsCount(3);
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var la = new List<CNV_ABONENT>();
            var lha = new List<CNV_HADDCHAR>();
            var laa = new List<CNV_AADDCHAR>();
            var lhcc = new List<CNV_CHARSHOUSES>();

            var lsrecode = new Dictionary<string, long>();
            long lastls = 101000000;
            var durecode = new Dictionary<string, long>();
            long lastducd = 0;
            var houserecode = new Dictionary<string, long>();
            long lasthousecd = 0;
            var placerecode = new Dictionary<string, List<string>>();
            var distrRecode = new Dictionary<string, long>();
            long lastDistId = 0;
            var emails = new Dictionary<string, string>();
            DbfManager.ExecuteQueryByReader(
                "select lshet, value_s from lschars where charcd = '62278e46-4e08-11e4-903c-001e8c71f1cc'",
                r =>
                {
                    string ls = r.GetString(0);
                    if (emails.ContainsKey(ls)) emails[ls] = r.GetString(1);
                    else emails.Add(ls, r.GetString(1));
                });
            var housesWithFlatNumbers = new HashSet<string>();
            DbfManager.ExecuteQueryByReader(
                @"select distinct hs.housecd
                from hschars hs
                where hs.charcd = 'fc7c4f38-aee3-48d8-bb79-98bb1ac2462f'",
                r =>
                {
                    housesWithFlatNumbers.Add(r.GetString(0).Trim());
                });
            var abnStartDates = new Dictionary<string, DateTime>();
            var abnEndDates = new Dictionary<string, DateTime>();
            DbfManager.ExecuteQueryByRow("select * from abnstate order by date", dr =>
            {
                var state = new AbnstateRecord();
                state.ReadDataRow(dr);
                Dictionary<string, DateTime> dic;
                switch (state.State.Trim())
                {
                    case "Включен":
                    case "Создан":
                        dic = abnStartDates;
                        break;
                    case "Выключен":
                    case "Закрыт":
                        dic = abnEndDates;
                        break;
                    default:
                        return;
                }
                if (dic.ContainsKey(state.Lshet))
                    dic[state.Lshet] = state.Date;
                else
                    dic.Add(state.Lshet, state.Date);
            });

            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);

                if (!lsKvcRegex.IsMatch(abonent.Lshet)) continue;

                var a = new CNV_ABONENT
                {
                    LSHET = Utils.GetValue(LsKvcWithoutKr(abonent.Lshet), lsrecode, ref lastls).ToString(),
                    EXTLSHET = abonent.Lshet.Trim().Replace("-",""),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    RAYONKOD = (int)abonent.Distkod,
                    RAYONNAME = abonent.Distname.Trim(),
                    DISTKOD = (int)Utils.GetValue(abonent.Diviscd.Trim(), distrRecode, ref lastDistId),
                    DISTNAME = abonent.Divisnm.Trim(),
                    PRIM_ = abonent.Prim.Trim(),
                    POSTINDEX = abonent.Postindex.Trim(),
                    PHONENUM = abonent.Phonenum.Trim()
                };

                string email;
                if (emails.TryGetValue(abonent.Lshet, out email))
                    a.EMAIL = email;

                DateTime closeDate;
                if (abnEndDates.TryGetValue(abonent.Lshet, out closeDate))
                    a.CLOSEDATE = closeDate;
                DateTime startDate;
                if (abnStartDates.TryGetValue(abonent.Lshet, out startDate))
                    a.STARTDATE = startDate;

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

                
                a.ULICAKOD = new LsKvc(abonent.Lshet, true).StreetId;
                a.ULICANAME = abonent.Ulicaname.Trim();
                if (!String.IsNullOrWhiteSpace(abonent.Ulicacut))
                    a.ULICANAME += " " + char.ToLowerInvariant(abonent.Ulicacut.Trim()[0]) + abonent.Ulicacut.Trim().Substring(1);
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
                if (Int32.TryParse(abonent.Korpus, out korpusno))
                {
                    a.KORPUSNO = korpusno;
                }
                else if (!String.IsNullOrEmpty(abonent.Korpustip.Trim()))
                {
                    a.HOUSEPOSTFIX = abonent.Korpustip.Substring(0, 3).Trim() + " " + abonent.Korpus.Trim();
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

                if(abonent.Komnata != 0) a.ROOMNO = (short)abonent.Komnata;

                List<string> lshetsForPlace;
                if (!placerecode.TryGetValue(abonent.Placecd, out lshetsForPlace))
                    placerecode.Add(abonent.Placecd, new List<string> {a.LSHET});
                else 
                    lshetsForPlace.Add(a.LSHET);

                la.Add(a);

                if (!String.IsNullOrWhiteSpace(abonent.Ptypenm))
                {
                    string flatType = StringRecode(abonent.Ptypenm);
                    if (!String.IsNullOrWhiteSpace(flatType))
                    {
                        int flatTypeId = 0;
                        if (!FlatTypeRecode.Any(r => r.Value.Any(v => v == flatType)))
                            Task.Factory.StartNew(() => MessageBox.Show($"Не найдена перекодировка типа помещения {abonent.Ptypenm}"));
                        else
                            flatTypeId = FlatTypeRecode.First(r => r.Value.Any(v => v == flatType)).Key;
                        laa.Add(new CNV_AADDCHAR
                        {
                            LSHET = a.LSHET,
                            ADDCHARCD = 1620203,
                            VALUE = flatTypeId.ToString()
                        });
                    }
                }

                if (!String.IsNullOrWhiteSpace(abonent.Htypenm))
                {
                    string houseType = StringRecode(abonent.Htypenm);
                    if (!String.IsNullOrWhiteSpace(houseType))
                    {
                        int houseTypeId = 0;
                        if (!HouseTypeRecode.Any(r => r.Value.Any(v => v == houseType)))
                            Task.Factory.StartNew(() => MessageBox.Show($"Не найдена перекодировка типа строения {abonent.Htypenm}"));
                        else
                            houseTypeId = HouseTypeRecode.First(r => r.Value.Any(v => v == houseType)).Key;
                        lha.Add(new CNV_HADDCHAR
                        {
                            HOUSECD = a.HOUSECD,
                            ADDCHARCD = 1620204,
                            VALUE_ = houseTypeId.ToString()
                        });

                        if (!housesWithFlatNumbers.Contains(abonent.Housecd))
                            lhcc.Add(new CNV_CHARSHOUSES
                            {
                                HOUSECD = (int)a.HOUSECD,
                                CHARCD = -4,
                                CHARNAME = "Многоквартирный",
                                VALUE_ = houseTypeId == 4 || houseTypeId == 5 ? 1 : 0,
                                DATE_ = MinConvertDate
                            });
                    }
                }

                Iterate();
            }
            StepFinish();

            Utils.SaveDictionary(lsrecode, LsRecodeFileName);
            Utils.SaveDictionary(durecode, DuRecodeFileName);
            Utils.SaveDictionary(houserecode, HouseRecodeFileName);
            File.WriteAllLines(PlaceToLshetRecodeFileName, placerecode.SelectMany(p => p.Value.Select(v => $"{p.Key};{v}")));
            File.WriteAllLines(HouseToLshetRecodeFileName, la.Select(a => $"{a.HOUSECD};{a.LSHET}"));
            File.WriteAllLines(HouseStreetNameRecodeFileName, la.Select(a => $"{a.HOUSECD};{a.ULICANAME} д.{a.HOUSENO}{a.HOUSEPOSTFIX}").Distinct());

            StepStart(3);
            BufferEntitiesManager.SaveDataToBufferIBScript(la);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(laa);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lha);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lhcc);
            StepFinish();

            var lsNotFromKvc = new List<string>();
            using (DataTable dtSaldo = Tmsource.GetDataTable(AbonentsNotFromKvcSaldoDbfTableName))
            {
                StepStart(dtSaldo.Rows.Count);
                foreach (DataRow dr in dtSaldo.Rows)
                {
                    var saldo = new SaldoNotFromKvcSaldoRecord(dr);
                    lsNotFromKvc.Add(saldo.LsKvc);
                }
                lsNotFromKvc = lsNotFromKvc.Distinct().ToList();
                File.WriteAllLines(AbonenstNotFromKvcFileName, lsNotFromKvc);
            }
            StepFinish();
        }
    }

    public class ConvertExtLshets : DbfConvertCase
    {
        public ConvertExtLshets()
        {
            ConvertCaseName = "EXTLSHET - внешние лицевые счета";
            Position = 21;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$EXTLSHET");
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            long orgCd = 3;
            var orgRecode = new Dictionary<string, long>();
            var extLshets = new List<CNV_EXTLSHET>();
            StepStart(1);
            DbfManager.ExecuteQueryByRow("select * from abnlshet", dr =>
            {
                var extLshetRecord = new AbnlshetRecord();
                extLshetRecord.ReadDataRow(dr);
                long intExtLs;
                string extLshet = extLshetRecord.Lsh.Replace(" ", "");
                if (!long.TryParse(extLshet, out intExtLs))
                    return;

                var lshet = FindLsRecode(extLshetRecord.Lshet, lsrecode);

                if (lshet != 0)
                {
                    extLshets.Add(new CNV_EXTLSHET
                    {
                        EXTORGCD = (int) Utils.GetValue(extLshetRecord.Orgcd, orgRecode, ref orgCd),
                        LSHET = lshet.ToString(),
                        EXTLSHET = extLshet,
                        EXTORGNAME = extLshetRecord.Orgnm
                    });
                }
            });
            StepFinish();

            StepStart(1);
            extLshets = extLshets
                .GroupBy(l => l.LSHET)
                .Select(gr => gr.First(l => Convert.ToInt64(l.EXTLSHET) == gr.Max(ll => Convert.ToInt64(ll.EXTLSHET))))
                .ToList();
            StepFinish();
            
            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(extLshets);
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

            //BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var cold = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                cold.ReadDataRow(dataRow);

                long lshet = FindLsRecode(cold.Lshet, lsrecode);
                if (lshet != 0)
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

            StepStart(2);
            lcc = CharsRecordUtils.CreateUniqueCchars(lcc);
            Iterate();
            lcc = CharsRecordUtils.ThinOutList(lcc, true);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lcc);
            StepFinish();
        }
    }

    public class ConvertOwnersChars : ConvertCase
    {
        public ConvertOwnersChars()
        {
            ConvertCaseName = "OWNERS CHARS - количество собственников из базы КВЦ";
            Position = 31;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);
            var form = new Form_ConnectionString("Строка подключения к БД КВЦ");
            if (form.ShowDialog() != DialogResult.OK) return;
            string kvcConnectionString = form.ConnectionString;
            var lsRecode = Utils.ReadDictionary(LsRecodeFileName);
            int extorgcd = 1;
            int ccharid = 11;
            DateTime maxDate = new DateTime(CurrentYear, CurrentMonth, 1);
            var kvcFb = new FbManager(kvcConnectionString);
            var ownersChars = new List<CNV_CHAR>();
            StepStart(1);
            kvcFb.ExecuteQueryByRow(
                String.Format(SelectOwnersSqlTemplate, ToSql(extorgcd), ToSql(11), ToSql(maxDate), ToSql(MinConvertDate)),
                dr =>
                {
                    var lsKvc = new LsKvc(dr["extlshet"].ToString(), false);
                    long lshet = FindLsRecode(lsKvc.Ls, lsRecode);
                    if (lshet == 0) return;
                    ownersChars.Add(new CNV_CHAR
                    {
                        LSHET = lshet.ToString(),
                        SortLshet = lshet,
                        CHARCD = 11,
                        DATE_ = Convert.ToDateTime(dr["abonentcchardate"]),
                        VALUE_ = Convert.ToDecimal(dr["significance"]),
                    });
                });
            StepFinish();
            StepStart(1);
            ownersChars = CharsRecordUtils.ThinOutList(ownersChars, true);
            StepFinish();
            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(ownersChars);
            StepFinish();
        }

        private string SelectOwnersSqlTemplate =
            "select ea.extlshet, ca.kodccharslist, ca.abonentcchardate, ca.significance " +
            "from ccharsabonentlist ca " +
            "inner join extorgaccounts ea on ea.extorgcd = {0} and ea.lshet = ca.lshet " +
            "where ca.kodccharslist = {1} " +
            "   and (ca.abonentcchardate < {2} and ca.abonentcchardate > {3}) " +
            " " +
            "union all " +
            " " +
            "select ea.extlshet, {1}, {3}, " +
            "   (select first 1 ca.significance " +
            "   from ccharsabonentlist ca " +
            "   where ca.kodccharslist = {1} and ca.lshet = l.lshet and ca.abonentcchardate <= {3} " +
            "   order by ca.abonentcchardate desc) " +
            "from( " +
            "   select distinct ca.lshet " +
            "   from ccharsabonentlist ca " +
            "   where ca.kodccharslist = {1} " +
            "   and ca.abonentcchardate <= {3} " +
            ") as l " +
            "inner join extorgaccounts ea on ea.extorgcd = {0} and ea.lshet = l.lshet";
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
            //BufferEntitiesManager.DropTableData("CNV$LCHARS");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lcc = new List<CNV_LCHAR>();

            using (var dt = Tmsource.GetDataTable("LCHARS"))
            {
                StepStart(dt.Rows.Count);
                var lcold = new LcharsRecord();
                foreach (DataRow dataRow in dt.Rows)
                {
                    lcold.ReadDataRow(dataRow);
                    if (lcold.Lcharcd == 5 && lcold.Lcharname.Trim() == "Электроэнергия: Гараж")
                        lcold.Lcharcd = 55;

                    long lshet = FindLsRecode(lcold.Lshet, lsrecode);
                    if (lshet != 0)
                    {
                        var recodeValue = LcharRecode[new KeyValuePair<long, long>(lcold.Lcharcd, lcold.Value_)];
                        foreach (var recode in recodeValue)
                        {
                            var c = new CNV_LCHAR
                            {
                                LSHET = lshet.ToString(),
                                SortLshet = lshet,
                                LCHARCD = recode.Key,
                                LCHARNAME = lcold.Lcharname.Trim(),
                                DATE_ = lcold.Date,
                                VALUE_ = recode.Value,
                                VALUEDESC = lcold.Valuedesc
                            };
                            lcc.Add(c);
                        }
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

            var nachFiles = ConvertNach.GetNachFiles().Select(nf => new {NachFile = nf, FileDate = ConvertNach.GetNachFileDate(nf)}).OrderBy(nf => nf.FileDate).ToArray();
            StepStart(nachFiles.Length);
            foreach (var file in nachFiles)
            {
                var fileDate = file.FileDate;
                INachImport import = ConvertNach.ImportResolver(file.NachFile);
                aConverterClassLibrary.Utils.ReadExcelFileByRow(file.NachFile, null, dr =>
                {
                    import.Import(dr, nachInfo =>
                    {
                        long lshet = FindLsRecode(nachInfo.LsKvc, lsrecode);
                        if (lshet != 0)
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

                            // Признак наличия счетчика берется из статуса оборудования
                            //if (nachInfo.Nach != NachExcelRecord.NachType.AddNach)
                            //{
                            //    if (nachInfo.Service == NachExcelRecord.ServiceType.Living)
                            //        lcc.Add(new CNV_LCHAR
                            //        {
                            //            LSHET = lshet.ToString(),
                            //            SortLshet = lshet,
                            //            LCHARCD = 21,
                            //            LCHARNAME = "Сч. электроэнергии",
                            //            DATE_ = fileDate,
                            //            VALUE_ = nachInfo.Nach == NachExcelRecord.NachType.WithDevice ? 1 : 0,
                            //            VALUEDESC = nachInfo.Nach.ToString()
                            //        });
                            //    else if (nachInfo.Service == NachExcelRecord.ServiceType.Odn)
                            //        lcc.Add(new CNV_LCHAR
                            //        {
                            //            LSHET = lshet.ToString(),
                            //            SortLshet = lshet,
                            //            LCHARCD = 22,
                            //            LCHARNAME = "Сч. электроэнергии ОДН",
                            //            DATE_ = fileDate,
                            //            VALUE_ = nachInfo.Nach == NachExcelRecord.NachType.WithDevice ? 1 : 0,
                            //            VALUEDESC = nachInfo.Nach.ToString()
                            //        });
                            //}
                        }
                    });
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
        private static Dictionary<string, long> houserecode;
        private static Dictionary<long, List<long>> houseToLshetRecode;

        private static string[] IgnoreCharsList =
        {
            "58ee8b6a-e74a-48e2-b215-7f5156c51d07",
            "8db2b0be-a293-48b1-b025-d3123c9f7b29"
        };

        public override void DoDbfConvert()
        {
            SetStepsCount(4);
            //BufferEntitiesManager.DropTableData("CNV$AADDCHAR");
            
            lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            houserecode = Utils.ReadDictionary(HouseRecodeFileName);
            var placeToLshetRecode = File.ReadAllLines(PlaceToLshetRecodeFileName)
                .Select(s =>
                {
                    string[] info = s.Split(';');
                    return new {Place = info[0], Lshet = long.Parse(info[1])};
                })
                .GroupBy(p => p.Place, p => p.Lshet)
                .ToDictionary(gp => gp.Key, gp => gp.ToList());
            houseToLshetRecode = File.ReadAllLines(HouseToLshetRecodeFileName)
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
                                            long lshet = FindLsRecode(charRecord.Owner, lsrecode);
                                            if (lshet == 0) continue;
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
                                                case AddCharRecodeRecord.AbonentType.AChar:
                                                    lshets.ForEach(ls => AddAChar(recode, charRecord, ls, isEnumChar));
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

            StepStart(1);
            var lsNotFromkvc = Utils.GetLsNotFromKvc();
            foreach (var ls in lsNotFromkvc)
            {
                long lshet = FindLsRecode(ls, lsrecode);
                if (lshet != 0)
                    laac.Add(new CNV_AADDCHAR
                    {
                        LSHET = lshet.ToString(),
                        ADDCHARCD = 16201105,
                        VALUE = "1"
                    });
            }
            StepFinish();
            StepStart(2);
            // инвертирование характеристик
            foreach (var lchar in lalc)
            {
                if (lchar.LCHARCD != 30) continue;
                lchar.VALUE_ = lchar.VALUE_ == 0 ? 1 : 0;
            }
            Iterate();
            foreach (var addchar in laac)
            {
                if (addchar.ADDCHARCD != 1620127 && addchar.ADDCHARCD != 1620142) continue;
                addchar.VALUE = addchar.VALUE == "0" ? "1" : "0";
            }
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


            lacc.Clear();
            lalc.Clear();
            laac.Clear();
            lhcc.Clear();
            lhlc.Clear();
            lhac.Clear();

            valueListRecode.Clear();
            lsrecode.Clear();
            houserecode.Clear();
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
            long housecd;
            switch (recode.RgmekCode)
            {
                case "1a083d4e-8798-48dd-98f1-568029c5ce2c":
                    if (!houserecode.TryGetValue(charRecord.Owner, out housecd)) break;
                    List<long> lshets;
                    if (!houseToLshetRecode.TryGetValue(housecd, out lshets)) break;
                    lshets.ForEach(ls =>
                        lalc.Add(new CNV_LCHAR
                        {
                            LSHET = ls.ToString(),
                            SortLshet = ls,
                            LCHARCD = 13,
                            LCHARNAME = "Электроэнергия ОДН",
                            VALUE_ = charRecord.Value.Trim().ToLower() == "орн" ? 1 : 0,
                            VALUEDESC = charRecord.Value,
                            DATE_ = charRecord.Date
                        }));
                    break;
                case "fc7c4f38-aee3-48d8-bb79-98bb1ac2462f":
                    if (!houserecode.TryGetValue(charRecord.Owner, out housecd)) break;
                    lhcc.Add(new CNV_CHARSHOUSES
                    {
                        HOUSECD = (int)housecd,
                        CHARCD = -5,
                        CHARNAME = "Количество квартир",
                        VALUE_ = Convert.ToDecimal(charRecord.Value),
                        DATE_ = charRecord.Date
                    });
                    lhcc.Add(new CNV_CHARSHOUSES
                    {
                        HOUSECD = (int)housecd,
                        CHARCD = -4,
                        CHARNAME = "Многоквартирный",
                        VALUE_ = Convert.ToDecimal(charRecord.Value) > 1 ? 1 : 0,
                        DATE_ = charRecord.Date
                    });
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
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$COUNTERTYPES");
            BufferEntitiesManager.DropTableData("CNV$COUNTERTYPEADDCHAR");
            BufferEntitiesManager.DropTableData("CNV$COUNTERADDCHAR");
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            
            var lc = new List<CNV_COUNTER>();
            var lca = new List<CNV_COUNTERADDCHAR>();
            var lta = new List<CNV_COUNTERTYPEADDCHAR>();

            var cnttyperecode = new Dictionary<string, CNV_COUNTERTYPE>();

            var counteridrecode = new Dictionary<string, long>();
            long counterid = 0;

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            StepStart(1);
            var digitCounterDic = GetCounterDigits(Tmsource);
            StepFinish();
            int lastcnttypeid = 0;
            var lcold = new CountersRecord();

            var multiscalesCounters = new HashSet<string>();
            var abonentTarifs = new Dictionary<string, NachExcelRecord.TarifType>();
            DbfManager.ExecuteQueryByReader(MultiscalesCountersSql, r =>
            {
                multiscalesCounters.Add(r.GetString(0));
                if (!abonentTarifs.ContainsKey(r.GetString(1)))
                    abonentTarifs.Add(r.GetString(1), NachExcelRecord.TarifType.Unknown);
            });

            var nachFile = ConvertNach.GetNachFiles()
                .Select(nf => new { NachFile = nf, FileDate = ConvertNach.GetNachFileDate(nf) })
                .OrderByDescending(nf => nf.FileDate)
                .FirstOrDefault(nf => nf.FileDate <= new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1));
            if (nachFile == null)
                nachFile = ConvertNach.GetNachFiles()
                    .Select(nf => new {NachFile = nf, FileDate = ConvertNach.GetNachFileDate(nf)})
                    .OrderByDescending(nf => nf.FileDate)
                    .First();
            aConverterClassLibrary.Utils.ReadExcelFileByRow(nachFile.NachFile, null, dr =>
            {
                var nachInfo = new NachExcelRecord(dr);
                if (nachInfo.Sum == 0 && nachInfo.SumCoef == 0 && nachInfo.Volume == 0) return;
                if (!abonentTarifs.ContainsKey(nachInfo.LsKvc)) return;
                abonentTarifs[nachInfo.LsKvc] = nachInfo.Tarif;
            });

            string sql = @"select {0} 
                from counters c
                left join (
            	    select oldcntid, max(date) as enddate 
            	    from cntrs_ac
            	    where opertype in ('Замена', 'Снятие')
            	    group by oldcntid
                ) d on d.oldcntid = c.counterid
                where c.counterid is not null and c.counterid <> ''";

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(String.Format(sql, "count(0)"))) + 1);

            DbfManager.ExecuteQueryByRow(String.Format(sql, "c.*, d.enddate"), dataRow =>
            { 
                Iterate();
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

                    if (lcold.Id_tpmeter > 0)
                    {
                        lta.Add(new CNV_COUNTERTYPEADDCHAR
                        {
                            COUNTERTYPEID = cnttype.ID,
                            ADDCHARCD = 907,
                            VALUE_ = lcold.Id_tpmeter.ToString()
                        });
                    }
                    if (!String.IsNullOrWhiteSpace(lcold.Phasecount))
                        lta.Add(new CNV_COUNTERTYPEADDCHAR
                        {
                            COUNTERTYPEID = cnttype.ID,
                            ADDCHARCD = 904,
                            VALUE_ = lcold.Phasecount.Trim()
                        });
                    if (!String.IsNullOrWhiteSpace(lcold.Amperage))
                        lta.Add(new CNV_COUNTERTYPEADDCHAR
                        {
                            COUNTERTYPEID = cnttype.ID,
                            ADDCHARCD = 905,
                            VALUE_ = lcold.Amperage.Trim()
                        });
                }

                long lshet = FindLsRecode(lcold.Lshet, lsrecode);
                if (lshet != 0)
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
                        SETUPPLACE = /*(int?) lcold.Instplid*/ 0
                    };
                    c.COUNTERID = Utils.GetValue(lcold.Counterid, counteridrecode, ref counterid).ToString();
                    if (lcold.Lastpov.Year > 1950)
                    {
                        c.LASTPOV = lcold.Lastpov;
                        if (lcold.Periodpov > 0)
                            c.NEXTPOV = lcold.Lastpov.AddMonths((int) lcold.Periodpov);
                    }

                    if (lcold.Id_meter > 0)
                        lca.Add(new CNV_COUNTERADDCHAR
                        {
                            COUNTERID = c.COUNTERID,
                            ADDCHARCD = 906,
                            VALUE_ = lcold.Id_meter.ToString()
                        });

                    if (!String.IsNullOrEmpty(lcold.Instplace))
                    {
                        string instPlace = StringRecode(lcold.Instplace);
                        if (!String.IsNullOrWhiteSpace(instPlace))
                        { 
                            int placeId = 0;
                            if (!CoutnerPlaceRecode.Any(r => r.Value.Any(v => v == instPlace)))
                                Task.Factory.StartNew(() => MessageBox.Show($"Не найдена перекодировка места установки {lcold.Instplace}"));
                            else
                                placeId = CoutnerPlaceRecode.First(r => r.Value.Any(v => v == instPlace)).Key;
                            lca.Add(new CNV_COUNTERADDCHAR
                            {
                                COUNTERID = c.COUNTERID,
                                ADDCHARCD = 900,
                                VALUE_ = placeId.ToString()
                            });
                        }
                    }

                    if (multiscalesCounters.Contains(lcold.Counterid))
                    {
                        var abnonentTarif = abonentTarifs[lcold.Lshet];

                        int dayRegim, nightRegim;
                        switch (abnonentTarif)
                        {
                            case NachExcelRecord.TarifType.Ep2:
                                dayRegim = (int) NachExcelRecord.TarifType.Ep2 + (int) NachExcelRecord.ZoneType.Day;
                                nightRegim = (int) NachExcelRecord.TarifType.Ep2 + (int) NachExcelRecord.ZoneType.Night;
                                break;
                            default:
                                dayRegim = (int) NachExcelRecord.TarifType.Gp2 + (int) NachExcelRecord.ZoneType.Day;
                                nightRegim = (int) NachExcelRecord.TarifType.Gp2 + (int) NachExcelRecord.ZoneType.Night;
                                break;
                        }

                        c.KODREGIM = dayRegim;
                        c.UNTINGID = c.COUNTERID;
                        lc.Add(c);

                        lc.Add(new CNV_COUNTER
                        {
                            LSHET = c.LSHET,
                            NAME = c.NAME,
                            SERIALNUM = c.SERIALNUM,
                            CNTNAME = c.CNTNAME,
                            SETUPDATE = c.SETUPDATE,
                            DEACTDATE = c.DEACTDATE,
                            GUID_ = c.GUID_,
                            CNTTYPE = c.CNTTYPE,
                            SETUPPLACE = c.SETUPPLACE,
                            COUNTERID = (++counterid).ToString(),
                            LASTPOV = c.LASTPOV,
                            NEXTPOV = c.NEXTPOV,
                            PRIM_ = c.PRIM_,
                            KODREGIM = nightRegim,
                            UNTINGID = c.COUNTERID
                        });
                    }
                    else
                        lc.Add(c);
                }
            });
            Utils.SaveDictionary(cnttyperecode.ToDictionary(ct => ct.Key, ct => (long)ct.Value.ID), CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, CounterIdRecodeFileName);
            Utils.SaveDictionary(lc
                .Select(c => new {c.COUNTERID, c.LSHET})
                .GroupBy(c => c.COUNTERID, c => c.LSHET)
                .Select(gc => new {COUNTERID = gc.Key, Lshet = gc.First()})
                .ToDictionary(c => c.COUNTERID.ToString(), c => Convert.ToInt64(c.Lshet)),
                CounterIdToLsRecodeFileName);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(cnttyperecode.Values);
            StepFinish();

            StepStart(3);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lca);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lta);
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

        public const string MultiscalesCountersSql =
            @"select distinct c.counterid, c.lshet
            from (
	            select cc.*
	            from (
		            select c.counterid
		            from (
			            select distinct c.counterid 
			            from counters c
		            ) c
		            inner join scales s on s.counterid = c.counterid
		            where s.name <> 'Основная'
		            group by c.counterid
		            having count(0) > 1
	            ) c
	            inner join counters cc on cc.counterid = c.counterid
            ) c
            inner join scales s on s.counterid = c.counterid";
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
            var lca = new List<CNV_COUNTERADDCHAR>();
            var lta = new List<CNV_COUNTERTYPEADDCHAR>();
            var cnttyperecode = new Dictionary<string, CNV_COUNTERTYPE>();

            var counteridrecode = new Dictionary<string, long>();
            long counterid = Utils.ReadDictionary(CounterIdRecodeFileName).Select(d => d.Value).Max() + 1;

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
                    if (String.IsNullOrWhiteSpace(cntRecord.Counterid)) continue;

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

                        if (!String.IsNullOrWhiteSpace(cntRecord.Amperage))
                            lta.Add(new CNV_COUNTERTYPEADDCHAR
                            {
                                COUNTERTYPEID = cnttype.ID,
                                ADDCHARCD = 905,
                                VALUE_ = cntRecord.Amperage.Trim()
                            });
                    }

                    long houseid;
                    if (houserecode.TryGetValue(cntRecord.Housecd, out houseid))
                    {
                        string[] abnList;
                        if (abnsInHouses.TryGetValue(cntRecord.Housecd, out abnList) && housestreetnamerecode.ContainsKey(houseid))
                        {
                            foreach (var lsKvc in abnList)
                            {
                                long lshet = FindLsRecode(lsKvc, lsrecode);
                                if (lshet == 0) continue;
                                var enddate = dr.IsNull("enddate") ? (DateTime?)null : Convert.ToDateTime(dr["enddate"]);
                                if (enddate == DateTime.MinValue) enddate = null;
                                var c = new CNV_COUNTER()
                                {
                                    LSHET = lshet.ToString(),
                                    NAME = housestreetnamerecode[houseid],
                                    SERIALNUM = cntRecord.Serialnum.Trim(),
                                    CNTNAME = cntRecord.Cntname,
                                    SETUPDATE = cntRecord.Setupdate,
                                    DEACTDATE = enddate == null || enddate == NullDate ? null : enddate,
                                    GUID_ = cntRecord.Counterid.Trim(),
                                    CNTTYPE = cnttype.ID,
                                    SETUPPLACE = (int?)cntRecord.Instplid,
                                    COUNTER_LEVEL = 1,
                                    DISTRIBUTINGMETHOD = 4,
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

                                if (!String.IsNullOrEmpty(cntRecord.Instplace))
                                {
                                    string instPlace = StringRecode(cntRecord.Instplace);
                                    if (!String.IsNullOrWhiteSpace(instPlace))
                                    {
                                        int placeId = 0;
                                        if (!CoutnerPlaceRecode.Any(r => r.Value.Any(v => v == instPlace)))
                                            Task.Factory.StartNew(() => MessageBox.Show($"Не найдена перекодировка места установки {cntRecord.Instplace}"));
                                        else
                                            placeId = CoutnerPlaceRecode.First(r => r.Value.Any(v => v == instPlace)).Key;
                                        lca.Add(new CNV_COUNTERADDCHAR
                                        {
                                            COUNTERID = c.COUNTERID,
                                            ADDCHARCD = 900,
                                            VALUE_ = placeId.ToString()
                                        });
                                    }
                                }

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

        private Dictionary<string, long> _lsToCounters;
        private Dictionary<long, string> _reverseLsRecode;
        private HashSet<string> _lsNotFromKvc;

        public override void DoDbfConvert()
        {
            SetStepsCount(4);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            var lc = new List<CNV_CNTRSIND>();
            _lsNotFromKvc = Utils.GetLsNotFromKvc();

            _lsToCounters = Utils.ReadDictionary(CounterIdToLsRecodeFileName);
            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            var groupcounteridrecode = Utils.ReadDictionary(GroupCounterIdRecodeFileName);
            _reverseLsRecode = Utils.ReadDictionary(LsRecodeFileName).ToDictionary(l => l.Value, l => l.Key);
            long counterid = 0;

            var multiscalesCounters = new HashSet<string>();
            DbfManager.ExecuteQueryByReader(ConvertCounters.MultiscalesCountersSql, r =>
            {
                multiscalesCounters.Add(r.GetString(0));
            });

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(
                @"select top 1
            	(select count(0) from CNTRSIND_2015) +
            	(select count(0) from CNTRSIND_2016) +
            	(select count(0) from CNTRSIND_2017) +
            	(select count(0) from CNTRSIND_2018) 
            from TARIFS
            order by TARIFCD")));
            DbfManager.ExecuteQueryByRow(@"select * from CNTRSIND_2015
                                        union all
                                        select * from CNTRSIND_2016
                                        union all
                                        select * from CNTRSIND_2017
                                        union all
                                        select * from CNTRSIND_2018", 
                                        dataRow =>
            {
                var cr = new CntrsindRecord();
                cr.ReadDataRow(dataRow);
                if (cr.Date < MinConvertDate) return;

                counterid = 0;

                bool isGroupCounter = false;
                if (!counteridrecode.TryGetValue(cr.Counterid, out counterid))
                {
                    isGroupCounter = groupcounteridrecode.TryGetValue(cr.Counterid, out counterid);
                }
            
                if (counterid != 0)
                {
                    if (multiscalesCounters.Contains(cr.Counterid))
                    {
                        if (cr.Scalecd > 1) counterid++;
                    }
                    var c = new CNV_CNTRSIND
                    {
                        COUNTERID = counterid.ToString(),
                        DOCUMENTCD = GetDocumentCd(cr.Doc),
                        INDDATE = cr.Date,
                        INDICATION = cr.Indication,
                        INDTYPE = 1,
                        CASETYPE = cr.Indtype.Trim() == "Расчетное" ? 3 : (int?) null,
                        OLDIND = cr.Indication,
                        OB_EM = 0
                    };

                    if (IsNorFromKvc(c.COUNTERID) || isGroupCounter)
                        c.INDTYPE = 0;

                    lc.Add(c);
                }
                Iterate();
            });
            StepFinish();

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(@"select top 1
                (select count(0) from CNTRSKVC_2015 where indtype = 'От абонента (по квитанции)') +
            	(select count(0) from CNTRSKVC_2016 where indtype = 'От абонента (по квитанции)') +
            	(select count(0) from CNTRSKVC_2017 where indtype = 'От абонента (по квитанции)') +
            	(select count(0) from CNTRSKVC_2018 where indtype = 'От абонента (по квитанции)') 
            from TARIFS
            order by TARIFCD")));
            DbfManager.ExecuteQueryByRow(@"select * from CNTRSKVC_2015
                                        where indtype = 'От абонента (по квитанции)'
                                        union all
                                        select * from CNTRSKVC_2015
                                        where indtype = 'От абонента (по квитанции)'
                                        union all
                                        select * from CNTRSKVC_2017
                                        where indtype = 'От абонента (по квитанции)'
                                        union all
                                        select * from CNTRSKVC_2018
                                        where indtype = 'От абонента (по квитанции)'",
                dataRow =>
                {
                    var cr = new CntrsindRecord();
                    cr.Counterid = dataRow["CounterID"].ToString().Trim();
                    cr.Doc = dataRow["Doc"].ToString().Trim();
                    cr.Date = Convert.ToDateTime(dataRow["DocDate"].ToString());
                    cr.Indication = Convert.ToDecimal(dataRow["Indication"]);

                    if (cr.Date < MinConvertDate) return;

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid) ||
                        groupcounteridrecode.TryGetValue(cr.Counterid, out counterid))
                    {
                        if (IsNorFromKvc(counterid.ToString())) return;
                        if (multiscalesCounters.Contains(cr.Counterid))
                        {
                            if (cr.Scalecd > 1) counterid++;
                        }
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

            //StepStart(1);
            //lc = CntrsindRecordUtils.ThinOutList(lc);
            //StepFinish();

            var mainInds = lc.Where(c => c.INDTYPE == 0).ToList();
            var addInds = lc.Where(c => c.INDTYPE == 1).ToList();

            StepStart(1);
            CntrsindRecordUtils.RestoreHistory(ref mainInds, RestoreHistoryType.С_конца_по_конечным_показаниям);
            StepFinish();

            mainInds.AddRange(addInds);

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(mainInds);
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

        private bool IsNorFromKvc(string counterid)
        {
            long lsByCounter;
            if (_lsToCounters.TryGetValue(counterid, out lsByCounter))
            {
                string lsKvcByCounter;
                if (_reverseLsRecode.TryGetValue(lsByCounter, out lsKvcByCounter))
                {
                    return _lsNotFromKvc.Contains(lsKvcByCounter);
                }
            }
            return false;
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
            //BufferEntitiesManager.DropTableData("CNV$OPLATA");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var sourcedocrecode = new Dictionary<string, long>();
            long lastsourcedoc = 10;

            var lp = new List<CNV_OPLATA>();
            int paydoc = 0;

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(@"select top 1
            	(select count(0) from PAYMENT_2018) 
            from TARIFS
            order by TARIFCD")) + 1);
            // (select count(0) from PAYMENT_2015_1) 
            //+
            //    (select count(0) from PAYMENT_2015_2) 
            //+
            //    (select count(0) from PAYMENT_2016_1) +
            //    (select count(0) from PAYMENT_2016_2) 
            //+
            //    (select count(0) from PAYMENT_2017) +
            //    (select count(0) from PAYMENT_2018) 
            DbfManager.ExecuteQueryByReader(@"select * from PAYMENT_2018
                                            ", reader =>
            // select * from PAYMENT_2015_1
            //union all
            //select * from PAYMENT_2015_2
            //union all
            //select * from PAYMENT_2016_1
            //union all
            //         select *from PAYMENT_2015_2
            //union all
            //select * from PAYMENT_2017
            //union all
            //         select *from PAYMENT_2018
            {
                var pr = new PaymentRecord();
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

                if (pr.Date < MinConvertDate) return;
                if (pr.Activcd == "11") return;

                long lshet = FindLsRecode(pr.Lshet, lsrecode);
                if (lshet != 0)
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
                        SOURCECD = (int) Utils.GetValue(pr.Paypostcd, sourcedocrecode, ref lastsourcedoc)
                    });
                }
            });
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
            return 9;
            //throw new Exception($"Необработанная услуга {pr.Servicenm}");
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

        public static readonly Regex FileNameRegex = new Regex(@"(?<month>январь|февраль|март|апрель|май|июнь|июль|август|сентябрь|октябрь|ноябрь|декабрь) *(?<year>\d{4})(?<addInfo>.*)\.xlsx");
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

        public static INachImport ImportResolver(string fileName)
        {
            var fileDate = GetNachFileDate(fileName);
            INachImportFactory importFactory;
            if (fileDate.Year == 2015) importFactory = new OldFileNachFactory();
            else importFactory = new CommonNachFactory();
            return importFactory.Create(fileName);
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            //BufferEntitiesManager.DropTableData("CNV$NACH");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var ln = new List<CNV_NACH>();

            int nachdoc = 0;
            var files = GetNachFiles();
            StepStart(files.Length);
            foreach (var file in files)
            {
                Iterate();
                var fileDate = GetNachFileDate(file);

                if (fileDate.Year != 2018) continue;

                INachImport import = ImportResolver(file);

                aConverterClassLibrary.Utils.ReadExcelFileByRow(file, null, dr =>
                {
                    import.Import(dr, nachInfo =>
                    {
                        long lshet = FindLsRecode(nachInfo.LsKvc, lsrecode);
                        if (lshet == 0) return;

                        var nach = new CNV_NACH
                        {
                            LSHET = lshet.ToString(),

                            MONTH_ = fileDate.Month,
                            MONTH2 = fileDate.Month,
                            YEAR_ = fileDate.Year,
                            YEAR2 = fileDate.Year,
                            DATE_VV = fileDate,

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
                                ln.Add(new CNV_NACH
                                {
                                    LSHET = nach.LSHET,
                                    MONTH_ = nach.MONTH_,
                                    MONTH2 = nach.MONTH2,
                                    YEAR_ = nach.YEAR_,
                                    YEAR2 = nach.YEAR2,
                                    DATE_VV = nach.DATE_VV,
                                    AUTOUSE = nach.AUTOUSE,
                                    CASETYPE = nach.CASETYPE,
                                    TYPE_ = nach.TYPE_,
                                    VTYPE_ = nach.VTYPE_,
                                    SERVICECD = nach.SERVICECD,
                                    SERVICENAME = nach.SERVICENAME,
                                    FNATH = 0,
                                    VOLUME = 0,
                                    PROCHL = nachInfo.Sum,
                                    PROCHLVOLUME = nachInfo.Volume,
                                    REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + (int) nachInfo.Zone,
                                    REGIMNAME = nachInfo.Nach.ToString(),
                                    DOCUMENTCD = $"N{++nachdoc}"
                                });
                            }
                        }
                        else
                        {
                            if (nachInfo.Sum != 0 && nachInfo.Volume != 0)
                            {
                                ln.Add(new CNV_NACH
                                {
                                    LSHET = nach.LSHET,
                                    MONTH_ = nach.MONTH_,
                                    MONTH2 = nach.MONTH2,
                                    YEAR_ = nach.YEAR_,
                                    YEAR2 = nach.YEAR2,
                                    DATE_VV = nach.DATE_VV,
                                    AUTOUSE = nach.AUTOUSE,
                                    CASETYPE = nach.CASETYPE,
                                    TYPE_ = nach.TYPE_,
                                    VTYPE_ = nach.VTYPE_,
                                    SERVICECD = nach.SERVICECD,
                                    SERVICENAME = nach.SERVICENAME,
                                    FNATH = nachInfo.Sum - nachInfo.SumCoef,
                                    VOLUME = nachInfo.Volume,
                                    PROCHL = 0,
                                    PROCHLVOLUME = 0,
                                    REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + (int) nachInfo.Zone,
                                    REGIMNAME = nachInfo.Nach.ToString(),
                                    DOCUMENTCD = $"N{++nachdoc}"
                                });
                            }
                            if (nachInfo.SumCoef != 0)
                            {
                                ln.Add(new CNV_NACH
                                {
                                    LSHET = nach.LSHET,
                                    MONTH_ = nach.MONTH_,
                                    MONTH2 = nach.MONTH2,
                                    YEAR_ = nach.YEAR_,
                                    YEAR2 = nach.YEAR2,
                                    DATE_VV = nach.DATE_VV,
                                    AUTOUSE = nach.AUTOUSE,
                                    CASETYPE = nach.CASETYPE,
                                    TYPE_ = nach.TYPE_,
                                    VTYPE_ = nach.VTYPE_,
                                    SERVICECD = nach.SERVICECD,
                                    SERVICENAME = nach.SERVICENAME,
                                    FNATH = nachInfo.SumCoef,
                                    VOLUME = 0,
                                    PROCHL = 0,
                                    PROCHLVOLUME = 0,
                                    REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + 100 + (int) nachInfo.Zone,
                                    REGIMNAME = nachInfo.Nach.ToString(),
                                    DOCUMENTCD = $"N{++nachdoc}"
                                });
                            }
                        }
                    });
                });

                BufferEntitiesManager.SaveDataToBufferIBScript(ln);
                
                ln.Clear();
                ln.TrimExcess();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
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
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lsNotFromKvc = Utils.GetLsNotFromKvc();

            var lnop = new List<CNV_NACHOPL>();

            var convertDate = new DateTime(CurrentYear, CurrentMonth, 1).AddDays(-1);

            using (var dt = Tmsource.ExecuteQuery("select * from lschars where charcd = 'd673ef5d-90b1-11df-ae5f-001e8c71f1cc'"))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var charRecord = new CommonAddCharRecord(dr);
                    if (lsNotFromKvc.Contains(charRecord.Owner)) continue;
                    long lshet = FindLsRecode(charRecord.Owner, lsrecode);
                    if (lshet != 0)
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

                StepStart(lsNotFromKvc.Count);
                using (DataTable dtSaldo = Tmsource.GetDataTable(AbonentsNotFromKvcSaldoDbfTableName))
                {
                    foreach (DataRow dr in dtSaldo.Rows)
                    {
                        var saldo = new SaldoNotFromKvcSaldoRecord(dr);
                        long lshet = FindLsRecode(saldo.LsKvc, lsrecode);
                        if (lshet != 0)
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
                                EDEBET = saldo.Saldo,
                                MONTH2 = convertDate.Month,
                                YEAR2 = convertDate.Year,
                                MONTH_ = convertDate.Month,
                                YEAR_ = convertDate.Year
                            });
                        }
                    }
                }
                StepFinish();

                StepStart(1);
                //SaveListInsertSQL(lnop, InsertRecordCount);
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
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "2", "0" });
            Iterate();
            fbm.ExecuteNonQuery("ALTER trigger LSHETS_CHANGE inactive");
            fbm.ExecuteProcedure("CNV$CNV_02150_MASSEXTLSHETS");
            fbm.ExecuteNonQuery("ALTER trigger LSHETS_CHANGE active");
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
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery(
                @"
INSERT INTO CNV$CHARS (LSHET, CHARCD, CHARNAME, VALUE_, DATE_)
select a.lshet, 24, 'Норма на эл. энергию ОДН', 1.25, '01.01.2000'
from cnv$abonent a
left join (
    select distinct ch.lshet
    from cnv$chars ch
    where ch.charcd = 24
) l on l.lshet = a.lshet
where l.lshet is null");
            Iterate();
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
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00910_ADDCHARS");
            Iterate();
            fbm.ExecuteQuery(
                @"INSERT INTO ABONENTADDITIONALCHARS (ADDITIONALCHARCD, LSHET, SIGNIFICANCE, CHANGEDOCUMENTCD)
with charlist as (
    select ac.additionalcharcd
    from additionalchars ac
    where ac.additionalchartype = 7 and ac.additionalcharmode = 1 and ac.additionalcharsgroupcd = 10
)
select cl.additionalcharcd, a.lshet, '0', null
from charlist cl
inner join abonents a on 1 = 1
where not exists (
    select 0
    from abonentadditionalchars ad
    where ad.additionalcharcd = cl.additionalcharcd and ad.lshet = a.lshet)");
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
            fbm.ExecuteNonQuery(
                @"update cnv$cntrsind ci
                set ci.ob_em = 0
                where abs(ci.ob_em) > 100000");
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0", "1", "0", "1" });
            StepFinish();
        }
    }

    public class TransferCounterChars : ConvertCase
    {
        public TransferCounterChars()
        {
            ConvertCaseName = "Перенос данных о характеристиках счетчиков";
            Position = 1041;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01110_COUNTERADDCHAR");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01120_CNTRTYPEADDCHAR");
            StepFinish();
        }
    }

    public class TransferGroupCounters : ConvertCase
    {
        public TransferGroupCounters()
        {
            ConvertCaseName = "Перенос данных о групповых счетчиках и показаниях";
            Position = 1045;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01050_GROUPCOUNTERS", new[] { "0", "1", "0" });
            Iterate();
        }
    }

    public class TransferCounterAttributeFromStatuses : ConvertCase
    {
        public TransferCounterAttributeFromStatuses()
        {
            ConvertCaseName = "Перенос данных о наличии счетчика из статусов счетчиков";
            Position = 1046;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(3);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("delete from cnv$lchars");
            Iterate();
            fbm.ExecuteNonQuery(@"INSERT INTO CNV$LCHARS (LSHET, DATE_, LCHARCD, VALUE_)
with lslist as (
    select ae.lshet, es.statusdate, coalesce(rc.counter_level, 0) counter_level
    from eqstatuses es
    inner join abonentsequipment ae on ae.equipmentid = es.equipmentid
    inner join resourcecounters rc on rc.kod = es.equipmentid
    group by ae.lshet, es.statusdate, rc.counter_level
)
select distinct ls.lshet, ls.statusdate, iif (ls.counter_level = 1, 22, 21) as lcharcd,
    iif (exists(
            select 0
            from abonentsequipment ae
            inner join resourcecounters rc on rc.kod = ae.equipmentid
                                        and coalesce(rc.counter_level, 0) = ls.counter_level
            where ae.lshet = ls.lshet
                and 0 < (select first 1 es.STATUSCD
                        from EQSTATUSES es
                        where es.equipmentid = rc.kod
                                        and es.statusdate <= ls.statusdate
                                        order by es.STATUSDATE desc)
        ), 1, 0) as lcharvalue
from lslist ls");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "0", "0" });
            StepFinish();
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
            SetStepsCount(2);
            var convertDate = new DateTime(CurrentYear, CurrentMonth, 1).AddDays(-1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(3);
            fbm.ExecuteScript(@"
SET TERM ^ ;

create procedure RestoreSaldo(saldoyear integer, saldomonth integer) as

declare variable lshet varchar(10);
declare variable servicecd integer;
declare variable year_ integer;
declare variable month_ integer;
declare variable begsaldo numeric(18,4);
declare variable nach numeric(18,4);
declare variable recalc numeric(18,4);
declare variable paysum numeric(18,4);
declare variable endsaldo numeric(18,4);
declare variable nachid integer;

declare variable prevlshet varchar(10);
declare variable prevservice integer;
declare variable prevsaldo numeric(18,4);
declare variable checkdate date;
begin
	merge into cnv$nachopl nop
	using (
		select cn.lshet, cn.servicecd, cn.year_, cn.month_, sum(cn.fnath) as nachsum, sum(cn.prochl) as recalcsum
		from cnv$nach cn
		group by cn.lshet, cn.servicecd, cn.year_, cn.month_
	) cn on nop.lshet = cn.lshet and nop.servicecd = cn.servicecd and nop.year_ = cn.year_ and nop.month_ = cn.month_
	when matched then
		update set nop.fnath = cn.nachsum, nop.prochl = cn.recalcsum
	when not matched then
		INSERT (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME)
			VALUES (cn.lshet, cn.month_, cn.year_, cn.month_, cn.year_, 0, cn.nachsum, cn.recalcsum, 0, 0, cn.servicecd, cn.servicecd);


	merge into cnv$nachopl nop
	using (
		select co.lshet, co.servicecd, co.year_, co.month_, sum(co.summa) as paysum
		from cnv$oplata co
		group by co.lshet, co.servicecd, co.year_, co.month_
	) co on nop.lshet = co.lshet and nop.servicecd = co.servicecd and nop.year_ = co.year_ and nop.month_ = co.month_
	when matched then
		update set nop.oplata = co.paysum
	when not matched then
		INSERT (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME)
			VALUES (co.lshet, co.month_, co.year_, co.month_, co.year_, 0, 0, 0, co.paysum, 0, co.servicecd, co.servicecd);

    prevlshet = '';
    prevservice = -1;
    checkdate = cast('01.'||:saldomonth||'.'||:saldoyear as date);
    for select nop.lshet, nop.servicecd, nop.year_, nop.month_
        from cnv$nachopl nop
        where nop.year_ * 12 + nop.month_ <= :saldoyear * 12 + :saldomonth
        order by nop.lshet, nop.servicecd, nop.year_ desc, nop.month_ desc
    into :lshet, :servicecd, :year_, :month_
    do begin
        if (:lshet <> :prevlshet or :servicecd <> :prevservice) then
            checkdate = cast('01.'||:saldomonth||'.'||:saldoyear as date);
        else
            checkdate = dateadd(month, -1, :checkdate);
        
        while (extract(year from :checkdate) <> :year_ or extract(month from :checkdate) <> :month_)
        do begin
            INSERT INTO cnv$nachopl (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME)
                VALUES (:lshet, extract(month from :checkdate), extract(year from :checkdate), extract(month from :checkdate), extract(year from :checkdate), 0, 0, 0, 0, 0, :servicecd, :servicecd);
            checkdate = dateadd(month, -1, :checkdate);
        end

        prevlshet = :lshet;
        prevservice = :servicecd;
    end


    prevlshet = '';
    prevservice = -1;
    for select nop.lshet, nop.servicecd, nop.year_, nop.month_, nop.bdebet, nop.fnath, nop.prochl, nop.oplata, nop.edebet, nop.id
            from cnv$nachopl nop
            where nop.year_ * 12 + nop.month_ <= :saldoyear * 12 + :saldomonth
            order by nop.lshet, nop.servicecd, nop.year_ desc, nop.month_ desc
    into :lshet, :servicecd, :year_, :month_, :begsaldo, :nach, :recalc, :paysum, :endsaldo, :nachid
    do begin
        if (:lshet <> prevlshet or :servicecd <> prevservice) then
        begin
            prevlshet = :lshet;
            prevservice = :servicecd;
            prevsaldo = :endsaldo + :paysum - :recalc - :nach;
            update cnv$nachopl nop
                set nop.bdebet = :prevsaldo
                where nop.id = :nachid;
        end
        else
        begin
            endsaldo = :prevsaldo;
            prevsaldo = :endsaldo + :paysum - :recalc - :nach;
            update cnv$nachopl nop
                set nop.edebet = :endsaldo,
                    nop.bdebet = :prevsaldo
                where nop.id = :nachid;
        end
    end
end^

SET TERM ; ^");
            Iterate();
            fbm.ExecuteProcedure("RestoreSaldo", new[] { convertDate.Year.ToString(), convertDate.Month.ToString() });
            Iterate();
            fbm.ExecuteNonQuery("DROP PROCEDURE RestoreSaldo");
            StepFinish();
            StepStart(1);
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] {CurrentYear.ToString(), CurrentMonth.ToString()});
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
