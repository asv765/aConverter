using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.Class.ConvertCases;
using aConverterClassLibrary.Class.Utils.KVC;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using FirebirdSql.Data.FirebirdClient;
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
        public static readonly string CommunalCounterIdRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\communalcounteridrecode.csv";
        public static readonly string IndtypeRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\indtyperecode.csv";
        public static readonly string PlaceToLshetRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\placetolshetrecode.csv";
        public static readonly string HouseToLshetRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\housetolshetrecode.csv";
        public static readonly string HouseStreetNameRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\housestreetnamerecode.csv";
        public static readonly string AbonenstNotFromKvcFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\abonentsnotfromkvc.txt";
        public static readonly string CounterIdToLsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\counteridtolsrecode.csv";
        public static readonly string LshetAddressNameFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\lshetaddressnamerecode.csv";
        public static readonly string NachFilesDirectory = aConverter_RootSettings.SourceDbfFilePath + @"\Nach";
        public static readonly string AddCharsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\AddCharsRecode.xlsx";
        public static readonly string PaymentSourcesRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\PaymentSources.xlsx";
        public static readonly string ActSaldoFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Сальдо по актам в А+.xls";
        public static readonly string OwnerPartCharsFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Разделение лицевых счетов.xlsx";

        public static readonly string AbonentsNotFromKvcSaldoDbfTableName = "saldoNotKvc";

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
            { new KeyValuePair<long, long>(5, 2), new[] {new KeyValuePair<int, int>(5, 7)} },
            { new KeyValuePair<long, long>(5, 3), new[] {new KeyValuePair<int, int>(5, 2)} },
            { new KeyValuePair<long, long>(5, 4), new[] {new KeyValuePair<int, int>(5, 3)} },
            { new KeyValuePair<long, long>(5, 5), new[] {new KeyValuePair<int, int>(5, 8)} },
            { new KeyValuePair<long, long>(5, 6), new[] {new KeyValuePair<int, int>(5, 6)} },
            { new KeyValuePair<long, long>(5, 7), new[] {new KeyValuePair<int, int>(5, 9)} },
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

        public static readonly int CurrentMonth = 04; // должен быть следующий месяц после последнего закрытого
        public static readonly int CurrentYear = 2018;
        public static readonly DateTime MinConvertDate = new DateTime(2015, 1, 1);
        public static readonly DateTime NullDate = new DateTime(1899, 12, 30);
        public static readonly DateTime InitialDate = new DateTime(2000,01,01);

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
            {9, new[] {"стройка"}},
        };

        public static readonly string[] PaymentTables =
        {
            "PAYMENT_2015_1",
            "PAYMENT_2015_2",
            "PAYMENT_2016_1",
            "PAYMENT_2016_2",
            "PAYMENT_2017",
            "PAYMENT_2018",
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
            {41, new[] {"кварт.", "квартира", "эл.эн./квартира"}},
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
            {77, new[] {"тп118", "тп-118"}},
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
            {97, new[] {"тп-759", "тп-759"}},
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
            {123, new[] {"подъезд№1(подвал)"}},
            {124, new[] {"тп-191"}},
            {125, new[] {"вруавтостоянка"}},
        };

        public static string LsKvcWithoutKr(string lsKvc)
        {
            if (lsKvc.Length != 19) return lsKvc;
            return lsKvc.Substring(0, 16);
        }

        public static long FindLsRecode(string lsKvc, Dictionary<string, long> lsRecode)
        {
            long recodedLs;
            lsRecode.TryGetValue(LsKvcWithoutKr(lsKvc.Trim()), out recodedLs);
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
                    try
                    {
                        dic.Add(line.Split(';')[0], Convert.ToInt64(line.Split(';')[1]));

                    }
                    catch (Exception e)
                    {
                        throw;
                    }
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

    public class FindNotExistedAbonentInNach : DbfConvertCase
    {
        public FindNotExistedAbonentInNach()
        {
            ConvertCaseName = "Поиск несоответствий";
            Position = 11;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            // Абоненты не с ЛС КВЦ
            /*{
                var notLsKvc = new List<string>();
                DbfManager.ExecuteQueryByReader("select * from abonent where lshet like '___-___-__-___-_-__'", r =>
                {
                    if (!lsKvcRegex.IsMatch(r.GetString(0))) notLsKvc.Add(r.GetString(0));
                });
                var result = String.Join(Environment.NewLine, notLsKvc);
            }*/

            // Абоненты с одинаковым ЛС КВЦ, но с разным КР
            /*{
                var ls = new List<string>();
                DbfManager.ExecuteQueryByReader("select lshet from abonent where lshet like '___-___-__-___-_-__'", r =>
                {
                    ls.Add(r.GetString(0));
                });
                var doubledKr = ls.Select(s => s.Substring(0, 16)).GroupBy(s => s).Where(gs => gs.Count() > 1).ToArray();
                var result = String.Join(Environment.NewLine, doubledKr.Select(ds => ds.Key));
            }*/

            // Округление.
            /*{
                var volumes = new List<decimal>();
                var sums = new List<decimal>();
                var nachFile = ConvertNach.GetNachFiles().First(n => ConvertNach.GetNachFileDate(n) == new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1));
                aConverterClassLibrary.Utils.ReadExcelFileByRow(nachFile, null, dr =>
                {
                    var nachInfo = new NachExcelRecord(dr);
                    string[] split = nachInfo.Volume.ToString().Split(',');
                    if (split.Length > 1 && split[1].Length > 2) volumes.Add(nachInfo.Volume);
                    split = nachInfo.Sum.ToString().Split(',');
                    if (split.Length > 1 && split[1].Length > 2) sums.Add(nachInfo.Sum);
                });

                var fullVolume = volumes.Sum();
                var fullSum = sums.Sum();

                var roundedVolume = volumes.Select(v => Math.Round(v, 2)).Sum();
                var roundedSum = sums.Select(s => Math.Round(s, 2)).Sum();

                var diffVolume = fullVolume - roundedVolume;
                var diffSum = fullSum - roundedSum;
            }*/

            // Вычет сальдо по актам
            /*{
                var actSaldoDic = new Dictionary<string, decimal>();

                decimal sum = 0;

                using (var dt = aConverterClassLibrary.Utils.ReadExcelFile(ActSaldoFileName, null))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var actSaldoRec = new ExtSaldo(dr, ExtSaldo.ExternalType.Acts);
                        actSaldoDic.Add(LsKvcWithoutKr(actSaldoRec.LsKvc), Math.Round(actSaldoRec.Saldo, 2));
                    }
                }

                using (var dt = Tmsource.ExecuteQuery("select * from lschars where charcd = 'd673ef5d-90b1-11df-ae5f-001e8c71f1cc' and lshet like '___-___-__-___-_-__'"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var charRecord = new CommonAddCharRecord(dr);
                        string lsKvc = LsKvcWithoutKr(charRecord.Owner);
                        decimal saldo = Math.Round(Convert.ToDecimal(charRecord.Value), 2);
                        if (actSaldoDic.ContainsKey(lsKvc))
                            sum += saldo - actSaldoDic[lsKvc];
                        else
                            sum += saldo;
                    }
                }
            }*/

            // Разница улицы и КВЦ
            /*{
                var abonents = new List<AbonentRecord>();
                DbfManager.ExecuteQueryByRow("select * from abonent", dr =>
                {
                    var ar = new AbonentRecord();
                    ar.ReadDataRow(dr);
                    if (!lsKvcRegex.Match(ar.Lshet).Success) return;
                    var lsKvc = new LsKvc(ar.Lshet, true);
                    int ulicakod;
                    if (!int.TryParse(ar.Ulicakod, out ulicakod)) return;
                    if (lsKvc.StreetId != ulicakod)
                        abonents.Add(ar);
                });
                var diff = abonents.Select(a => new {new LsKvc(a.Lshet, true).StreetId, a.Ulicakod, a.Ulicaname}).Distinct().ToArray();
            }*/

            // Поиск абонентов в файле с начислениями, которые отсутствуют в БД.
            { 
                var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
                var nachFile = ConvertNach.GetNachFiles().First(n => ConvertNach.GetNachFileDate(n) == new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1));
                var existedNach = new List<NachExcelRecord>();
                var notExistedNach = new List<NachExcelRecord>();
                var addNach = new List<NachExcelRecord>();
                aConverterClassLibrary.Utils.ReadExcelFileByRow(nachFile, null, dr =>
                {
                    var nachInfo = new NachExcelRecord(dr);
                    if (lsrecode.ContainsKey(LsKvcWithoutKr(nachInfo.LsKvc)))
                    {
                        if (nachInfo.Nach == NachExcelRecord.NachType.AddNach)
                            addNach.Add(nachInfo);
                        existedNach.Add(nachInfo);
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
                var existedSum = existedNach.Sum(n => n.Sum);
                var eistedVol = existedNach.Sum(n => n.Volume);
                var nachSumWithoutVol = existedNach.Where(n => n.Volume == 0).Sum(n => n.Sum);
                var nachVolWithoutSum = existedNach.Where(n => n.Sum == 0).Sum(n => n.Volume);


                var fileLsList = existedNach.Where(n => Math.Abs(n.Sum) < 0).Select(n => n.LsKvc).Distinct().Select(l => new LsKvc(l, true).CombinedLs).ToArray();
                var dbLsList = new HashSet<string>();;
                FbManager fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
                using (var dt = fb.ExecuteQuery("select distinct ea.EXTLSHET " +
                                                "from NACHISLVOLUMS nv " +
                                                "inner join EXTORGACCOUNTS ea on ea.EXTORGCD = 2 and ea.LSHET = nv.LSHET " +
                                                "where nv.NYEAR = 2018 and nv.NMONTH = 02"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dbLsList.Add(dt.Rows[i][0].ToString());
                    }
                }
                var notExistedLs = fileLsList.Where(fl => !dbLsList.Contains(fl)).ToArray();
            }
        }
    }

    public class ConvertBadInd : DbfConvertCase
    {
        public ConvertBadInd()
        {
            ConvertCaseName = "Доконвертация непересенных показаний";
            Position = 12;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DateTime convertDate = new DateTime(2018, 03, 31);
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var dbCounters = new Dictionary<string, List<CNV_CNTRSIND>>();
            var fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            using (var dt = fb.ExecuteQuery(String.Format(SelectCountersSql, DateToSql(convertDate))))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var dbCounter= new CNV_CNTRSIND
                    {
                        DOCUMENTCD = dr["LSHET"].ToString(),
                        COUNTERID = dr["KOD"].ToString(),
                        OLDIND = Convert.ToDecimal(dr["lastind"]),
                        INDDATE = string.IsNullOrWhiteSpace(dr["INDICATIONDATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["INDICATIONDATE"])
                    };
                    if (dbCounters.ContainsKey(dbCounter.DOCUMENTCD))
                        dbCounters[dbCounter.DOCUMENTCD].Add(dbCounter);
                    else
                        dbCounters.Add(dbCounter.DOCUMENTCD, new List<CNV_CNTRSIND> { dbCounter });
                }
            }
            var diffCounters = new List<CNV_CNTRSIND>();
            DbfManager.ExecuteQueryByRow(String.Format(SelectCharsSql, convertDate.Year, convertDate.Month, convertDate.Day), dr =>
            {
                var dbfInd = new CommonAddCharRecord(dr);
                var lshetLong = FindLsRecode(dbfInd.Owner, lsrecode);
                if (lshetLong == 0) return;
                string lshet = lshetLong.ToString();
                List<CNV_CNTRSIND> lsCounters;
                if (!dbCounters.TryGetValue(lshet, out lsCounters)) return;
                var kvcInd = decimal.Parse(dbfInd.Value) % 10000000;
                decimal diff = Decimal.MaxValue;
                CNV_CNTRSIND dbCounter = null;
                foreach (var counter in lsCounters)
                {
                    //if (counter.INDDATE > dbfInd.Date) continue;
                    var curDiff = Math.Abs(kvcInd - counter.OLDIND.Value);
                    if (curDiff < diff)
                    {
                        dbCounter = counter;
                        diff = curDiff;
                    }
                }
                if (dbCounter == null /*|| dbCounter.OLDIND == kvcInd*/) return;
                dbCounter.INDICATION = kvcInd;
                dbCounter.OB_EM = dbCounter.INDICATION - dbCounter.OLDIND;
                //dbCounter.INDDATE = dbfInd.Date;
                dbCounter.INDDATE = convertDate;
                diffCounters.Add(dbCounter);
            });

            BufferEntitiesManager.SaveDataToBufferIBScript(diffCounters);
        }

        private class DbCounter
        {
            public string Lshet;
            public int CounterId;
            public decimal LastInd;
            public DateTime? IndDate;
            public decimal NewInd;

            public DbCounter(DataRow dr)
            {
                Lshet = dr["LSHET"].ToString();
                CounterId = Convert.ToInt32(dr["KOD"]);
                LastInd = Convert.ToDecimal(dr["lastind"]);
                IndDate = string.IsNullOrWhiteSpace(dr["INDICATIONDATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["INDICATIONDATE"]);
            }
        }

        private string DateToSql(DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }

        private const string SelectCountersSql =
@"select i.*, coalesce(ci.INDICATIONVALUE, 0) as lastind, ci.INDICATIONDATE
from (
    select ae.LSHET, rc.KOD, (
            select first 1 ci.COUNTERINDICATIONFACTID
            from COUNTERINDICATION ci
            where ci.KOD = rc.KOD and ci.INDICATIONDATE <= '{0}'
            order by ci.INDICATIONDATE DESC, ci.COUNTERINDICATIONFACTID desc
        ) as lastindid
    from RESOURCECOUNTERS rc
    inner join ABONENTSEQUIPMENT ae on ae.EQUIPMENTID = rc.KOD
    inner join ABONENTADDITIONALCHARS ad on ad.LSHET = ae.LSHET and ad.ADDITIONALCHARCD = 1620134
    where rc.COUNTER_LEVEL = 0
        and 0 < (
            select first 1 es.STATUSCD
            from EQSTATUSES es
            where es.EQUIPMENTID = rc.KOD and es.STATUSDATE <= '{0}'
            order by es.STATUSDATE desc
        )
) i
left join COUNTERINDICATION ci on ci.COUNTERINDICATIONFACTID = i.lastindid";

        private const string SelectCharsSql =
            @"select * from lschars where charcd = 'd673ef5c-90b1-11df-ae5f-001e8c71f1cc'";
	/*and (year(date) <> {0}
	or month(date) <> {1}
	or day(date) <> {2})";*/
    }

    public class ConvertDoubledCounterStatus : DbfConvertCase
    {
        public ConvertDoubledCounterStatus()
        {
            ConvertCaseName = "Конвертация задвоенных статусов счетчиков";
            Position = 13;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            var counterRecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            FbManager fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            var dbCounters = new Dictionary<long, List<DbCounter>>();
            fb.ExecuteQueryByRow(
@"select pe.IMPORTTAG, pe.EQUIPMENTID, ae.LSHET, coalesce(rc.TARGETBALANCE_KOD, 9) as service
from PARENTEQUIPMENT pe
inner join RESOURCECOUNTERS rc on rc.KOD = pe.EQUIPMENTID
inner join ABONENTSEQUIPMENT ae on ae.EQUIPMENTID = pe.EQUIPMENTID
where IMPORTTAG is not null", dr =>
            {
                var dbCounter = new DbCounter(dr);
                if (dbCounters.ContainsKey(dbCounter.ImportTag))
                    dbCounters[dbCounter.ImportTag].Add(dbCounter);
                else
                    dbCounters.Add(dbCounter.ImportTag, new List<DbCounter> {dbCounter});
            });
            var lshetsChars = new List<DbCounter>();
            var counterStatuses = new List<DbCounter>();
            foreach (var dobuledCounter in DobuledCounters)
            {
                long importTag;
                if (!counterRecode.TryGetValue(dobuledCounter.CounterId, out importTag))
                {
                    Task.Factory.StartNew(() =>
                    {
                        //MessageBox.Show($"Не найдена перекодировка счетчика {dobuledCounter.CounterId}");
                    });
                    continue;
                }
                List<DbCounter> counters;
                if (!dbCounters.TryGetValue(importTag, out counters))
                {
                    Task.Factory.StartNew(() =>
                    {
                        //MessageBox.Show($"Не найдена перекодировка импорт тега {importTag}");
                    });
                    continue;
                }
                counters.ForEach(c => c.Date = dobuledCounter.Date);
                lshetsChars.AddRange(counters);
                counterStatuses.Add(counters.First());
            }

            var lshetList = new List<DbCounter>();
            foreach (var lshet in lshetsChars)
            {
                int value = (int)fb.ExecuteScalar(
$@"select coalesce(SIGNIFICANCE, 0)
from (
    select first 1 la.SIGNIFICANCE
    from LCHARSABONENTLIST la
    where la.LSHET = '{lshet.Lshet}'
        and la.KODLCHARSLIST = {lshet.LcharId}
        and la.ABONENTLCHARDATE <= '{lshet.Date.ToString("dd.MM.yyyy")}'
    order by la.ABONENTLCHARDATE desc
)");
                if (value == 0) lshetList.Add(lshet);
            }

            var sql = new StringBuilder();
            sql.AppendLine("execute procedure createdocument('Импорт задвоенных статусов счетчиков');");
            foreach (var counter in counterStatuses)
            {
                sql.AppendLine($"UPDATE OR INSERT INTO EQSTATUSES(EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD) VALUES({counter.CounterId}, '{counter.Date.ToString("dd.MM.yyyy")}', 1, gen_id(documents_gen, 0)) MATCHING(EQUIPMENTID, STATUSDATE);");
            }
            foreach (var lshet in lshetList)
            {
                sql.AppendLine($"UPDATE OR INSERT INTO LCHARSABONENTLIST(LSHET, KODLCHARSLIST, ABONENTLCHARDATE, DOCUMENTCD, SIGNIFICANCE) VALUES('{lshet.Lshet}', {lshet.LcharId}, '{lshet.Date.ToString("dd.MM.yyyy")}', gen_id(documents_gen, 0), 1) MATCHING(LSHET, KODLCHARSLIST, ABONENTLCHARDATE);");
            }

            Clipboard.SetText(sql.ToString());

            string lshets = String.Join("\r\n", lshetList.Select(s => s.Lshet).Distinct());
        }

        private class DbCounter
        {
            public long ImportTag;
            public string CounterId;
            public string Lshet;
            public bool GroupCounter;
            
            public DateTime Date;

            public int LcharId => GroupCounter ? 22 : 21;

            public DbCounter(DataRow dr)
            {
                ImportTag = Convert.ToInt64(dr["IMPORTTAG"].ToString());
                CounterId = dr["EQUIPMENTID"].ToString();
                Lshet = dr["LSHET"].ToString();
                GroupCounter = dr["service"].ToString() != "9";
            }
        }

        private class DobuledCounter
        {
            public DateTime Date;
            public string CounterId;

            public DobuledCounter(string date, string counterId)
            {
                Date = Convert.ToDateTime(date);
                CounterId = counterId;
            }
        }

        private static readonly DobuledCounter[] DobuledCounters = 
        {
            new DobuledCounter("2010-02-19", "c201f124-78e9-480f-a606-48557ed3ae0e"),
new DobuledCounter("2010-05-25", "ab7c2b85-f231-4acf-b621-2c877fc07051"),
new DobuledCounter("2010-06-03", "4f8e736a-4f0e-4518-a3ea-66590ec88934"),
new DobuledCounter("2010-06-22", "2650f43a-d46c-4405-ad11-49ea803b74c9"),
new DobuledCounter("2010-07-01", "183e145a-77f9-40ff-b32a-4f28ede22245"),
new DobuledCounter("2010-07-01", "9b2e77f9-bf19-43b7-87a6-7ccf41154a4b"),
new DobuledCounter("2010-07-01", "ec2d8239-81e4-4888-a8b7-4dc841c4332c"),
new DobuledCounter("2010-07-02", "61dad263-0bcb-4291-adc9-c198850ec056"),
new DobuledCounter("2010-07-02", "ce0f7cff-8ad4-471c-ba9a-43645489c7e1"),
new DobuledCounter("2010-07-03", "322fa913-daa9-4b9c-ba70-f0e86319f288"),
new DobuledCounter("2010-07-03", "323643a0-999d-4c4c-8417-bf2bca4b5c51"),
new DobuledCounter("2010-07-03", "3751adb2-f8f5-4936-8321-32b42211fa3f"),
new DobuledCounter("2010-07-03", "511d773b-dde9-476a-84b8-958483e842c2"),
new DobuledCounter("2010-07-03", "53e77c58-1151-4692-a665-df847d0078ad"),
new DobuledCounter("2010-07-03", "556d49db-3771-48d7-adc4-7462d3c0c849"),
new DobuledCounter("2010-07-03", "574b8229-0f85-4445-8527-e7643a8098cc"),
new DobuledCounter("2010-07-03", "67c2bed9-17c1-40d5-bfba-abc341696c78"),
new DobuledCounter("2010-07-03", "8194b622-1429-48e8-945c-a862ec541ec0"),
new DobuledCounter("2010-07-03", "88c13dca-f306-4b57-b27f-3830d416c0f2"),
new DobuledCounter("2010-07-03", "8a678d70-adfe-4e12-bfed-9a43a4489ff7"),
new DobuledCounter("2010-07-03", "a7e21a61-d66d-48d7-8421-50b34ea43719"),
new DobuledCounter("2010-07-03", "c93a5dca-b42f-49b0-9e00-0e0eaddf2613"),
new DobuledCounter("2010-07-03", "cdb45b21-35f2-4f9a-beef-c18530050fd2"),
new DobuledCounter("2010-07-03", "d071cd0b-4bd1-4680-9ab7-d4e571b0b829"),
new DobuledCounter("2010-07-03", "f57f72b9-c424-4b83-8355-e815769a1281"),
new DobuledCounter("2010-07-05", "02e0e234-90f7-4803-b5bb-18ad05882025"),
new DobuledCounter("2010-07-05", "0a593ac5-8b72-47a0-ad7a-ee9e82e22718"),
new DobuledCounter("2010-07-05", "0b4fd1b3-153a-4e75-b511-ba5b91d4c9e6"),
new DobuledCounter("2010-07-05", "210e27e0-75ac-484f-ba2a-f196c00984cd"),
new DobuledCounter("2010-07-05", "21d45e9e-d7cc-4d7e-abd3-498673a3fde6"),
new DobuledCounter("2010-07-05", "3af96dbc-1108-49ba-94ff-41593ac522fd"),
new DobuledCounter("2010-07-05", "40f8919f-92b4-4e1d-8761-d71616d5c8cd"),
new DobuledCounter("2010-07-05", "451f2eb9-e689-4102-926b-eb86f8ffc23b"),
new DobuledCounter("2010-07-05", "4c181fe1-d04c-4fec-8fbf-b365809aa69a"),
new DobuledCounter("2010-07-05", "62a7422d-5288-4fbb-acf9-8c4ada584706"),
new DobuledCounter("2010-07-05", "6fe8c403-4065-4ddd-80a6-b8e8afebfe91"),
new DobuledCounter("2010-07-05", "73fe24d1-bd54-476e-8037-e46061d534c5"),
new DobuledCounter("2010-07-05", "7c05cdec-84fe-4da1-8f1f-6cfef204ebe1"),
new DobuledCounter("2010-07-05", "7d2ed6ac-1fff-42fc-9369-1ba57e9c2933"),
new DobuledCounter("2010-07-05", "7ef9a6b0-41b3-4ee2-81da-35908fe3de4b"),
new DobuledCounter("2010-07-05", "884d1f09-d0d3-4a76-8cc0-bc90e797b1fa"),
new DobuledCounter("2010-07-05", "90bc9811-ad9e-4d02-86b7-25dfd21eea41"),
new DobuledCounter("2010-07-05", "97413916-9830-4994-b6d9-ec73c66021a4"),
new DobuledCounter("2010-07-05", "b53cea87-a374-452b-8f5b-be5d244606c4"),
new DobuledCounter("2010-07-05", "b67dd1bb-4a61-4de8-85c7-69aebd87e34e"),
new DobuledCounter("2010-07-05", "b843a364-8e9f-477f-a0d4-81c28038acb8"),
new DobuledCounter("2010-07-05", "bffd5f11-7875-4317-b850-d8fb79bc1301"),
new DobuledCounter("2010-07-05", "c530604f-601f-4cd7-9b27-299b190f1465"),
new DobuledCounter("2010-07-05", "c58b57de-a798-4779-b604-0c5afd660ca0"),
new DobuledCounter("2010-07-05", "ca529cd4-c6ee-443b-87e6-bdf9d4c6a47e"),
new DobuledCounter("2010-07-05", "daa78397-1441-4bed-8665-2c5d3d727e36"),
new DobuledCounter("2010-07-05", "e4bf99b1-0bae-4da1-93dc-dd6155c9f541"),
new DobuledCounter("2010-07-05", "ebbe06ad-d04b-47f3-a6de-15e4719ff477"),
new DobuledCounter("2010-07-05", "ec72b9ea-f3d3-42ab-91ef-d84bed4722a7"),
new DobuledCounter("2010-07-05", "ed782c86-f31d-4608-97d1-b7ab1801e881"),
new DobuledCounter("2010-07-05", "f04a5803-6792-41ac-a9d2-ea5672c0ea07"),
new DobuledCounter("2010-07-06", "0d8dac25-a013-417c-b7e1-163043465206"),
new DobuledCounter("2010-07-06", "109e7f82-bd2d-4d6c-be22-ebd2df1e7150"),
new DobuledCounter("2010-07-06", "13c2c00c-421c-46a3-98bd-10d5056117df"),
new DobuledCounter("2010-07-06", "218123d5-1cfe-408a-b4d2-55468d92ed00"),
new DobuledCounter("2010-07-06", "24c054c5-ffd6-4838-8aee-a59c634317f5"),
new DobuledCounter("2010-07-06", "2abf7334-11ac-4ea4-9d55-42d7adfe835c"),
new DobuledCounter("2010-07-06", "2bc66418-660d-4245-b414-eef5649b09c4"),
new DobuledCounter("2010-07-06", "355d0d0a-6556-4758-bcbe-6b4b9f1a1a31"),
new DobuledCounter("2010-07-06", "3cb44516-fab7-4692-b099-bd6d7190d252"),
new DobuledCounter("2010-07-06", "8b604918-c217-4292-9b81-3ef8077df364"),
new DobuledCounter("2010-07-06", "8bcadb8f-254c-4b34-b579-2b27a5038dda"),
new DobuledCounter("2010-07-06", "8c8ded95-9eb3-44ac-ae86-f6eb9559c4e1"),
new DobuledCounter("2010-07-06", "9f50df07-ccec-4b10-b87b-8730ae408f94"),
new DobuledCounter("2010-07-06", "a0be0b03-b67a-422e-8915-01e326976d6e"),
new DobuledCounter("2010-07-06", "a22dd566-af41-4a89-a6c5-f4971fc6b992"),
new DobuledCounter("2010-07-06", "a2989c42-f745-422c-9a47-284dd087f829"),
new DobuledCounter("2010-07-06", "a7e16279-598c-4496-9ea1-14ed92057c1a"),
new DobuledCounter("2010-07-06", "b1a14438-f850-42b9-b924-4c565ca2f739"),
new DobuledCounter("2010-07-06", "b5dca9b6-a5f9-4b24-8e82-5fb36606a69f"),
new DobuledCounter("2010-07-06", "bcdb4e8b-f1d9-4aec-bd65-160f1cca8fa7"),
new DobuledCounter("2010-07-06", "d72dedd6-788d-466f-b6d7-d4a88fa8f6d7"),
new DobuledCounter("2010-07-06", "e1480b45-badd-4415-adcb-8333afe542bc"),
new DobuledCounter("2010-07-06", "e32c1ab6-d077-4a5e-a91b-d7f679482a65"),
new DobuledCounter("2010-07-06", "e8f15ddb-954c-49d6-9329-77c89d630dd9"),
new DobuledCounter("2010-07-06", "f8d59de4-d6eb-4c1d-abc4-5ffc2f73cecd"),
new DobuledCounter("2010-07-06", "fd0f1678-d778-4738-acc8-bc14d07fc502"),
new DobuledCounter("2010-07-07", "128932a1-a947-4a83-ba80-2768c48686f4"),
new DobuledCounter("2010-07-07", "2d6b49f8-21eb-4959-a5a5-966bf08b1183"),
new DobuledCounter("2010-07-07", "34316a5f-e9e3-43c8-a748-e0a90a869bba"),
new DobuledCounter("2010-07-07", "6e9cfb3b-84ad-42b5-ab3d-fec82c8d944d"),
new DobuledCounter("2010-07-07", "cbbcc1b5-04c5-4aef-9b01-1ce345d9efe1"),
new DobuledCounter("2010-07-07", "cc4d22cc-1bed-43ec-bddc-d85b0d1a0c38"),
new DobuledCounter("2010-07-07", "dd520736-a81f-4770-80d4-2cd807e07b69"),
new DobuledCounter("2010-07-08", "0f574bdc-e169-41e3-8cbc-4c80d60202a4"),
new DobuledCounter("2010-07-08", "3f5b12a5-89be-4fa9-a7d1-924a2764dafe"),
new DobuledCounter("2010-07-08", "74cb88db-8fae-4c4b-a7b8-a755659a23a7"),
new DobuledCounter("2010-07-08", "8499d7cb-cf42-444a-8589-4f5ac6c7991c"),
new DobuledCounter("2010-07-09", "464e3a6c-f078-41f6-b9df-3906f1f4ca2f"),
new DobuledCounter("2010-07-12", "1afb7b12-c1aa-4e88-932a-f8a6071e3b4b"),
new DobuledCounter("2010-07-19", "0f2fc940-694b-466f-82fd-81da5078597c"),
new DobuledCounter("2010-07-19", "264a5309-b831-4301-9252-203fd42201cf"),
new DobuledCounter("2010-07-19", "856fcbf9-50c9-45b2-bfba-e8958ea65447"),
new DobuledCounter("2010-07-19", "d9584d53-364e-47e4-a1a5-23bcab7e6473"),
new DobuledCounter("2010-07-19", "fb06e700-44d8-463a-8d97-1723618b426f"),
new DobuledCounter("2010-07-19", "fd9c5efc-8da0-4300-92b7-8cd83dfd07ea"),
new DobuledCounter("2010-07-20", "0747c4e7-e978-4808-89d6-38c8924a0c9d"),
new DobuledCounter("2010-07-20", "75decedc-e516-4e1a-9ed3-54890747ba23"),
new DobuledCounter("2010-07-26", "a3d67400-9a60-42b7-b858-051f5e19f53a"),
new DobuledCounter("2010-07-28", "eb77cba1-3f18-42fb-a7ef-f16854b8f2e3"),
new DobuledCounter("2010-08-19", "63c95c64-fa8e-4176-934b-a9d84abeb32f"),
new DobuledCounter("2010-08-19", "ddd20e98-602f-4892-8288-e79da27694cc"),
new DobuledCounter("2010-08-20", "1613a523-75a6-4a0e-bbcb-928d3cdc98a1"),
new DobuledCounter("2010-08-20", "1be32300-7085-4f33-9062-e30e66251290"),
new DobuledCounter("2010-08-20", "33f0e26f-6e28-4abf-8b35-bbebed6406d6"),
new DobuledCounter("2010-08-20", "526860c0-6a17-4681-85b9-18cf31e87eee"),
new DobuledCounter("2010-08-20", "566c0cb2-ff05-4f8a-812c-9a1704908e3f"),
new DobuledCounter("2010-08-20", "5c09d8e4-7fd2-4b18-8c40-6d3c8256c975"),
new DobuledCounter("2010-08-20", "5f5cfe0d-da68-40a8-8393-630c0aadc3e6"),
new DobuledCounter("2010-08-20", "5fb606f0-32e5-4d28-af5f-ac19e4d1bb4d"),
new DobuledCounter("2010-08-20", "70601f69-1dad-4ae8-90d3-ac5d6065be7d"),
new DobuledCounter("2010-08-20", "97981296-652f-4133-97cf-6061e7a0f9bf"),
new DobuledCounter("2010-08-20", "ac1e7243-2c0c-44dd-a678-5b5bee5f057b"),
new DobuledCounter("2010-08-20", "b8751801-547b-49e5-b53a-4ab944632087"),
new DobuledCounter("2010-08-20", "bb68e8b0-780e-4d4e-9761-be06fe644ee3"),
new DobuledCounter("2010-08-20", "bf006511-eb94-44e5-9d4a-06b296109f26"),
new DobuledCounter("2010-08-20", "c5969cbe-949d-4d8a-a9f5-1184ba9e5a75"),
new DobuledCounter("2010-08-20", "c9206c81-415d-4d05-a12e-4b41dbc18d3d"),
new DobuledCounter("2010-08-20", "d30db71b-7832-43aa-8fe6-8512f0887342"),
new DobuledCounter("2010-08-20", "d4b0d75c-13e4-46c4-beeb-02cfc25fb5f0"),
new DobuledCounter("2010-08-20", "d71aec9a-1c52-40f0-86c8-f4d295218081"),
new DobuledCounter("2010-08-20", "da2a616d-431d-49f5-af67-e2954e1372a9"),
new DobuledCounter("2010-08-20", "dd11a9d5-d7ce-47c5-9b14-0e88a9498491"),
new DobuledCounter("2010-08-24", "45286661-a733-4711-a8c6-f250b7d6b47c"),
new DobuledCounter("2010-09-08", "34a65a9d-a6b7-4c98-8a63-c159568437bd"),
new DobuledCounter("2010-09-08", "abc3684c-3830-4f11-b790-6f29a7e5c131"),
new DobuledCounter("2010-09-14", "de3d2d8f-4586-472f-a08a-f7d9716a2fed"),
new DobuledCounter("2010-09-17", "56ddf9ac-e53b-4c97-8478-0c0cfa1070a4"),
new DobuledCounter("2010-10-04", "fbcb8de2-a079-11df-b9eb-001e8c71f1cd"),
new DobuledCounter("2010-10-06", "44ffea79-70cf-49ee-a088-76d02a287bd6"),
new DobuledCounter("2010-11-03", "6129ea3a-99bb-48fd-8494-e017a0eab61b"),
new DobuledCounter("2010-11-18", "5c2bfbd7-44bb-434c-9949-7bd6fe8de908"),
new DobuledCounter("2010-12-02", "da28b1e0-7a33-4f6d-a7c1-3d0b15e6a7a3"),
new DobuledCounter("2010-12-16", "7e05c4e1-605c-47ed-ab56-8c4571b09377"),
new DobuledCounter("2011-01-20", "1de07161-23c6-11e0-848a-e0cb4e5a973c"),
new DobuledCounter("2011-02-16", "b27db495-5562-11e0-b7c0-001e8c71f1cd"),
new DobuledCounter("2011-02-22", "51f18797-4bbb-4590-a8ac-1777ece4267d"),
new DobuledCounter("2011-02-22", "610c2e91-5560-11e0-b7c0-001e8c71f1cd"),
new DobuledCounter("2011-03-12", "fc2c5206-2c83-41e4-97e8-4e849cb058ca"),
new DobuledCounter("2011-03-23", "d4477d00-fd8a-4c45-8290-6efcbb199f76"),
new DobuledCounter("2011-04-21", "02102265-aab1-49b9-a2e7-788205240fa5"),
new DobuledCounter("2011-05-12", "1f05780b-e210-47ce-9f4f-f67a17ee99ed"),
new DobuledCounter("2011-06-10", "d2edb857-b697-11e0-829c-001e8c71f1cd"),
new DobuledCounter("2011-07-12", "332c959b-f792-11df-b495-e0cb4e5a973c"),
new DobuledCounter("2011-07-23", "5c1539cd-218d-4e22-88f9-081543406245"),
new DobuledCounter("2012-08-01", "1898241f-d4c5-4071-adb8-d3ee4d5fc341"),
new DobuledCounter("2013-10-01", "5bd4c8e9-ada8-11e2-8fe7-001e8c71f1cd"),
new DobuledCounter("2013-10-01", "5bd4c8ef-ada8-11e2-8fe7-001e8c71f1cd"),
new DobuledCounter("2014-04-01", "0b63e19d-8342-11e3-bdb9-001e8c71f1cd"),
new DobuledCounter("2014-04-01", "b101f5fd-b58b-11e3-9bb1-001e8c71f1cd"),
new DobuledCounter("2014-04-01", "b101f603-b58b-11e3-9bb1-001e8c71f1cd"),
new DobuledCounter("2014-05-01", "72d2008c-8831-11e3-9db8-001e8c71f1cd"),
new DobuledCounter("2014-05-01", "e2b434fa-8321-11e3-bdb9-001e8c71f1cd"),
new DobuledCounter("2015-03-01", "32667f35-889f-11e3-9db8-001e8c71f1cd"),
new DobuledCounter("2015-11-01", "cc746ac3-2d26-11e1-9579-001e8c71f1cd"),
new DobuledCounter("2016-01-25", "d6b928ca-947d-492c-a460-65f37ec98ee7"),
new DobuledCounter("2016-02-01", "0cee06d6-2d1c-11e4-ac3f-001e8c71f1cc"),
new DobuledCounter("2016-02-01", "1fc80308-133d-11e5-bf81-001e8c71f1cc"),
new DobuledCounter("2016-02-01", "759f7dba-c50f-11e5-8076-002590c76e1b"),
new DobuledCounter("2016-02-01", "fd46468a-026e-11e4-a519-001e8c71f1cd"),
new DobuledCounter("2016-03-21", "17c6191d-eb93-47a3-8ef6-bbdfbb3e4279"),
new DobuledCounter("2016-04-21", "db79b842-a5a1-11e1-80a1-001e8c71f1cd"),
new DobuledCounter("2016-06-01", "2764787c-3f21-11e4-903c-001e8c71f1cc"),
new DobuledCounter("2016-08-01", "0a66e8ec-ad3d-11e5-b7f4-002590c76e1b"),
new DobuledCounter("2016-08-01", "0a66e8f2-ad3d-11e5-b7f4-002590c76e1b"),
new DobuledCounter("2016-08-02", "b941800b-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2016-08-02", "b9418011-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2016-08-02", "b9418017-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2016-08-02", "b941801d-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2016-08-02", "b9418023-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2016-08-02", "b9418029-6ce4-11e6-ba53-002590c76e1b"),
new DobuledCounter("2017-06-15", "947e71b5-26c0-11e5-bf81-001e8c71f1cc"),
        };
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
        Regex korpusRegex = new Regex(@"^(?<house>\d+)[Кк](?<korpus>\d+)$");
        Regex fractionRegex = new Regex(@"^(?<house>\d+)(?<postfix>\/\d+)$");
        Regex literaRegex = new Regex(@"^(?<house>\d+)\/(?<litera>[A-Za-zа-яА-Я])$");
        Regex firstNubmerRegex = new Regex(@"^(?<house>\d+)(?<postfix>[^\d].*)$");

        public override void DoDbfConvert()
        {
            SetStepsCount(3);
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            BufferEntitiesManager.DropTableData("CNV$ABONENTPHONES");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var la = new List<CNV_ABONENT>();
            var lap = new List<CNV_ABONENTPHONES>();
            var lha = new List<CNV_HADDCHAR>();
            var laa = new List<CNV_AADDCHAR>();
            //var lhcc = new List<CNV_CHARSHOUSES>();

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            long lastls = lsrecode.Max(l => l.Value);
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
            var existedLs = new HashSet<string>();
            //var housesWithFlatNumbers = new HashSet<string>();
            //DbfManager.ExecuteQueryByReader(
            //    @"select distinct hs.housecd
            //    from hschars hs
            //    where hs.charcd = 'fc7c4f38-aee3-48d8-bb79-98bb1ac2462f'",
            //    r =>
            //    {
            //        housesWithFlatNumbers.Add(r.GetString(0).Trim());
            //    });
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

            var abonentPhones = new Dictionary<string, List<AbnphoneRecord>>();
            DbfManager.ExecuteQueryByRow("select * from abnphone", dr =>
            {
                var phone = new AbnphoneRecord();
                phone.ReadDataRow(dr);
                if (abonentPhones.ContainsKey(phone.Lshet))
                    abonentPhones[phone.Lshet].Add(phone);
                else
                    abonentPhones.Add(phone.Lshet, new List<AbnphoneRecord> {phone});
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
                    //PHONENUM = abonent.Phonenum.Trim().Replace(",", ";")
                };

                List<AbnphoneRecord> phones;
                if (abonentPhones.TryGetValue(abonent.Lshet, out phones))
                {
                    foreach (var phone in phones)
                    {
                        int phoneType;
                        switch (phone.Type.Trim().ToLower())
                        {
                            case "мобильный":
                                phoneType = 2;
                                break;
                            case "стационарный":
                                phoneType = 1;
                                break;
                            default:
                                phoneType = 0;
                                break;
                        }
                        if (phone.Isdeleted == 1) phoneType = -1;
                        string phonenumber = phone.Nomer.Trim();
                        //if (phonenumber.Length == 10 && phonenumber[0] == '9')
                        //    phonenumber = "8" + phonenumber;
                        lap.Add(new CNV_ABONENTPHONES
                        {
                            LSHET = a.LSHET,
                            PHONENUMBER = phonenumber,
                            TYPEID = phoneType
                        });
                    }
                }

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
                //a.HOUSENO = abonent.Ndoma.Trim();

                //int korpusno;
                //if (Int32.TryParse(abonent.Korpus, out korpusno))
                //{
                //    a.KORPUSNO = korpusno;
                //}
                //else if (!String.IsNullOrEmpty(abonent.Korpustip.Trim()))
                //{
                //    a.HOUSEPOSTFIX = abonent.Korpustip.Substring(0, 3).Trim() + " " + abonent.Korpus.Trim();
                //}
                //else
                //{
                //    a.HOUSEPOSTFIX = abonent.Korpus.Trim();
                //}

                abonent.Ndoma = abonent.Ndoma.Trim();
                if (String.IsNullOrWhiteSpace(abonent.Korpus))
                {
                    a.HOUSENO = abonent.Ndoma;
                    a.HOUSEPOSTFIX = null;
                    a.KORPUSNO = null;
                }
                else
                {
                    abonent.Ndoma_dop = abonent.Ndoma_dop.Trim();
                    var match = korpusRegex.Match(abonent.Ndoma_dop);
                    if (match.Success)
                    {
                        a.HOUSENO = match.Groups["house"].Value;
                        a.HOUSEPOSTFIX = null;
                        a.KORPUSNO = int.Parse(match.Groups["korpus"].Value);
                    }
                    else
                    {
                        match = fractionRegex.Match(abonent.Ndoma_dop);
                        if (match.Success)
                        {
                            a.HOUSENO = match.Groups["house"].Value;
                            a.HOUSEPOSTFIX = match.Groups["postfix"].Value;
                            a.KORPUSNO = null;
                        }
                        else
                        {
                            match = literaRegex.Match(abonent.Ndoma_dop);
                            if (match.Success)
                            {
                                a.HOUSENO = match.Groups["house"].Value;
                                a.HOUSEPOSTFIX = match.Groups["litera"].Value;
                                a.KORPUSNO = null;
                            }
                            else
                            {
                                match = firstNubmerRegex.Match(abonent.Ndoma_dop);
                                if (match.Success)
                                {
                                    a.HOUSENO = match.Groups["house"].Value;
                                    a.HOUSEPOSTFIX = match.Groups["postfix"].Value;
                                    a.KORPUSNO = null;
                                }
                                else
                                {
                                    int houseno;
                                    if (int.TryParse(abonent.Ndoma_dop, out houseno))
                                    {
                                        a.HOUSENO = houseno.ToString();
                                        a.HOUSEPOSTFIX = null;
                                        a.KORPUSNO = null;
                                    }
                                    else
                                    {
                                        a.HOUSENO = null;
                                        a.HOUSEPOSTFIX = abonent.Ndoma_dop;
                                        a.KORPUSNO = null;
                                    }
                                }
                            }
                        }
                    }
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
                existedLs.Add(a.LSHET);

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

                        //if (!housesWithFlatNumbers.Contains(abonent.Housecd))
                        //    lhcc.Add(new CNV_CHARSHOUSES
                        //    {
                        //        HOUSECD = (int)a.HOUSECD,
                        //        CHARCD = -4,
                        //        CHARNAME = "Многоквартирный",
                        //        VALUE_ = houseTypeId == 4 || houseTypeId == 5 ? 1 : 0,
                        //        DATE_ = MinConvertDate
                        //    });
                    }
                }

                Iterate();
            }
            StepFinish();

            File.WriteAllLines(LsRecodeFileName, lsrecode.Where(lr => existedLs.Contains(lr.Value.ToString())).Select(lr => $"{lr.Key};{lr.Value}"));
            Utils.SaveDictionary(durecode, DuRecodeFileName);
            Utils.SaveDictionary(houserecode, HouseRecodeFileName);
            File.WriteAllLines(PlaceToLshetRecodeFileName, placerecode.SelectMany(p => p.Value.Select(v => $"{p.Key};{v}")));
            File.WriteAllLines(HouseToLshetRecodeFileName, la.Select(a => $"{a.HOUSECD};{a.LSHET}"));
            File.WriteAllLines(HouseStreetNameRecodeFileName, la.Select(a => $"{a.HOUSECD};{a.ULICANAME} д.{a.HOUSENO}{a.HOUSEPOSTFIX}").Distinct());
            File.WriteAllLines(LshetAddressNameFileName, la.Select(l => $"{l.LSHET};{l.ULICANAME} д.{l.HOUSENO}{l.HOUSEPOSTFIX} кв.{l.FLATNO}{l.FLATPOSTFIX}"));

            StepStart(4);
            BufferEntitiesManager.SaveDataToBufferIBScript(la);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(laa);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lha);
            Iterate();
            //BufferEntitiesManager.SaveDataToBufferIBScript(lhcc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lap);
            StepFinish();

            var lsNotFromKvc = new List<string>();
            using (DataTable dtSaldo = Tmsource.GetDataTable(AbonentsNotFromKvcSaldoDbfTableName))
            {
                StepStart(dtSaldo.Rows.Count);
                foreach (DataRow dr in dtSaldo.Rows)
                {
                    var saldo = new ExtSaldo(dr, ExtSaldo.ExternalType.NotFromKvc);
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
            long orgCd = 5;
            var orgRecode = new Dictionary<string, long>
            {
                { "4c873282-aca2-45f6-9a2d-04d08b59ed38", 5 }, // ЦОКП.
                { "5050b2c7-64a6-11df-9e0f-001e8c71f1cc", 4 }, // РЭС.
            };
            var extLshets = new List<CNV_EXTLSHET>();
            StepStart(1);
            DbfManager.ExecuteQueryByRow("select * from abnlshet", dr =>
            {
                var extLshetRecord = new AbnlshetRecord();
                extLshetRecord.ReadDataRow(dr);

                long intExtLs;
                string extLshet = extLshetRecord.Lsh.Replace(" ", "").Replace(new string((char)160, 1), "");
                if (!long.TryParse(extLshet, out intExtLs))
                    return;

                var lshet = FindLsRecode(extLshetRecord.Lshet, lsrecode);

                if (lshet != 0)
                {
                    var ls = new CNV_EXTLSHET
                    {
                        EXTORGCD = (int) Utils.GetValue(extLshetRecord.Orgcd, orgRecode, ref orgCd),
                        LSHET = lshet.ToString(),
                        EXTLSHET = intExtLs.ToString(),
                        EXTORGNAME = extLshetRecord.Orgnm
                    };
                    if (ls.EXTORGCD == 4) ls.EXTLSHET = ls.EXTLSHET.PadLeft(8, '0');
                    extLshets.Add(ls);
                }
            });
            StepFinish();

            StepStart(1);
            extLshets = extLshets
                .GroupBy(l => new {l.LSHET, l.EXTORGCD})
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

    public class ConvertOwnerPartChars : ConvertCase
    {
        public ConvertOwnerPartChars()
        {
            ConvertCaseName = "OWNER PART CHARS - характеристики для расчета пропорционально долям собственности";
            Position = 32;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lc = new List<CNV_CHAR>();
            var ll = new List<CNV_LCHAR>();

            using (var dt = aConverterClassLibrary.Utils.ReadExcelFile(OwnerPartCharsFileName, null))
            {
                StepStart(dt.Rows.Count + 1);
                foreach (DataRow dr in dt.Rows)
                {
                    Iterate();
                    int rowNumber;
                    if (!int.TryParse(dr[0].ToString(), out rowNumber)) continue;
                    long lshet = FindLsRecode(dr[1].ToString(), lsrecode);
                    if (lshet == 0) continue;
                    if (!String.IsNullOrWhiteSpace(dr[4].ToString()))
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet.ToString(),
                            CHARCD = 27,
                            CHARNAME = "Индивидуальный норматив",
                            DATE_ = InitialDate,
                            VALUE_ = Convert.ToDecimal(dr[4])
                        });
                    if (!String.IsNullOrWhiteSpace(dr[5].ToString()))
                        ll.Add(new CNV_LCHAR
                        {
                            LSHET = lshet.ToString(),
                            LCHARCD = 30,
                            LCHARNAME = "Повышающий коэффициент",
                            DATE_ = InitialDate,
                            VALUE_ = dr[5].ToString().Trim().ToLower() == "применять" ? 0 : 1
                        });
                    if (!String.IsNullOrWhiteSpace(dr[6].ToString()))
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet.ToString(),
                            CHARCD = 25,
                            CHARNAME = "Доля собственности (для абонента)",
                            DATE_ = InitialDate,
                            VALUE_ = Convert.ToDecimal(dr[6])
                        });
                }
                StepFinish();
            }

            StepStart(2);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(ll);
            StepFinish();
        }
    }

    public class ConvertCoeffCounter : DbfConvertCase
    {
        public ConvertCoeffCounter()
        {
            ConvertCaseName = "COEFF COUNTER CHARS - коэффециент распределения объема по счетчикам";
            Position = 33;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(3);

            var lcc = new List<CNV_CHAR>();

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            string sql =
@"select {0}
from (
	select lshet, max(date) as dat
	from cntrs_kf
	where servicenm like '%Электроэнергия%'
		and lshet like '___-___-__-___-_-__'
	group by lshet	
) l
inner join cntrs_kf k on k.lshet = l.lshet
					and k.date = l.dat
where k.lshet like '___-___-__-___-_-__'";

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(String.Format(sql, "count(*)"))));
            var cold = new Cntrs_kfRecord();
            DbfManager.ExecuteQueryByRow(String.Format(sql, "k.*"), dr =>
            {
                cold.ReadDataRow(dr);

                long lshet = FindLsRecode(cold.Lshet, lsrecode);
                if (lshet != 0)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = lshet.ToString(),
                        SortLshet = lshet,
                        CHARCD = 26,
                        CHARNAME = "Коэф. распр. V по ГПУ",
                        DATE_ = cold.Date,
                        VALUE_ = cold.Koeff
                    };
                    lcc.Add(c);
                }
                Iterate();
            });
            StepFinish();

            StepStart(1);
            lcc = CharsRecordUtils.CreateUniqueCchars(lcc);
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
                        KeyValuePair<int, int>[] recodeValue;
                        if (LcharRecode.TryGetValue(new KeyValuePair<long, long>(lcold.Lcharcd, lcold.Value_), out recodeValue))
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

    public class ConvertContracts : DbfConvertCase
    {
        public ConvertContracts()
        {
            ConvertCaseName = "CONTRACTS - конвертация информации о договорах";
            Position = 42;
            IsChecked = false;
        }

        public static readonly Dictionary<int, int> ServiceRecode = new Dictionary<int, int>
        {
            {1, 9},
            {2, 9},
            {3, 9},
            {4, 29},
            {5, 9}
        };

        private const string GetContractsWithServicesSql =
            "select distinct c.*, l.lcharcd " +
            "from contract c " +
            "inner join lchars l on l.lshet = c.lshet and l.contractcd = c.contractcd " +
            "where c.isdeleted = 0";

        public override void DoDbfConvert()
        {
            SetStepsCount(2);
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lc = new Dictionary<string, CNV_ABONENTCONTRACT>();
            var fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            int countractId = (int)fb.ExecuteScalar("select count(0) from CNV$CONTRACTADDCHAR");

            StepStart(1);
            DbfManager.ExecuteQueryByRow(GetContractsWithServicesSql, dr =>
            {
                var record = new ContractRecord();
                record.ReadDataRow(dr);
                int service;
                if (!ServiceRecode.TryGetValue(Convert.ToInt32(dr["lcharcd"]), out service))
                    throw new Exception($"Не найдена перекодировка характеристики в услугу с кодом {dr["lcharcd"]}");
                long lshet = FindLsRecode(record.Lshet, lsrecode);
                if (lshet == 0) return;
                CNV_ABONENTCONTRACT contract;
                if (lc.TryGetValue(record.Contractcd, out contract))
                {
                    var cntr = lc[record.Contractcd];
                    if (cntr.SERVICES.Split(';').All(s => s != service.ToString()))
                        cntr.SERVICES += ";" + service.ToString();
                }
                else
                {
                    lc.Add(record.Contractcd, new CNV_ABONENTCONTRACT
                    {
                        ID = ++countractId,
                        LSHET = lshet.ToString(),
                        TYPEID = 13, // Договор с абонентом.
                        DOC_NUMBER = record.Nomer.Trim(),
                        DOC_DATE = record.Date,
                        STARTDATE = record.Begdate == NullDate ? (DateTime?)null : record.Begdate,
                        ENDDATE = record.Enddate == NullDate ? (DateTime?)null : record.Enddate,
                        SERVICES = service.ToString(),
                        ORGID = 1, // РГМЭК.
                        PRIM = record.Abnnm.Trim()
                        //NAME = ,
                    });
                }
            });
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc.Select(d => d.Value));
            StepFinish();
        }
    }

    public class ConvertActs : DbfConvertCase
    {
        public ConvertActs()
        {
            ConvertCaseName = "ACTS - конвертации информации об актах тех. присоединения";
            Position = 43;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);
            BufferEntitiesManager.DropTableData("CNV$CONTRACTADDCHAR");
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lc = new List<CNV_ABONENTCONTRACT>();
            var lcc = new List<CNV_CONTRACTADDCHAR>();
            var fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            int countractId = (int)fb.ExecuteScalar("select count(0) from CNV$CONTRACTADDCHAR");
            var format = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            StepStart(1);
            DbfManager.ExecuteQueryByRow("select * from tp_act where isdeleted = 0", dr =>
            {
                var record = new Tp_actRecord();
                record.ReadDataRow(dr);
                long lshet = FindLsRecode(record.Lshet, lsrecode);
                if (lshet == 0) return;
                lc.Add(new CNV_ABONENTCONTRACT
                {
                    ID = ++countractId,
                    LSHET = lshet.ToString(),
                    TYPEID = 16200001, // Акт тех. присоединения.
                    NAME = record.Doc.Trim(),
                    DOC_NUMBER = record.Nomer.Trim(),
                    DOC_DATE = record.Date,
                    STARTDATE = record.Date,
                    ORGID = 1, // РГМЭК.
                });

                if (!String.IsNullOrWhiteSpace(record.Point))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201200, // Точка присоединения.
                        VALUE_ = record.Point.Trim()
                    });
                if (record.Instcap != 0)
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201201, // Максимальная установленная мощность.
                        VALUE_ = record.Instcap.ToString(format)
                    });
                if (record.Estcap != 0)
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201202, // Максимальная расчетная мощность.
                        VALUE_ = record.Estcap.ToString(format)
                    });
                if (!String.IsNullOrWhiteSpace(record.Voltage))
                {
                    decimal voltage;
                    if (decimal.TryParse(record.Voltage.Replace('.', ','), out voltage))
                        lcc.Add(new CNV_CONTRACTADDCHAR
                        {
                            CONTRACTID = countractId,
                            ADDCHARCD = 16201203, // Рабочее напряжение.
                            VALUE_ = voltage.ToString(format)
                        });
                }
                if (!String.IsNullOrWhiteSpace(record.Reliabil))
                {
                    int reliabil;
                    if (int.TryParse(record.Reliabil, out reliabil))
                        lcc.Add(new CNV_CONTRACTADDCHAR
                        {
                            CONTRACTID = countractId,
                            ADDCHARCD = 16201204, // Категория надежности.
                            VALUE_ = reliabil.ToString()
                        });
                }
                if (!String.IsNullOrWhiteSpace(record.Descrip))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201205, // Описание ТП.
                        VALUE_ = record.Descrip.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Balan_lim))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201206, // Границы балансовой пренадлежности.
                        VALUE_ = record.Balan_lim.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Liabil_lim))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201207, // Границы ответственности.
                        VALUE_ = record.Liabil_lim.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Netorg_eq))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201208, // Оборудование сетевой организации.
                        VALUE_ = record.Netorg_eq.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Client_eq))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201209, // Оборудование заявителя.
                        VALUE_ = record.Client_eq.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Netorge_eq))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201210, // Оборудование в эксплуатации сетевой организации.
                        VALUE_ = record.Netorge_eq.Trim()
                    });
                if (!String.IsNullOrWhiteSpace(record.Cliente_eq))
                    lcc.Add(new CNV_CONTRACTADDCHAR
                    {
                        CONTRACTID = countractId,
                        ADDCHARCD = 16201211, // Оборудование в эксплуатации заявителя.
                        VALUE_ = record.Cliente_eq.Trim()
                    });
            });
            StepFinish();

            StepStart(2);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            Iterate();
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
                                                case AddCharRecodeRecord.AbonentType.CChar:
                                                    AddCChar(recode, charRecord, lshet);
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
                case "2":
                    if (recode.RType != AddCharRecodeRecord.RgmekType.HsChar) return false;
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

            string sql = 
@"select {0}
from (
	select c.*, t.koeff
	from (
		select c.*
		from counters c
		left join (
			select c.counterid
			from (
				select c.lshet, c.counterid
				from counters c
				where c.counterid is not null
					and c.lshet like '___-___-__-___-_-__'
				group by c.lshet, c.counterid
			) c
			group by c.counterid
			having count(0) > 1
		) gc on c.counterid = gc.counterid
		where gc.counterid is null
	) c
	left join (
		select t.*
		from (
			select t.placecd, t.instplid, max(date) as dat
			from cntrs_tr t
			where t.placecd is not null
			group by t.placecd, t.instplid
		) mt
		inner join cntrs_tr t on t.placecd = mt.placecd
							and t.instplid = mt.instplid
							and t.date = mt.dat
	) t on t.placecd = c.placecd and t.instplid = c.instplid
) c
left join (
	select oldcntid, max(date) as enddate 
	from cntrs_ac
	where opertype in ('Замена', 'Снятие')
	group by oldcntid
) d on d.oldcntid = c.counterid
where c.counterid is not null and c.counterid <> ''
	and c.lshet like '___-___-__-___-_-__'
{1}";

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar(String.Format(sql, "count(*)", ""))) + 1);

            DbfManager.ExecuteQueryByRow(String.Format(sql, "c.*, d.enddate", "order by c.setupdate desc"), dataRow =>
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
                        NAME = GetCounterName(lcold.Cntname, digitcount, lcold.Precision, lcold.Amperage),
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

        public static string GetCounterName(string name, int digitCount, decimal precision, string amperage)
        {
            const int maxNameLength = 150;
            string result = name.Trim();
            if (digitCount > 0)
            {
                string digitStr = $"разр. {digitCount}";
                result += ", " + digitStr;
            }
            if (precision > 0)
            {
                string precisionStr = $"кл.точ. {precision:F1}".Replace(',', '.');
                result += ", " + precisionStr;
            }
            if (!String.IsNullOrWhiteSpace(amperage))
            {
                string amprStr = $"{amperage.Trim()} А";
                result += ", " + amprStr;
            }
            if (result.Length > maxNameLength) result = result.Substring(0, maxNameLength);
            return result;
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
            var cnttyperecode = Utils.ReadDictionary(CnttypeRecodeFileName).ToDictionary(c => c.Key, c => new CNV_COUNTERTYPE {ID = (int)c.Value});

            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            long counterid = counteridrecode.Select(d => d.Value).Max() + 1;

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

            using (var dt = Tmsource.ExecuteQuery(
@"select c.*, d.enddate
from (
	select c.*, t.koeff
	from cntrs_gr c
	left join (
		select t.*
		from (
			select t.housecd, t.instplid, max(date) as dat
			from cntrs_tr t
			where t.housecd is not null
			group by t.housecd, t.instplid
		) mt
		inner join cntrs_tr t on t.housecd = mt.housecd
							and t.instplid = mt.instplid
							and t.date = mt.dat
	) t on t.housecd = c.housecd and t.instplid = c.instplid
) c
left join (
	select oldcntid, max(date) as enddate 
	from cntrs_ac
	where opertype in ('Замена', 'Снятие')
	group by oldcntid
) d on d.oldcntid = c.counterid
where c.counterid is not null and c.counterid <> ''
order by c.setupdate desc"))
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
                            NAME = ConvertCounters.GetCounterName(cntRecord.Cntname, digitcount, cntRecord.Precision, cntRecord.Amperage),
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
                                    TARGETNEGATIVEBALANCE_KOD = 29,
                                    RECOUNTKOEFFICIENT = String.IsNullOrWhiteSpace(dr["koeff"].ToString()) ? 1 : Convert.ToInt32(dr["koeff"])
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

            StepFinish();

            StepStart(1);
            var alreadyLoadedTypes = Utils.ReadDictionary(CnttypeRecodeFileName).Select(t => t.Value);
            BufferEntitiesManager.SaveDataToBufferIBScript(cnttyperecode.Where(t => !alreadyLoadedTypes.Contains(t.Value.ID)).Select(t => t.Value));

            Utils.SaveDictionary(cnttyperecode.ToDictionary(ct => ct.Key, ct => (long)ct.Value.ID), CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, CounterIdRecodeFileName);
            File.WriteAllLines(GroupCounterIdRecodeFileName, lc.Select(c => c.GUID_).Distinct());
            StepFinish();

            StepStart(2);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lta);
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

    public class ConvertCommunalCounters : DbfConvertCase
    {
        public ConvertCommunalCounters()
        {
            ConvertCaseName = "COMMUNAL COUNTERS - конвертация информации о коммунальных счетчиках";
            Position = 56;
            IsChecked = false;
        }
        
        public override void DoDbfConvert()
        {
            var lc = new List<CNV_COUNTER>();
            var lca = new List<CNV_COUNTERADDCHAR>();
            var lta = new List<CNV_COUNTERTYPEADDCHAR>();
            var cnttyperecode = Utils.ReadDictionary(CnttypeRecodeFileName).ToDictionary(c => c.Key, c => new CNV_COUNTERTYPE { ID = (int)c.Value });

            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            long counterid = counteridrecode.Select(d => d.Value).Max() + 1;

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lshetAddressNameRecode = File.ReadAllLines(LshetAddressNameFileName)
                .Select(s =>
                {
                    string[] info = s.Split(';');
                    return new {Lshet = info[0], AddressName = info[1]};
                })
                .GroupBy(l => l.Lshet, l => l.AddressName)
                .ToDictionary(p => p.Key, p => p.First());

            StepStart(1);
            var digitCounterDic = ConvertCounters.GetCounterDigits(Tmsource);
            StepFinish();

            using (var dt = Tmsource.ExecuteQuery(
@"select c.*, d.enddate
from (
	select c.*, t.koeff
	from (
		select c.*
		from counters c
		inner join (
			select c.counterid
			from (
				select c.lshet, c.counterid
				from counters c
				where c.counterid is not null
					and c.lshet like '___-___-__-___-_-__'
				group by c.lshet, c.counterid
			) c
			group by c.counterid
			having count(0) > 1
		) gc on c.counterid = gc.counterid
	) c
	left join (
		select t.*
		from (
			select t.placecd, t.instplid, max(date) as dat
			from cntrs_tr t
			where t.placecd is not null
			group by t.placecd, t.instplid
		) mt
		inner join cntrs_tr t on t.placecd = mt.placecd
							and t.instplid = mt.instplid
							and t.date = mt.dat
	) t on t.placecd = c.placecd and t.instplid = c.instplid
) c
left join (
	select oldcntid, max(date) as enddate 
	from cntrs_ac
	where opertype in ('Замена', 'Снятие')
	group by oldcntid
) d on d.oldcntid = c.counterid
where c.counterid is not null and c.counterid <> ''
	 and c.lshet like '___-___-__-___-_-__' 
order by c.setupdate desc"))
            {
                int lastcnttypeid = (int)Utils.ReadDictionary((CnttypeRecodeFileName)).Select(cr => cr.Value).Max();
                StepStart(dt.Rows.Count);
                var cntRecord = new CountersRecord();
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
                            NAME = ConvertCounters.GetCounterName(cntRecord.Cntname, digitcount, cntRecord.Precision, cntRecord.Amperage),
                            ACCURACY = cntRecord.Precision,
                            COEFFICIENT = 1,
                            DIGITCOUNT = digitcount,
                        };
                        cnttyperecode.Add(cntRecord.Cnttype, cnttype);

                        if (cntRecord.Id_tpmeter > 0)
                        {
                            lta.Add(new CNV_COUNTERTYPEADDCHAR
                            {
                                COUNTERTYPEID = cnttype.ID,
                                ADDCHARCD = 907,
                                VALUE_ = cntRecord.Id_tpmeter.ToString()
                            });
                        }
                        if (!String.IsNullOrWhiteSpace(cntRecord.Phasecount))
                            lta.Add(new CNV_COUNTERTYPEADDCHAR
                            {
                                COUNTERTYPEID = cnttype.ID,
                                ADDCHARCD = 904,
                                VALUE_ = cntRecord.Phasecount.Trim()
                            });
                        if (!String.IsNullOrWhiteSpace(cntRecord.Amperage))
                            lta.Add(new CNV_COUNTERTYPEADDCHAR
                            {
                                COUNTERTYPEID = cnttype.ID,
                                ADDCHARCD = 905,
                                VALUE_ = cntRecord.Amperage.Trim()
                            });
                    }

                    long ls;
                    if (lsrecode.TryGetValue(LsKvcWithoutKr(cntRecord.Lshet), out ls) && lshetAddressNameRecode.ContainsKey(ls.ToString()))
                    {
                        var enddate = dr.IsNull("enddate") ? (DateTime?)null : Convert.ToDateTime(dr["enddate"]);
                        if (enddate == DateTime.MinValue) enddate = null;
                        var c = new CNV_COUNTER()
                        {
                            LSHET = ls.ToString(),
                            NAME = "к.с. " + lshetAddressNameRecode[ls.ToString()],
                            SERIALNUM = cntRecord.Serialnum.Trim(),
                            CNTNAME = cntRecord.Cntname,
                            SETUPDATE = cntRecord.Setupdate,
                            DEACTDATE = enddate == null || enddate == NullDate ? null : enddate,
                            GUID_ = cntRecord.Counterid.Trim(),
                            CNTTYPE = cnttype.ID,
                            SETUPPLACE = (int?)cntRecord.Instplid,
                            COUNTER_LEVEL = 1,
                            DISTRIBUTINGMETHOD = 4,
                            GROUPCOUNTERMODULEID = 162010002,
                            TARGETBALANCE_KOD = 9,
                            TARGETNEGATIVEBALANCE_KOD = 9,
                            RECOUNTKOEFFICIENT = String.IsNullOrWhiteSpace(dr["koeff"].ToString()) ? 1 : Convert.ToInt32(dr["koeff"])
                        };
                        c.COUNTERID = Utils.GetValue(cntRecord.Counterid, counteridrecode, ref counterid).ToString();
                        if (cntRecord.Lastpov.Year > 1950)
                        {
                            c.LASTPOV = cntRecord.Lastpov;
                            if (cntRecord.Periodpov > 0)
                                c.NEXTPOV = cntRecord.Lastpov.AddMonths((int)cntRecord.Periodpov);
                        }

                        if (cntRecord.Id_meter > 0)
                            lca.Add(new CNV_COUNTERADDCHAR
                            {
                                COUNTERID = c.COUNTERID,
                                ADDCHARCD = 906,
                                VALUE_ = cntRecord.Id_meter.ToString()
                            });

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

            StepFinish();

            StepStart(1);
            var alreadyLoadedTypes = Utils.ReadDictionary(CnttypeRecodeFileName).Select(t => t.Value);
            BufferEntitiesManager.SaveDataToBufferIBScript(cnttyperecode.Where(t => !alreadyLoadedTypes.Contains(t.Value.ID)).Select(t => t.Value));

            Utils.SaveDictionary(cnttyperecode.ToDictionary(ct => ct.Key, ct => (long)ct.Value.ID), CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, CounterIdRecodeFileName);
            File.WriteAllLines(CommunalCounterIdRecodeFileName, lc.Select(c => c.GUID_).Distinct());
            StepFinish();

            StepStart(2);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            Iterate();
            BufferEntitiesManager.SaveDataToBufferIBScript(lta);
            StepFinish();
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
            var lcRgmek = new List<CNV_CNTRSIND>();
            var lcKvc = new List<CNV_CNTRSIND>();
            _lsNotFromKvc = Utils.GetLsNotFromKvc();

            _lsToCounters = Utils.ReadDictionary(CounterIdToLsRecodeFileName);
            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            var groupcounteridrecode = new HashSet<string>();
            File.ReadAllLines(GroupCounterIdRecodeFileName).ToList().ForEach(s => groupcounteridrecode.Add(s));
            var communalcounteridrecode = new HashSet<string>();
            File.ReadAllLines(CommunalCounterIdRecodeFileName).ToList().ForEach(s => communalcounteridrecode.Add(s)); ;
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

                counteridrecode.TryGetValue(cr.Counterid, out counterid);
                if (counterid != 0)
                {
                    bool isGroupCounter = groupcounteridrecode.Contains(cr.Counterid);
                    bool isCommunalCounter = communalcounteridrecode.Contains(cr.Counterid);
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
                        OLDIND = cr.Predind,
                        OB_EM = cr.Rashod
                    };

                    if (IsNorFromKvc(c.COUNTERID) || isGroupCounter || isCommunalCounter)
                        c.INDTYPE = 0;

                    lcRgmek.Add(c);
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

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid))
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
                        lcKvc.Add(c);
                    }
                    Iterate();
                });
            StepFinish();
            StepStart(1);
            CntrsindRecordUtils.RestoreHistory(ref lcKvc, RestoreHistoryType.С_конца_по_конечным_показаниям);
            StepFinish();

            lcRgmek.AddRange(lcKvc);

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lcRgmek);
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
            SetStepsCount(1);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            BufferEntitiesManager.DropTableData("CNV$OPLATA");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var sourcedocrecode = new Dictionary<string, long>();
            long lastsourcedoc = 10;

            var lp = new List<CNV_OPLATA>();
            int paydoc = 0;

            StepStart(PaymentTables.Length);
            foreach (var paymentTable in PaymentTables)
            {
                DbfManager.ExecuteQueryByReader($@"select * from {paymentTable}", reader =>
                {
                    var pr = new PaymentRecord();
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
                            SUMMA = Math.Round(pr.Summa, 2),
                            DOCUMENTCD = $"P{++paydoc}",
                            SOURCENAME = pr.Paypostnm,
                            SOURCECD = (int) Utils.GetValue(pr.Paypostcd, sourcedocrecode, ref lastsourcedoc)
                        });
                    }
                });
                BufferEntitiesManager.SaveDataToBufferIBScript(lp);
                lp.Clear();
                lp.TrimExcess();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Iterate();
            }
            StepFinish();
            Utils.SaveDictionary(sourcedocrecode, PaymentSourcesRecodeFileName);
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
                                    PROCHL = Math.Round(nachInfo.Sum, 2),
                                    PROCHLVOLUME = Math.Round(nachInfo.Volume, 2),
                                    REGIMCD = nachInfo.Tarif == NachExcelRecord.TarifType.Unknown ? 10 : (int) nachInfo.Tarif + (int) nachInfo.Zone,
                                    REGIMNAME = nachInfo.Nach.ToString(),
                                    DOCUMENTCD = $"N{++nachdoc}"
                                });
                            }
                        }
                        else
                        {
                            if (nachInfo.Sum != 0 || nachInfo.Volume != 0)
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
                                    FNATH = nachInfo.Sum == 0 ? 0 : Math.Round(nachInfo.Sum - nachInfo.SumCoef, 2),
                                    VOLUME = Math.Round(nachInfo.Volume, 2),
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
                                    FNATH = Math.Round(nachInfo.SumCoef, 2),
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
            SetStepsCount(5);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lsNotFromKvc = Utils.GetLsNotFromKvc();

            var lnop = new List<CNV_NACHOPL>();
            var actSaldoDic = new Dictionary<string, decimal>();

            var convertDate = new DateTime(CurrentYear, CurrentMonth, 1).AddDays(-1);

            /*using (var dt = aConverterClassLibrary.Utils.ReadExcelFile(ActSaldoFileName, null))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var actSaldoRec = new ExtSaldo(dr, ExtSaldo.ExternalType.Acts);
                    long lshet = FindLsRecode(actSaldoRec.LsKvc, lsrecode);
                    if (lshet != 0)
                    {
                        var nachopl = new CNV_NACHOPL
                        {
                            LSHET = lshet.ToString(),
                            SERVICENAME = "Акты",
                            SERVICECD = 39,
                            PROCHL = 0,
                            FNATH = 0,
                            OPLATA = 0,
                            BDEBET = 0,
                            EDEBET = Math.Round(actSaldoRec.Saldo, 2),
                            MONTH2 = convertDate.Month,
                            YEAR2 = convertDate.Year,
                            MONTH_ = convertDate.Month,
                            YEAR_ = convertDate.Year
                        };
                        actSaldoDic.Add(nachopl.LSHET, nachopl.EDEBET);
                        lnop.Add(nachopl);
                    }
                }
                StepFinish();
            }

            using (var dt = Tmsource.ExecuteQuery("select * from lschars where charcd = 'd673ef5d-90b1-11df-ae5f-001e8c71f1cc'"))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var charRecord = new CommonAddCharRecord(dr);
                    //if (lsNotFromKvc.Contains(charRecord.Owner)) continue;
                    long lshet = FindLsRecode(charRecord.Owner, lsrecode);
                    if (lshet != 0)
                    {
                        var nachopl = new CNV_NACHOPL
                        {
                            LSHET = lshet.ToString(),
                            SERVICENAME = "Электроэнергия",
                            SERVICECD = 9,
                            PROCHL = 0,
                            FNATH = 0,
                            OPLATA = 0,
                            BDEBET = 0,
                            EDEBET = Math.Round(Convert.ToDecimal(charRecord.Value), 2),
                            MONTH2 = convertDate.Month,
                            YEAR2 = convertDate.Year,
                            MONTH_ = convertDate.Month,
                            YEAR_ = convertDate.Year
                        };
                        if (actSaldoDic.ContainsKey(nachopl.LSHET))
                            nachopl.EDEBET -= actSaldoDic[nachopl.LSHET];
                        lnop.Add(nachopl);
                    }
                    Iterate();
                }
                StepFinish();
            }*/

            /*
            StepStart(lsNotFromKvc.Count);
            using (DataTable dtSaldo = Tmsource.GetDataTable(AbonentsNotFromKvcSaldoDbfTableName))
            {
                foreach (DataRow dr in dtSaldo.Rows)
                {
                    var saldo = new ExtSaldo(dr, ExtSaldo.ExternalType.NotFromKvc);
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
                            EDEBET = Math.Round(saldo.Saldo, 2),
                            MONTH2 = convertDate.Month,
                            YEAR2 = convertDate.Year,
                            MONTH_ = convertDate.Month,
                            YEAR_ = convertDate.Year
                        });
                    }
                }
            }
            StepFinish();
            */

            var odnDate = new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1);
            //using (var dt = Tmsource.ExecuteQuery(SelectSaldoOdnSql))
            using (var dt = Tmsource.GetDataTable("SALDO_ODN"))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var saldoRecord = new SaldoRecord();
                    saldoRecord.ReadDataRow(dr);
                    long lshet = FindLsRecode(saldoRecord.Lshet, lsrecode);
                    if (lshet != 0)
                    {
                        lnop.Add(new CNV_NACHOPL
                        {
                            LSHET = lshet.ToString(),
                            SERVICENAME = "Электроэнергия ОДН",
                            SERVICECD = 29,
                            PROCHL = 0,
                            FNATH = 0,
                            OPLATA = 0,
                            BDEBET = 0,
                            EDEBET = Math.Round(Convert.ToDecimal(saldoRecord.Summa), 2),
                            MONTH2 = convertDate.Month,
                            YEAR2 = convertDate.Year,
                            MONTH_ = convertDate.Month,
                            YEAR_ = convertDate.Year
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }

            Task.Factory.StartNew(() =>
            {
                var doubledSaldo = lnop.GroupBy(n => new {n.LSHET, n.SERVICECD}).Where(gn => gn.Count() > 1).ToArray();
                if (!doubledSaldo.Any()) return;
                MessageBox.Show($"Имеется задублированно сальдо{Environment.NewLine}{string.Join(Environment.NewLine, doubledSaldo.Select(s => $"Lshet: {s.Key.LSHET}, Service: {s.Key.SERVICECD}"))}");
            });
           
            StepStart(1);
            //SaveListInsertSQL(lnop, InsertRecordCount);
            BufferEntitiesManager.SaveDataToBufferIBScript(lnop);
            StepFinish();
        }

        private const string SelectSaldoOdnSql =
@"select s.*
from (
	select lshet, servicenm, max(date_deist) as date_deist from saldo
	where (servicenm = 'Общедовые нужды' or servicenm = 'Общедомовые нужды')
	group by lshet, servicenm
) d
inner join saldo s on s.lshet = d.lshet
					and s.servicenm = d.servicenm
					and s.date_deist = d.date_deist
where (s.servicenm = 'Общедовые нужды' or s.servicenm = 'Общедомовые нужды')";


        //private const string SelectSaldoOdnSql =
        //    "select s.* " +
        //    "from( " +
        //    "   select lshet, max(date_deist) as date_deist " +
        //    "   from saldo " +
        //    "   where servicenm = 'Общедовые нужды' or servicenm = 'Общедомовые нужды' " +
        //    "   group by lshet " +
        //    ") l " +
        //    "inner join saldo s on s.lshet = l.lshet and s.date_deist = l.date_deist " +
        //    "where servicenm = 'Общедовые нужды' or servicenm = 'Общедомовые нужды'";
    }

    public class ConvertSaldoPeni : DbfConvertCase
    {
        public ConvertSaldoPeni()
        {
            ConvertCaseName = "SALDO PENI - сальдо по пене";
            Position = 110;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);
            BufferEntitiesManager.DropTableData("CNV$PENISUMMA");
            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);
            var lp = new List<CNV_PENISUMMA>();
            var convertedAbonentServices = new HashSet<Tuple<long, int>>();
            var convertDate = new DateTime(CurrentYear, CurrentMonth, 1).AddMonths(-1);
            var peniDate = convertDate.AddDays(-1);

            using (var dt = Tmsource.ExecuteQuery(SelectPeniSaldoSql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    var saldoRecord = new Saldo_pnRecord();
                    saldoRecord.ReadDataRow(dr);
                    long lshet = FindLsRecode(saldoRecord.Lshet.Trim(), lsrecode);
                    if (lshet != 0)
                    {
                        int service;
                        if (saldoRecord.Servicenm.Contains("Элек")) service = 9;
                        else if (saldoRecord.Servicenm.Contains("Общед")) service = 29;
                        else service = 0;
                        if (service != 0)
                        {
                            var key = new Tuple<long, int>(lshet, service);
                            if (!convertedAbonentServices.Contains(key))
                            {
                                lp.Add(new CNV_PENISUMMA
                                {
                                    LSHET = lshet.ToString(),
                                    SERVICECD = service,
                                    FDATE = peniDate,
                                    FDAY = peniDate.Day,
                                    FMONTH = peniDate.Month,
                                    FYEAR = peniDate.Year,
                                    ABONENTSALDO = 0,
                                    PENINACHISLSUMMA = saldoRecord.Summa,
                                    NDATE = peniDate,
                                    ISCONTROLPOINT = 0,
                                    IZMEN = 1
                                });
                                convertedAbonentServices.Add(key);
                            }
                        }
                    }
                    Iterate();
                }
                StepFinish();
            }

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lp);
            StepFinish();
        }

        private const string SelectPeniSaldoSql =
@"select s.*
from (
	select lshet, servicenm, max(period) as period from saldo_pn
	group by lshet, servicenm
) d
inner join saldo_pn s on s.lshet = d.lshet
					and s.servicenm = d.servicenm
					and s.period = d.period";

        //private const string SelectPeniSaldoSql =
        //    "select s.* " +
        //    "from( " +
        //    "   select lshet, " +
        //    "       iif(servicenm like '%Элек%', 1, iif(servicenm like '%Общед%', 2, 0)) as service, " +
        //    "       {0} as periodyear, {1} as periodmonth, {2} as periodday " +
        //    "   from saldo_pn " +
        //    "   group by lshet, " +
        //    "       iif(servicenm like '%Элек%', 1, iif(servicenm like '%Общед%', 2, 0)) " +
        //    ") as l " +
        //    "inner join saldo_pn s on s.lshet = l.lshet " +
        //    "   and((l.service = 1 and s.servicenm like '%Элек%') " +
        //    "       or(l.service = 2 and s.servicenm like '%Общед%')) " +
        //    "   and year(s.period) = l.periodyear " +
        //    "   and month(s.period) = l.periodmonth " +
        //    "   and day(s.period) = l.periodday";
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
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_03500_ABONENTPHONES");
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

    public class TransferGroupCounters : ConvertCase
    {
        public TransferGroupCounters()
        {
            ConvertCaseName = "Перенос данных о групповых счетчиках и показаниях";
            Position = 1041;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01050_GROUPCOUNTERS", new[] { "0", "1", "0" });
            Iterate();
            fbm.ExecuteNonQuery(@"execute block as
declare variable countername varchar(150);
declare variable counterid integer;
declare variable number integer;
declare variable newcounterid integer;
declare variable etaloncounter integer;
declare variable moduleid integer = 162010010;
declare variable parentcounter integer;
begin
    for select rc.name
        from RESOURCECOUNTERS rc
        where coalesce(rc.TARGETBALANCE_KOD, 9) = 29
        group by rc.NAME
        having count(0) > 1
    into :countername
    do begin
        NEWCOUNTERID = 0;
        if (1 < (select count(0)
            from RESOURCECOUNTERS rc
            where rc.NAME = :countername
                and 0 < coalesce((select first 1 es.STATUSCD
                                from EQSTATUSES es
                                where es.EQUIPMENTID = rc.KOD
                                order by es.STATUSDATE desc), 0))) then
        begin
            NEWCOUNTERID = gen_id(PARENTEQUI_GEN, 1);
            etaloncounter = (select first 1 rc.KOD
                            from RESOURCECOUNTERS rc
                            where rc.NAME = :COUNTERNAME
                                and 0 < coalesce((select first 1 es.STATUSCD
                                                from EQSTATUSES es
                                                where es.EQUIPMENTID = rc.KOD
                                                order by es.STATUSDATE desc), 0));

            insert into PARENTEQUIPMENT (EQUIPMENTID, SERIALNUMBER, IMPORTTAG, NOTE, UNITINGID)
                values (:NEWCOUNTERID, null, null, 'Общий счетчик для расчета', :NEWCOUNTERID);
            insert into RESOURCECOUNTERS (KOD, KODCOUNTERSTYPES, SETUPDATE, COUNTER_LEVEL, COUNTERPLACE, DATEPPR, LASTPPRDATE, NAME, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD, GROUPCOUNTERMODULEID)
                select first 1 :NEWCOUNTERID,  rc.KODCOUNTERSTYPES, rc.SETUPDATE, 1, rc.COUNTERPLACE, rc.DATEPPR, rc.LASTPPRDATE, :COUNTERNAME, rc.TARGETBALANCE_KOD, 5, rc.TARGETNEGATIVEBALANCE_KOD, :MODULEID
                from RESOURCECOUNTERS rc
                where rc.KOD = :ETALONCOUNTER;
            insert into EQSTATUSES (EQUIPMENTID, STATUSDATE, STATUSCD, DOCUMENTCD)
                select first 1 :NEWCOUNTERID, es.STATUSDATE, es.STATUSCD, null
                from EQSTATUSES es
                where es.EQUIPMENTID = :ETALONCOUNTER
                order by es.STATUSDATE desc;
            insert into ABONENTSEQUIPMENT (LSHET, EQUIPMENTID, INSTALLDATE, REMOVEDATE)
                select ae.LSHET, :NEWCOUNTERID, ae.INSTALLDATE, ae.REMOVEDATE
                from ABONENTSEQUIPMENT ae
                where ae.EQUIPMENTID = :ETALONCOUNTER;
        end

        number = 0;
        for select rc.KOD, iif(:NEWCOUNTERID = 0, null, :NEWCOUNTERID)
            from RESOURCECOUNTERS rc
            where rc.NAME = :countername
                and rc.KOD <> :NEWCOUNTERID
        into :COUNTERID, :PARENTCOUNTER
        do begin
            number = :number + 1;
            update RESOURCECOUNTERS rc
                set rc.NAME = rc.NAME || ' (' || cast(:number as varchar(2)) || ')',
                    rc.PARENTCOUNTERID = :PARENTCOUNTER
                where rc.KOD = :counterid;
        end
    end
end");
            Iterate();
        }
    }

    public class TransferCounterChars : ConvertCase
    {
        public TransferCounterChars()
        {
            ConvertCaseName = "Перенос данных о характеристиках счетчиков";
            Position = 1045;
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
    select ae.lshet, es.statusdate, coalesce(rc.TARGETBALANCE_KOD, 9) targetBalance
    from eqstatuses es
    inner join abonentsequipment ae on ae.equipmentid = es.equipmentid
    inner join resourcecounters rc on rc.kod = es.equipmentid
    group by ae.lshet, es.statusdate, rc.TARGETBALANCE_KOD
)
select distinct ls.lshet, ls.statusdate, iif (ls.targetBalance = 29, 22, 21) as lcharcd,
    iif (exists(
            select 0
            from abonentsequipment ae
            inner join resourcecounters rc on rc.kod = ae.equipmentid
                                        and coalesce(rc.TARGETBALANCE_KOD, 9) = ls.targetBalance
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
            StepStart(4);
            fbm.ExecuteScript(@"
SET TERM ^ ;

create or alter procedure RestoreSaldo(saldoyear integer, saldomonth integer) as

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
declare variable saldoyearstr char(4);
declare variable saldomonthstr char(2);

declare variable prevlshet varchar(10);
declare variable prevservice integer;
declare variable prevsaldo numeric(18,4);
declare variable checkdate date;
begin
    saldoyearstr = cast(:saldoyear as char(4));
    saldomonthstr = substring(cast(:saldomonth + 100 as char(3)) from 2);

	merge into cnv$nachopl nop
	using (
		select cn.lshet, cn.servicecd, cn.year_, cn.month_, sum(cn.fnath) as nachsum, sum(cn.prochl) as recalcsum
		from cnv$nach cn
        where cn.year_ * 12 + cn.month_ <= :saldoyear * 12 + :saldomonth
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
        where co.year_ * 12 + co.month_ <= :saldoyear * 12 + :saldomonth
		group by co.lshet, co.servicecd, co.year_, co.month_
	) co on nop.lshet = co.lshet and nop.servicecd = co.servicecd and nop.year_ = co.year_ and nop.month_ = co.month_
	when matched then
		update set nop.oplata = co.paysum
	when not matched then
		INSERT (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME)
			VALUES (co.lshet, co.month_, co.year_, co.month_, co.year_, 0, 0, 0, co.paysum, 0, co.servicecd, co.servicecd);

    prevlshet = '';
    prevservice = -1;
    checkdate = cast('01.'||:saldomonthstr||'.'||:saldoyearstr as date);
    for select nop.lshet, nop.servicecd, nop.year_, nop.month_
        from cnv$nachopl nop
        where nop.year_ * 12 + nop.month_ <= :saldoyear * 12 + :saldomonth
        order by nop.lshet, nop.servicecd, nop.year_ desc, nop.month_ desc
    into :lshet, :servicecd, :year_, :month_
    do begin
        if (:lshet <> :prevlshet or :servicecd <> :prevservice) then
            checkdate = cast('01.'||:saldomonthstr||'.'||:saldoyearstr as date);
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
            Iterate();
            fbm.ExecuteScript(@"--- корректировка первой строчки сальдо
execute block as
declare variable lshet varchar(10);
declare variable year_ integer;
declare variable month_ integer;
declare variable service integer;
declare variable saldosum numeric(18,4);
declare variable documentcd integer;
declare variable tempdate date;
begin
    for select s.LSHET, s.NYEAR, s.NMONTH, s.BALANCE_KOD, s.BEGINSUMMA
        from (
            select s.LSHET, s.BALANCE_KOD, min(s.NYEAR * 12 + s.NMONTH) as sdate
            from saldo s
            group by s.LSHET, s.BALANCE_KOD
        ) d
        inner join saldo s on s.LSHET = d.lshet
                            and s.BALANCE_KOD = d.balance_kod
                            and s.NYEAR * 12 + s.NMONTH = d.sdate
        where s.BEGINSUMMA <> 0
    into :lshet, :year_, :month_, :service, :saldosum
    do begin
        tempdate = DATEADD(DAY, -1, cast('01.'||:month_||'.'||:year_ as date));
        year_ = extract(year from :tempdate);
        month_ = extract(month from :tempdate);
        INSERT INTO SALDO (NYEAR, NMONTH, BALANCE_KOD, LSHET, DATA, BEGINSUMMA, ENDSUMMA)
            VALUES (:year_, :month_, :service, :lshet, :tempdate, 0, :saldosum);
        select documentcd from CREATEDOCUMENT('Импорт сальдо') into :documentcd;
        INSERT INTO PERERASHETCASE (CASEID, LSHET, NACHISLCASEID, BEGINDATE, AUTOUSE, IZMEN, FYEAR, FMONTH, FDAY, ISMONTH, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, CASETYPE, NOTEID, DATE4PENI)
            VALUES (:documentcd , :lshet, :documentcd, NULL, 0, 1, :year_, :month_, 1, 0, :year_, :month_, 1, :year_, :month_, 1, NULL, NULL, NULL);
        INSERT INTO NACHISLSUMMA (LSHET, CASEID, KODREGIM, BALANCE_KOD, SUMMATYPE, NYEAR, NMONTH, NDAY, AYEAR, AMONTH, ADAY, SUMMA, NORMTYPE, SUPPLIERID, KNOTLEVELONEID, KNOTLEVELTWOID, COUNTERCD)
            VALUES (:lshet, :documentcd, 10, :service, 0, :year_, :month_, 1, :year_, :month_, 1, :saldosum, 0, NULL, NULL, NULL, NULL);
    end
end;");
            StepFinish();
        }
    }

    public class TransferHchars : ConvertCase
    {
        public TransferHchars()
        {
            ConvertCaseName = "Перенос характеристик домов";
            Position = 1100;
            IsChecked = false;
        }

        public override void DoConvert()
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

    public class TransferAbonentContracts : ConvertCase
    {
        public TransferAbonentContracts()
        {
            ConvertCaseName = "Перенос договоров абонентов";
            Position = 1110;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(1);
            fbm.ExecuteProcedure("CNV$CNV_03400_ABONENTCONTRACTS");
        }
    }

    public class TransferNewOrg : ConvertCase
    {
        public TransferNewOrg()
        {
            ConvertCaseName = "Перенос характеристики \"Вид сетевой организации\"";
            Position = 1120;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            string[] oboronHousesRgmek =
            {
                "b25a8982-b277-11e4-903c-001e8c71f1cc", // г Рязань, ул. Забайкальская, дом № 11, корп. 2	
                "f0e93786-b277-11e4-903c-001e8c71f1cc", // г Рязань, ул. Загородная, дом № 22, корп. 4	
                "19f3d822-b27a-11e4-903c-001e8c71f1cc", // г Рязань, ул. Загородная, дом № 22, корп. 5
                "93e69271-b27a-11e4-903c-001e8c71f1cc", // г Рязань, ул. Загородная, дом № 22, корп. 6	
                "93e6929e-b27a-11e4-903c-001e8c71f1cc", // г Рязань, ул. Загородная, дом № 22, корп. 7
                "c8b84b48-b27b-11e4-903c-001e8c71f1cc", // г Рязань, ул. Загородная, дом № 22, корп. 8
                "9ee06a7d-64ad-11df-9e0f-001e8c71f1cc", // г Рязань, ул. Белякова, дом № 30а
            };
            string[] rpkHousesRgmek =
            {
                "ed981fb3-9861-11e3-9db8-001e8c71f1cd", // г Рязань, ул. Зубковой, дом № 18 корп. 10	
                "54b12d61-4d0f-11e4-903c-001e8c71f1cc", // г Рязань, ул. Касимовское ш., дом № 20	
                "319b82f9-5bca-11e7-a37c-002590c76e1b", // г Рязань, ул.Московская, дом № 6	
                "66e7eefd-cad1-11e7-a8c0-002590c76e1b", // г Рязань, ул.Московская, дом № 8, корп. 1	
                "c10dddf0-c892-11e4-903c-001e8c71f1cc", // г Рязань, ул.Старообрядческий пр., дом № 11	
                "540daf36-e2af-11e4-b4d2-001e8c71f1cc", // г Рязань, ул. Нижне-Трубежная, дом № 1	
                "ae810bce-c891-11e4-903c-001e8c71f1cc", // г Рязань, ул. Л.Чайкиной, дом № 6	
            };

            StepStart(1);
            var houseRecode = Utils.ReadDictionary(HouseRecodeFileName);
            string[] oboronHousesAbonent = houseRecode.Where(h => oboronHousesRgmek.Contains(h.Key)).Select(h => h.Value.ToString()).ToArray();
            string[] rpkHousesAbonent = houseRecode.Where(h => rpkHousesRgmek.Contains(h.Key)).Select(h => h.Value.ToString()).ToArray();
            StepFinish();
           
            StepStart(3);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery(String.Format(InsertSql, "1", String.Join(",", oboronHousesAbonent.Concat(rpkHousesAbonent)), "not"));
            Iterate();
            fbm.ExecuteNonQuery(String.Format(InsertSql, "2", String.Join(",", oboronHousesAbonent), ""));
            Iterate();
            fbm.ExecuteNonQuery(String.Format(InsertSql, "3", String.Join(",", rpkHousesAbonent), ""));
            StepFinish();
        }

        private const string InsertSql =
            "INSERT INTO ABONENTTRANSPORTLINKS (LSHET, KNOTLEVELTWOID, CONNECTDATETIME, BALANCE_KOD, CONNECTSTATE, CONNECTPOWER, DOCUMENTCD) " +
            "select a.LSHET, {0}, '01.01.2000', 9, 1, 1, NULL " +
            "from abonents a " +
            "where a.HOUSECD {2} in ({1}) " +
            "union all " +
            "select a.LSHET, {0}, '01.01.2000', 29, 1, 1, NULL " +
            "from abonents a " +
            "where a.HOUSECD {2} in ({1});";
    }

    public class TransferPeni : KvcConvertCase
    {
        public TransferPeni()
        {
            ConvertCaseName = "Перенос данных о пене";
            Position = 2010;
            IsChecked = false;

        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();
                var fbt = fbc.BeginTransaction();
                var command = fbc.CreateCommand();
                command.Transaction = fbt;
                command.CommandText = @"ALTER trigger PENISUMMA_BI_RESTRICT inactive";
                command.ExecuteNonQuery();
                fbt.Commit();
            }
            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();
                var fbt = fbc.BeginTransaction();
                var command = fbc.CreateCommand();
                command.Transaction = fbt;
                command.CommandText = @"execute procedure CNV$CNV_01900_PENISUMMA";
                command.ExecuteNonQuery();
                fbt.Commit();
            }
            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();
                var fbt = fbc.BeginTransaction();
                var command = fbc.CreateCommand();
                command.Transaction = fbt;
                command.CommandText = @"ALTER trigger PENISUMMA_BI_RESTRICT active";
                command.ExecuteNonQuery();
                fbt.Commit();
            }

            StepFinish();
        }
    }

    #endregion
}
