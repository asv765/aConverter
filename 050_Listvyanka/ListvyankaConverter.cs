using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace _050_Listvyanka
{
    public class Consts
    {
        public static readonly ExcelFileInfo AlexandrovoFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Александрово390512.xls",
            ListName = "Лист1",
            StartDataRow = 3,
            EndDataRow = 724
        };

        public static readonly ExcelFileInfo ListvyankaFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\ЛИСТВЯНКА 390542.xls",
            ListName = "Лист1",
            StartDataRow = 3,
            EndDataRow = 2426
        };

        public static readonly ExcelFileInfo NaumovoFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Наумово390544 Шелудино.xls",
            ListName = "Лист1",
            StartDataRow = 2,
            EndDataRow = 308
        };

        public static readonly ExcelFileInfo BoloshnevoFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Болошнево 390513, Колос, Зубенки Храпылево.xls",
            ListName = "Лист1",
            StartDataRow = 3,
            EndDataRow = 649
        };

        public static string GetLs(string ls)
        {
            return String.Format("16205{0:D5}", Convert.ToInt64(ls));
        }

        public const int InsertRecordCount = 1000;
    }

    public class ExcelFileInfo
    {
        public string FileName;
        public string ListName;
        public int StartDataRow;
        public int EndDataRow;
    }

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

    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENTS - конвертация информации об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(7);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            BufferEntitiesManager.DropTableData("CNV$CITIZENS");

            var la = new List<CNV_ABONENT>();
            var lc = new List<CNV_CITIZEN>();

            string wrongDates = "";

            DateTime citizenDate = new DateTime();

            // Alexandrovo
            ExcelFileInfo alexandrovoFile = Consts.AlexandrovoFile;
            DataTable alexandrovoTable = Utils.ReadExcelFile(alexandrovoFile.FileName, alexandrovoFile.ListName);
            StepStart(alexandrovoTable.Rows.Count);
            for (int i = alexandrovoFile.StartDataRow - 2; i <= alexandrovoFile.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(alexandrovoTable.Rows[i], 4, 5);
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(lsInfo.Lshet),
                    ISDELETED = 0,
                    TOWNSNAME = lsInfo.townName,
                    ULICANAME = lsInfo.streetName,
                    HOUSENO = lsInfo.houseNumber,
                    HOUSEPOSTFIX = lsInfo.housePostfix,
                    FLATNO = lsInfo.flatNumber,
                    FLATPOSTFIX = lsInfo.flatPostfix,
                    DUCD = 1,
                    DUNAME = "Листвянка"
                };
                var citizen = new CNV_CITIZEN
                {
                    LSHET = abonent.LSHET,
                    HIDDEN = 0,
                    ISMAINCITYZEN = lsInfo.IsMain,
                    REGISTRATIONTYPE = 1,
                    STATUSID = lsInfo.StateId,
                    STATUSDATE = lsInfo.StatusDate,
                    OWNERSHIPTYPE = lsInfo.Ownershiptype,
                    STARTDATE = lsInfo.StartDate
                };
                lsInfo.ExtractFio(ref abonent);
                lsInfo.ExtractFio(ref citizen);
                if (!String.IsNullOrEmpty(abonent.F + abonent.I + abonent.O))
                {
                    if (DateTime.TryParse(lsInfo.Birthdate, out citizenDate))
                    {
                        citizen.BIRTHDATE = citizenDate;
                    }
                    else if (!String.IsNullOrEmpty(citizen.F + citizen.I + citizen.O) &&
                        !String.IsNullOrEmpty(lsInfo.Birthdate))
                    {
                        wrongDates += "№" + (i + 2) + " " + lsInfo.townName + " ул." + lsInfo.streetName + " д. " +
                            lsInfo.houseNumber + lsInfo.housePostfix + " кв. " + lsInfo.flatNumber +
                            lsInfo.flatPostfix + " " + citizen.F + " " + citizen.I + " " + citizen.O +
                            " дата: \"" + lsInfo.Birthdate + "\"\r\n";
                    }
                }

                if (lsInfo.IsMain == 1)
                {
                    la.Add(abonent);
                }
                lc.Add(citizen);
                Iterate();
            }
            StepFinish();

            // Listvyanka
            ExcelFileInfo listvyankaFile = Consts.ListvyankaFile;
            DataTable listvyankaTable = Utils.ReadExcelFile(listvyankaFile.FileName, listvyankaFile.ListName);
            StepStart(listvyankaTable.Rows.Count);

            var listvAbonents = new Dictionary<String, CNV_ABONENT>();
            var listvCitizens = new Dictionary<String, CNV_CITIZEN>();
            
            for (int i = listvyankaFile.StartDataRow - 2; i <= listvyankaFile.EndDataRow - 2; i++)
            {
                var AbnLsInfo = new LsInfo(listvyankaTable.Rows[i], 4, 5);
                var CitizenLsInfo = new LsInfo(listvyankaTable.Rows[i], 6, 7);

                var SobAbonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(AbnLsInfo.Lshet),
                    ISDELETED = 0,
                    TOWNSNAME = AbnLsInfo.townName,
                    ULICANAME = AbnLsInfo.streetName,
                    HOUSENO = AbnLsInfo.houseNumber,
                    HOUSEPOSTFIX = AbnLsInfo.housePostfix,
                    FLATNO = AbnLsInfo.flatNumber,
                    FLATPOSTFIX = AbnLsInfo.flatPostfix,
                    DUCD = 1,
                    DUNAME = "Листвянка"
                };
                var SobCitizen = new CNV_CITIZEN
                {
                    LSHET = SobAbonent.LSHET,
                    HIDDEN = 0,
                    ISMAINCITYZEN = AbnLsInfo.IsMain,
                    REGISTRATIONTYPE = 1,
                    STATUSID = 1,
                    STATUSDATE = AbnLsInfo.StatusDate,
                    OWNERSHIPTYPE = AbnLsInfo.Ownershiptype,
                    STARTDATE = AbnLsInfo.StartDate
                };

                var ZaregCitizen = new CNV_CITIZEN
                {
                    LSHET = SobAbonent.LSHET,
                    HIDDEN = 0,
                    ISMAINCITYZEN = CitizenLsInfo.IsMain,
                    REGISTRATIONTYPE = 1,
                    STATUSID = CitizenLsInfo.StateId,
                    STATUSDATE = CitizenLsInfo.StatusDate,
                    OWNERSHIPTYPE = CitizenLsInfo.Ownershiptype,
                    STARTDATE = CitizenLsInfo.StartDate
                };

                AbnLsInfo.ExtractFio(ref SobAbonent);
                AbnLsInfo.ExtractFio(ref SobCitizen);
                CitizenLsInfo.ExtractFio(ref ZaregCitizen);

                if (DateTime.TryParse(AbnLsInfo.Birthdate, out citizenDate))
                {
                    SobCitizen.BIRTHDATE = citizenDate;
                }
                else if (!String.IsNullOrEmpty(SobCitizen.F + SobCitizen.I + SobCitizen.O) && 
                    !String.IsNullOrEmpty(AbnLsInfo.Birthdate))
                {
                    wrongDates += "№" + (i + 2) + " " + AbnLsInfo.townName + " ул." + AbnLsInfo.streetName + " д. " +
                        AbnLsInfo.houseNumber  + AbnLsInfo.housePostfix + " кв. " + AbnLsInfo.flatNumber + 
                        AbnLsInfo.flatPostfix + " " + SobCitizen.F + " " + SobCitizen.I + " " + SobCitizen.O + 
                        " дата: \"" + AbnLsInfo.Birthdate + "\"\r\n";
                }

                if (DateTime.TryParse(CitizenLsInfo.Birthdate, out citizenDate))
                {
                    ZaregCitizen.BIRTHDATE = citizenDate;
                }
                else if (!String.IsNullOrEmpty(ZaregCitizen.F + ZaregCitizen.I + ZaregCitizen.O) && 
                    !String.IsNullOrEmpty(CitizenLsInfo.Birthdate))
                {
                    wrongDates += "№ " + (i + 2) + " " + CitizenLsInfo.townName + " " + CitizenLsInfo.streetName + " " +
                        CitizenLsInfo.houseNumber + " " + CitizenLsInfo.housePostfix + " " + CitizenLsInfo.flatNumber + " " +
                        CitizenLsInfo.flatPostfix + " " + ZaregCitizen.F + " " + ZaregCitizen.I + " " + ZaregCitizen.O +
                        " дата: " + CitizenLsInfo.Birthdate + "\r\n"; 
                }

                String SobAbonentKey = AbnLsInfo.townName + AbnLsInfo.streetName + AbnLsInfo.houseNumber + 
                    AbnLsInfo.flatNumber + AbnLsInfo.FIO + AbnLsInfo.Birthdate;
                String ZaregAbonentKey = CitizenLsInfo.townName + CitizenLsInfo.streetName + CitizenLsInfo.houseNumber +
                    CitizenLsInfo.flatNumber + CitizenLsInfo.FIO + CitizenLsInfo.Birthdate;

                if (!String.IsNullOrEmpty(SobAbonent.F + SobAbonent.I + SobAbonent.O))
                {
                    var ZaregSob = new CNV_CITIZEN();
                    // Если собственник есть в списке зарегистрированных граждан
                    if (listvCitizens.TryGetValue(SobAbonentKey, out ZaregSob))
                    {
                        //ZaregSob.ISMAINCITYZEN = 1;
                        ZaregSob.STATUSID = 1;
                        ZaregSob.OWNERSHIPTYPE = 1;
                    } 
                    else
                    {
                        listvCitizens.Add(SobAbonentKey, SobCitizen);
                    }
                }
                if (!listvAbonents.ContainsKey(SobAbonentKey))
                {
                    listvAbonents.Add(SobAbonentKey, SobAbonent);
                }
                else
                {
                    CNV_ABONENT existingLshet = new CNV_ABONENT();
                    listvAbonents.TryGetValue(SobAbonentKey, out existingLshet);
                    SobCitizen.LSHET = existingLshet.LSHET;
                    ZaregCitizen.LSHET = existingLshet.LSHET;
                }

                // Если зарегистрированный гражданин есть в списке собственников
                if (listvAbonents.ContainsKey(ZaregAbonentKey))
                {
                    //ZaregCitizen.ISMAINCITYZEN = 1;
                    ZaregCitizen.STATUSID = 1;
                    ZaregCitizen.OWNERSHIPTYPE = 1;
                }
                if (!String.IsNullOrEmpty(ZaregCitizen.F + ZaregCitizen.I + ZaregCitizen.O) 
                    && !listvCitizens.ContainsKey(ZaregAbonentKey))
                {
                    listvCitizens.Add(ZaregAbonentKey, ZaregCitizen);
                }
            
                Iterate();
            }
            la.AddRange(listvAbonents.Values);
            lc.AddRange(listvCitizens.Values);

            StepFinish();

            // Naumovo
            ExcelFileInfo naumovoFile = Consts.NaumovoFile;
            DataTable naumovoTable = Utils.ReadExcelFile(naumovoFile.FileName, naumovoFile.ListName);
            StepStart(naumovoTable.Rows.Count);
            for (int i = naumovoFile.StartDataRow - 2; i <= naumovoFile.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(naumovoTable.Rows[i], 4, 5);
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(lsInfo.Lshet),
                    ISDELETED = 0,
                    TOWNSNAME = lsInfo.townName,
                    ULICANAME = lsInfo.streetName,
                    HOUSENO = lsInfo.houseNumber,
                    HOUSEPOSTFIX = lsInfo.housePostfix,
                    FLATNO = lsInfo.flatNumber,
                    FLATPOSTFIX = lsInfo.flatPostfix,
                    DUCD = 1,
                    DUNAME = "Листвянка"
                };
                var citizen = new CNV_CITIZEN
                {
                    LSHET = abonent.LSHET,
                    HIDDEN = 0,
                    ISMAINCITYZEN = lsInfo.IsMain,
                    REGISTRATIONTYPE = 1,
                    STATUSID = lsInfo.StateId,
                    STATUSDATE = lsInfo.StatusDate,
                    OWNERSHIPTYPE = lsInfo.Ownershiptype,
                    STARTDATE = lsInfo.StartDate
                };
                lsInfo.ExtractFio(ref abonent);
                lsInfo.ExtractFio(ref citizen);
                if (!String.IsNullOrEmpty(abonent.F + abonent.I + abonent.O))
                {
                    if (DateTime.TryParse(lsInfo.Birthdate, out citizenDate))
                    {
                        citizen.BIRTHDATE = citizenDate;
                    }
                    else if (!String.IsNullOrEmpty(citizen.F + citizen.I + citizen.O) &&
                        !String.IsNullOrEmpty(lsInfo.Birthdate))
                    {
                        wrongDates += "№" + (i + 2) + " " + lsInfo.townName + " ул." + lsInfo.streetName + " д. " +
                            lsInfo.houseNumber + lsInfo.housePostfix + " кв. " + lsInfo.flatNumber +
                            lsInfo.flatPostfix + " " + citizen.F + " " + citizen.I + " " + citizen.O +
                            " дата: \"" + lsInfo.Birthdate + "\"\r\n";
                    }
                }
                if (lsInfo.IsMain == 1)
                {
                    la.Add(abonent);
                }
                lc.Add(citizen);
                Iterate();
            }
            StepFinish();

            // Boloshnevo
            ExcelFileInfo boloshnevoFile = Consts.BoloshnevoFile;
            DataTable boloshnevoTable = Utils.ReadExcelFile(boloshnevoFile.FileName, boloshnevoFile.ListName);
            StepStart(boloshnevoTable.Rows.Count);
            for (int i = boloshnevoFile.StartDataRow - 2; i <= boloshnevoFile.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(boloshnevoTable.Rows[i], 4, 5);
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(lsInfo.Lshet),
                    ISDELETED = 0,
                    TOWNSNAME = lsInfo.townName,
                    ULICANAME = lsInfo.streetName,
                    HOUSENO = lsInfo.houseNumber,
                    HOUSEPOSTFIX = lsInfo.housePostfix,
                    FLATNO = lsInfo.flatNumber,
                    FLATPOSTFIX = lsInfo.flatPostfix,
                    DUCD = 1,
                    DUNAME = "Листвянка"
                };
                var citizen = new CNV_CITIZEN
                {
                    LSHET = abonent.LSHET,
                    HIDDEN = 0,
                    ISMAINCITYZEN = lsInfo.IsMain,
                    REGISTRATIONTYPE = 1,
                    STATUSID = lsInfo.StateId,
                    STATUSDATE = lsInfo.StatusDate,
                    OWNERSHIPTYPE = lsInfo.Ownershiptype,
                    STARTDATE = lsInfo.StartDate
                };
                lsInfo.ExtractFio(ref abonent);
                lsInfo.ExtractFio(ref citizen);
                if (!String.IsNullOrEmpty(abonent.F + abonent.I + abonent.O))
                {
                    if (DateTime.TryParse(lsInfo.Birthdate, out citizenDate))
                    {
                        citizen.BIRTHDATE = citizenDate;
                    }
                    else if (!String.IsNullOrEmpty(citizen.F + citizen.I + citizen.O) &&
                        !String.IsNullOrEmpty(lsInfo.Birthdate))
                    {
                        wrongDates += "№" + (i + 2) + " " + lsInfo.townName + " ул." + lsInfo.streetName + " д. " +
                            lsInfo.houseNumber + lsInfo.housePostfix + " кв. " + lsInfo.flatNumber +
                            lsInfo.flatPostfix + " " + citizen.F + " " + citizen.I + " " + citizen.O +
                            " дата: \"" + lsInfo.Birthdate + "\"\r\n";
                    }
                }
                if (lsInfo.IsMain == 1)
                {
                    la.Add(abonent);
                }
                lc.Add(citizen);
                Iterate();
            }
            StepFinish();
            
            if (!String.IsNullOrWhiteSpace(wrongDates))
            {
                File.WriteAllText(aConverter_RootSettings.SourceDbfFilePath + "\\BAD_DATES.txt", wrongDates);
                Task.Run(() => MessageBox.Show(
                    "Обнаружены граждане, у которых некорректно задана дата рождения (см. файл в директории с исходными данными BAD_DATES.txt)"));
            }

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(la, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueDistkod(la, 0);
            StepFinish();

            SaveList(la, Consts.InsertRecordCount);
            SaveList(lc, Consts.InsertRecordCount);
        }
    }

    public class LsInfo
    {
        private static int lshetIterator = 0;

        private static string currentTownName = "";
        private static string currentStreetName = "";
        private static string currentHouseNum = "";
        private static string currentHousePostfix = "";
        private static int currentFlatNum = 0;
        private static string currentFlatPostfix = "";

        public string Lshet;
        public string FIO = "";
        public int IsMain = 0;
        public string Birthdate = "";
        public string townName = "";
        public string streetName = "";
        public string houseNumber = "";
        public string housePostfix = "";
        public int flatNumber = 0;
        public string flatPostfix = "";
        public int StateId = 1;
        internal DateTime StatusDate;
        internal int? Ownershiptype = 1;
        internal DateTime StartDate;

        public LsInfo(DataRow dr, int FioCol, int DateCol)
        {
            Regex numberPostfixRegex = new Regex(@"(\d+)([^\d]+.*)?");

            bool isNewLshet = false;

            StatusDate = new DateTime(2017, 8, 1);
            StartDate = new DateTime(2017, 8, 1);

            // Город
            townName = dr[0].ToString().Trim();
            if (townName == currentTownName || String.IsNullOrWhiteSpace(townName))
            {
                townName = currentTownName;
            }
            else
            {
                currentTownName = townName;
                isNewLshet = true;
            }

            // Улица
            streetName = dr[1].ToString().Trim();
            if (streetName == currentStreetName || String.IsNullOrWhiteSpace(streetName))
            {
                streetName = currentStreetName;
            }
            else
            {
                currentStreetName = streetName;
                isNewLshet = true;
            }

            // Дома
            houseNumber = numberPostfixRegex.Match(dr[2].ToString().Trim()).Groups[1].Value;
            housePostfix = numberPostfixRegex.Match(dr[2].ToString().Trim()).Groups[2].Value;
            if ((houseNumber + housePostfix) == (currentHouseNum + currentHousePostfix)
                || String.IsNullOrWhiteSpace(houseNumber))
            {
                houseNumber = currentHouseNum;
                housePostfix = currentHousePostfix;
            }
            else
            {
                currentHouseNum = houseNumber;
                currentHousePostfix = housePostfix;
                isNewLshet = true;
            }

            // Квартиры
            if (!String.IsNullOrEmpty(dr[3].ToString().Trim()) && dr[3].ToString().Trim() != "станция Листвянка")
            {
                try
                {
                    flatNumber = Convert.ToInt32(numberPostfixRegex.Match(dr[3].ToString().Trim()).Groups[1].Value);
                    flatPostfix = numberPostfixRegex.Match(dr[3].ToString().Trim()).Groups[2].Value;
                    if ((flatNumber + flatPostfix) != (currentFlatNum + currentFlatPostfix))
                    {
                        currentFlatNum = flatNumber;
                        currentFlatPostfix = flatPostfix;
                        isNewLshet = true;
                    }
                }
                catch (Exception ex)
                {
                    flatPostfix = dr[3].ToString().Trim();
                }
            }
            else
            {
                if (!isNewLshet)
                {
                    flatNumber = currentFlatNum;
                    flatPostfix = currentFlatPostfix;
                }
            }

            FIO = dr[FioCol].ToString().Trim();


            if (isNewLshet)
            {
                Lshet = Convert.ToString(++lshetIterator);
                IsMain = 1;
                StateId = 1;
                Ownershiptype = 1;
            }
            else
            {
                Lshet = Convert.ToString(lshetIterator);
                IsMain = 0;
                StateId = 3;
                Ownershiptype = 0;
            }

            Birthdate = dr[DateCol].ToString().Trim();
        }

        public void ExtractFio(ref CNV_ABONENT abonent)
        {
            if (FIO == "собственник не известен" || FIO == "собственик не известен" || FIO == "собственник неизвестен" ||
                String.IsNullOrWhiteSpace(FIO)) return;

            if (FIO == "муниципальное" || FIO == "МО - Рязанский муниципальный район" || // Александрово
                FIO == "администрация Листвянского с.п." || FIO == "администрация Листвянского сп" ||
                FIO == "Администрация Рязанского района" || FIO == "администрация Рязанского района" || 
                FIO == "ОАО\"Агенство финансирования жил стр\"" || // Листвянка
                FIO == "д.ШЕЛУДИНО-2") // Наумово
            {
                abonent.F = FIO;
            }
            else
            {
                string[] splittedFio = FIO.Split(' ');
                if (splittedFio.Length == 1 && splittedFio[0].Length > 30)
                {
                    splittedFio[0] = splittedFio[0].Substring(0, 30);
                }
                if (splittedFio.Length == 2 && splittedFio[1].Length > 20)
                {
                    splittedFio[1] = splittedFio[1].Substring(0, 20);
                }
                if (splittedFio.Length == 3 && splittedFio[2].Length > 20)
                {
                    splittedFio[2] = splittedFio[2].Substring(0, 20);
                }

                if (splittedFio.Length == 1)
                {
                    abonent.F = splittedFio[0];
                }
                else if (splittedFio.Length == 2)
                {
                    abonent.F = splittedFio[0];
                    abonent.I = splittedFio[1];
                }
                else if (splittedFio.Length == 3)
                {
                    abonent.F = splittedFio[0];
                    abonent.I = splittedFio[1];
                    abonent.O = splittedFio[2];
                }
                
            }
        }

        public void ExtractFio(ref CNV_CITIZEN citizen)
        {
            if (String.IsNullOrWhiteSpace(FIO) || FIO == "никто не зарегистрирован") return;
            string[] splittedFio = FIO.Split(' ');

            if (splittedFio.Length == 1 && splittedFio[0].Length > 30)
            {
                splittedFio[0] = splittedFio[0].Substring(0, 30);
            }
            if (splittedFio.Length == 2 && splittedFio[1].Length > 20)
            {
                splittedFio[1] = splittedFio[1].Substring(0, 20);
            }
            if (splittedFio.Length == 3 && splittedFio[2].Length > 20)
            {
                splittedFio[2] = splittedFio[2].Substring(0, 20);
            }

            if (splittedFio.Length == 1)
            {
                citizen.F = splittedFio[0];
            }
            else if (splittedFio.Length == 2)
            {
                citizen.F = splittedFio[0];
                citizen.I = splittedFio[1];
            }
            else if (splittedFio.Length == 3)
            {
                citizen.F = splittedFio[0];
                citizen.I = splittedFio[1];
                citizen.O = splittedFio[2];
            }
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
            StepStart(6);

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
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
            Iterate();

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

    public class TransferCityzens : ConvertCase
    {
        public TransferCityzens()
        {
            ConvertCaseName = "Перенос данных о гражданах";
            Position = 1320;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01800_CITIZENS");
            fbm.ExecuteNonQuery(@"INSERT INTO CITYZENMIGRATION (CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION)
                                select c.cityzen_id, c.startdate, 1, 1
                                from cityzens c
                                where c.registrationtype = 1");
            Iterate();
            StepFinish();
        }
    }
}
