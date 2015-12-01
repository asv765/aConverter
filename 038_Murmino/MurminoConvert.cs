using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _038_Murmino
{
    public static class Consts
    {
        public static string GetLs(long intls)
        {
            return String.Format("86{0:D6}", intls);
        }
    }

    /// <summary>
    /// Создать базу данных для конвертации
    /// </summary>
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = 10;
            IsChecked = true;
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

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);
                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(abonent.Lshet)),
                    EXTLSHET = abonent.Lshet.Trim(),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = Convert.ToInt32(abonent.Ducd),
                    DUNAME = abonent.Duname.Trim(),
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    PRIM_ = abonent.Prim_.Trim(),
                    F = abonent.Fio.Trim()
                };

                // Парсинг ФИО
                var regex = new Regex(@"(\.|\,| )+((([^., ]+)(\.|\,| )*)?){2}$", RegexOptions.Multiline);
                var matches = regex.Matches(a.F);
                if (matches.Count > 0)
                {
                    a.F = regex.Replace(a.F, "");
                    var captures = matches[0].Groups[4].Captures;
                    if (captures.Count > 0)
                    {
                        a.I = captures[0].Value;
                        if (captures.Count > 1) a.O = captures[1].Value;
                    }
                }

                #region Парсинг адреса
                string clearedAddress = abonent.Address.Trim().ToLower();

                // Парсинг индекса
                regex = new Regex("39[0-9]{4}");
                a.POSTINDEX = regex.Match(clearedAddress).Value;

                // Парсинг населенного пункта
                bool addressFound = false;
                foreach (var knownTown in KnownAddress.KnownTowns)
                {
                    foreach (var name in knownTown.ParsingNames)
                    {
                        if (!clearedAddress.Contains(name)) continue;
                        a.TOWNSNAME = knownTown.TrueName + " " + knownTown.TruePrefix;
                        addressFound = true;
                        clearedAddress = new Regex(String.Format(".*({0})", name),
                            RegexOptions.IgnoreCase).Replace(clearedAddress, "");
                        break;
                    }
                    if (addressFound) break;
                }

                // Парсинг улицы
                addressFound = false;
                foreach (var knownStreet in KnownAddress.KnownStreets)
                {
                    foreach (var name in knownStreet.ParsingNames)
                    {
                        if (!clearedAddress.Contains(name)) continue;
                        a.ULICANAME = knownStreet.TrueName + " " + knownStreet.TruePrefix;
                        addressFound = true;
                        clearedAddress = new Regex(String.Format(@".*({0})(.*(ул|пер|пл))?", name),
                            RegexOptions.IgnoreCase).Replace(clearedAddress, "");
                        break;
                    }
                    if (addressFound) break;
                }

                regex = new Regex(@"([0-9]+)(-?([0-9а-яё]+))?[^0-9-]*([0-9]+)?");
                var match = regex.Match(clearedAddress);
                if (match.Groups.Count > 1)
                {
                    a.HOUSENO = match.Groups[1].Value;
                    a.HOUSEPOSTFIX = match.Groups[3].Value;
                    int flatno;
                    Int32.TryParse(match.Groups[4].Value, out flatno);
                    if (flatno == 0) a.FLATNO = null;
                    else a.FLATNO = flatno;
                }
                #endregion

                lca.Add(a);
                Iterate();
            }
            StepFinish();

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            StepStart(1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(lca);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

    public class KnownAddress
    {
        public string[] ParsingNames;

        public string TrueName;
        public string TruePrefix;

        #region Список известных городов
        public static readonly KnownAddress[] KnownTowns =
        {
            new KnownAddress {ParsingNames = new[] {"мурмино"}, TrueName = "Мурмино", TruePrefix = RpPrefix},
            new KnownAddress {ParsingNames = new[] {"дубровичи"}, TrueName = "Дубровичи", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"казарь"}, TrueName = "Казарь", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"долгинино"}, TrueName = "Долгинино", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"сёмкино", "семкино"}, TrueName = "Сёмкино", TruePrefix = VillagePrefix}
        };

        public const string VillagePrefix = "с.";
        public const string RpPrefix = "рп";
        #endregion

        #region Список известных улиц
        public static readonly KnownAddress[] KnownStreets =
        {
            new KnownAddress{ParsingNames = new[] {"лесная"},  TrueName = "Лесная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"верхне-Садовая"},  TrueName = "Верхне-Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"нижне-Садовая"},  TrueName = "Нижне-Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовая"},  TrueName = "Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"школьная"},  TrueName = "Школьная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"советская пл"},  TrueName = "Советская", TruePrefix = SquarePrefix},  
            new KnownAddress{ParsingNames = new[] {"советская","советсая"},  TrueName = "Советская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"колхозная","колхлзная","кохозная"},  TrueName = "Колхозная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"озерная","озёрная"},  TrueName = "Озёрная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"молодежная","молодёжная"},  TrueName = "Молодёжная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"1 мая","1мая","1-ое мая"},  TrueName = "1 Мая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"новая"},  TrueName = "Новая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"центральная"},  TrueName = "Центральная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"рабочая"},  TrueName = "Рабочая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"горка"},  TrueName = "Горка", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"бутырки"},  TrueName = "Бутырки", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"родниковая"},  TrueName = "Родниковая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"фабричная"},  TrueName = "Фабричная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"полевая"},  TrueName = "Полевая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"городцова"},  TrueName = "Городцова", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"левобережная"},  TrueName = "Левобережная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"весовая"},  TrueName = "Весовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"пионерская"},  TrueName = "Пионерская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовый тупик"},  TrueName = "Садовый тупик", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"советской Армии"},  TrueName = "Советской Армии", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"луговая"},  TrueName = "Луговая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовый переулок"},  TrueName = "Садовый переулок", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"нижняя"},  TrueName = "Нижняя", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"комсомольская"},  TrueName = "Комсомольская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"комсомола"},  TrueName = "Комсомола", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"береговая"},  TrueName = "Береговая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"берегова"},  TrueName = "Берегова", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"запрудная"},  TrueName = "Запрудная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"песчаная"},  TrueName = "Песчаная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"заречная"},  TrueName = "Заречная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"есенина"},  TrueName = "Есенина", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"революции"},  TrueName = "Революции", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"зеленая"},  TrueName = "Зеленая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"кооперативная"},  TrueName = "Кооперативная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"кооператив"},  TrueName = "Кооператив", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"радищева"},  TrueName = "Радищева", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"свободы"},  TrueName = "Свободы", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"сиреневая"},  TrueName = "Сиреневая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"охотничий ряд"},  TrueName = "Охотничий ряд", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"охотничий пер"},  TrueName = "Охотничий пер", TruePrefix =""},  
            new KnownAddress{ParsingNames = new[] {"владимировка"},  TrueName = "Владимировка", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"моховой"},  TrueName = "Моховой", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"птичий"},  TrueName = "Птичий", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"широкий"},  TrueName = "Широкий", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"долина"},  TrueName = "Долина", TruePrefix =StreetPrefix}  
        };

        public const string StreetPrefix = "ул.";
        public const string AlleyPrefix = "пер.";
        public const string SquarePrefix = "пл.";
        #endregion
    }
}
