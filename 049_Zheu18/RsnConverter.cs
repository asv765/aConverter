using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using aConverterClassLibrary.Class.ConvertCases;
using System.IO;
using aConverterClassLibrary.Class.Utils;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using OpenAccessRuntime.common;
using static _049_Zheu18.Consts;
using Utils = aConverterClassLibrary.Utils;
using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace _049_Zheu18
{
    public static class Consts
    {
        public static bool ИнсертитьВоВременныеТаблицы = true;
        public static bool ПроряжатьХарактеристики = true;
        public static bool WithRsn3 = true;
        public static bool LsFromFile = false;

        /// <summary>
        /// Конвертация. В порядке убывания
        /// </summary>
        //public readonly static DateTime[] ConvertingDates = { new DateTime(2017, 05, 01), new DateTime(2017, 04, 01), new DateTime(2017, 03, 01), new DateTime(2017, 02, 01), new DateTime(2017, 01, 01), new DateTime(2016, 12, 01), new DateTime(2016, 11, 01), new DateTime(2016, 10, 01), new DateTime(2016, 09, 01), new DateTime(2016, 08, 01), new DateTime(2016, 07, 01), new DateTime(2016, 06, 01), new DateTime(2016, 05, 01), new DateTime(2016, 04, 01), new DateTime(2016, 03, 01), new DateTime(2016, 02, 01), new DateTime(2016, 01, 01), new DateTime(2015, 12, 01), new DateTime(2015, 11, 01), new DateTime(2015, 10, 01), new DateTime(2015, 09, 01)/**/};
        /// <summary>
        /// Выравнивание сальдо по пене на начало июня
        /// </summary>
        public readonly static DateTime[] ConvertingDates = { new DateTime(2017, 05, 01) };
        /// <summary>
        /// Конвертаця старый наем
        /// </summary>
        //public readonly static DateTime[] ConvertingDates = { new DateTime(2015, 08, 01), new DateTime(2015, 07, 01), new DateTime(2015, 06, 01), new DateTime(2015, 05, 01), new DateTime(2015, 04, 01), new DateTime(2015, 03, 01), new DateTime(2015, 02, 01), new DateTime(2015, 01, 01), new DateTime(2014, 12, 01), new DateTime(2014, 11, 01), new DateTime(2014, 10, 01),/**/};  
        /// <summary>
        /// Поиск граждан, присутствующих при конвертации старого наема, но отсутствующих при последнее конвертации (Задача 30807, лс 99081116)
        /// </summary>
        // public readonly static DateTime[] ConvertingDates = { new DateTime(2017, 04, 01), new DateTime(2017, 03, 01), new DateTime(2017, 02, 01), new DateTime(2017, 01, 01), new DateTime(2016, 12, 01), new DateTime(2016, 11, 01), new DateTime(2016, 10, 01), new DateTime(2016, 09, 01), new DateTime(2016, 08, 01), new DateTime(2016, 07, 01), new DateTime(2016, 06, 01), new DateTime(2016, 05, 01), new DateTime(2016, 04, 01), new DateTime(2016, 03, 01), new DateTime(2016, 02, 01), new DateTime(2016, 01, 01), new DateTime(2015, 12, 01), new DateTime(2015, 11, 01), new DateTime(2015, 10, 01), new DateTime(2015, 09, 01), new DateTime(2015, 08, 01), new DateTime(2015, 07, 01), new DateTime(2015, 06, 01), new DateTime(2015, 05, 01), new DateTime(2015, 04, 01), new DateTime(2015, 03, 01), new DateTime(2015, 02, 01), new DateTime(2015, 01, 01), new DateTime(2014, 12, 01), new DateTime(2014, 11, 01), new DateTime(2014, 10, 01) };  
        public const int InsertRecordCount = 1000;
        public const int CurrentYear = 2017;
        public const int CurrentMonth = 05;
        public static readonly int YearToConvert = ConvertingDates[0].Year;
        public static readonly int MonthToConvert = ConvertingDates[0].Month;
        public static readonly int NextYearToConvert = 2017;
        public static readonly int NextMonthToConvert = 05;
        public static readonly int SprYearToConvert = 2017;
        public static readonly int SprMonthToConvert = 05;
        public static List<ReadRsnForm.LsNotInFile> LsNotInLastFile;
        public static List<ReadRsnForm.LsNotInFile> LsLostedInFiles;
        public static Dictionary<string, Dictionary<DateTime, Dictionary<byte, int>>> RegimRecode; // [ЛС] - {[вид]-режим, [вид]-режим}
        public static Dictionary<byte, Dictionary<ushort, int>> OwnersRecode; // [вид][микрокод] = макрокод

        //public static readonly string CharseRecodeFilePath = @"D:\dk\Data\dbf\convert\Характеристики_v1.8.8.xlsx";
        //public static readonly string RsnFilePath = @"D:\dk\Projects\aConverter-master\RsnReader\Sources";
        //public static readonly string SutFile = @"D:\dk\Projects\aConverter-master\RsnReader\Sources\sut9";
        //public static readonly string CcFileName = @"D:\dk\Projects\aConverter-master\RsnReader\Sources";
        //public static readonly string CitizenRecode = @"D:\dk\Data\dbf\convert\cit.xlsx";
        //public static readonly string CitizenRecodeRel = @"D:\dk\Data\dbf\convert\Relations.txt";

        public static readonly string CharseRecodeFilePath = aConverter_RootSettings.SourceDbfFilePath + @"\Характеристики_v2.0.xlsx";
        public static readonly string RsnFilePath = aConverter_RootSettings.SourceDbfFilePath;
        public static readonly string SutFile = aConverter_RootSettings.SourceDbfFilePath +  @"\sut9";
        public static readonly string CcFileName = aConverter_RootSettings.SourceDbfFilePath;
        public static readonly string CitizenRecode = aConverter_RootSettings.SourceDbfFilePath + @"\cit.xlsx";
        public static readonly string CitizenRecodeRel = aConverter_RootSettings.SourceDbfFilePath + @"\Relations.txt";
        public static readonly string PereForNextMonth = aConverter_RootSettings.SourceDbfFilePath + @"\per_1705.xls";
        public static readonly string LongTermCounters = aConverter_RootSettings.SourceDbfFilePath + @"\Declar.xlsx";

        public static RsnAbonent[] RsnAbonents;
        public static Dictionary<string, string> LsDic;
        public static Dictionary<byte, int> resourceRecode;
        public static Spr0 spr0;
        public static Spr1 spr1;
        public static Spr2 spr2;
        public static Spr4 spr4;

        public static Dictionary<byte, byte> PeniRecode = new Dictionary<byte, byte>
        {
            { 41, 1 },
            { 42, 2 },
            { 43, 3 },
            { 45, 5 },
            { 46, 6 },
            { 47, 7 },
            { 50, 10 },
            { 55, 15 },
            { 56, 16 },
            { 57, 17 },
            { 65, 35 },
            { 73, 83 },
            { 74, 34 },
            { 75, 85 },
            { 77, 87 },
        };
    }

    #region Конвертация во временные таблицы

    public class TransferServices : KvcConvertCase
    {
        public TransferServices()
        {
            ConvertCaseName = "Перенос данных об услугах и организациях";
            Position = 03;
            IsChecked = false;
        }

        private static readonly Dictionary<int, string> ResourceCounterDic = new Dictionary<int, string>
        {
            {4, "18" },
            {5, "19" },
            {8, "18" },
            {9, "21" }
        };

        public override void DoKvcConvert()
        {
            SetStepsCount(1 + 1 * ConvertingDates.Length);
            StepStart(4);
            var owners = spr1.GetSubSpr(Spr1.SubSpr.Хозяева);
            var ownresAdd = spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);
            var ownersAddress = spr1.GetSubSpr(Spr1.SubSpr.АдресаИТелефоныХозяев);

            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();

                string sql = @"
delete from balanceslist_logicvalues bllv where bllv.kod > 100 and bllv.kod < 200;
delete from logicvalues lv where lv.kod > 100 and lv.kod < 200;
delete from balanceslist;
delete from EXTORGSPR ex where ex.extorgcd <> 1;";

                var script = new FbScript(sql);
                script.Parse();
                new FbBatchExecution(fbc, script).Execute();
            }
            Iterate();

            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();

                var fbt = fbc.BeginTransaction();
                var command = fbc.CreateCommand();
                command.Transaction = fbt;
                var allUniqueOwners = OwnersRecode.SelectMany(o => o.Value).Select(o => o.Value).Distinct()
                    .Union(new[] { 0509, 1109, 1111, 3400, 4901, 4902, 4903, 4904, 4905, 4906, 4907, 4908, 4909, 4910, 4911, 4912, 4913, 4914, 4915, 4916, 4918, 4919, 4920, 4921, 4922, 4923, 4924, 4925, 4926, 4927, 4928, 4929, 4930, 4931, 4932, 4933, 4934, 4935, 5710, 5831, 6016, 6054, 6059, 6068, 6070, 6077, 6081, 6098, 6101, 6103, 6107, 6111, 6121, 6134, 6172, 6175, 6184, 6192, 6198, 6199, 6213, 6222, 6230, 6234, 7803 })
                    .Union(new[] { 1689, 6202, 5160, 1020, 1040, 1330, 6360, 7830, 5180, 6740, 7340, 6087 })
                    .OrderBy(o => o).Select(o => owners.First(os => os.R1 == o)).GroupBy(o => o.Sr40).Select(o => o.First()).ToArray();
                var newOwnersDic = new Dictionary<string, int>();
                foreach (var owner in allUniqueOwners)
                {
                    var ownerAdd = ownresAdd.FirstOrDefault(oa => oa.R1 == owner.R1);
                    var ownerAddress = ownersAddress.FirstOrDefault(o => o.R1 == owner.R1);
                    string note = ownerAdd == null || string.IsNullOrWhiteSpace(ownerAdd.Sr20) ? "NULL" : "'" + ownerAdd.Sr20 + "'";
                    string phone = ownerAddress == null || string.IsNullOrWhiteSpace(ownerAddress.Sr20) ? "NULL" : "'" + ownerAddress.Sr20 + "'";
                    string address = ownerAddress == null || string.IsNullOrWhiteSpace(ownerAddress.Sr40) ? "NULL" : "'" + ownerAddress.Sr40 + "'";
                    command.CommandText = $@"INSERT INTO EXTORGSPR(EXTORGCD, BANK, BIK, CANGIVELGOT, CANISSUEPASSP, CHARSIMPORT, DEPARTMENTCD, DIRECTOR, EMAIL, EQUIPMENTMAKE, EQUIPMENTSALE, EXPORTTOPAYSYSTEM, EXTORGNM, HASOWNADRESSCD, INN, ISACCOUNT, ISBASEORGANIZATION, ISDEFAULTORG, ISEXTERNALCALC, ISMANAGMENTCOMPANY, ISMILITARYCOMISSION, ISPROVIDER, ISREGISTRATIONAUTHORITY, ISTAX, JURIDICALADDRESS, KORACCOUNT, KPP, LSHETFORMAT, MAINACCOUNTANT, NOTE, OGRN, OKPO, PAYIMPORT, PHONE, POSTADDRESS, RS, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE) 
                                            VALUES({owner.R1} ,NULL ,NULL ,'0' ,'0' ,'0' ,NULL ,NULL ,NULL ,'0' ,'0' ,'0' ,'{owner.Sr40 + ownerAdd?.Sr40}','0' ,'{owner.R4}','0' ,'1' ,'0' ,'0' ,'0' ,'0' ,'1' ,'0' ,'0' ,NULL ,NULL ,'{owner.R2}' ,NULL ,NULL ,{note} ,NULL ,NULL ,'0' ,{phone} ,{address} ,NULL ,NULL ,NULL ,NULL  );";
                    newOwnersDic.Add(owner.Sr40, owner.R1);
                    command.ExecuteNonQuery();
                }
                fbt.Commit();
                Iterate();

                fbt = fbc.BeginTransaction();
                command = fbc.CreateCommand();
                command.Transaction = fbt;
                foreach (var recode in OwnersRecode)
                {
                    if (recode.Key == 0) continue;
                    if (RsnHelper.PeniResources.Contains(recode.Key)) continue;
                    var resourceId = resourceRecode[recode.Key];
                    string iscounterservice;
                    if (!ResourceCounterDic.TryGetValue(resourceId, out iscounterservice))
                        iscounterservice = "NULL";
                    command.CommandText = $"select rl.name from resourceslist rl where rl.kod = {resourceId}";
                    var reader = command.ExecuteReader();
                    reader.Read();
                    string resourceName = reader.GetString(0);
                    foreach (var owner in recode.Value.Select(o => o.Value).Distinct())
                    {
                        var spravOwner = owners.First(o => o.R1 == owner);
                        var spravOwnerAdd = ownresAdd.FirstOrDefault(oa => oa.R1 == owner);
                        string orgName = $"{owner:D4} {(spravOwner.Sr40 + spravOwnerAdd?.Sr40).Replace("'", "''")}";
                        command.CommandText = $@"INSERT INTO BALANCESLIST (BALANCE_KOD, KOD, NAME, ISPENIPRESENT, ADELAYYEAR, ADELAYMONTH, ADELAYDAY, PRIORITET, PERCENT, BEGINDATE, CHILDBALANCE_KOD, VOLUMERECOUNTFORMULA, CALCOVERDUESALDO, EXTORGCD, ISCOUNTERLCHARCD, CHECKABONENTACTIVEMODULEID, SYSTEMBALANCE_KOD, CHECKACTIVELCHARCD, ISAGENTSERVICE, VIRTALGID, VIRTUALBALANCEBASE, FULLSUMMA_PRECISION, FULLVOLUME_PRECISION, LGOTSUMMA_PRECISION, LGOTVOLUME_PRECISION, COMMISSIONFROMABONENT, PAYREQUISITESFROMHOUSE, SENDVOLUMSTOPAYSYSTEM) 
                                    VALUES ({resourceId * 10000 + owner}, {resourceId}, '{resourceName} ({orgName})', 0, 0, 1, 10, 0, 100, NULL, NULL, NULL, NULL, {newOwnersDic[spravOwner.Sr40]}, {iscounterservice}, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);";
                        command.ExecuteNonQuery();
                        command.CommandText = $"insert into logicvalues(KOD, SIGNIFICANCE, LOGICSIGNIFICANCE, COLOR) values({resourceId + 100}, {owner}, '{orgName}', 0);";
                        command.ExecuteNonQuery();
                        command.CommandText = $"insert into balanceslist_logicvalues(BALANCE_KOD, KOD, SIGNIFICANCE) values({resourceId * 10000 + owner}, {resourceId + 100}, {owner});";
                        command.ExecuteNonQuery();
                    }
                }
                fbt.Commit();
                Iterate();

                fbt = fbc.BeginTransaction();
                command = fbc.CreateCommand();
                command.Transaction = fbt;
                foreach (var peni in RecodeOwners.PeniAlgs)
                {
                    int balanceKod = resourceRecode[peni.Vid] * 10000 + OwnersRecode[peni.Vid][peni.OwnerId];
                    command.CommandText = $"update BALANCESLIST set ISPENIPRESENT = 1 where BALANCE_KOD = {balanceKod}";
                    command.ExecuteNonQuery();
                    command.CommandText = $"update or INSERT INTO ABONENTSDELAYPENI (KODBALANCESLIST, PENIGROUPCD, FILTERID, FILTERORDER) values( {balanceKod}, 1, 15, 0)";
                    command.ExecuteNonQuery();
                }
                command.CommandText = "update balanceslist bl set bl.childbalance_kod = (select first 1 bl2.balance_kod from balanceslist bl2 where bl2.kod = 8) where bl.kod in (4,5)";
                command.ExecuteNonQuery();
                fbt.Commit();
            }
            StepFinish();

            List<string> passOrgs = new List<string>();
            ConvertUniqueCCAbonents((ccAbonent) =>
            {
                passOrgs.AddRange(ccAbonent.Жители.Select(c => c.КемВыданДокумент));
            }
            , CcFileName, LsNotInLastFile, true, ConvertingDates, true);
            passOrgs = passOrgs.Distinct().ToList();

            StepStart(passOrgs.Count);
            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();

                var fbt = fbc.BeginTransaction();
                var command = fbc.CreateCommand();
                command.Transaction = fbt;
                foreach (var org in passOrgs)
                {
                    command.CommandText = $@"INSERT INTO EXTORGSPR(EXTORGCD, EXTORGROLEGISZKHID, BANK, BIK, CANGIVELGOT, CANISSUEPASSP, CHARSIMPORT, DEPARTMENTCD, DIRECTOR, EMAIL, EQUIPMENTMAKE, EQUIPMENTSALE, EXPORTTOPAYSYSTEM, EXTORGNM, HASOWNADRESSCD, INN, ISACCOUNT, ISALTERNATIVEACCOUNT, ISBASEORGANIZATION, ISDEFAULTORG, ISEXTERNALCALC, ISMANAGMENTCOMPANY, ISMILITARYCOMISSION, ISPROVIDER, ISREGISTRATIONAUTHORITY, ISTAX, JURIDICALADDRESS, KORACCOUNT, KPP, LSHETFORMAT, MAINACCOUNTANT, NOTE, OGRN, OKPO, PAYIMPORT, PHONE, POSTADDRESS, RS, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE) 
                                VALUES(gen_id(extorgspr_g, 1) ,NULL ,NULL ,NULL ,'0' ,'1' ,'0' ,NULL ,NULL ,NULL ,'0' ,'0' ,NULL ,'{org}' ,'0' ,NULL ,'0' ,NULL ,'0' ,'0' ,'0' ,NULL ,'0' ,'0' ,NULL ,'0' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,'0' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL);";
                    command.ExecuteNonQuery();
                    Iterate();
                }
                fbt.Commit();
            }
            StepFinish();

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1001);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1002);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1003);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1004);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1005);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1006);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1651, 1007);");
            fbm.ExecuteNonQuery("insert into regimlist_logicvalues values  (101, 1550, 1009);");

            fbm.ExecuteNonQuery("UPDATE ABONENTSDELAYPENI set PENIGROUPCD=2 where KODBALANCESLIST in (select balance_kod from balanceslist where kod=11);");
        }
    }

    public class FillConverter : KvcConvertCase
    {
        public FillConverter()
        {
            ReadRsnForm.RsnFilePath = aConverter_RootSettings.SourceDbfFilePath;
            FullSpr.SPRFilePath = aConverter_RootSettings.SourceDbfFilePath + @"\SPR";

            ConvertCaseName = "Заполнить список абонентов";
            Position = 01;
            IsChecked = false;
        }

        public void SetLsFilter()
        {
            LsFilter = null;

            //var tempFilter = new HashSet<string>();
            //// Фильтр по тем, у кого найм жилья - Управление энергетики и жилищно-коммунального хозяйства г. Рязани.
            //ConvertRsnFiles(rsnAbonent =>
            //{
            //    if (!rsnAbonent.Алгоритмы.Any(al => al.Вид == 16 && al.ХозяинВида == 1651)) return;
            //    if (!tempFilter.Contains(rsnAbonent.LsKvc.Ls)) tempFilter.Add(rsnAbonent.LsKvc.Ls);
            //}, null, RsnFilePath, true, ConvertingDates, WithRsn3);

            //LsFilter = tempFilter;
        }

        public override void DoKvcConvert()
        {
            LsDic = new Dictionary<string, string>();
            LsMap = new Dictionary<string, long>();
            resourceRecode = new Dictionary<byte, int>();
            RegimRecode = new Dictionary<string, Dictionary<DateTime, Dictionary<byte, int>>>();
            ReadRsnForm.FillLsDic(out LsDic, out LsNotInLastFile, WithRsn3, ConvertingDates);

            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                connection.Open();
                LsDic = new Dictionary<string, string>();
                var command = connection.CreateCommand();
                command.CommandText = "select ea.extlshet, ea.lshet from extorgaccounts ea where ea.extorgcd = 1";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LsDic.Add(new LsKvc(uint.Parse(reader.GetString(0).Substring(0, 8)), uint.Parse(reader.GetString(0).Substring(8, 6))).Ls, reader.GetString(1));
                }
            }

            foreach (var pair in LsDic)
            {
                LsMap.Add(pair.Key, long.Parse(pair.Value));
            }

            //FirstNotExistedLs = ConvertingDates.Length > 1
            //    ? LsNotInLastFile.SelectMany(l => l.LsList).ToArray().Select(l => LsMap[l]).Min()
            //    : long.MaxValue;

            spr0 = new Spr0(SprYearToConvert, SprMonthToConvert);
            spr1 = new Spr1(SprYearToConvert, SprMonthToConvert);
            spr2 = new Spr2(SprYearToConvert, SprMonthToConvert);
            spr4 = new Spr4(SprYearToConvert, SprMonthToConvert);

            DataTable dt = Utils.ReadExcelFile(aConverter_RootSettings.SourceDbfFilePath + @"\ResAll.xlsx", "Лист1");
            foreach (DataRow dr in dt.Rows)
            {
                resourceRecode.Add(Convert.ToByte(dr[1]), Convert.ToInt32(dr[2]));
            }

            if (LsFromFile)
            {
                var lsDicFormFile = File.ReadAllLines(aConverter_RootSettings.SourceDbfFilePath + @"\LsDic.csv").ToDictionary(l => l.Substring(0, 19), l => l.Substring(20, 8));
                long maxLs = lsDicFormFile.Select(ls => long.Parse(ls.Value)).Max();
                int i = 0;
                int lsDicCount = LsDic.Count;
                for (int j = 0; j < lsDicCount; j++)
                {
                    var ls = LsDic.ElementAt(j);
                    string lsAbn;
                    if (!lsDicFormFile.TryGetValue(ls.Key, out lsAbn))
                        lsAbn = (maxLs + ++i).ToString();
                    LsDic[ls.Key] = lsAbn;
                }
            }

            LsLostedInFiles = new List<ReadRsnForm.LsNotInFile>();
            var allDates = ConvertingDates.ToList();
            if (WithRsn3) allDates.Add(new DateTime(NextYearToConvert, NextMonthToConvert, 1));
            allDates = allDates.OrderByDescending(d => d).ToList();
            bool firstDate = WithRsn3;
            var allLs = LsDic.Select(l => l.Key).ToArray();
            foreach (var date in allDates)
            {
                var allLsInFile = ReadRsnForm.GetAllLs(date, firstDate);
                if (firstDate) firstDate = false;
                LsLostedInFiles.Add(new ReadRsnForm.LsNotInFile
                {
                    FileYear = date.Year,
                    FileMonth = date.Month,
                    LsList = allLs.Except(allLsInFile).ToArray()
                });
            }

            SetLsFilter();

            //var sb = new StringBuilder("");
            //foreach (var ls in LsDic)
            //{
            //    sb.Append(ls.Key);
            //    sb.Append(";");
            //    sb.Append(ls.Value);
            //    sb.Append("\r\n");
            //}
            //File.WriteAllText(aConverter_RootSettings.SourceDbfFilePath + @"\NewLsDic.csv", sb.ToString());

            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            //    var rmbLines = File.ReadAllLines(@"D:\shared\Список договоров по РМБ (1).csv", Encoding.GetEncoding(1251));
            //    var nameRegex = new Regex(@"(?<prof>.*)\s(?<f>[^\s]+)\s(?<i>[^\s]+)\s(?<o>[^\s]+)");
            //    List<string> profs = new List<string>();
            //    string creatingUsers = "";
            //    foreach (var rmb in rmbLines)
            //    {
            //        var rmbInfo = rmb.Split('|');

            //        string number = rmbInfo[0];
            //        string name = rmbInfo[1];
            //        string worker = rmbInfo[2];
            //        string address = rmbInfo[3];
            //        string user = rmbInfo[4];
            //        string password = rmbInfo[5];


            //        string insertService = $@"INSERT INTO SERVICES (SERVICEID, SERVICENAME, ADDRESS, PHONE, GENERATORNAME, SHORTNAME, CURRENTYEAR, CANRECEIVEREPAIRQUERY, EXTORGCD)
            //                                VALUES ((select iif(max(s.serviceid) is null, 1, max(s.serviceid) + 1) from services s), '{name}', '{address}', null, null, null, 0, 0, (select extorgcd from extorgspr where extorgnm = '{name}'));";
            //        fbm.ExecuteNonQuery(insertService);

            //        var match = nameRegex.Match(worker);
            //        int postid = 0;
            //        switch (match.Groups["prof"].Value.Trim())
            //        {
            //            case "Директор":
            //                postid = 6;
            //                break;
            //            case "Индивидуальный предприниматель":
            //                postid = 14;
            //                break;
            //            case "Председатель":
            //                postid = 15;
            //                break;
            //            case "Генеральный директор":
            //                postid = 16;
            //                break;
            //        }
            //        string insertEmployee = $@"INSERT INTO EMPLOYEES (TABNUMBER, SERVICEID, POSTID, EMPLOYEESURNAME, EMPLOYEENAME, EMPLOYEEPATRONYMIC, EMPLOYEEADDRESS, EMPLOYEEPHONE, BOSS, WARRANT_NUMBER, WARRANT_DATE)
            //                                VALUES ((select iif(max(e.TABNUMBER) is null, 1, max(e.TABNUMBER) + 1) from EMPLOYEES e), (select max(s.serviceid) from services s), {postid}, '{match.Groups["f"].Value.Trim()}', '{match.Groups["i"].Value.Trim()}', '{match.Groups["o"].Value.Trim()}', NULL, NULL, NULL, NULL, NULL);";
            //        fbm.ExecuteNonQuery(insertEmployee);


            //        var sqlResult = context.ExecuteQuery<int>($"select first 1 e.extorgcd from extorgspr e where e.extorgnm = '{name}'");
            //        int extorgcd = sqlResult[0];


            //        string insertUser = $@"INSERT INTO USERS (USERCD, TABNUMBER, SYSNM, DESCRIPTION, STATUS,MASTERFILTERNAME,MASTERFILTERTEXT,MASTERFILTERMODE,RESTRICTBALANCESBYORG)
            //                            VALUES ((select iif(max(u.USERCD) is null, 1, max(u.USERCD) + 1) from USERS u), (select max(e.TABNUMBER) from EMPLOYEES e), '{user}', '{name}', 2/*заблокирован*/,'(Владелец равно АО ""{name}"")', 'select a.LSHET from ABONENTS a inner join informationowners io on io.ownerid = a.ownerid where (((io.extorgcd = {extorgcd})))', 1, 1); ";
            //        fbm.ExecuteNonQuery(insertUser);

            //        string insertUserGroup = @"INSERT INTO EMPLOYEEGROUPS (USERGROUPCD, USERCD)
            //                                    VALUES (12, (select max(u.USERCD) from USERS u));";
            //        fbm.ExecuteNonQuery(insertUserGroup);
            //    }
            //}
            //return;
        }
    }

    public class RecodeOwners : KvcConvertCase
    {
        public RecodeOwners()
        {
            ConvertCaseName = "Перекодировать микрокод в макрокод";
            Position = 02;
            IsChecked = false;
        }

        public class TempOwners
        {
            public byte Vid;
            public ushort OwnerId;

            public class TempOwnersComparer : IEqualityComparer<TempOwners>
            {
                public bool Equals(TempOwners x, TempOwners y)
                {
                    return GetHashCode(x) == GetHashCode(y);
                }

                public int GetHashCode(TempOwners obj)
                {
                    return obj.Vid * 10000 + obj.OwnerId;
                }
            }
        }

        private static List<TempOwners> _tempOwners = new List<TempOwners>();
        public static List<TempOwners> PeniAlgs = new List<TempOwners>();

        public override void DoKvcConvert()
        {
            SetStepsCount(1 * ConvertingDates.Length);
            ConvertRsnFiles(rsnAbonent =>
            {
                _tempOwners.AddRange(rsnAbonent.Алгоритмы.Select(al => new TempOwners() { Vid = al.Вид, OwnerId = al.ХозяинВида }));

                PeniAlgs.AddRange(rsnAbonent.Алгоритмы.Where(al => RsnHelper.PeniResources.Contains(al.Вид) && al.Алгоритм == 96).Select(al => new TempOwners() { Vid = PeniRecode[al.Вид], OwnerId = al.ХозяинВида }));
            }
            , null, RsnFilePath, true, ConvertingDates, WithRsn3);

            NotFoundedLs = NotFoundedLs.Distinct().ToList();
            if (NotFoundedLs.Count > 0)
            {
                Task.Factory.StartNew(() => MessageBox.Show(
                    $"Не найдено {NotFoundedLs.Count} лицевых счетов. См. файл notFoundedLs.txt в рабочем каталоге"));
                File.WriteAllLines("notFoundedLs.txt", NotFoundedLs);
            }
        }

        public override void ActionAfterConvert()
        {
            var ownersEnter = spr1.GetSubSpr(Spr1.SubSpr.ВходимостьХозяев);
            var groupedOwners = _tempOwners.Distinct().OrderBy(o => o.Vid).ThenBy(o => o.OwnerId).GroupBy(o => o.Vid, o => o.OwnerId).ToArray();
            OwnersRecode = new Dictionary<byte, Dictionary<ushort, int>>();
            foreach (var owners in groupedOwners)
            {
                byte vid = owners.Key;
                if (RsnHelper.PeniResources.Contains(owners.Key)) vid = PeniRecode[owners.Key];
                var ownersDic = new Dictionary<ushort, int>();
                foreach (var owner in owners.Distinct())
                {
                    var ownSpr = ownersEnter.FirstOrDefault(oe => oe.R1 == vid && oe.R3 == owner);
                    ownersDic.Add(owner, ownSpr == null ? owner : ownSpr.R2);
                }
                OwnersRecode.Add(owners.Key, ownersDic);
            }
            var comparer = new TempOwners.TempOwnersComparer();
            PeniAlgs = PeniAlgs.Distinct(comparer).ToList();

            _tempOwners.Clear();
            _tempOwners.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //var sb = new StringBuilder("");
            //foreach (var peni in PeniAlgs)
            //{
            //    sb.AppendLine((resourceRecode[peni.Vid]*10000 + OwnersRecode[peni.Vid][peni.OwnerId]).ToString());
            //}
            //File.Delete("LastPeniServs.txt");
            //File.WriteAllText("LastPeniServs.txt", sb.ToString());

            //var sb = new StringBuilder("");
            //foreach (var recodeVid in OwnersRecode)
            //{
            //    foreach (var recodeOwner in recodeVid.Value)
            //    {
            //        sb.Append(recodeVid.Key);
            //        sb.Append(";");
            //        sb.Append(recodeOwner.Key);
            //        sb.Append(";");
            //        sb.Append(recodeOwner.Value);
            //        sb.AppendLine();
            //    }
            //}
            //File.Delete("MikroToMakro.csv");
            //File.WriteAllText("MikroToMakro.csv", sb.ToString());

            //var rsnAbonents = new List<RsnAbonent>();
            //ConvertRsnAbonent(rsnAbonent =>
            //{
            //    rsnAbonents.Add(rsnAbonent);
            //}, 2017, 05, false, true);

            //var allAlgs = rsnAbonents.SelectMany(a => a.Алгоритмы.Select(al => new {Lshet = a.LsKvc.Ls, Vid = al.Вид, Alg = al.Алгоритм, Owner = al.ХозяинВида})).ToArray();

            //var oldResult = allAlgs
            //    .Where(al => RsnHelper.PeniResources.Contains(al.Vid))
            //    .GroupBy(al => resourceRecode[PeniRecode[al.Vid]]*10000 + OwnersRecode[PeniRecode[al.Vid]][al.Owner])
            //    .Where(gal => gal.Any(al => al.Alg != 96))
            //    .Select(gal => gal.Key)
            //    .ToArray();

            //var goodResult = allAlgs
            //    .Where(al => RsnHelper.PeniResources.Contains(al.Vid))
            //    .GroupBy(al => resourceRecode[PeniRecode[al.Vid]] * 10000 + OwnersRecode[PeniRecode[al.Vid]][al.Owner])
            //    .Where(gal => gal.All(al => al.Alg != 96))
            //    .Select(gal => gal.Key)
            //    .ToArray();

            //var badDelited = allAlgs
            //    .Where(al => RsnHelper.PeniResources.Contains(al.Vid))
            //    .GroupBy(al => resourceRecode[PeniRecode[al.Vid]] * 10000 + OwnersRecode[PeniRecode[al.Vid]][al.Owner])
            //    .Where(gal => gal.Any(al => al.Alg != 96) && gal.Any(al => al.Alg == 96))
            //    .Select(gal => gal.Key)
            //    .ToArray();


            //List<int> result = new List<int>();
            //var sb = new StringBuilder("");
            //ConvertRsnAbonent(rsnAbonent =>
            //{
            //    foreach (var alg in rsnAbonent.Алгоритмы)
            //    {
            //        if (!RsnHelper.PeniResources.Contains(alg.Вид) || alg.Алгоритм != 96) continue;
            //        var vid = PeniRecode[alg.Вид];
            //        if (PeniAlgs.Any(pa => pa.Vid == vid && pa.OwnerId == alg.ХозяинВида))
            //            result.Add(resourceRecode[vid] * 10000 + OwnersRecode[vid][alg.ХозяинВида]);
            //    }
            //}, 2017, 05, false, true);

            //result = result.Distinct().ToList();

            //foreach (var peni in RecodeOwners.PeniAlgs)
            //{
            //    int balanceKod = resourceRecode[peni.Vid] * 10000 + OwnersRecode[peni.Vid][peni.OwnerId];
            //    if (result.Contains(balanceKod)) continue;
            //    sb.AppendLine(balanceKod.ToString());
            //}

            //File.Delete("NoMorePeni.csv");
            //File.WriteAllText("NoMorePeni.csv", sb.ToString());

            //string recodeOutput = "";
            //foreach (var recodeVid in OwnersRecode)
            //{
            //    int vid = 0;
            //    if (RsnHelper.PeniResources.Contains(recodeVid.Key))
            //    {
            //        vid = resourceRecode[PeniRecode[recodeVid.Key]] * 10000;
            //    }
            //    else vid = resourceRecode[recodeVid.Key] * 10000;
            //    foreach(var recodeOwner in recodeVid.Value)
            //    {
            //        recodeOutput += $"{vid + recodeOwner.Key};{vid + recodeOwner.Value}\r\n";
            //    }
            //}
            //File.Delete("MikroToMakro.csv");
            //File.WriteAllText("MikroToMakro.csv", recodeOutput);
        }
    }

    public class CreateAllFiles : KvcConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = 10;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropProcedureByFullName("KvcChangesCitizensImport");
            BufferEntitiesManager.DropProcedureByFullName("KvcChangesHouseCchImport");
            BufferEntitiesManager.DropProcedureByFullName("KvcChangesHouseLchImport");
            BufferEntitiesManager.DropProcedureByFullName("KvcChangesHouseOdnCounters");
            BufferEntitiesManager.DropProcedureByFullName("KvcChangesCitizenRelations");

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();
            BufferEntitiesManager.CreateAllEntities();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    public class ConvertAbonents : KvcConvertCase
    {
        public ConvertAbonents()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            if (LsDic == null || !LsDic.Any()) throw new Exception("Необходимо заполнить список лицевых счетов");
            SetStepsCount(3 - 1 + ConvertingDates.Length);
            var sprStreet = spr1.GetSubSpr(Spr1.SubSpr.Улицы);
            var sprStreetsAdd = spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеУлиц);
            var sprRegions = spr1.GetSubSpr(Spr1.SubSpr.Округа);
            var sprOwners = spr1.GetSubSpr(Spr1.SubSpr.Хозяева);
            var sprOwnersAdd = spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);
            var la = new List<CNV_ABONENT>();
            ConvertUniqueRsnAbonents(rsnAbonent =>
            {
                switch(rsnAbonent.LsKvc.Ls)
                {
                    case "786-114-00-001-0-47":
                    case "786-114-00-002-0-43":
                        rsnAbonent.Округ = 4;
                        break;
                    case "438-011-00-001-0-32":
                    case "434-059-00-001-0-69":
                    case "061-006-00-002-0-24":
                        rsnAbonent.Округ = 8;
                        break;
                    case "761-001-00-074-0-14":
                        rsnAbonent.Округ = 14;
                        break;
                }

                var a = new CNV_ABONENT();
                a.LSHET = LsDic[rsnAbonent.LsKvc.Ls];
                a.EXTLSHET = $"{rsnAbonent.LsKvc.Adr1:D8}{rsnAbonent.LsKvc.Adr2:D6}";
                a.DUCD = rsnAbonent.ХозяинЛС;
                a.DUNAME = $"{rsnAbonent.ХозяинЛС:D4}" + " " + sprOwners.First(od => od.R1 == rsnAbonent.ХозяинЛС).Sr40 + sprOwnersAdd.FirstOrDefault(od => od.R1 == rsnAbonent.ХозяинЛС)?.Sr40;
                var street = sprStreet.First(s => s.R1 == rsnAbonent.LsKvc.StreetId);
                a.ULICAKOD = rsnAbonent.LsKvc.StreetId;
                bool isDeleted = WithRsn3
                    ? rsnAbonent.FileYear != NextYearToConvert || rsnAbonent.FileMonth != NextMonthToConvert
                    : rsnAbonent.FileYear != ConvertingDates[0].Year || rsnAbonent.FileMonth != ConvertingDates[0].Month;
                if (isDeleted)
                {
                    a.ISDELETED = 1;
                    a.CLOSEDATE = new DateTime(rsnAbonent.FileYear, rsnAbonent.FileMonth, DateTime.DaysInMonth(rsnAbonent.FileYear, rsnAbonent.FileMonth));
                }
                else
                {
                    a.ISDELETED = 0;
                }
                if (a.DUNAME.Length > 50)
                {
                    if (a.DUNAME.Contains("Общество с ограниченной ответственностью")) a.DUNAME = a.DUNAME.Replace("Общество с ограниченной ответственностью", "ООО");
                    else if (a.DUNAME.Contains("Фонд капитального ремонта многоквартирных домов Рязанской области")) a.DUNAME = a.DUNAME.Replace("Фонд капитального ремонта многоквартирных домов Рязанской области", "Фонд кап. ремонта многокв. домов Ряз. обл.");
                    else if (a.DUNAME.Contains("ООО \"Управляющая жилищная компания \"Зеленый сад-Мой дом\"")) a.DUNAME = a.DUNAME.Replace("ООО \"Управляющая жилищная компания \"Зеленый сад-Мой дом\"", "ООО \"Упр. жил. комп. \"Зеленый сад-Мой дом\"");
                }

                var abonentRegion = sprRegions.First(r => r.R1 == rsnAbonent.Округ);
                if (abonentRegion.R3 == 0)
                {
                    a.RAYONNAME = "Рязанский район";
                    a.DISTNAME = abonentRegion.Sr40;
                    a.TOWNSNAME = abonentRegion.Sr20;
                    a.ULICANAME = street.Sr40;

                }
                else if (abonentRegion.R3 == 1)
                {
                    a.RAYONNAME = abonentRegion.Sr20;
                    a.TOWNSNAME = abonentRegion.Sr40;
                    a.ULICANAME = street.Sr40;
                }
                else
                {
                    a.RAYONNAME = abonentRegion.Sr20;
                    if (String.IsNullOrWhiteSpace(street.Sr20))
                    {
                        a.TOWNSNAME = street.Sr40;
                        a.ULICANAME = "";
                    }
                    else
                    {
                        a.TOWNSNAME = street.Sr20;
                        a.ULICANAME = street.Sr40;
                    }
                }
                a.SETTLEMENTNAME = sprStreetsAdd.FirstOrDefault(s => s.R1 == rsnAbonent.LsKvc.StreetId)?.Sr40;
                if (String.IsNullOrWhiteSpace(a.SETTLEMENTNAME)) a.SETTLEMENTNAME = null;

                string f, i, o;
                ParseFIO(rsnAbonent.FIO, out f, out i, out o);
                a.F = f;
                a.I = i;
                a.O = o;
                ParseHouseAddress(rsnAbonent, ref a);
                ParseFlatAddress(rsnAbonent, ref a);

                la.Add(a);
            }, RsnFilePath, LsNotInLastFile, true, ConvertingDates, WithRsn3);

            StepStart(6);
            AbonentRecordUtils.SetUniqueRayonkod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueSettlementkod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueTownskod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueDistkod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(la, 0);
            StepFinish();

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(la, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(la);

            FreeListMemory(la);
        }

        private static readonly Regex fioRegex = new Regex("(?<f>[^ ]+) *(?<i>[^ ]+)? *(?<o>[^ ].*)?", RegexOptions.IgnoreCase);
        public static void ParseFIO(string input, out string f, out string i, out string o)
        {
            f = null;
            i = null;
            o = null;
            if (String.IsNullOrWhiteSpace(input)) return;
            var match = fioRegex.Match(input);
            if (!match.Success) throw new Exception("Не удалось распарсить ФИО " + input);
            f = match.Groups["f"].Value;
            i = match.Groups["i"].Value;
            o = match.Groups["o"].Value;
            if (String.IsNullOrWhiteSpace(f)) f = null;
            if (String.IsNullOrWhiteSpace(i)) i = null;
            if (String.IsNullOrWhiteSpace(o)) o = null;
        }

        private static readonly Spr[] _houses = new Spr1(SprYearToConvert, SprMonthToConvert).GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир).Where(s => s.R2 == 0 && !String.IsNullOrWhiteSpace(s.Sr40)).ToArray();
        private static readonly Spr[] _flats = new Spr1(SprYearToConvert, SprMonthToConvert).GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир).Where(s => s.R2 != 0 && !String.IsNullOrWhiteSpace(s.Sr40)).ToArray();
        private static readonly Regex dRegex = new Regex(@"^(д\. *(.+)$|\d+.*)", RegexOptions.IgnoreCase);
        private static readonly Regex houseNumberRegex = new Regex(@"(?<num>\d+)(?<post>[^\d].*)?", RegexOptions.IgnoreCase);
        private static readonly Regex housePostfixRegex = new Regex(@"^(?<post>[а-яa-z]{1}|\/\d+)(?<note>[^а-яa-z].*)", RegexOptions.IgnoreCase);
        private static readonly Regex korposRegex = new Regex(@"к(орп)?\.(?<knum>\d+)", RegexOptions.IgnoreCase);
        private static readonly Regex areaRegex = new Regex(@"уч\.?[^\d]*(?<num>\d+)(?<post>[^\d]+)?", RegexOptions.IgnoreCase);
        private static readonly Regex dHouseNumRegex = new Regex(@"д\.? *(?<num>\d+)(?<post>[^\d]+)?", RegexOptions.IgnoreCase);

        public static void ParseHouseAddress(RsnAbonent rsnAbonent, ref CNV_ABONENT cnvAbonent)
        {
            var house = _houses.FirstOrDefault(h => h.R1 == rsnAbonent.LsKvc.Adr1);
            if (house == null)
            {
                cnvAbonent.HOUSENO = rsnAbonent.LsKvc.HouseId == 0 ? null : rsnAbonent.LsKvc.HouseId.ToString();
                cnvAbonent.KORPUSNO = rsnAbonent.LsKvc.KorpusId == 0 ? null : (ushort?)rsnAbonent.LsKvc.KorpusId;
                return;
            }

            var match = dRegex.Match(house.Sr40);
            if (match.Success)
            {
                string info = match.Groups[0].Value;

                match = korposRegex.Match(info);
                if (match.Success)
                {
                    cnvAbonent.KORPUSNO = Int32.Parse(match.Groups["knum"].Value);
                    info = korposRegex.Replace(info, "");
                }
                info = info.Replace(",", " ");
                match = houseNumberRegex.Match(info);
                if (match.Success)
                {
                    cnvAbonent.HOUSENO = match.Groups["num"].Value;
                    info = match.Groups["post"].Value;
                    if (!String.IsNullOrWhiteSpace(info))
                    {
                        if (info.Length > 10)
                        {
                            match = housePostfixRegex.Match(info);
                            if (match.Success)
                            {
                                cnvAbonent.HOUSEPOSTFIX = match.Groups["post"].Value;
                                info = match.Groups["note"].Value;
                            }
                            cnvAbonent.HOUSENOTE = info;
                        }
                        else
                            cnvAbonent.HOUSEPOSTFIX = info;
                    }
                }
                else
                {
                    if (info.Length > 10) cnvAbonent.HOUSENOTE = info;
                    else cnvAbonent.HOUSEPOSTFIX = cnvAbonent.HOUSEPOSTFIX;
                }
            }
            else
            {
                if (house.Sr40.ToLower()[0] == 'у' && house.Sr40.ToLower()[1] == 'ч')
                {
                    string info = house.Sr40.Replace("участок", "уч");
                    match = areaRegex.Match(info);
                    if (match.Success)
                    {
                        cnvAbonent.HOUSENO = match.Groups["num"].Value;
                        cnvAbonent.HOUSEPOSTFIX = match.Groups["post"].Value + " уч.";
                    }
                    else
                    {
                        throw new Exception("Не удалось распарсить дом " + house.Sr40);
                    }
                }
                else
                {
                    switch (house.R1)
                    {
                        case 92299901:
                            cnvAbonent.HOUSEPOSTFIX = house.Sr40;
                            break;
                        case 50710101:
                            cnvAbonent.HOUSENO = "101";
                            cnvAbonent.HOUSEPOSTFIX = "-А стр";
                            cnvAbonent.HOUSENOTE = "а/к 'Газовик'";
                            break;
                        case 75700302:
                            cnvAbonent.HOUSEPOSTFIX = house.Sr40;
                            break;
                        default:
                            match = dHouseNumRegex.Match(house.Sr40);
                            if (match.Success)
                            {
                                cnvAbonent.HOUSENO = match.Groups["num"].Value;
                                string info = match.Groups["post"].Value;
                                if (!String.IsNullOrWhiteSpace(info))
                                {
                                    if (info.Length > 10) cnvAbonent.HOUSENOTE = info;
                                    else cnvAbonent.HOUSEPOSTFIX = info;
                                }
                                else
                                {
                                    info = dHouseNumRegex.Replace(house.Sr40, "").Trim();
                                    if (info.Length > 10) cnvAbonent.HOUSENOTE = info;
                                    else cnvAbonent.HOUSEPOSTFIX = info;
                                }
                            }
                            else
                            {
                                cnvAbonent.HOUSENOTE = house.Sr40;
                            }
                            break;
                    }
                }
            }
        }

        private static readonly Regex roomRegex = new Regex(@"ком[^\d]*(?<num>[\d]+)(?<post>[^\d]{1}.*)?", RegexOptions.IgnoreCase);
        private static readonly Regex flatRegex = new Regex(@"\s*кв[^\d]*(?<num>[\d]+)(?<post>[^\d]{1}.*)?", RegexOptions.IgnoreCase);
        private static readonly Regex nFlatRegex = new Regex(@"\s*(?<let>Н|H|д|ж|к|м|п|стр|уч|бокс)(\s|-|\.)?(?<num>\d+)", RegexOptions.IgnoreCase);
        private static readonly Regex kadastrRegex = new Regex(@"62:15.+", RegexOptions.IgnoreCase);
        private static readonly Regex flatDigitRegex = new Regex(@"(?<num>\d+)(?<post>[^\d]{1}.*)?", RegexOptions.IgnoreCase);

        public static void ParseFlatAddress(RsnAbonent rsnAbonent, ref CNV_ABONENT cnvAbonent)
        {
            var flat = _flats.FirstOrDefault(f => f.R1 == rsnAbonent.LsKvc.Adr1 && f.R2 == rsnAbonent.LsKvc.Adr2);
            if (flat == null)
            {
                if (rsnAbonent.НеПечататьАдр2 == 1) return;
                cnvAbonent.FLATNO = rsnAbonent.LsKvc.FlatId == 0 ? null : (ushort?)rsnAbonent.LsKvc.FlatId;
                cnvAbonent.ROOMNO = (short)rsnAbonent.LsKvc.RoomNumber;
                return;
            }
            
            var match = roomRegex.Match(flat.Sr40);
            string info = flat.Sr40;
            if (match.Success)
            {
                cnvAbonent.ROOMNO = Int16.Parse(match.Groups["num"].Value);
                if (!String.IsNullOrWhiteSpace(match.Groups["post"].Value))
                    cnvAbonent.ROOMPOSTFIX = match.Groups["post"].Value;
                info = roomRegex.Replace(info, "").Trim(' ').Trim(',').Trim(' ');
            }
            if (String.IsNullOrWhiteSpace(info)) return;
            match = flatRegex.Match(info);
            if (match.Success)
            {
                cnvAbonent.FLATNO = Int32.Parse(match.Groups["num"].Value);
                if (!String.IsNullOrWhiteSpace(match.Groups["post"].Value))
                    cnvAbonent.FLATPOSTFIX = match.Groups["post"].Value;
            }
            else
            {
                match = nFlatRegex.Match(info);
                if (match.Success)
                {
                    if (info.Length > 10)
                    {
                        cnvAbonent.FLATNO = 0;
                        cnvAbonent.FLATPOSTFIX = info.Substring(0, 10);
                        cnvAbonent.PRIM_ = info;
                    }
                    else
                    {
                        cnvAbonent.FLATNO = 0;
                        cnvAbonent.FLATPOSTFIX = info;
                    }
                }
                else
                {
                    match = kadastrRegex.Match(info);
                    if (match.Success)
                    {
                        cnvAbonent.PRIM_ = info;
                    }
                    else
                    {
                        match = flatDigitRegex.Match(info);
                        if (match.Success)
                        {
                            cnvAbonent.FLATNO = Int32.Parse(match.Groups["num"].Value);
                            if (!String.IsNullOrWhiteSpace(match.Groups["post"].Value))
                                cnvAbonent.FLATPOSTFIX = match.Groups["post"].Value;
                        }
                        else
                        {
                            if (info.Length > 10) cnvAbonent.PRIM_ = info;
                            else
                            {
                                cnvAbonent.FLATNO = 0;
                                cnvAbonent.FLATPOSTFIX = info;
                            }
                        }
                    }
                }
            }
        }
    }

    public class ConvertCchars : KvcConvertCase
    {
        public ConvertCchars()
        {
            ConvertCaseName = "CCHAR - количественный характеристики";
            Position = 30;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        private static Dictionary<string, decimal> _abonentsTotalSquare;
        private static Dictionary<string, decimal> _abonentsTotalSquareForTests;

        public override void ActionBeforeConvert()
        {
            List<RsnAbonent> abonents = new List<RsnAbonent>();
            if (WithRsn3)
            {
                ConvertRsnAbonent(rsnAbonent =>
                {
                    abonents.Add(rsnAbonent);
                }, NextYearToConvert, NextMonthToConvert, false, true);

                _abonentsTotalSquare = abonents
                    .GroupBy(a => new {a.LsKvc.StreetId, a.LsKvc.HouseId, a.LsKvc.KorpusId})
                    .ToDictionary(h => $"{h.Key.StreetId:D3}{h.Key.HouseId:D3}{h.Key.KorpusId:D2}",
                        h => h.Select(a => a.ОбщаяПлощадь).Sum());

                abonents.Clear();
                abonents.TrimExcess();
            }

            if (!WithRsn3)
            {
                ConvertRsnAbonent(rsnAbonent =>
                {
                    abonents.Add(rsnAbonent);
                }, YearToConvert, MonthToConvert, false);

                _abonentsTotalSquareForTests = abonents
                    .GroupBy(a => new {a.LsKvc.StreetId, a.LsKvc.HouseId, a.LsKvc.KorpusId })
                    .ToDictionary(h => $"{h.Key.StreetId:D3}{h.Key.HouseId:D3}{h.Key.KorpusId:D2}",
                        h => h.Select(a => a.ОбщаяПлощадь).Sum());
            }
            abonents.Clear();
            abonents.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(4 - 1 + ConvertingDates.Length);
            var lc = new List<CNV_CHAR>();

            ConvertRsnFiles(rsnAbonent =>
            {
                var chardate = CurrnetConvertDate;
                string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                long intlshet = Int64.Parse(lshet);

                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 1,
                    CHARNAME = "Число зарегистрированных",
                    VALUE_ = rsnAbonent.КоличествоПМЖ + rsnAbonent.КоличествоВо + rsnAbonent.КоличествоПМП
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 3,
                    CHARNAME = "Число проживающих",
                    VALUE_ = rsnAbonent.ЧислоПроживающих
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 10,
                    CHARNAME = "Число временно выбывших",
                    VALUE_ = rsnAbonent.КоличествоВо
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 12,
                    CHARNAME = "Число временно зарегистрированных",
                    VALUE_ = rsnAbonent.КоличествоПМП
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 13,
                    CHARNAME = "Численность для расчета",
                    VALUE_ =  rsnAbonent.ТипПомещения == 0 
                        ?  rsnAbonent.ЧислоПроживающих > 0 
                                ? rsnAbonent.ЧислоПроживающих 
                                : rsnAbonent.ФормаСобственностиЖилогоПомещения == 6 ? 0 : rsnAbonent.КоличествоСобственников
                        : 0
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 2,
                    CHARNAME = "Общая площадь",
                    VALUE_ = rsnAbonent.ОбщаяПлощадь
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 14,
                    CHARNAME = "Жилая площадь",
                    VALUE_ = rsnAbonent.ЖилаяПлощадь
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 5,
                    CHARNAME = "Площадь комнат",
                    VALUE_ = rsnAbonent.ПлощадьКомнат
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 4,
                    CHARNAME = "Отапливаемая площадь",
                    VALUE_ = rsnAbonent.ВидЖилогоПомещения == 3 || rsnAbonent.ВидЖилогоПомещения == 6
                        ? rsnAbonent.ПлощадьКомнат == 0 ? rsnAbonent.ОбщаяПлощадь : rsnAbonent.ПлощадьКомнат
                        : rsnAbonent.ОбщаяПлощадь
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 11,
                    CHARNAME = "Количество собственников",
                    VALUE_ = rsnAbonent.ФормаСобственностиЖилогоПомещения == 6 ? 0 : rsnAbonent.КоличествоСобственников
                });

                var sodhvtarif = rsnAbonent.ТарифыСодЖилКомп.FirstOrDefault(t => t.Вид == 105);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 30,
                    CHARNAME = "Тариф ОДН ХВС",
                    VALUE_ = sodhvtarif == null ? 0 : sodhvtarif.Тариф
                });
                var sodgvtarif = rsnAbonent.ТарифыСодЖилКомп.FirstOrDefault(t => t.Вид == 107);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 31,
                    CHARNAME = "Тариф ОДН ГВС",
                    VALUE_ = sodgvtarif == null ? 0 : sodgvtarif.Тариф
                });

                var sodeltarif = rsnAbonent.ТарифыСодЖилКомп.FirstOrDefault(t => t.Вид == 103);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 32,
                    CHARNAME = "Тариф ОДН электроэнергия",
                    VALUE_ = sodeltarif == null ? 0 : sodeltarif.Тариф
                });
                var norm3 = rsnAbonent.Нормативы.FirstOrDefault(t => t.Вид == 3);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 22,
                    CHARNAME = "Норма на эл.",
                    VALUE_ = norm3 == null ? 0 : norm3.Значение1 * 100 * (rsnAbonent.ЧислоПроживающих > 0 ? rsnAbonent.ЧислоПроживающих : rsnAbonent.КоличествоСобственников)
                });
                var sodTarif = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 1);
                if (sodTarif != null)
                {
                    var clearSodTarifValue = sodTarif.Значение - (sodhvtarif == null ? 0 : sodhvtarif.Тариф) - (sodgvtarif == null ? 0 : sodgvtarif.Тариф) - (sodeltarif == null ? 0 : sodeltarif.Тариф);
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 20,
                        CHARNAME = "Тариф содержание",
                        VALUE_ = clearSodTarifValue
                    });
                }
                else
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 20,
                        CHARNAME = "Тариф содержание",
                        VALUE_ = 0
                    });
                }
                if (sodTarif == null || sodTarif.Значение == 0)
                {
                    var sodNach = rsnAbonent.НачисленияПоНормативам.Select(n => new { Vid = n.Вид, Sum = n.Значение }).Union(rsnAbonent.НачисленияПоСчетчикам.Select(n => new { Vid = n.Вид, Sum = n.Сумма })).FirstOrDefault(n => n.Vid == 1 && n.Sum > 0);
                    if (sodNach != null && sodNach.Sum > 0)
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            CHARCD = 21,
                            CHARNAME = "Сумма содержание",
                            VALUE_ = sodNach.Sum
                        });
                    else
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            CHARCD = 21,
                            CHARNAME = "Сумма содержание",
                            VALUE_ = 0
                        });
                }
                else
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 21,
                        CHARNAME = "Сумма содержание",
                        VALUE_ = 0
                    });
                var norm83 = rsnAbonent.Нормативы.FirstOrDefault(n => n.Вид == 83);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 60,
                    CHARNAME = "Норма на эл. ОДН",
                    VALUE_ = norm83 == null ? 0 : norm83.Значение1
                });
                var tarif5 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 5);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 55,
                    CHARNAME = "М3 ХВС",
                    VALUE_ = tarif5 == null ? 0 : tarif5.Значение
                });
                var normOtopl = rsnAbonent.Нормативы.FirstOrDefault(n => n.Вид == 6);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 151,
                    CHARNAME = "Норматив отопление",
                    VALUE_ = normOtopl == null ? 0 : normOtopl.Значение1
                });
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 152,
                    CHARNAME = "Объем отопление",
                    VALUE_ = normOtopl == null ? 0 : normOtopl.Значение2
                });
                var tarif6 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 6);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 59,
                    CHARNAME = "Гкал. Отопл.",
                    VALUE_ = tarif6 == null ? 0 : tarif6.Значение
                });
                var nach6 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 6);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 61,
                    CHARNAME = "Сумма на отопление",
                    VALUE_ = (tarif6 == null || tarif6.Значение == 0) && (nach6 != null && nach6.Значение > 0) ? nach6.Значение : 0
                });
                var tarif7 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 7);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 56,
                    CHARNAME = "М3 ГВС",
                    VALUE_ = tarif7 == null ? 0 : tarif7.Значение
                });
                var tarif8 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 8);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 27,
                    CHARNAME = "Сумма домофон",
                    VALUE_ = tarif8 == null ? 0 : tarif8.Значение
                });
                var nach12 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 12);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 39,
                    CHARNAME = "Сумма кабельное ТВ",
                    VALUE_ = nach12 == null ? 0 : nach12.Значение
                });
                var nach13 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 13);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 54,
                    CHARNAME = "Сумма кабельное ТВ2",
                    VALUE_ = nach13 == null ? 0 : nach13.Значение
                });
                var tarif15 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 15);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 36,
                    CHARNAME = "Тариф ТБО",
                    VALUE_ = tarif15 == null ? 0 : tarif15.Значение
                });
                var nach15 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 15);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 63,
                    CHARNAME = "Сумма на вывоз ТБО",
                    VALUE_ = (tarif15 == null || tarif15.Значение == 0) && (nach15 != null && nach15.Значение > 0) ? nach15.Значение : 0
                });

                var tarif17 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 17);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 57,
                    CHARNAME = "М3 Водоотведение",
                    VALUE_ = tarif17 == null ? 0 : tarif17.Значение
                });
                var tarif2 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 2);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 64,
                    CHARNAME = "М3 Газ",
                    VALUE_ = tarif2 == null ? 0 : tarif2.Значение
                });
                var tarif34 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 34);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 53,
                    CHARNAME = "Тариф на кап. ремонт",
                    VALUE_ = tarif34 == null ? 0 : tarif34.Значение
                });
                var tarif3 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 3);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 58,
                    CHARNAME = "Квт электроэнергия",
                    VALUE_ = tarif3 == null ? 0 : tarif3.Значение
                });
                var tarif3St2 = rsnAbonent.ТарифыПоСчетчикам.FirstOrDefault(t => t.Вид == 3 && t.КодСчетчика >= 21 && t.КодСчетчика <= 28);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 66,
                    CHARNAME = "Квт электроэнергия (2-я ставка)",
                    VALUE_ = tarif3St2 == null ? 0 : tarif3St2.Значение
                });
                var tarif3St3 = rsnAbonent.ТарифыПоСчетчикам.FirstOrDefault(t => t.Вид == 3 && t.КодСчетчика >= 31 && t.КодСчетчика <= 39);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 67,
                    CHARNAME = "Квт электроэнергия (3-я ставка)",
                    VALUE_ = tarif3St3 == null ? 0 : tarif3St3.Значение
                });
                var nach3 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 3);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 62,
                    CHARNAME = "Сумма на электроэнергию",
                    VALUE_ = (tarif3 == null || tarif3.Значение == 0) && (nach3 != null && nach3.Значение > 0) ? nach3.Значение : 0
                });
                var nach19 = rsnAbonent.НачисленияПоНормативам.Where(n => n.Вид == 19).Select(n => n.Значение).Concat(rsnAbonent.НачисленияПоСчетчикам.Where(n => n.Вид == 19).Select(n => n.Сумма)).ToArray();
                if (nach19.Any())
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 26,
                        CHARNAME = "Сумма интернет",
                        VALUE_ = nach19.Sum()
                    });
                }
                else
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 26,
                        CHARNAME = "Сумма интернет",
                        VALUE_ = 0
                    });
                }
                var tarif20 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 20);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 28,
                    CHARNAME = "Сумма водоочистка",
                    VALUE_ = tarif20 == null ? 0 : tarif20.Значение
                });
                var tarif28 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 28);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 38,
                    CHARNAME = "Сумма видеонаблюдение",
                    VALUE_ = tarif28 == null ? 0 : tarif28.Значение
                });

                var polPlo = rsnAbonent.ПлощадиНормативы.FirstOrDefault(p => p.Вид == 5);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 7,
                    CHARNAME = "Поливная площадь",
                    VALUE_ = polPlo == null ? 0 : polPlo.Значение1
                });

                var ploUch = rsnAbonent.ПлощадиНормативы.FirstOrDefault(p => p.Вид == 29);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 8,
                    CHARNAME = "Площадь участка",
                    VALUE_ = ploUch == null ? 0 : ploUch.Значение1
                });

                var roomCount = rsnAbonent.ПлощадиНормативы.FirstOrDefault(n => n.Вид == 03 && n.Значение2 > 0);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 23,
                    CHARNAME = "Число комнат",
                    VALUE_ = roomCount == null ? 0 : roomCount.Значение2
                });

                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 46,
                    CHARNAME = "Количество каналов",
                    VALUE_ = rsnAbonent.КоличествоКаналов
                });

                var alg9 = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == 9);
                if (alg9 != null && (alg9.Алгоритм > 14 || alg9.Алгоритм == 12))
                {
                    var nach9 = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == 9);
                    if (nach9 != null)
                    {
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            CHARCD = 47,
                            CHARNAME = "Сумма на антенну",
                            VALUE_ = nach9.Значение
                        });
                    }
                    else
                        lc.Add(new CNV_CHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            CHARCD = 47,
                            CHARNAME = "Сумма на антенну",
                            VALUE_ = 0
                        });
                }
                else
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 47,
                        CHARNAME = "Сумма на антенну",
                        VALUE_ = 0
                    });

                var tarif10 = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 10);
                if (tarif10 != null && tarif10.Значение > 0)
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 48,
                        CHARNAME = "Тариф на очистку",
                        VALUE_ = tarif10.Значение
                    });
                }
                else
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 48,
                        CHARNAME = "Тариф на очистку",
                        VALUE_ = 0
                    });

                var kapTarif = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == 34);
                if (kapTarif != null && kapTarif.Значение > 0)
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 53,
                        CHARNAME = "Тариф на кап. ремонт",
                        VALUE_ = kapTarif.Значение
                    });
                }
                else
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = 53,
                        CHARNAME = "Тариф на кап. ремонт",
                        VALUE_ = 0
                    });

                var poolVol = rsnAbonent.ПараметрыУчастка.FirstOrDefault(p => p.КодПараметра >= 212 && p.КодПараметра <= 214 && p.Значение > 0);
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 24,
                    CHARNAME = "Объем бассейна",
                    VALUE_ = poolVol == null ? 0 : poolVol.Значение
                });

                decimal hvcHeating = 0;
                var alg7 = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == 7);
                if (alg7 != null)
                {
                    var gvsOwn = OwnersRecode[7][alg7.ХозяинВида];
                    if (new[] { 5771, 6095, 6241 }.Contains(gvsOwn))
                        hvcHeating = tarif7 == null ? 0 : tarif7.Значение;
                    else if (new[] { 3, 4, 5, 6, 6007, 6238, 7970, 7975, 7976 }.Contains(gvsOwn))
                    {
                        if (new[] { 1, 2, 5, 6, 11, 101, 102, 105, 106, 111 }.Contains(alg7.Алгоритм))
                            hvcHeating = 23.92m;
                        else if (new[] { 51, 52, 53, 54, 55, 56, 151, 152, 153, 154, 155, 156 }.Contains(alg7.Алгоритм))
                            hvcHeating = 22m;
                    }
                }
                lc.Add(new CNV_CHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    CHARCD = 65,
                    CHARNAME = "Стоимость м3 ХВС для подогрева",
                    VALUE_ = hvcHeating
                });


                List<int> animalChars = new List<int> { 40, 41, 42, 43, 44, 45 };
                for (int j = 0; j < rsnAbonent.ПараметрыУчастка.Count; j++)
                {
                    int charcd;
                    string charName;
                    var sectorParam = rsnAbonent.ПараметрыУчастка[j];
                    switch (sectorParam.КодПараметра)
                    {
                        case 200:
                            charcd = 40;
                            charName = "Число коров";
                            break;
                        case 202:
                            charcd = 41;
                            charName = "Число овец";
                            break;
                        case 201:
                            charcd = 42;
                            charName = "Число свиней";
                            break;
                        case 205:
                            charcd = 43;
                            charName = "Число птиц";
                            break;
                        case 203:
                            charcd = 44;
                            charName = "Число лошадей";
                            break;
                        case 204:
                            charcd = 45;
                            charName = "Число коз";
                            break;
                        default:
                            continue;
                    }
                    animalChars.Remove(charcd);
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = charcd,
                        CHARNAME = charName,
                        VALUE_ = sectorParam.Значение
                    });
                }
                for (int j = 0; j < animalChars.Count; j++)
                {
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        CHARCD = animalChars[j],
                        //CHARNAME = charName,
                        VALUE_ = 0
                    });
                }

                if (rsnAbonent.Алгоритмы.Any(al => (al.Вид == 83 || al.Вид == 85 || al.Вид == 87) && al.Алгоритм > 0))
                { 
                     if (WithRsn3 && rsnAbonent.FileYear == NextYearToConvert && rsnAbonent.FileMonth == NextMonthToConvert)
                     {
                         decimal totalSum = _abonentsTotalSquare[$"{rsnAbonent.LsKvc.StreetId:D3}{rsnAbonent.LsKvc.HouseId:D3}{rsnAbonent.LsKvc.KorpusId:D2}"];
                         var sumNoInDb = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 17);
                         if (sumNoInDb != null) totalSum += sumNoInDb.Значение;
                         decimal mopValue = 0;
                         if (totalSum != 0)
                         {
                             var houseMop = rsnAbonent.ПараметрыДома.FirstOrDefault(h => h.КодРеквезита == 25);
                             mopValue = Math.Round(rsnAbonent.ОбщаяПлощадь * (houseMop == null ? 0 : houseMop.Значение) / totalSum, 4);
                         }
                         lc.Add(new CNV_CHAR
                         {
                             LSHET = lshet,
                             SortLshet = intlshet,
                             DATE_ = chardate,
                             CHARCD = 150,
                             CHARNAME = "Доля площади МОП",
                             VALUE_ = mopValue
                         });
                     }
                     else if (!WithRsn3 && rsnAbonent.FileYear == YearToConvert && rsnAbonent.FileMonth == MonthToConvert)
                     {
                         decimal totalSum = _abonentsTotalSquareForTests[$"{rsnAbonent.LsKvc.StreetId:D3}{rsnAbonent.LsKvc.HouseId:D3}{rsnAbonent.LsKvc.KorpusId:D2}"];
                         var sumNoInDb = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 17);
                         if (sumNoInDb != null) totalSum += sumNoInDb.Значение;
                         decimal mopValue = 0;
                         if (totalSum != 0)
                         {
                             var houseMop = rsnAbonent.ПараметрыДома.FirstOrDefault(h => h.КодРеквезита == 25);
                             mopValue = Math.Round(rsnAbonent.ОбщаяПлощадь * (houseMop == null ? 0 : houseMop.Значение) / totalSum, 4);
                         }
                         lc.Add(new CNV_CHAR
                         {
                             LSHET = lshet,
                             SortLshet = intlshet,
                             DATE_ = chardate,
                             CHARCD = 150,
                             CHARNAME = "Доля площади МОП",
                             VALUE_ = mopValue
                         });
                     }
                }
            },
                () =>
                {
                    if (ПроряжатьХарактеристики) lc = CharsRecordUtils.ThinOutList(lc, true);
                    RemoveChars(lc);
                },
            RsnFilePath, false, ConvertingDates, WithRsn3);

            StepStart(lc.Count);
            long lastLs = -1;
            int? lastChar = -1;
            for (int i = 0; i < lc.Count; i++)
            {
                var chr = lc[i];
                if (chr.VALUE_ == 0 && (chr.SortLshet != lastLs || chr.CHARCD != lastChar))
                {
                    lc[i] = null;
                }
                else
                {
                    lastLs = chr.SortLshet;
                    lastChar = chr.CHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lc.RemoveAll(c => c == null);
            StepFinish();

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lc);

            FreeListMemory(lc);
        }

        private void RemoveChars(List<CNV_CHAR> lc)
        {
            //return;
            int?[] needChars = {1, 2};
            lc.RemoveAll(c => !needChars.Contains(c.CHARCD));
        }
    }

    public class ConvertLchars : KvcConvertCase
    {
        public ConvertLchars()
        {
            ConvertCaseName = "LCHAR - качественные характеристики";
            Position = 39;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            
            SetStepsCount(5 - 1 + ConvertingDates.Length);
            //BufferEntitiesManager.DropTableData("CNV$LCHARS");

            var regimRecode = new Dictionary<byte, RegimLogicValues[]>();
            StepStart(1);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<RegimLogicValuesTable>($@"select rgr.resourceid as ResourceId, rrl.name as RegimName, rl.kodregim as RegimId, rl.kod as LcharId, rl.significance as LcharValue from regimlist_logicvalues rl
                                                                            inner join resourcesregimslist rrl on rrl.kodregim = rl.kodregim
                                                                            inner join RESOURCESGROUPSREGIMSLIST rgr on rgr.kod = rrl.kodresourcesgroupsregimslist
                                                                            order by rgr.resourceid");
                var groupedByResource = sqlResult.GroupBy(r => r.ResourceId).ToArray();
                foreach (var groupByResource in groupedByResource)
                {
                    var groupedByRegim = groupByResource.GroupBy(gr => gr.RegimId).ToArray();
                    var regimLogicValues = new RegimLogicValues[groupedByRegim.Length];
                    for (int j = 0; j < groupedByRegim.Length; j++)
                    {
                        var groupByRegim = groupedByRegim[j];
                        regimLogicValues[j] = new RegimLogicValues
                        {
                            RegimId = groupByRegim.First().RegimId,
                            RegimName = groupByRegim.First().RegimName,
                            LcharLogicValues = groupByRegim.GroupBy(gr => gr.LcharId).Select(gr => new LogicValue(gr)).ToArray()
                        };
                    }

                    var recode = resourceRecode.FirstOrDefault(rr => rr.Value == groupByResource.FirstOrDefault()?.ResourceId).Key;
                    if (recode != 0) regimRecode.Add(recode,regimLogicValues);
                }
            }
            StepFinish();

            var lc = new List<CNV_LCHAR>();
            var recodeTable = Recode.GetRecodeTable(CharseRecodeFilePath, "LCHARS");
            var allLcharIds = recodeTable.Where(r => r.LcharId < 100).Select(r => r.LcharId).Distinct().ToArray();
            var tempAllLchars = allLcharIds.ToList();
            tempAllLchars.AddRange(new[] { 1, 12, 37, 46, 47, 48, 49, 50, 51, 52, 53, 54, 99 });
            allLcharIds = tempAllLchars.ToArray();
            var animalsCodes = new[] { 200, 202, 201, 205, 203, 204 };
            ConvertRsnFiles(rsnAbonent =>
            {
                var tempAbonentLchars = new List<CNV_LCHAR>();
                var chardate = CurrnetConvertDate;
                string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                long intlshet = Int64.Parse(lshet);

                foreach (var recode in recodeTable)
                {
                    decimal checkField = 0;
                    bool allConditionTrue = false;
                    for (int j = 0; j < recode.ConditionFunc.Length; j++)
                    {
                        switch (recode.CheckField[j])
                        {
                            case Recode.CheckFieldEnum.Tarif:
                                var tarif = rsnAbonent.Тарифы.FirstOrDefault(t => t.Вид == recode.Vid);
                                checkField = tarif != null ? tarif.Значение : 0m;
                                break;
                            case Recode.CheckFieldEnum.Norm1:
                                var norm1 = rsnAbonent.Нормативы.FirstOrDefault(n => n.Вид == recode.Vid);
                                checkField = norm1 != null ? norm1.Значение1 : 0m;
                                break;
                            case Recode.CheckFieldEnum.Norm2:
                                var norm2 = rsnAbonent.Нормативы.FirstOrDefault(n => n.Вид == recode.Vid);
                                checkField = norm2 != null ? norm2.Значение2 : 0m;
                                break;
                            case Recode.CheckFieldEnum.AlgId:
                                var alg = rsnAbonent.Алгоритмы.FirstOrDefault(a => a.Вид == recode.Vid);
                                checkField = alg != null ? alg.Алгоритм : 0m;
                                break;
                            case Recode.CheckFieldEnum.Pknach:
                                var pknach = rsnAbonent.НачисленияПоНормативам.FirstOrDefault(n => n.Вид == recode.Vid && n.Коэффециент > 0);
                                checkField = pknach != null ? pknach.Коэффециент : 0m;
                                break;
                            case Recode.CheckFieldEnum.Nachsum:
                                var nach = rsnAbonent.НачисленияПоНормативам.Select(n => new { Vid = n.Вид, Sum = n.Значение }).Union(rsnAbonent.НачисленияПоСчетчикам.Select(n => new { Vid = n.Вид, Sum = n.Сумма })).FirstOrDefault(n => n.Vid == recode.Vid);
                                checkField = nach != null ? nach.Sum : 0m;
                                break;
                            case Recode.CheckFieldEnum.Livcount:
                                checkField = rsnAbonent.ЧислоПроживающих;
                                break;
                            case Recode.CheckFieldEnum.Sumanimals:
                                checkField = rsnAbonent.ПараметрыУчастка.Where(pu => animalsCodes.Contains(pu.КодПараметра)).Sum(pu => pu.Значение);
                                break;
                            case Recode.CheckFieldEnum.Ownerid:
                                var algOwn = rsnAbonent.Алгоритмы.FirstOrDefault(a => a.Вид == recode.Vid);
                                checkField = algOwn != null ? OwnersRecode[recode.Vid][algOwn.ХозяинВида] : -1m;
                                break;
                            case Recode.CheckFieldEnum.Regcount:
                                checkField = rsnAbonent.КоличествоПМЖ + rsnAbonent.КоличествоВо + rsnAbonent.КоличествоПМП;
                                break;
                            case Recode.CheckFieldEnum.Stavka:
                                var cnt = rsnAbonent.УЗСнаНачМес.FirstOrDefault(c => c.Вид == recode.Vid);
                                if (cnt == null)
                                    checkField = 0m;
                                else
                                {
                                    if (cnt.Счетчик.Код <= 19) checkField = 1;
                                    if (cnt.Счетчик.Код >= 21 && cnt.Счетчик.Код <= 28) checkField = 2;
                                    if (cnt.Счетчик.Код >= 31 && cnt.Счетчик.Код <= 39) checkField = 3;
                                }
                                break;
                            case Recode.CheckFieldEnum.Lchar:
                                var lchar = tempAbonentLchars.FirstOrDefault(l => l.LCHARCD == recode.LcharsToCheck[j]);
                                checkField = lchar != null ? (int)lchar.VALUE_ : 0m;
                                break;
                            case Recode.CheckFieldEnum.Polivsqr:
                                var polPlo = rsnAbonent.ПлощадиНормативы.FirstOrDefault(p => p.Вид == 5);
                                checkField = polPlo != null ? polPlo.Значение1 : 0m;
                                break;
                            case Recode.CheckFieldEnum.None:
                                break;
                        }
                        allConditionTrue = recode.ConditionFunc[j](checkField);
                        if (!allConditionTrue) break;
                    }
                    if (allConditionTrue)
                    {
                        int val = 0;
                        switch (recode.ValueType)
                        {
                            case Recode.ValueTypeEnum.RecodeValue:
                                val = recode.Value;
                                break;
                            case Recode.ValueTypeEnum.OwnerId:
                                val = OwnersRecode[recode.Vid][rsnAbonent.Алгоритмы.First(a => a.Вид == recode.Vid).ХозяинВида];
                                break;
                        }

                        tempAbonentLchars.Add(new CNV_LCHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            LCHARCD = recode.LcharId,
                            VALUE_ = val
                        });
                    }
                }

                int value = 0;
                switch (rsnAbonent.ХарактеристикаКвартиры)
                {
                    case 0:
                        value = 0;
                        break;
                    case 6:
                    case 9:
                        value = 1;
                        break;
                    case 8:
                    case 10:
                        value = 2;
                        break;
                    case 11:
                    case 13:
                        value = 3;
                        break;
                    case 12:
                    case 14:
                        value = 4;
                        break;
                    case 15:
                    case 16:
                        value = 5;
                        break;
                    case 17:
                    case 18:
                        value = 6;
                        break;
                    case 19:
                        value = 7;
                        break;
                    case 20:
                        value = 8;
                        break;
                }
                tempAbonentLchars.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    LCHARCD = 1,
                    VALUE_ = value
                });

                tempAbonentLchars.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    LCHARCD = 37,
                    VALUE_ = rsnAbonent.ФормаСобственностиЖилогоПомещения == 0 ? 6 : rsnAbonent.ФормаСобственностиЖилогоПомещения - 1
                });

                tempAbonentLchars.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    LCHARCD = 52,
                    VALUE_ = rsnAbonent.ВидЖилогоПомещения == 0 ? 6 : (rsnAbonent.ВидЖилогоПомещения - 1)
                });
                tempAbonentLchars.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = intlshet,
                    DATE_ = chardate,
                    LCHARCD = 53,
                    VALUE_ = rsnAbonent.ТипПомещения
                });

                var pools = rsnAbonent.ПараметрыУчастка.Where(p => p.КодПараметра >= 206 && p.КодПараметра <= 214 && p.Значение > 0).ToArray();
                bool poolCounter = rsnAbonent.УЗСнаНачМес.Any(c => c.Вид == 5 && (c.Счетчик.Код == 9 || c.Счетчик.Код == 10));
                int poolLcharcd, poolLcharvalue, poolCounterLcharcd;
                foreach (var pool in pools)
                {
                    poolLcharcd = 0;
                    poolLcharvalue = 0;
                    poolCounterLcharcd = 0;
                    switch (pool.КодПараметра)
                    {
                        case 206:
                            poolLcharcd = 48;
                            poolLcharvalue = 1;
                            poolCounterLcharcd = 51;
                            break;
                        case 207:
                            poolLcharcd = 48;
                            poolLcharvalue = 2;
                            poolCounterLcharcd = 51;
                            break;
                        case 208:
                            poolLcharcd = 46;
                            poolLcharvalue = 1;
                            poolCounterLcharcd = 49;
                            break;
                        case 209:
                            poolLcharcd = 46;
                            poolLcharvalue = 2;
                            poolCounterLcharcd = 49;
                            break;
                        case 210:
                            poolLcharcd = 46;
                            poolLcharvalue = 3;
                            poolCounterLcharcd = 49;
                            break;
                        case 211:
                            poolLcharcd = 46;
                            poolLcharvalue = 4;
                            poolCounterLcharcd = 49;
                            break;
                        case 212:
                            poolLcharcd = 47;
                            poolLcharvalue = 1;
                            poolCounterLcharcd = 50;
                            break;
                        case 213:
                            poolLcharcd = 47;
                            poolLcharvalue = 3;
                            poolCounterLcharcd = 50;
                            break;
                        case 214:
                            poolLcharcd = 47;
                            poolLcharvalue = 4;
                            poolCounterLcharcd = 50;
                            break;
                    }
                    tempAbonentLchars.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        SortLshet = intlshet,
                        DATE_ = chardate,
                        LCHARCD = poolLcharcd,
                        VALUE_ = poolLcharvalue
                    });
                    if (poolCounter)
                        tempAbonentLchars.Add(new CNV_LCHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            LCHARCD = poolCounterLcharcd,
                            VALUE_ = 1
                        });
                }

                for (int j = 0; j < allLcharIds.Length; j++)
                {
                    var lcharid = allLcharIds[j];
                    bool founded = false;
                    for (int k = 0; k < tempAbonentLchars.Count; k++)
                    {
                        if (tempAbonentLchars[k].LCHARCD == lcharid)
                        {
                            founded = true;
                            break;
                        }
                    }
                    if (!founded)
                        tempAbonentLchars.Add(new CNV_LCHAR
                        {
                            LSHET = lshet,
                            SortLshet = intlshet,
                            DATE_ = chardate,
                            LCHARCD = lcharid,
                            VALUE_ = 0
                        });
                }

                 lc.AddRange(tempAbonentLchars);
                
                var abonentRegims = new Dictionary<byte, int>();
                var abonentVids = rsnAbonent.Алгоритмы
                    .Select(al => al.Вид)
                    .Where(v => !RsnHelper.PeniResources.Contains(v))
                    .ToArray();
                foreach (var vid in abonentVids)
                {
                    RegimLogicValues[] regimsByVid;
                    if (!regimRecode.TryGetValue(vid, out regimsByVid)) regimsByVid = new RegimLogicValues[0];
                    int regimCd = 10;
                    foreach (var regim in regimsByVid)
                    {
                        var allLchars = regim.LcharLogicValues.Select(l => l.LcharId).ToArray();
                        var abonentLchars = tempAbonentLchars.Where(l => allLchars.Contains((int)l.LCHARCD)).ToDictionary(l => l.LCHARCD, l => l.VALUE_);
                        bool allMatch = true;
                        foreach (var lchar in regim.LcharLogicValues)
                        {
                            if (!abonentLchars.ContainsKey(lchar.LcharId))
                            {
                                allMatch = false;
                                break;
                            }
                            bool anyInLchar = false;
                            foreach (var lvalue in lchar.Values)
                            {
                                if (lvalue == abonentLchars[lchar.LcharId].Value)
                                {
                                    anyInLchar = true;
                                    break;
                                }
                            }
                            if (!anyInLchar)
                            {
                                allMatch = false;
                                break;
                            }
                        }
                        if (allMatch)
                        {
                            regimCd = regim.RegimId;
                            break;
                        }
                    }
                    abonentRegims.Add(vid, regimCd);
                }

                Dictionary<DateTime, Dictionary<byte, int>> dateRegimRecode;
                if (!RegimRecode.TryGetValue(rsnAbonent.LsKvc.Ls, out dateRegimRecode))
                {
                    dateRegimRecode = new Dictionary<DateTime, Dictionary<byte, int>>();
                    RegimRecode.Add(rsnAbonent.LsKvc.Ls, dateRegimRecode);
                }
                dateRegimRecode.Add(CurrnetConvertDate, abonentRegims);
            },
            () =>
            {
                if (ПроряжатьХарактеристики) lc = LcharsRecordUtils.ThinOutList(lc, true);
                RemoveChars(lc);
            },
            RsnFilePath, false, ConvertingDates, WithRsn3);

            StepStart(lc.Count);
            long lastLs = -1;
            int? lastChar = -1;
            for (int i = 0; i < lc.Count; i++)
            {
                var chr = lc[i];
                if (chr.VALUE_ == 0 && (chr.SortLshet != lastLs || chr.LCHARCD != lastChar))
                {
                    lc[i] = null;
                }
                else
                {
                    lastLs = chr.SortLshet;
                    lastChar = chr.LCHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lc.RemoveAll(c => c == null);
            StepFinish();

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lc);

            FreeListMemory(lc);
        }

        private void RemoveChars(List<CNV_LCHAR> lc)
        {
            int?[] needChars = {101, 1, 37};
            lc.RemoveAll(c => !needChars.Contains(c.LCHARCD));
        }

        public override void ActionAfterConvert()
        {
            if (WithRsn3)
            {
                var lsInHouse = new Dictionary<string, int>();

                using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
                {
                    var sqlResult = context.ExecuteQuery<ConvertHChars.HouseExt>(@"select HOUSECD, EXTLSHET from cnv$abonent
                                                                union
                                                                select housecd, extlshet from abonents a
                                                                inner join extorgaccounts ea on ea.lshet = a.lshet");
                    StepStart(sqlResult.Count);
                    for (int i = 0; i < sqlResult.Count; i++)
                    {
                        var house = sqlResult[i];
                        lsInHouse.Add(house.EXTLSHET, sqlResult.Count(s => s.HOUSECD == house.HOUSECD));
                        Iterate();
                    }
                    StepFinish();
                }

                var llc = new List<CNV_LCHAR>();
                var lchardate = new DateTime(NextYearToConvert, NextMonthToConvert, 1);
                ConvertRsnAbonent(rsnAbonent =>
                {
                   if (rsnAbonent.Округ > 4) return;
                   if (rsnAbonent.Алгоритмы.Any(al => al.Вид == 16 && al.ХозяинВида > 0)) return;
                   if (lsInHouse[rsnAbonent.LsKvc.CombinedLs] < 2) return;

                    string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                   llc.Add(new CNV_LCHAR
                   {
                       LSHET = lshet,
                       SortLshet = long.Parse(lshet),
                       DATE_ = lchardate,
                       LCHARCD = 101,
                       VALUE_ = 1651
                   });

                }, NextYearToConvert, NextMonthToConvert, false, true);

                //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(llc, InsertRecordCount, false);
                if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(llc);
            }
        }

        public class Recode
        {
            public byte Vid;
            public CheckFieldEnum[] CheckField;
            public Func<decimal, bool>[] ConditionFunc;
            public int[] LcharsToCheck;
            public int LcharId;
            public ValueTypeEnum ValueType;
            public int Value;

            private static readonly Regex ConditionRegex = new Regex(@"(?<field>[\w\(\)\d]+)\s*(?<operator><>|>=|<=|>|<|=)\s*(?<checkval>[\d\,]+)", RegexOptions.IgnoreCase);
            private static readonly Regex LcharCheckRegex = new Regex(@"lchar\((\d+)\)");
            public Recode(DataRow dr)
            {
                Vid = Byte.Parse(dr[0].ToString());
                if (dr[1].ToString().Trim().ToLower() == "true")
                {
                    CheckField = new[] { CheckFieldEnum.None };
                    ConditionFunc = new Func<decimal, bool>[] { check => true };
                }
                else
                {
                    string condition = dr[1].ToString();
                    var allConditions = condition.Split(';');
                    CheckField = new CheckFieldEnum[allConditions.Length];
                    ConditionFunc = new Func<decimal, bool>[allConditions.Length];
                    LcharsToCheck = new int[allConditions.Length];
                    for (int i = 0; i < allConditions.Length; i++)
                    {
                        var match = ConditionRegex.Match(allConditions[i]);
                        if (!match.Success) throw new Exception($"Не удалось распарсить условие {dr[1]}");
                        LcharsToCheck[i] = 0;
                        var checkField = match.Groups["field"].Value.Trim().ToLower();
                        switch (checkField)
                        {
                            case "tarif":
                                CheckField[i] = CheckFieldEnum.Tarif;
                                break;
                            case "norm1":
                                CheckField[i] = CheckFieldEnum.Norm1;
                                break;
                            case "norm2":
                                CheckField[i] = CheckFieldEnum.Norm2;
                                break;
                            case "algid":
                                CheckField[i] = CheckFieldEnum.AlgId;
                                break;
                            case "pknach":
                                CheckField[i] = CheckFieldEnum.Pknach;
                                break;
                            case "nachsum":
                                CheckField[i] = CheckFieldEnum.Nachsum;
                                break;
                            case "livcount":
                                CheckField[i] = CheckFieldEnum.Livcount;
                                break;
                            case "sumanimals":
                                CheckField[i] = CheckFieldEnum.Sumanimals;
                                break;
                            case "ownerid":
                                CheckField[i] = CheckFieldEnum.Ownerid;
                                break;
                            case "regcount":
                                CheckField[i] = CheckFieldEnum.Regcount;
                                break;
                            case "stavka":
                                CheckField[i] = CheckFieldEnum.Stavka;
                                break;
                            case "polivsqr":
                                CheckField[i] = CheckFieldEnum.Polivsqr;
                                break;
                            default:
                                if (checkField.Contains("lchar"))
                                {
                                    LcharsToCheck[i] = int.Parse(LcharCheckRegex.Match(checkField).Groups[1].Value);
                                    CheckField[i] = CheckFieldEnum.Lchar;
                                    break;
                                }
                                throw new Exception($"Необработанное поле для проверки {match.Groups["field"].Value}");
                        }
                        var checkVal = Decimal.Parse(match.Groups["checkval"].Value);
                        switch (match.Groups["operator"].Value)
                        {
                            case "=":
                                ConditionFunc[i] = check => Math.Abs(check - checkVal) < 0.001m;
                                break;
                            case ">":
                                ConditionFunc[i] = check => check > checkVal;
                                break;
                            case "<":
                                ConditionFunc[i] = check => check < checkVal;
                                break;
                            case ">=":
                                ConditionFunc[i] = check => check >= checkVal;
                                break;
                            case "<=":
                                ConditionFunc[i] = check => check <= checkVal;
                                break;
                            case "<>":
                                ConditionFunc[i] = check => Math.Abs(check - checkVal) > 0.001m;
                                break;
                            default:
                                throw new Exception($"Необработанный оператор {match.Groups["operator"].Value}");
                        }
                    }
                }
                LcharId = Int32.Parse(dr[2].ToString());
                string valueStr = dr[3].ToString();
                if (!Int32.TryParse(valueStr, out Value))
                {
                    switch (valueStr.Trim().ToLower())
                    {
                        case "ownerid":
                            ValueType = ValueTypeEnum.OwnerId;
                            break;
                        default:
                            throw new Exception($"Необработанный тип значения {valueStr}");
                    }
                }
                else ValueType = ValueTypeEnum.RecodeValue;
            }

            public static Recode[] GetRecodeTable(string excelFileName, string sheetList)
            {
                DataTable dt = Utils.ReadExcelFile(excelFileName, sheetList);
                var tempList = new List<Recode>();
                foreach (DataRow dr in dt.Rows)
                {
                    tempList.Add(new Recode(dr));
                }
                return tempList.ToArray();
            }

            public enum CheckFieldEnum
            {
                None,
                Tarif,
                Norm1,
                Norm2,
                AlgId,
                Pknach,
                Nachsum,
                Livcount,
                Sumanimals,
                Ownerid,
                Regcount,
                Stavka,
                Lchar,
                Polivsqr
            }

            public enum ValueTypeEnum
            {
                RecodeValue,
                OwnerId
            }
        }

        private class RegimLogicValuesTable
        {
            public int ResourceId { get; set; }
            public int RegimId { get; set; }
            public string RegimName { get; set; }
            public int LcharId { get; set; }
            public int LcharValue { get; set; }
        }

        private class RegimLogicValues
        {
            public int RegimId;
            public string RegimName;
            public LogicValue[] LcharLogicValues;
        }

        private class LogicValue
        {
            public int LcharId;
            public int[] Values;

            public LogicValue() { }

            public LogicValue(IGrouping<int, RegimLogicValuesTable> r)
            {
                LcharId = r.Key;
                Values = r.Select(l => l.LcharValue).ToArray();
            }
        }
    }

    public class ConvertOtopl : KvcConvertCase
    {
        public ConvertOtopl()
        {
            ConvertCaseName = "Отключение отопления";
            Position = 40;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(2 + ConvertingDates.Length);
            var lc = new List<CNV_LCHAR>();
            ConvertRsnFiles(rsnAbonent =>
            {
                var days = DateTime.DaysInMonth(CurrnetConvertDate.Year, CurrnetConvertDate.Month);
                if (rsnAbonent.КолвоДнейСОтоплением >= days) return;
                string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                lc.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = Int64.Parse(lshet),
                    LCHARCD = 45,
                    VALUE_ = 0,
                    DATE_ = CurrnetConvertDate.AddDays(rsnAbonent.КолвоДнейСОтоплением)
                });
            }, null, RsnFilePath, false, new[] {new DateTime(2017, 04, 01)}, WithRsn3);

            lc = LcharsRecordUtils.ThinOutList(lc, true);

            //SaveListInsertSQL(lc, InsertRecordCount);
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
        }
    }

    public class ConvertCounters : KvcConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - информация о счетчиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            
            SetStepsCount(2 - 1 + ConvertingDates.Length);
            var lcc = new List<CNV_COUNTER>();
            var vids = spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var cntSpr = spr4.GetSubSpr(Spr4.SubSpr.КодСчетчика);

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                ConvertUniqueRsnAbonents(rsnAbonent =>
                {
                    int counterCount = rsnAbonent.УЗСнаНачМес.Count;
                    string lset = LsDic[rsnAbonent.LsKvc.Ls];
                    for (int j = 0; j < counterCount; j++)
                    {
                        var prevInd = rsnAbonent.УЗСнаНачМес[j];

                        int cntType, setupPlace;
                        ConvertCounterType(prevInd.Вид, prevInd.Счетчик, out cntType, out setupPlace);
                        int? nocalcchildbalances = null;
                        var cntFromSpr = cntSpr.FirstOrDefault(cnt => cnt.R1 == prevInd.Вид && cnt.R2 == prevInd.Счетчик.Код);
                        if (cntFromSpr != null && cntFromSpr.R4 == 0) nocalcchildbalances = 1;
                        else
                        {
                            var vodootvedAlg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == 17);
                            if (vodootvedAlg != null)
                            {
                                if (prevInd.Вид == 07 && (vodootvedAlg.Алгоритм == 120 || vodootvedAlg.Алгоритм == 10)) nocalcchildbalances = 1;
                                else if (prevInd.Вид == 05 && (vodootvedAlg.Алгоритм == 102 || vodootvedAlg.Алгоритм == 01)) nocalcchildbalances = 1;
                            }
                        }
                        var counter = new CNV_COUNTER
                        {
                            LSHET = lset,
                            COUNTERID = $"{lset.Substring(2, 6)}{prevInd.Вид:D2}{prevInd.Счетчик.Код:D2}",
                            //SETUPDATE = counterInfo.SetupDate,
                            //SERIALNUM = counterInfo.SerialNumber,
                            CNTTYPE = cntType,
                            SETUPPLACE = setupPlace,
                            COUNTER_LEVEL = 0,
                            NAME = $"Сч. {vids.Single(v => v.R1 == prevInd.Вид).Sr40} {prevInd.Счетчик.Код}",
                            NOCALCCHILDBALANCES = nocalcchildbalances
                            // коэф перерасчета
                        };

                        if (prevInd.Вид == 3)
                        {
                            if (prevInd.Счетчик.Код >= 21 && prevInd.Счетчик.Код <= 39)
                            {
                                var lchar12Value = context.ExecuteScalar<int?>($"Select lh.value_ from cnv$lchars lh where lh.lshet = '{lset}' and lh.lcharcd = 12 and lh.date_ = (select max(l1.date_) from cnv$lchars l1 where l1.lshet = '{lset}' and l1.lcharcd = 12)");
                                if (lchar12Value.HasValue)
                                {
                                    if (prevInd.Счетчик.Код >= 21 && prevInd.Счетчик.Код <= 28)
                                    { 
                                        switch (prevInd.Счетчик.Код)
                                        {
                                            case 21:
                                            case 23:
                                            case 25:
                                            case 27:
                                                switch (lchar12Value.Value)
                                                {
                                                    case 5: counter.KODREGIM = 9001; break;
                                                    case 7: counter.KODREGIM = 9002; break;
                                                    case 9: counter.KODREGIM = 9003; break;
                                                }
                                                break;

                                            case 22:
                                            case 24:
                                            case 26:
                                            case 28:
                                                switch (lchar12Value.Value)
                                                {
                                                    case 5: counter.KODREGIM = 9005; break;
                                                    case 7: counter.KODREGIM = 9007; break;
                                                    case 9: counter.KODREGIM = 9009; break;
                                                }
                                                break;
                                        }
                                    }
                                    else if (prevInd.Счетчик.Код >= 31 && prevInd.Счетчик.Код <= 39)
                                    {
                                        switch (prevInd.Счетчик.Код)
                                        {
                                            case 32:
                                            case 35:
                                            case 38:
                                                switch (lchar12Value.Value)
                                                {
                                                    case 6: counter.KODREGIM = 9001; break;
                                                    case 8: counter.KODREGIM = 9002; break;
                                                    case 10: counter.KODREGIM = 9003; break;
                                                }
                                                break;

                                            case 33:
                                            case 36:
                                            case 39:
                                                switch (lchar12Value.Value)
                                                {
                                                    case 6: counter.KODREGIM = 9005; break;
                                                    case 8: counter.KODREGIM = 9007; break;
                                                    case 10: counter.KODREGIM = 9009; break;
                                                }
                                                break;

                                            case 31:
                                            case 34:
                                            case 37:
                                                switch (lchar12Value.Value)
                                                {
                                                    case 6: counter.KODREGIM = 9006; break;
                                                    case 8: counter.KODREGIM = 9008; break;
                                                    case 10: counter.KODREGIM = 9010; break;
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        lcc.Add(counter);
                    }
                }, RsnFilePath, LsNotInLastFile, true, ConvertingDates, WithRsn3);
            }
            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcc);

            FreeListMemory(lcc);
        }

        public static void ConvertCounterType(byte vid, RsnCounterKodRaz counter, out int cnttype, out int setupplace)
        {
            ConvertCounterType(vid, counter.Код, counter.Разрядность, out cnttype, out setupplace);
        }

        public static void ConvertCounterType(byte vid, byte cntKod, byte cntRazryad, out int cnttype, out int setupplace)
        {
            switch (vid)
            {
                case 03:
                    switch (cntRazryad)
                    {
                        case 0:
                            cnttype = 117;
                            break;
                        case 4:
                            cnttype = 105;
                            break;
                        case 5:
                            cnttype = 106;
                            break;
                        case 6:
                            cnttype = 107;
                            break;
                        case 7:
                            cnttype = 108;
                            break;
                        default:
                            throw new Exception($"Необработанная разрядность {cntRazryad} для считчика вида 03");
                    }
                    switch (cntKod)
                    {
                        //case 21:
                        //    setupplace = 121;
                        //    break;
                        //case 22:
                        //    setupplace = 122;
                        //    break;
                        default:
                            setupplace = 3;
                            break;
                    }
                    break;
                case 05:
                    switch (cntRazryad)
                    {
                        case 0:
                            cnttype = 118;
                            break;
                        case 3:
                            cnttype = 109;
                            break;
                        case 5:
                            cnttype = 110;
                            break;
                        case 6:
                            cnttype = 111;
                            break;
                        case 7:
                            cnttype = 112;
                            break;
                        default:
                            throw new Exception($"Необработанная разрядность {cntRazryad} для считчика вида 05");
                    }
                    switch (cntKod)
                    {
                        case 1:
                            setupplace = 101;
                            break;
                        case 2:
                            setupplace = 102;
                            break;
                        case 3:
                            setupplace = 103;
                            break;
                        case 5:
                            setupplace = 105;
                            break;
                        case 9:
                        case 10:
                            setupplace = 110;
                            break;
                        case 11:
                            setupplace = 111;
                            break;
                        case 12:
                            setupplace = 112;
                            break;
                        default:
                            setupplace = 3;
                            break;
                    }
                    break;
                case 07:
                    switch (cntRazryad)
                    {
                        case 0:
                            cnttype = 119;
                            break;
                        case 3:
                            cnttype = 113;
                            break;
                        case 5:
                            cnttype = 114;
                            break;
                        case 6:
                            cnttype = 115;
                            break;
                        case 7:
                            cnttype = 116;
                            break;
                        default:
                            throw new Exception($"Необработанная разрядность {cntRazryad} для считчика вида 07");
                    }
                    switch (cntKod)
                    {
                        case 1:
                            setupplace = 101;
                            break;
                        case 2:
                            setupplace = 102;
                            break;
                        case 3:
                            setupplace = 103;
                            break;
                        default:
                            setupplace = 3;
                            break;
                    }
                    break;
                default:
                    throw new Exception($"Необработанный вид {vid} для определения типа счетчика");
            }
        }
    }

    public class ConvertCntinds : KvcConvertCase
    {
        public ConvertCntinds()
        {
            ConvertCaseName = "CNTINDS - информация о показаниях";
            Position = 41;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            
            SetStepsCount(2 - 1 + ConvertingDates.Length);
            var lci = new List<CNV_CNTRSIND>();

            ConvertRsnFiles(rsnAbonent =>
            {
                int counterCount = rsnAbonent.УЗСнаНачМес.Count;
                string lset = LsDic[rsnAbonent.LsKvc.Ls];
                string shortLs = lset.Substring(2, 6);
                string shortDate = $"{int.Parse(CurrnetConvertDate.Year.ToString().Substring(2, 2)):D2}{CurrnetConvertDate.Month:D2}";
                for (int j = 0; j < counterCount; j++)
                {
                    var prevInd = rsnAbonent.УЗСнаНачМес[j];

                    decimal? oldind, volume, newind;
                    if (CurrnetConvertDate.Year == NextYearToConvert && CurrnetConvertDate.Month == NextMonthToConvert)
                    {
                        oldind = prevInd.Счетчик.Значение;
                        volume = 0;
                        newind = oldind;
                    }
                    else
                    {
                        oldind = prevInd.Счетчик.Значение;
                        volume = rsnAbonent.ПотребленияПоСчетчикам.FirstOrDefault(p => p.Вид == prevInd.Вид && p.КодСчетчика == prevInd.Счетчик.Код)?.Потребление;
                        newind = rsnAbonent.УЗСнаКонМес.First(ei => ei.Вид == prevInd.Вид && ei.Счетчик.Код == prevInd.Счетчик.Код).Счетчик.Значение;
                    }

                    var sorInf = rsnAbonent.ИстИнфПоСчетчикам.FirstOrDefault(inf => inf.Вид == prevInd.Вид && prevInd.Счетчик.Код == inf.КодСчетчика);
                    int? caseType = null;
                    if (sorInf != null)
                    {
                        if (sorInf.ИстИнф == 3) caseType = 3;
                        if (sorInf.ИстИнф == 4) caseType = 4;
                    }
                    string caseTypeId = caseType == null ? "0" : caseType.ToString();

                    var indication = new CNV_CNTRSIND
                    {
                        COUNTERID = $"{shortLs}{prevInd.Вид:D2}{prevInd.Счетчик.Код:D2}",
                        DOCUMENTCD = $"I{lset}{prevInd.Вид:D2}{prevInd.Счетчик.Код:D2}{shortDate}{caseTypeId}",
                        OLDIND = oldind,
                        OB_EM = volume,
                        INDICATION = newind,
                        INDDATE = rsnAbonent.FileDate,
                        INDTYPE = 0
                    };

                    lci.Add(indication);
                }
            }, null, RsnFilePath, false, ConvertingDates, WithRsn3);
            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lci, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lci);

            FreeListMemory(lci);
        }
    }

    public class ConvertLongTermCounters : KvcConvertCase
    {
        public ConvertLongTermCounters()
        {
            ConvertCaseName = "Долгосрочные заявки по счетчикам";
            Position = 42;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            var lcc = new List<CNV_CHAR>();
            var llc = new List<CNV_LCHAR>();

            var longInfo = Utils.ReadExcelFile(LongTermCounters, "Лист1");
            foreach (DataRow row in longInfo.Rows)
            {
                var lsKvc = new LsKvc(uint.Parse(row[0].ToString()), uint.Parse(row[1].ToString()));
                if (!ShouldConvert(lsKvc)) continue;
                byte vid = byte.Parse(row[3].ToString());
                byte counterKod = byte.Parse(row[4].ToString());
                int volume = int.Parse(row[5].ToString());
                DateTime startDate = DateTime.Parse(row[6].ToString());
                DateTime endDate = DateTime.Parse(row[7].ToString());
                string lshet = LsDic[lsKvc.Ls];
                long intlshet = Int64.Parse(lshet);

                int lcharcd = 0;
                switch (vid)
                {
                    case 3:
                        lcharcd = 10009;
                        break;
                    case 5:
                        lcharcd = 10004;
                        break;
                    case 7:
                        lcharcd = 10005;
                        break;
                }

                llc.Add(new CNV_LCHAR
                {
                    LCHARCD = lcharcd,
                    LSHET = lshet,
                    VALUE_ = 1,
                    DATE_ = startDate,
                    SortLshet = intlshet
                });
                llc.Add(new CNV_LCHAR
                {
                    LCHARCD = lcharcd,
                    LSHET = lshet,
                    VALUE_ = 0,
                    DATE_ = endDate.AddDays(1),
                    SortLshet = intlshet
                });
                lcc.Add(new CNV_CHAR
                {
                    CHARCD = lcharcd,
                    LSHET = lshet,
                    VALUE_ = volume,
                    DATE_ = startDate,
                    SortLshet = intlshet
                });
                if (volume != 0)
                    lcc.Add(new CNV_CHAR
                    {
                        CHARCD = lcharcd,
                        LSHET = lshet,
                        VALUE_ = 0,
                        DATE_ = endDate.AddDays(1),
                        SortLshet = intlshet
                    });
            }

            // TODO временно
            /*lcc = lcc.GroupBy(c => new {c.LSHET, c.CHARCD, c.DATE_}).Select(g => g.Last()).ToList();
            llc = llc.GroupBy(c => new {c.LSHET, c.LCHARCD, c.DATE_}).Select(g => g.Last()).ToList();


            lcc = CharsRecordUtils.ThinOutList(lcc, true);
            llc = LcharsRecordUtils.ThinOutList(llc, true);

            if (ИнсертитьВоВременныеТаблицы)
            {
                SaveListInsertSQL(lcc, InsertRecordCount);
                SaveListInsertSQL(llc, InsertRecordCount);
            }*/

            FreeListMemory(lcc);
            FreeListMemory(llc);
        }
    }

    public class ConvertNachopl : KvcConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL - история оплат и начислений";
            Position = 50;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            //SetStepsCount(5 - 3 + ConvertingDates.Length * 3 * 2);
            SetStepsCount(5 * ConvertingDates.Length + 1);

            var banks = spr2.GetSubSpr(Spr2.SubSpr.Банки).Where(b => !String.IsNullOrEmpty(b.Sr20)).ToArray();
            var lnach = new List<CNV_NACH>();
            var lopl = new List<CNV_OPLATA>();
            var lsaldo = new List<CNV_NACHOPL>();
            ConvertRsnFiles(rsnAbonent =>
            {
                string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                var regimRecode = RegimRecode[rsnAbonent.LsKvc.Ls][CurrnetConvertDate];
                string shortDate = $"{int.Parse(CurrnetConvertDate.Year.ToString().Substring(2, 2)):D2}{CurrnetConvertDate.Month:D2}";
                for (int j = 0; j < rsnAbonent.НачисленияПоСчетчикам.Count; j++)
                {
                    var nach = rsnAbonent.НачисленияПоСчетчикам[j];
                    if (RsnHelper.PeniResources.Contains(nach.Вид)) continue;
                    var vol = rsnAbonent.ПотребленияПоСчетчикам.FirstOrDefault(p => p.Вид == nach.Вид && nach.КодСчетчика == p.КодСчетчика)?.Потребление;
                    var sorInf = rsnAbonent.ИстИнфПоСчетчикам.FirstOrDefault(inf => inf.Вид == nach.Вид && nach.КодСчетчика == inf.КодСчетчика);
                    int? caseType = null;
                    if (sorInf != null)
                    {
                        if (sorInf.ИстИнф == 3) caseType = 3;
                        if (sorInf.ИстИнф == 4) caseType = 4;
                    }
                    string caseTypeId = caseType == null ? "0" : caseType.ToString();
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 1,
                        VTYPE_ = 2,
                        VOLUME = vol != null ? (decimal)vol : 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[nach.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(nach.Вид, rsnAbonent),
                        SERVICENAME = nach.Вид.ToString(),
                        LSHET = lshet,
                        FNATH = nach.Сумма,
                        PROCHL = 0,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = CurrnetConvertDate,
                        //DOCUMENTCD = $"N{shortDate}{caseTypeId}",
                        DOCUMENTCD = $"I{lshet}{nach.Вид:D2}{nach.КодСчетчика:D2}{shortDate}{caseTypeId}",
                        PROCHLVOLUME = 0,
                        AUTOUSE = 1,
                        CASETYPE = caseType
                    });
                }
                for (int j = 0; j < rsnAbonent.НачисленияПоНормативам.Count; j++)
                {
                    var nach = rsnAbonent.НачисленияПоНормативам[j];
                    if (RsnHelper.PeniResources.Contains(nach.Вид)) continue;
                    var vol = rsnAbonent.ПотреблениеПоНормативам.FirstOrDefault(p => p.Вид == nach.Вид)?.Значение; //TODO есть те, у кого два по одному виду
                    if (nach.Вид == 1 && CurrnetConvertDate.Year == 2017)
                    {
                        decimal totalKompSum = 0;
                        for (int k = 0; k < rsnAbonent.ТарифыСодЖилКомп.Count; k++)
                        {
                            var kompNach = rsnAbonent.ТарифыСодЖилКомп[k];
                            int kompNachVid = 10;
                            switch (kompNach.Вид)
                            {
                                case 103:
                                    kompNachVid = 2004;
                                    break;
                                case 105:
                                    kompNachVid = 2002;
                                    break;
                                case 107:
                                    kompNachVid = 2003;
                                    break;
                            }

                            lnach.Add(new CNV_NACH
                            {
                                TYPE_ = 1,
                                VTYPE_ = 2,
                                VOLUME = 0,
                                REGIMCD = kompNachVid,
                                REGIMNAME = "Неизвестен", //TODO
                                SERVICECD = CalcServiceCd(1, rsnAbonent),
                                SERVICENAME = kompNach.Вид.ToString(),
                                LSHET = lshet,
                                FNATH = kompNach.Начисление,
                                PROCHL = 0,
                                MONTH_ = rsnAbonent.FileMonth,
                                MONTH2 = rsnAbonent.FileMonth,
                                YEAR_ = rsnAbonent.FileYear,
                                YEAR2 = rsnAbonent.FileYear,
                                DATE_VV = CurrnetConvertDate,
                                DOCUMENTCD = $"N{shortDate}0",
                                PROCHLVOLUME = 0,
                                AUTOUSE = 1
                            });
                            totalKompSum += kompNach.Начисление;
                        }
                        lnach.Add(new CNV_NACH
                        {
                            TYPE_ = 1,
                            VTYPE_ = 2,
                            VOLUME = 0,
                            REGIMCD = 2001,
                            REGIMNAME = "Неизвестен", //TODO
                            SERVICECD = CalcServiceCd(nach.Вид, rsnAbonent),
                            SERVICENAME = nach.Вид.ToString(),
                            LSHET = lshet,
                            FNATH = Math.Round(nach.Значение - totalKompSum, 2),
                            PROCHL = 0,
                            MONTH_ = rsnAbonent.FileMonth,
                            MONTH2 = rsnAbonent.FileMonth,
                            YEAR_ = rsnAbonent.FileYear,
                            YEAR2 = rsnAbonent.FileYear,
                            DATE_VV = CurrnetConvertDate,
                            DOCUMENTCD = $"N{shortDate}0",
                            PROCHLVOLUME = 0,
                            AUTOUSE = 1
                        });
                    }
                    else
                    {
                        lnach.Add(new CNV_NACH
                        {
                            TYPE_ = 0,
                            VTYPE_ = 1,
                            VOLUME = vol != null ? (decimal) vol : 0,
                            //REGIMCD = 10, //TODO
                            REGIMCD = regimRecode[nach.Вид], //TODO
                            REGIMNAME = "Неизвестен", //TODO
                            SERVICECD = CalcServiceCd(nach.Вид, rsnAbonent),
                            SERVICENAME = nach.Вид.ToString(),
                            LSHET = lshet,
                            FNATH = nach.Значение,
                            PROCHL = 0,
                            MONTH_ = rsnAbonent.FileMonth,
                            MONTH2 = rsnAbonent.FileMonth,
                            YEAR_ = rsnAbonent.FileYear,
                            YEAR2 = rsnAbonent.FileYear,
                            DATE_VV = CurrnetConvertDate,
                            DOCUMENTCD = $"N{shortDate}0",
                            PROCHLVOLUME = 0,
                            AUTOUSE = 1,
                            //CASETYPE = 4
                        });
                    }
                }

                // КорректНачислПоСчетчикам ???? не дублируются ли они, нужно их отдельно вставлять?
                for (int j = 0; j < rsnAbonent.Суммы_автопрошлых.Count; j++)
                {
                    var pere = rsnAbonent.Суммы_автопрошлых[j];
                    if (RsnHelper.PeniResources.Contains(pere.Вид)) continue;
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[pere.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(pere.Вид, rsnAbonent),
                        SERVICENAME = pere.Вид.ToString(),
                        LSHET = lshet,
                        PROCHL = pere.Сумма,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = rsnAbonent.FileDate,
                        DOCUMENTCD = $"R{shortDate}0",
                        AUTOUSE = 1
                    });
                }
                for (int j = 0; j < rsnAbonent.Суммы_ручныхПрошлых.Count; j++)
                {
                    var pere = rsnAbonent.Суммы_ручныхПрошлых[j];
                    if (RsnHelper.PeniResources.Contains(pere.Вид)) continue;
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[pere.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(pere.Вид, rsnAbonent),
                        SERVICENAME = pere.Вид.ToString(),
                        LSHET = lshet,
                        PROCHL = pere.Сумма,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = rsnAbonent.FileDate,
                        DOCUMENTCD = $"R{shortDate}0",
                        AUTOUSE = 0
                    });
                }
                for (int j = 0; j < rsnAbonent.СписанныеСальдо.Count; j++)
                {
                    var pere = rsnAbonent.СписанныеСальдо[j];
                    if (RsnHelper.PeniResources.Contains(pere.Вид)) continue;
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[pere.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(pere.Вид, rsnAbonent),
                        SERVICENAME = pere.Вид.ToString(),
                        LSHET = lshet,
                        PROCHL = pere.Сумма,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = rsnAbonent.FileDate,
                        DOCUMENTCD = $"R{shortDate}0",
                    });
                }
                for (int j = 0; j < rsnAbonent.КорректПотребПоСчетчикам.Count; j++)
                {
                    var pere = rsnAbonent.КорректПотребПоСчетчикам[j];
                    if (RsnHelper.PeniResources.Contains(pere.Вид)) continue;
                    lnach.Add(new CNV_NACH
                    {
                        VTYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[pere.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(pere.Вид, rsnAbonent),
                        SERVICENAME = pere.Вид.ToString(),
                        LSHET = lshet,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = rsnAbonent.FileDate,
                        DOCUMENTCD = $"R{shortDate}0",
                        PROCHLVOLUME = pere.Потребление
                    });
                }
                for (int j = 0; j < rsnAbonent.КорректировкиОплат.Count; j++)
                {
                    var pere = rsnAbonent.КорректировкиОплат[j];
                    if (RsnHelper.PeniResources.Contains(pere.Вид)) continue;
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[pere.Вид],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(pere.Вид, rsnAbonent),
                        SERVICENAME = pere.Вид.ToString(),
                        LSHET = lshet,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        DATE_VV = rsnAbonent.FileDate,
                        DOCUMENTCD = $"R{shortDate}0",
                        PROCHL = pere.Сумма * -1
                    });
                }

                RemoveNach(lnach);

                var allSaldoVids = rsnAbonent.СальдоНаНачало.Select(s => s.Вид).Union(rsnAbonent.СальдоНаКонец.Select(s => s.Вид)).ToArray();

                for (int j = 0; j < allSaldoVids.Length; j++)
                {
                    var vid = allSaldoVids[j];
                    if (RsnHelper.PeniResources.Contains(vid)) continue;
                    var bsaldo = rsnAbonent.СальдоНаНачало.FirstOrDefault(s => s.Вид == vid);
                    var esaldo = rsnAbonent.СальдоНаКонец.FirstOrDefault(s => s.Вид == vid);
                    lsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        SERVICECD = CalcServiceCd(vid, rsnAbonent),
                        SERVICENAME = vid.ToString(),
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        BDEBET = bsaldo == null ? 0 : bsaldo.Сумма,
                        EDEBET = esaldo == null ? 0 : esaldo.Сумма
                    });
                }

                RemoveSaldo(lsaldo);
            }, () =>
            {
                //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lnach, InsertRecordCount);
                if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lnach);
                FreeListMemory(lnach);
                //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lsaldo, InsertRecordCount);
                if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lsaldo);
                FreeListMemory(lsaldo);

                string oplataFileName = SutFile + RsnHelper.GetShortDate(CurrnetConvertDate.Year, CurrnetConvertDate.Month);
                using (BinaryReader reader = new BinaryReader(File.OpenRead(oplataFileName), Encoding.GetEncoding(1251)))
                {
                    int totalCount = (int)(reader.BaseStream.Length / 39);
                    int j = 1;
                    reader.ReadBytes(39);
                    StepStart(totalCount + 1);
                    while (j < totalCount)
                    {
                        try
                        {
                            j++;
                            Iterate();
                            var sut = new SutRecord(reader.ReadBytes(39));
                            if (!ShouldConvert(sut.LsKvc) || !ShouldConvertInIteration(sut.LsKvc)) continue;
                            if (RsnHelper.PeniResources.Contains(sut.Вид)) continue;
                            string ls = LsDic[sut.LsKvc.Ls];

                            var sourcecd = $"{sut.БанкПП:D3}{sut.ПодразделениеПП:D3}";
                            string sourceName;
                            if (!BankRename.TryGetValue(sut.БанкПП, out sourceName))
                                sourceName = banks.SingleOrDefault(b => b.R1 == sut.БанкПП)?.Sr20;
                            if (String.IsNullOrWhiteSpace(sourceName))
                            {
                                sourceName = "Неизвестен";
                            }
                            lopl.Add(new CNV_OPLATA
                            {
                                SERVICECD = CalcServiceCd(sut.Вид, sut.ХозяинВида),
                                SERVICENAME = sut.Вид.ToString(),
                                SOURCECD = int.Parse(sourcecd),
                                SOURCENAME = $"{sourceName} п.{sut.ПодразделениеПП}",
                                LSHET = ls,
                                SUMMA = sut.Сумма,
                                MONTH_ = sut.ЗаМесяц,
                                YEAR_ = sut.ЗаГод,
                                DATE_ = sut.ДеньОплаты,
                                DATE_VV = sut.DateVv,
                                DOCUMENTCD = $"P{sut.DateVv.Year.ToString().Substring(2, 2)}{sut.DateVv.Month:D2}{sourcecd}"
                            });
                        }
                        catch (Exception ex)
                        {
                            new Task(()=>MessageBox.Show($"Ошибка при конвертации оплаты из файла {oplataFileName}\r\n{ex}")).Start();
                        }
                    }
                    StepFinish();
                }

                RemoveOplata(lopl);

                //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lopl, InsertRecordCount);
                if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lopl);
                FreeListMemory(lopl);

            }, RsnFilePath, false, ConvertingDates);

            if (WithRsn3 && false)
            {
                lnach = new List<CNV_NACH>();
                var pereForNextMonth = Utils.ReadExcelFile(PereForNextMonth, "Лист1");
                var nextDate = new DateTime(NextYearToConvert, NextMonthToConvert, 1);
                foreach (DataRow row in pereForNextMonth.Rows)
                {
                    string lsKvc = row[0].ToString();
                    string[] lsKvcParts = lsKvc.Split('-');
                    var lsKvcObj = new LsKvc(UInt32.Parse(lsKvcParts[0] + lsKvcParts[1] + lsKvcParts[2]),
                        UInt32.Parse(lsKvcParts[3] + lsKvcParts[4] + lsKvcParts[5]));
                    if (!ShouldConvert(lsKvcObj)) continue;
                    byte vid = byte.Parse(row[1].ToString());
                    if (RsnHelper.PeniResources.Contains(vid)) continue;
                    var regimRecode = RegimRecode[lsKvc][nextDate];
                    ushort owner = UInt16.Parse(row[2].ToString());
                    decimal sum = Decimal.Parse(row[3].ToString());
                    if (sum == 0) continue;
                    string lshet = LsDic[lsKvc];
                    lnach.Add(new CNV_NACH
                    {
                        TYPE_ = 0,
                        //REGIMCD = 10, //TODO
                        REGIMCD = regimRecode[vid],
                        REGIMNAME = "Неизвестен", //TODO
                        SERVICECD = CalcServiceCd(vid, owner),
                        SERVICENAME = vid.ToString(),
                        LSHET = lshet,
                        PROCHL = sum,
                        MONTH_ = NextMonthToConvert,
                        MONTH2 = NextMonthToConvert,
                        YEAR_ = NextYearToConvert,
                        YEAR2 = NextYearToConvert,
                        DATE_VV = nextDate,
                        DOCUMENTCD = $"N{NextYearToConvert.ToString().Substring(2, 2)}{NextMonthToConvert:D2}0",
                        AUTOUSE = 0
                    });
                }
                //SaveListInsertSQL(lnach, InsertRecordCount);
                BufferEntitiesManager.SaveDataToBufferIBScript(lnach);
            }
        }


        private void RemoveNach(List<CNV_NACH> ln)
        {
            ln.RemoveAll(n => n.SERVICECD != 11651);
        }

        private void RemoveOplata(List<CNV_OPLATA> lo)
        {
            lo.RemoveAll(o => o.SERVICECD != 11651);
        }

        private void RemoveSaldo(List<CNV_NACHOPL> ln)
        {
            ln.RemoveAll(n => n.SERVICECD != 11651);
        }

        public static int CalcServiceCd(byte vid, RsnAbonent rsnAbonent)
        {
            return CalcServiceCd(vid, rsnAbonent.Алгоритмы.First(a => a.Вид == vid).ХозяинВида);
        }

        public static int CalcServiceCd(byte vid, ushort OwnerId)
        {
            return resourceRecode[vid] * 10000 + OwnersRecode[vid][OwnerId];
        }

        private static readonly Dictionary<byte, string> BankRename = new Dictionary<byte, string>
        {
            {3, "ООО \"РГМЭК\"" },
            {93, "Сайт МП \"КВЦ\"" },
            {51, "МП \"КВЦ\"" },
            {52, "ООО \"ПРОМЖИЛСТРОЙ\"" },
            {54, "ООО \"Мера\"" },
            {56, "ИП КАРАКУЦ В В" },
            {58, "ООО \"ПРОМСТРОЙМОНТАЖ\"" },
            {53, "ЗАО \"РЯЗАНСКАЯ РАДИОЭЛЕКТРОННАЯ КОМПАНИЯ\"" },
            {55, "АППАРАТ РЯЗАНСКОЙ ОБЛАСТНОЙ ДУМЫ" },
            {57, "ООО \"АССЕЛЬ\"" },
            {59, "ООО \"Новый день\"" },
            {60, "ООО \"ОПТИМ\"" },
            {61, "ИП \"ПЕВНЫЙ Б.Г.\"" },
            {62, "ООО \"Призма\"" },
            {63, "ИП ВАСИЛЕНКО А.Н." },
            {64, "ООО \"Инар\"" },
            {65, "ИП ЦАРЕВ А Н" },
            {66, "ИП ДАНИЛКИН А В" },
            {67, "ИП ТРУБИЦЫН И.В." },
            {68, "ИП АЛЕКСЕЕВА Т.Ю." },
            {69, "ООО \"ВЕРНИСАЖ\"" },
            {70, "ООО \"КРЕЗ\"" },
            {71, "ООО \"Набатпром\"" },
        };
    }

    public class ConvertCitizens : KvcConvertCase
    {
        public ConvertCitizens()
        {
            ConvertCaseName = "CITIZEN - граждане";
            Position = 60;
            IsChecked = false;
        }

        public static RecodeShip[] recodeShip;
        public static ExtOrgCd[] extOrgsPass;

        public static RecodeRelations[] RelationsRecode;

        public override void DoKvcConvert()
        {
            SetStepsCount(6 - 1 + ConvertingDates.Length);
            var lc = new List<CNV_CITIZEN>();
            var lcr = new List<CNV_CITIZENRELATIONS>();

            NotFoundedRelations = new List<ExtOrgCd>();
            NotFoundedCitizenShip = new List<string>();
            DataTable dt = Utils.ReadExcelFile(CitizenRecode, "ship");
            object[] dr = dt.Rows[0].ItemArray;
            recodeShip = new RecodeShip[dr.Length];
            StepStart(dt.Rows.Count);
            for (int i = 0; i < dr.Length; i++)
            {
                recodeShip[i] = new RecodeShip
                {
                    Id = Int32.Parse(dr[i].ToString()),
                    Names = new List<string>()
                };
                if (recodeShip[i].Id == 0) recodeShip[i].Id = null;
            }
            Iterate();
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i].ItemArray;
                for (int j = 0; j < dr.Length; j++)
                {
                    string name = dr[j].ToString();
                    if (String.IsNullOrWhiteSpace(name)) continue;
                    recodeShip[j].Names.Add(name.Trim().ToLower());
                }
                Iterate();
            }
            StepFinish();
            string[] relationsRecodeLines = File.ReadAllLines(CitizenRecodeRel);
            RelationsRecode = new RecodeRelations[relationsRecodeLines.Length];
            for (int i = 0; i < relationsRecodeLines.Length; i++)
            {
                string[] recodeRelStr = relationsRecodeLines[i].Split('\t');
                RelationsRecode[i] = new RecodeRelations
                {
                    RelId = Int32.Parse(recodeRelStr[0]),
                    Sex = Int32.Parse(recodeRelStr[1]),
                    RelationName = recodeRelStr[2],
                    RecodeRelId = Int32.Parse(recodeRelStr[3])
                };
            }

            StepStart(1);
            int maxOrgCd;
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                extOrgsPass = context.ExecuteQuery<ExtOrgCd>(@"select es.extorgcd, es.extorgnm from extorgspr es where es.canissuepassp = 1").ToArray();
                maxOrgCd = context.ExecuteQuery<int>(@"select gen_id(extorgspr_g, 0) from rdb$database")[0];
            }
            StepFinish();

            ConvertUniqueCCAbonents((ccAbonent) =>
            {
                ConvertCitizen(ccAbonent, ref lc, ref lcr);
            }, CcFileName, LsNotInLastFile, false, ConvertingDates, WithRsn3);

            StepStart(1);
            CitizenRecordUtils.SetUniqueDorgcd(lc, maxOrgCd);
            StepFinish();

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcr, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcr);

            FreeListMemory(lc);
            FreeListMemory(lcr);
        }

        public override void ActionAfterConvert()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteQuery(@"update cnv$abonent ca
                                set ca.f = (select first 1 cc.f from cnv$citizens cc where cc.lshet = ca.lshet and cc.ismaincityzen = 1 and cc.statusid not in (2,5)),
                                    ca.i = (select first 1 cc.i from cnv$citizens cc where cc.lshet = ca.lshet and cc.ismaincityzen = 1 and cc.statusid not in (2,5)),
                                    ca.o = (select first 1 cc.o from cnv$citizens cc where cc.lshet = ca.lshet and cc.ismaincityzen = 1 and cc.statusid not in (2,5))
                                where 0 < (select count(0) from cnv$citizens cc where cc.lshet = ca.lshet and cc.ismaincityzen = 1 and cc.statusid not in (2,5));");
        }

        private static readonly Regex OwnerPartRegex = new Regex($@"(.*[^\d])?(?<num>([^0]|[1-9]\d+))[^\d]*(\/|\\)[^\d]*(?<denum>\d+)([^\d].*)?");
        private static readonly Regex IntPartRegex = new Regex($@"(?<num>\d+)%?");

        private static readonly DateTime FakeStartDate = new DateTime(1980, 1, 1);

        public static void ConvertCitizen(CcAbonent abonent, ref List<CNV_CITIZEN> lc, ref List<CNV_CITIZENRELATIONS> lcr)
        {
            string lshet;
            try
            {
                lshet = LsDic[abonent.LsKvc.Ls];
            }
            catch (KeyNotFoundException ex)
            {
                //throw new KeyNotFoundException("Не найден абонент с лс " + abonent.LsKvc.Ls, ex);
                return;
            }
            for (int i = 0; i < abonent.Жители.Count; i++)
            {
                var kvcCitizen = abonent.Жители[i];
                var citizen = new CNV_CITIZEN
                {
                    LSHET = lshet,
                    CITIZENID = Int32.Parse($"{lshet.Substring(2, 6)}{kvcCitizen.НомерЖителя:D2}"),
                    SEX = kvcCitizen.Пол,
                    STATUSID = RecodeSobStatus(kvcCitizen.СтатусСобственности),
                    STATUSDATE = new DateTime(abonent.FileYear, abonent.FileMonth, 1),
                    SERIA = kvcCitizen.СерияДокумента,
                    NOMER = kvcCitizen.НомерПаспорта.ToString(),
                    DATDN = kvcCitizen.ДатаВыдачиДокумента,
                    ISMAINCITYZEN = kvcCitizen.ПризнакОтветственного,
                    STARTDATE = kvcCitizen.ДатаРегистрации,
                    COMMENT_ = kvcCitizen.ДопИнформация,
                    BIRTHDATE = kvcCitizen.ДатаРождения,
                    DOCTYPEID = kvcCitizen.КодДокумента != 0 ? kvcCitizen.КодДокумента - 1 + 200000 : (int?)null,
                    DEATHDATE = kvcCitizen.ДатаСмерти,
                    HIDDEN = /*abonent.СостояниеЛс == 5 ||*/ kvcCitizen.СтатусРегистрации == 9 /*|| kvcCitizen.СтатусСобственности == 2 || kvcCitizen.СтатусСобственности == 4*/ ? 1 : 0,
                    EGRPNUMBER = kvcCitizen.НомерЕГРП,
                    EGRPDATE = kvcCitizen.ДатаВыдачиЕГРП
                };
                if (kvcCitizen.СтатусРегистрации == 1 || kvcCitizen.СтатусРегистрации == 2 || kvcCitizen.СтатусРегистрации == 3)
                {
                    if (citizen.STARTDATE == null) citizen.STARTDATE = FakeStartDate;
                }
                if (kvcCitizen.ДатаСнятияСРегистрации != null)
                    citizen.ENDDATE = kvcCitizen.ДатаСнятияСРегистрации.Value/*.AddMonths(1)*/;
                else if (kvcCitizen.ДатаОкончанияВремРегистрации != null)
                    citizen.ENDDATE = kvcCitizen.ДатаОкончанияВремРегистрации.Value.AddMonths(1);
                if (citizen.ENDDATE != null && citizen.STARTDATE == null)
                    citizen.STARTDATE = FakeStartDate;

                if (kvcCitizen.СтатусРегистрации == 0 || kvcCitizen.СтатусРегистрации == 9)
                {
                    if (citizen.STARTDATE == null && citizen.ENDDATE != null) citizen.STARTDATE = FakeStartDate;
                    if (citizen.ENDDATE == null && citizen.STARTDATE != null) citizen.ENDDATE = new DateTime(abonent.FileYear, abonent.FileMonth, 1);
                }

                if (citizen.HIDDEN == 1)
                {
                    if (citizen.STARTDATE == null && citizen.ENDDATE != null) citizen.STARTDATE = FakeStartDate;
                    if (citizen.ENDDATE == null && citizen.STARTDATE != null) citizen.ENDDATE = new DateTime(abonent.FileYear, abonent.FileMonth, 1);
                }

                string f, fi, o;
                ConvertAbonents.ParseFIO(kvcCitizen.ФИО, out f, out fi, out o);
                if (f != null && f.Length > 30) f = f.Substring(0, 30);
                if (fi != null && fi.Length > 20) fi = fi.Substring(0, 20);
                if (o != null && o.Length > 20) o = o.Substring(0, 20);

                citizen.F = f;
                citizen.I = fi;
                citizen.O = o;

                if (!String.IsNullOrWhiteSpace(kvcCitizen.МестоРождения))
                {
                    var place = kvcCitizen.МестоРождения.Split(';');
                    if (place.Length == 4)
                    {
                        citizen.BIRTHCOUNTRY = String.IsNullOrWhiteSpace(place[0]) ? null : place[0].Trim();
                        citizen.BIRTHDISTRICT = String.IsNullOrWhiteSpace(place[1]) ? null : place[1].Trim();
                        citizen.BIRTHREGION = String.IsNullOrWhiteSpace(place[2]) ? null : place[2].Trim();
                        citizen.BIRTHCITY = String.IsNullOrWhiteSpace(place[3]) ? null : place[3].Trim();
                    }
                }

                if (kvcCitizen.СтатусРегистрации == 1 || kvcCitizen.СтатусРегистрации == 3) citizen.REGISTRATIONTYPE = 1;
                if (kvcCitizen.ДатаОкончанияВремРегистрации != null || kvcCitizen.СтатусРегистрации == 2) citizen.REGISTRATIONTYPE = 2;
                if (kvcCitizen.СтатусРегистрации == 9) citizen.HIDDEN = 1;

                if (citizen.STATUSID == 1)
                {
                    if (citizen.DEATHDATE != null)
                    {
                        citizen.STATUSID = 2;
                        citizen.STATUSDATE = citizen.DEATHDATE;
                    }
                    else if (kvcCitizen.СтатусРегистрации == 9)
                    {
                        citizen.STATUSID = 3;
                    }
                }

                if (kvcCitizen.ДатаОкончанияСобственности != null)
                {
                    citizen.STATUSID = 3;
                    citizen.STATUSDATE = kvcCitizen.ДатаОкончанияСобственности;
                }

                citizen.OWNERSHIPTYPE = citizen.STATUSID == 1 ? 2 : (int?)null;
                if (!String.IsNullOrWhiteSpace(kvcCitizen.ДоляПлощади))
                {
                    string lpr = kvcCitizen.ДоляПлощади.ToLower();
                    if (lpr.Contains("сов") || lpr.Contains("общ"))
                        citizen.OWNERSHIPTYPE = 1;
                    else
                    {
                        var match = OwnerPartRegex.Match(kvcCitizen.ДоляПлощади);
                        if (match.Success)
                        {
                            citizen.OWNERSHIPNUMERATOR = Int32.Parse(match.Groups["num"].Value);
                            citizen.OWNERSHIPDENOMINATOR = Int32.Parse(match.Groups["denum"].Value);
                        }
                        else
                        {
                            match = IntPartRegex.Match(kvcCitizen.ДоляПлощади);
                            if (match.Success)
                            {
                                int num = Int32.Parse(match.Groups["num"].Value);
                                if (num == 1)
                                {
                                    citizen.OWNERSHIPNUMERATOR = 1;
                                    citizen.OWNERSHIPDENOMINATOR = 1;
                                }
                                else if (num > 0)
                                {
                                    citizen.OWNERSHIPNUMERATOR = 1;
                                    citizen.OWNERSHIPDENOMINATOR = 100 / num;
                                }
                            }
                            else
                            {
                                decimal dec;
                                if (Decimal.TryParse(kvcCitizen.ДоляПлощади, out dec))
                                {
                                    if (dec < 1)
                                    {
                                        citizen.OWNERSHIPNUMERATOR =
                                            Int32.Parse(dec.ToString().Replace(".", ",").Split(',')[1]);
                                        citizen.OWNERSHIPDENOMINATOR = 10*citizen.OWNERSHIPNUMERATOR.ToString().Length;
                                    }
                                    else
                                    {
                                        citizen.OWNERSHIPNUMERATOR = 1;
                                        citizen.OWNERSHIPDENOMINATOR = (int)(100 / dec);
                                    }
                                }
                            }
                        }
                    }
                }

                switch (kvcCitizen.ПричинаВыписки)
                {
                    case 1: citizen.LEAVECASEID = 3; break;
                    case 2: citizen.LEAVECASEID = 4; break;
                    default: break;
                }

                if (!String.IsNullOrWhiteSpace(kvcCitizen.Гражданство))
                {
                    string checkShip = kvcCitizen.Гражданство.Trim().ToLower();
                    bool founded = false;
                    for (int j = 0; j < recodeShip.Length; j++)
                    {
                        var recode = recodeShip[j];
                        for (int k = 0; k < recode.Names.Count; k++)
                        {
                            if (checkShip == recode.Names[k])
                            {
                                citizen.CITIZENSHIP = recode.Id;
                                founded = true;
                                break;
                            }
                        }
                        if (founded) break;
                    }
                    if (!founded)
                    {
                        if (!NotFoundedCitizenShip.Any(cs => cs == kvcCitizen.Гражданство))
                        {
                            NotFoundedCitizenShip.Add(kvcCitizen.Гражданство);
                            new Task(() => { MessageBox.Show($"Нераспознанное гражданство {kvcCitizen.Гражданство}"); }).Start();
                        }
                    }
                }

                if (!String.IsNullOrWhiteSpace(kvcCitizen.КемВыданДокумент))
                {
                    citizen.DORGNAME = kvcCitizen.КемВыданДокумент;
                    for (int j = 0; j < extOrgsPass.Length; j++)
                    {
                        var org = extOrgsPass[j];
                        if (org.EXTORGNM == kvcCitizen.КемВыданДокумент)
                        {
                            citizen.DORGCD = org.EXTORGCD;
                            break;
                        }
                    }
                    //if (citizen.DORGCD == null)
                    //    new Task(() => { MessageBox.Show($"Нераспознанная организация выдачи документа {kvcCitizen.КемВыданДокумент}"); }).Start();
                }

                if (kvcCitizen.Пол != 0)
                {
                    for (int j = 0; j < kvcCitizen.РодственныеСвязи.Count; j++)
                    {
                        var kvcRel = kvcCitizen.РодственныеСвязи[j];
                        var recode = RelationsRecode.FirstOrDefault(rr => rr.RelId == kvcRel.КодСвязи && rr.Sex == kvcCitizen.Пол);
                        if (recode == null)
                        {
                            if (!NotFoundedRelations.Any(r => r.EXTORGCD == kvcRel.КодСвязи && r.EXTORGNM == kvcCitizen.Пол.ToString()))
                            {
                                NotFoundedRelations.Add(new ExtOrgCd { EXTORGCD = kvcRel.КодСвязи, EXTORGNM = kvcCitizen.Пол.ToString() });
                                new Task(() => { MessageBox.Show($"Не удалось найти родственную связь {kvcRel.КодСвязи} для пола {kvcCitizen.Пол}"); });
                            }
                        }
                        else
                            lcr.Add(new CNV_CITIZENRELATIONS
                            {
                                CITIZENIDFROM = (int)citizen.CITIZENID,
                                CITIZENIDTO = Int32.Parse($"{lshet.Substring(2, 6)}{kvcRel.НомерЖителя:D2}"),
                                RELATIONID = recode.RecodeRelId,
                                RELATIONNAME = recode.RelationName
                            });
                    }
                }

                lc.Add(citizen);
            }
        }

        public static List<ExtOrgCd> NotFoundedRelations;
        public static List<string> NotFoundedCitizenShip;
        private static int RecodeSobStatus(byte kvcStatus)
        {
            switch (kvcStatus)
            {
                case 0: return 3;
                case 1: return 1;
                case 2: return 2;
                case 3: return 6;
                case 4: return 5;
                case 5: return 4;
                case 9: return 3;
                case 7: return 7;
                default: throw new Exception($"Необработаный код статуса жителя квц {kvcStatus}");
            }
        }

        public class RecodeShip
        {
            public int? Id;
            public List<string> Names;
        }

        public class ExtOrgCd
        {
            public int EXTORGCD { get; set; }
            public string EXTORGNM { get; set; }
        }

        public class RecodeRelations
        {
            public int RelId;
            public int Sex;
            public string RelationName;
            public int RecodeRelId;
        }
    }

    public class ConvertCitizenMigration : KvcConvertCase
    {
        public ConvertCitizenMigration()
        {
            ConvertCaseName = "CITIZEN MIGRATION - миграция граждан";
            Position = 61;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(3 - 1 + ConvertingDates.Length);
            var lcm = new List<CNV_CITIZENMIGRATION>();

            var exitedLs = new Dictionary<int, DateTime>();
            ConvertCcFiles((ccAbonent) =>
            {
                string lshet = LsDic[ccAbonent.LsKvc.Ls];
                for (int i = 0; i < ccAbonent.Жители.Count; i++)
                {
                    var citizen = ccAbonent.Жители[i];
                    int citizenid = Int32.Parse($"{lshet.Substring(2, 6)}{citizen.НомерЖителя:D2}");

                    if (citizen.ДатаОкончанияВыбытия != null)
                    {
                        var enterDate = citizen.ДатаОкончанияВыбытия.Value.AddMonths(1);
                        var cnv = new CNV_CITIZENMIGRATION
                        {
                            CITIZENID = citizenid,
                            DATE_ = enterDate,
                            DIRECTION = 1,
                            MIGRATIONTYPE = 1
                        };
                        lcm.Add(cnv);
                    }

                    if (citizen.СтатусРегистрации == 3)
                    {
                        lcm.Add(new CNV_CITIZENMIGRATION
                        {
                            CITIZENID = citizenid,
                            DATE_ = CurrnetConvertDate,
                            DIRECTION = 2,
                            MIGRATIONTYPE = 3
                        });

                        var curDate = citizen.ДатаОкончанияВыбытия != null
                            ? citizen.ДатаОкончанияВыбытия.Value.AddMonths(1)
                            : DateTime.MaxValue;
                        if (exitedLs.ContainsKey(citizenid)) exitedLs[citizenid] = curDate;
                        else exitedLs.Add(citizenid, curDate);

                        //DateTime lastDate;
                        //if (exitedLs.TryGetValue(citizenid, out lastDate))
                        //{
                        //    if (lastDate < enterDate) exitedLs[citizenid] = lastDate;
                        //}
                        //else
                        //    exitedLs.Add(citizenid, enterDate);
                    }
                    else
                    {
                        DateTime exitedDate;
                        if (exitedLs.TryGetValue(citizenid, out exitedDate))
                        {
                            if (exitedDate >= CurrnetConvertDate)
                                lcm.Add(new CNV_CITIZENMIGRATION
                                {
                                    CITIZENID = citizenid,
                                    DATE_ = CurrnetConvertDate,
                                    DIRECTION = 1,
                                    MIGRATIONTYPE = 1
                                });
                        }
                    }
                }
            }
            , () =>
            {
                lcm = lcm.GroupBy(cm => new {cm.CITIZENID, cm.DATE_}).Select(gcm => gcm.Last()).ToList();
                lcm = CitizenRecordUtils.ThinOutList(lcm);
            }
            , CcFileName, false, ConvertingDates, WithRsn3);

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcm, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcm);
        }
    }

    public class ConvertADDChars : KvcConvertCase
    {
        public ConvertADDChars()
        {
            ConvertCaseName = "ADDCHARS - дополнительные характеристики";
            Position = 70;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            if (LsDic == null || !LsDic.Any()) throw new Exception("Необходимо заполнить список лицевых счетов");

            SetStepsCount(2 - 1 + ConvertingDates.Length);

            var lcc = new List<CNV_AADDCHAR>();
            ConvertUniqueRsnAbonents(rsnAbonent =>
            {
                string lshet = LsDic[rsnAbonent.LsKvc.Ls];

                lcc.Add(new CNV_AADDCHAR
                {
                    LSHET = lshet,
                    ADDCHARCD = 754,
                    VALUE = rsnAbonent.СпособУправления.ToString()
                });
                lcc.Add(new CNV_AADDCHAR
                {
                    LSHET = lshet,
                    ADDCHARCD = 755,
                    VALUE = rsnAbonent.ЦельИспользованияЖилищногоФонда.ToString()
                });

                if (rsnAbonent.СостояниеЛС == 5)
                    lcc.Add(new CNV_AADDCHAR
                    {
                        LSHET = lshet,
                        ADDCHARCD = 6200602,
                        VALUE = "1"
                    });

            }, RsnFilePath, LsNotInLastFile, true, ConvertingDates, WithRsn3);

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcc);

            FreeListMemory(lcc);
        }
    }

    public class ConvertHChars : KvcConvertCase
    {
        public ConvertHChars()
        {
            ConvertCaseName = "HCHARS - характеристики домов";
            Position = 80;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(8 - 1 + ConvertingDates.Length);

            var convertedHouses = new Dictionary<string, int>();
            var lsInHouse = new Dictionary<int, int>();
            var llc = new List<CNV_LCHARHOUSES>();
            var lcc = new List<CNV_CHARSHOUSES>();


            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<HouseExt>(@"select HOUSECD, EXTLSHET from cnv$abonent
                                                                union
                                                                select housecd, extlshet from abonents a
                                                                inner join extorgaccounts ea on ea.lshet = a.lshet");
                StepStart(sqlResult.Count);
                int lastHouse = 0;
                for (int i = 0; i < sqlResult.Count; i++)
                {
                    var house = sqlResult[i];
                    if (house.HOUSECD != lastHouse)
                    {
                        convertedHouses.Add(house.EXTLSHET, house.HOUSECD);
                        lastHouse = house.HOUSECD;
                        lsInHouse.Add(house.HOUSECD, sqlResult.Count(s => s.HOUSECD == house.HOUSECD));
                    }
                    Iterate();
                }
                StepFinish();
            }


            ConvertRsnFiles(rsnAbonent =>
            {
                int housecd;
                if (!convertedHouses.TryGetValue(rsnAbonent.LsKvc.CombinedLs, out housecd)) return;
                var date = new DateTime(rsnAbonent.FileYear, rsnAbonent.FileMonth, 1);

                Record_200 param;

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 1);
                var buildingYear = param == null ? 0 : param.Значение;
                if (buildingYear > 0)
                {
                    if (buildingYear < 20) buildingYear += 2000;
                    else if (buildingYear < 100) buildingYear += 1900;
                }
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 101026,
                    VALUE_ = buildingYear,
                    DATE_ = date
                });
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = -1,
                    VALUE_ = buildingYear,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 3);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = -2,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 4);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = -3,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 7);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 101039,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 8);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 32009,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 9);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 101040,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 25);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 32010,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 29);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 32001,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 30);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 32002,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 16);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31014,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 17);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31013,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 27);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31012,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 23);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31007,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 14);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31008,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 13);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31009,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 15);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31010,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 22);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31011,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = -4,
                    VALUE_ = lsInHouse[housecd] > 1 ? 1 : 0,
                    DATE_ = date
                });


                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 26);
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62663002,
                    VALUE_ = param == null ? 0 : (int)param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 28);
                if (param?.Значение == 4) param.Значение = 0;
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990001,
                    VALUE_ = param == null ? 0 : (int)param.Значение,
                    DATE_ = date
                });


                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 6);
                string houseEqVal = param == null ? $"{0:D6}" : $"{int.Parse(Convert.ToString(Convert.ToInt32(param.Значение), 2)):D6}";

                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990007,
                    VALUE_ = houseEqVal[0] - '0',
                    DATE_ = date
                });
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990006,
                    VALUE_ = houseEqVal[1] - '0',
                    DATE_ = date
                });
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990005,
                    VALUE_ = houseEqVal[2] - '0',
                    DATE_ = date
                });
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990004,
                    VALUE_ = houseEqVal[3] - '0',
                    DATE_ = date
                });
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990003,
                    VALUE_ = houseEqVal[4] - '0',
                    DATE_ = date
                });
                llc.Add(new CNV_LCHARHOUSES
                {
                    HOUSECD = housecd,
                    LCHARCD = 62990002,
                    VALUE_ = houseEqVal[5] - '0',
                    DATE_ = date
                });

            }, () =>
            {
                lcc = HcharsRecordUtils.ThinOutList(lcc);
                llc = HcharsRecordUtils.ThinOutList(llc);
            },
            RsnFilePath, false, ConvertingDates, WithRsn3);

            StepStart(lcc.Count);
            int? lastHc = -1;
            int? lastChar = -1;
            for (int i = 0; i < lcc.Count; i++)
            {
                var chr = lcc[i];
                if (chr.VALUE_ == 0 && (chr.HOUSECD != lastHc || chr.CHARCD != lastChar))
                {
                    lcc[i] = null;
                }
                else
                {
                    lastHc = chr.HOUSECD;
                    lastChar = chr.CHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc.RemoveAll(c => c == null);
            StepFinish();

            StepStart(llc.Count);
            lastHc = -1;
            lastChar = -1;
            for (int i = 0; i < llc.Count; i++)
            {
                var chr = llc[i];
                if (chr.VALUE_ == 0 && (chr.HOUSECD != lastHc || chr.LCHARCD != lastChar))
                {
                    llc[i] = null;
                }
                else
                {
                    lastHc = chr.HOUSECD;
                    lastChar = chr.LCHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            llc.RemoveAll(c => c == null);
            StepFinish();

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcc);
            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(llc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(llc);

            FreeListMemory(lcc);
            FreeListMemory(llc);
        }

        public override void ActionAfterConvert()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteQuery(@"delete from cnv$charshouses c1
where c1.value_ = 0 and c1.date_ = (select min(c2.date_) from cnv$charshouses c2 where c2.housecd = c1.housecd and c2.charcd = c1.charcd);");
            fbm.ExecuteQuery(@"delete from cnv$lcharhouses c1
where c1.value_ = 0 and c1.date_ = (select min(c2.date_) from cnv$lcharhouses c2 where c2.housecd = c1.housecd and c2.lcharcd = c1.lcharcd);");
        }

        public class HouseExt
        {
            public int HOUSECD { get; set; }
            public string EXTLSHET { get; set; }
            public int LsCount;
        }
    }

    public class ConvertGroupCounters : KvcConvertCase
    {
        public ConvertGroupCounters()
        {
            ConvertCaseName = "GROUP COUNTERS - информация о групповых счетчиках";
            Position = 90;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(2 - 1 + ConvertingDates.Length);
            var lcc = new List<CNV_COUNTER>();
            var vids = spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var streets = spr1.GetSubSpr(Spr1.SubSpr.Улицы);
            var houses = spr1.GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир);

            ConvertUniqueRsnAbonents((rsnAbonent) =>
            {
                string lset = LsDic[rsnAbonent.LsKvc.Ls];

                for (int j = 0; j < rsnAbonent.ОПУ.Count; j++)
                {
                    var gcounter = rsnAbonent.ОПУ[j];

                    int cntType, setupPlace;
                    var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == gcounter.Вид);
                    if (alg == null || (alg.Вид == 6 && alg.Алгоритм != 2) || (alg.Вид != 6 && alg.Алгоритм != 101)) continue;
                    int serviceid = ConvertNachopl.CalcServiceCd(gcounter.Вид, alg.ХозяинВида);
                    ConvertCounterType(gcounter, out cntType, out setupPlace);
                    var houseInfo = houses.FirstOrDefault(h => h.R1 == rsnAbonent.LsKvc.Adr1 && h.R2 == 0);
                    string houseNo = houseInfo == null
                        ? "д." + rsnAbonent.LsKvc.HouseId + (rsnAbonent.LsKvc.KorpusId == 0
                            ? ""
                            : " корп." + rsnAbonent.LsKvc.KorpusId)
                        : houseInfo.Sr40;
                    var counter = new CNV_COUNTER
                    {
                        LSHET = lset,
                        COUNTERID = $"{rsnAbonent.LsKvc.Adr1:D8}{gcounter.Вид:D2}{gcounter.НомерСчетчика:D2}",
                        CNTTYPE = cntType,
                        SETUPPLACE = setupPlace,
                        COUNTER_LEVEL = 1,
                        GROUPCOUNTERMODULEID = 23,
                        TARGETBALANCE_KOD = serviceid,
                        TARGETNEGATIVEBALANCE_KOD = serviceid,
                        STATUSID = ConvertStatus(gcounter),
                        STATUSDATE = CurrnetConvertDate,
                        NAME = $"ОПУ {vids.Single(v => v.R1 == gcounter.Вид).Sr40} №{gcounter.НомерСчетчика} {streets.First(s => s.R1 == rsnAbonent.LsKvc.StreetId).Sr40} {houseNo}"
                    };
                    lcc.Add(counter);
                }

            }, RsnFilePath, LsNotInLastFile, true, ConvertingDates);

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);

            FreeListMemory(lcc);
        }

        public static int ConvertStatus(Record_201 rec)
        {
            switch (rec.СтатусСчетчика)
            {
                case 0: return 1;
                case 1: return -1;
                case 2: return -99;
                default: return -99;
                //default: throw new Exception($"Неизвестный статус {rec.СтатусСчетчика}");
            }
        }

        public static void ConvertCounterType(Record_201 rec, out int cnttype, out int setupplace)
        {
            setupplace = 2;
            switch (rec.Вид)
            {
                case 83:
                    switch (rec.Разрядность)
                    {
                        case 4:
                            cnttype = 105;
                            break;
                        case 5:
                            cnttype = 106;
                            break;
                        case 6:
                            cnttype = 107;
                            break;
                        case 7:
                            cnttype = 108;
                            break;
                        default:
                            cnttype = 117;
                            break;
                    }
                    break;
                case 85:
                    switch (rec.Разрядность)
                    {
                        case 3:
                            cnttype = 109;
                            break;
                        case 5:
                            cnttype = 110;
                            break;
                        case 6:
                            cnttype = 111;
                            break;
                        case 7:
                            cnttype = 112;
                            break;
                        default:
                            cnttype = 118;
                            break;
                    }
                    break;
                case 87:
                    switch (rec.Разрядность)
                    {
                        case 3:
                            cnttype = 113;
                            break;
                        case 5:
                            cnttype = 114;
                            break;
                        case 6:
                            cnttype = 115;
                            break;
                        case 7:
                            cnttype = 116;
                            break;
                        default:
                            cnttype = 119;
                            break;
                    }
                    break;
                case 06:
                    cnttype = 120;
                    break;
                case 03:
                    switch (rec.Разрядность)
                    {
                        case 4:
                            cnttype = 105;
                            break;
                        case 5:
                            cnttype = 106;
                            break;
                        case 6:
                            cnttype = 107;
                            break;
                        case 7:
                            cnttype = 108;
                            break;
                        default:
                            cnttype = 117;
                            break;
                    }
                    break;
                default:
                    throw new Exception($"Необработанный вид {rec.Вид} для определения типа счетчика");
            }
        }

        public static int? CalcServiceCd(byte vid, RsnAbonent rsnAbonent)
        {
            var owner = rsnAbonent.Алгоритмы.SingleOrDefault(a => a.Вид == vid);
            if (owner == null) return null;
            return ConvertNachopl.CalcServiceCd(vid, owner.ХозяинВида);
        }
    }

    public class ConvertGroupCntinds : KvcConvertCase
    {
        public ConvertGroupCntinds()
        {
            ConvertCaseName = "GROUP CNTINDS - информация о показаниях групповых счетчиков";
            Position = 91;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            
            SetStepsCount(3 - 1 + ConvertingDates.Length - 1);
            var lci = new List<CNV_CNTRSIND>();
            var convertedCounters = new List<string>();
            ConvertRsnFiles((rsnAbonent) =>
            {
                string lset = LsDic[rsnAbonent.LsKvc.Ls];

                int cc = 0;
                if (rsnAbonent.ПоказанияОПУ.Count >= rsnAbonent.ПотреблениеОПУ.Count)
                {
                    for (int j = 0; j < rsnAbonent.ПоказанияОПУ.Count; j++)
                    {
                        var ind = rsnAbonent.ПоказанияОПУ[j];
                        string cntid = $"{rsnAbonent.LsKvc.Adr1:D8}{ind.Вид:D2}{ind.НомерСчетчика:D2}";
                        bool founded = false;
                        for (int k = 0; k < convertedCounters.Count; k++)
                        {
                            if (convertedCounters[k] == cntid)
                            {
                                founded = true;
                                break;
                            }
                        }
                        if (founded) continue;
                        var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == ind.Вид);
                        //if (alg == null) continue;
                        if (alg == null || (alg.Вид == 6 && alg.Алгоритм != 2) || (alg.Вид != 6 && alg.Алгоритм != 101)) continue;
                        var potr = rsnAbonent.ПотреблениеОПУ.FirstOrDefault(p => p.Вид == ind.Вид && p.НомерСчетчика == ind.НомерСчетчика);
                        var gcounter = rsnAbonent.ОПУ.First(o => o.Вид == ind.Вид && o.НомерСчетчика == ind.НомерСчетчика);
                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = cntid,
                            DOCUMENTCD = $"{++cc}{rsnAbonent.FileDate.ToShortDateString()}",
                            OLDIND = potr == null ? ind.Значение : Math.Round(ind.Значение - potr.Значение / gcounter.Коэффициент, 4), //TODO проверить
                            OB_EM = potr?.Значение,
                            INDICATION = ind.Значение,
                            INDDATE = rsnAbonent.FileDate,
                            INDTYPE = 0
                        });
                        convertedCounters.Add(cntid);
                    }
                }
                else
                {
                    for (int j = 0; j < rsnAbonent.ПотреблениеОПУ.Count; j++)
                    {
                        var potr = rsnAbonent.ПотреблениеОПУ[j];
                        string cntid = $"{rsnAbonent.LsKvc.Adr1:D8}{potr.Вид:D2}{potr.НомерСчетчика:D2}";
                        bool founded = false;
                        for (int k = 0; k < convertedCounters.Count; k++)
                        {
                            if (convertedCounters[k] == cntid)
                            {
                                founded = true;
                                break;
                            }
                        }
                        if (founded) continue;
                        var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == potr.Вид);
                        //if (alg == null) continue;
                        if (alg == null || (alg.Вид == 6 && alg.Алгоритм != 2) || (alg.Вид != 6 && alg.Алгоритм != 101)) continue;
                        var ind = rsnAbonent.ПоказанияОПУ.FirstOrDefault(p => p.Вид == potr.Вид && p.НомерСчетчика == potr.НомерСчетчика);
                        var gcounter = rsnAbonent.ОПУ.First(o => o.Вид == potr.Вид && o.НомерСчетчика == potr.НомерСчетчика);
                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = $"{rsnAbonent.LsKvc.Adr1:D8}{potr.Вид:D2}{potr.НомерСчетчика:D2}",
                            DOCUMENTCD = $"{++cc}{rsnAbonent.FileDate.ToShortDateString()}",
                            OLDIND = ind == null ? (decimal?)null : Math.Round(ind.Значение - potr.Значение / gcounter.Коэффициент), //TODO проверить
                            OB_EM = potr.Значение,
                            INDICATION = ind?.Значение,
                            INDDATE = rsnAbonent.FileDate,
                            INDTYPE = 0
                        });
                        convertedCounters.Add(cntid);
                    }
                }

            }, () => convertedCounters = new List<string>(),
            RsnFilePath, false, ConvertingDates);

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lci, InsertRecordCount);

            FreeListMemory(lci);
        }
    }

    public class ConvertPeni : KvcConvertCase
    {
        public ConvertPeni()
        {
            ConvertCaseName = "PENI - информация по пене";
            Position = 100;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            
            SetStepsCount(2 - 1 + ConvertingDates.Length);
            var lp = new List<CNV_PENISUMMA>();
            var date = new DateTime(YearToConvert, MonthToConvert, DateTime.DaysInMonth(YearToConvert, MonthToConvert));

            ConvertRsnAbonent((rsnAbonent) =>
            {
                string lset = LsDic[rsnAbonent.LsKvc.Ls];

                for (int i = 0; i < rsnAbonent.СальдоНаКонец.Count; i++)
                {
                    var saldo = rsnAbonent.СальдоНаКонец[i];
                    if (!RsnHelper.PeniResources.Contains(saldo.Вид) || saldo.Сумма == 0) continue;
                    lp.Add(new CNV_PENISUMMA
                    {
                        SERVICECD = ConvertNachopl.CalcServiceCd(PeniRecode[saldo.Вид], rsnAbonent.Алгоритмы.First(a => a.Вид == saldo.Вид).ХозяинВида),
                        LSHET = lset,
                        FDATE = date,
                        FDAY = date.Day,
                        FMONTH = date.Month,
                        FYEAR = date.Year,
                        ABONENTSALDO = 0,
                        PENINACHISLSUMMA = saldo.Сумма,
                        NDATE = date,
                        ISCONTROLPOINT = 0,
                        IZMEN = 1
                    });
                }

            }, YearToConvert, MonthToConvert);

            if (WithRsn3)
            {
                var pereForNextMonth = Utils.ReadExcelFile(PereForNextMonth, "Лист1");
                var nextDate = new DateTime(NextYearToConvert, NextMonthToConvert, 1);
                foreach (DataRow row in pereForNextMonth.Rows)
                {
                    string lsKvc = row[0].ToString();
                    string[] lsKvcParts = lsKvc.Split('-');
                    var lsKvcObj = new LsKvc(UInt32.Parse(lsKvcParts[0] + lsKvcParts[1] + lsKvcParts[2]),
                        UInt32.Parse(lsKvcParts[3] + lsKvcParts[4] + lsKvcParts[5]));
                    if (!ShouldConvert(lsKvcObj)) continue;
                    byte vid = byte.Parse(row[1].ToString());
                    if (!RsnHelper.PeniResources.Contains(vid)) continue;
                    ushort owner = UInt16.Parse(row[2].ToString());
                    decimal sum = Decimal.Parse(row[3].ToString());
                    if (sum == 0) continue;
                    string lshet = LsDic[lsKvc];

                    lp.Add(new CNV_PENISUMMA
                    {
                        SERVICECD = ConvertNachopl.CalcServiceCd(PeniRecode[vid], owner),
                        LSHET = lshet,
                        FDATE = nextDate,
                        FDAY = nextDate.Day,
                        FMONTH = nextDate.Month,
                        FYEAR = nextDate.Year,
                        ABONENTSALDO = 0,
                        PENINACHISLSUMMA = sum,
                        NDATE = nextDate,
                        ISCONTROLPOINT = 0,
                        IZMEN = 1,
                    });
                }
            }

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lp, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lp);

            FreeListMemory(lp);
        }
    }

    #endregion

    #region Перенос данных из временных таблиц

    public class SplitterTransfer : ConvertCase
    {
        public SplitterTransfer()
        {
            ConvertCaseName = "";
            Position = 998;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            
        }
    }

    public class TransferDisableTriggers : KvcConvertCase
    {
        public TransferDisableTriggers()
        {
            ConvertCaseName = "Отключить триггеры изменений";
            Position = 999;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("ALTER trigger ABONENTADDDOCUMENT inactive");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_INSERT inactive");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_CHANGE inactive");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_DELETE inactive");
            fbm.ExecuteNonQuery("ALTER trigger LSHETS_CHANGE inactive");
            fbm.ExecuteNonQuery("ALTER trigger EQSTATUSES_INSCHANGEDOC inactive");
            fbm.ExecuteNonQuery("ALTER trigger CITYZENS_INSERT inactive");
            StepFinish();
        }
    }

    public class TransferAddressObjects : KvcConvertCase
    {
        public TransferAddressObjects()
        {
            ConvertCaseName = "Перенос данных об адресных объектах";
            Position = 1000;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(2);
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

            StepStart(4);
            fbm.ExecuteNonQuery(@"update informationowners io
                                    set io.extorgcd = io.ownerid
                                    where exists(select e.extorgcd from extorgspr e where e.extorgcd = io.ownerid);");
            Iterate();
            fbm.ExecuteNonQuery(@"update informationowners io
                                    set io.extorgcd = (select first 1 e.extorgcd from extorgspr e where e.extorgnm = substring(io.ownername from 6))
                                    where 1 = (select count(0) from extorgspr e where e.extorgnm = substring(io.ownername from 6));");
            Iterate();
            var recodeOwners = new Dictionary<int, int>
            {
                {6189, 1689},
                {6212, 6202},
                {7480, 5160},
                {1029, 1020},
                {1038, 1040},
                {1039, 1040},
                {1041, 1040},
                {1042, 1040},
                {1043, 1040},
                {1044, 1040},
                {1045, 1040},
                {1046, 1040},
                {1047, 1040},
                {1048, 1040},
                {1049, 1040},
                {1333, 1330},
                {6361, 6360},
                {7342, 7340},
                {7591, 5180},
                {7592, 5180},
                {7593, 5180},
                {7594, 5180},
                {7595, 5180},
                {7596, 5180},
                {7597, 5180},
                {7598, 5180},
                {7599, 5180},
                {7600, 5180},
                {7601, 5180},
                {7611, 6740},
                {7615, 6740}
            };
            foreach (var recode in recodeOwners)
            {
                fbm.ExecuteNonQuery($"update informationowners set extorgcd = {recode.Value} where ownerid = {recode.Key};");
            }
            Iterate();
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var emptyIo = context.ExecuteScalar<int>("select count(0) from informationowners io where io.extorgcd is null");
                if (emptyIo > 0 )
                    new Task(() => MessageBox.Show("Не сопоставлено владельцев " + emptyIo)).Start();
            }
            StepFinish();
        }
    }

    public class TransferAbonents : KvcConvertCase
    {
        public TransferAbonents()
        {
            ConvertCaseName = "Перенос данных об абонентах";
            Position = 1010;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
            Iterate();
            StepFinish();
        }
    }

    public class TransferExtlshet : KvcConvertCase
    {
        public TransferExtlshet()
        {
            ConvertCaseName = "Перенос данных о внешних лицевых счетах";
            Position = 1015;
            IsChecked = false;

        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            //fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
            //Iterate();
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "1", "0" });
            Iterate();
        }
    }

    public class TransferChars : KvcConvertCase
    {
        public TransferChars()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках";
            Position = 1020;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferLchars : KvcConvertCase
    {
        public TransferLchars()
        {
            ConvertCaseName = "Перенос данных о качественных характеристиках";
            Position = 1030;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferCounters : KvcConvertCase
    {
        public TransferCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках и показаниях";
            Position = 1040;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0", "1", "0" });
            Iterate();

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var lseqList = context.ExecuteQuery<LsEqOrm>(
@"select ae.lshet as Lshet, er.equipmentid as EquipmentId from EQUIPMENT_REGIMCONSUM er
inner join abonentsequipment ae on ae.equipmentid = er.equipmentid
order by ae.lshet, er.equipmentid");
                if (lseqList == null || lseqList.Count == 0) return;
                var groupedLsEq = lseqList.GroupBy(le => le.Lshet);
                foreach (var ls in groupedLsEq)
                {
                    var etalonEqId = ls.First().EquipmentId;
                    foreach (var eq in ls)
                    {
                        fbm.ExecuteNonQuery($"update parentequipment pe set pe.unitingid = {etalonEqId} where pe.equipmentid = {eq.EquipmentId}");
                    }
                }
            }
        }

        private class LsEqOrm
        {
            public string Lshet { get; set; }
            public int EquipmentId { get; set; }
        }
    }

    public class TransferNachopl : KvcConvertCase
    {
        public TransferNachopl()
        {
            ConvertCaseName = "Перенос данных о истории оплат и начислений";
            Position = 1070;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(5);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            Iterate();
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { CurrentYear.ToString(CultureInfo.InvariantCulture),
                CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            StepFinish();
        }
    }

    public class TransferSourceDocTypes : KvcConvertCase
    {
        public TransferSourceDocTypes()
        {
            ConvertCaseName = "Группировка источников документов по типам";
            Position = 1071;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(6);
            fbm.ExecuteNonQuery(@"update sourcedoc sd
                                set sd.paymethodcd = 3
                                where (sd.sourcedoccd like '13___') or (sd.sourcedoccd like '33___')");
            Iterate();
            fbm.ExecuteNonQuery(@"update sourcedoc sd
                                set sd.paymethodcd = 2
                                where (sd.sourcedoccd like '1___') or
                                    (sd.sourcedoccd like '2___') or
                                    (sd.sourcedoccd like '6___') or
                                    (sd.sourcedoccd like '7___') or
                                    (sd.sourcedoccd like '8___') or
                                    (sd.sourcedoccd like '9___') or
                                    (sd.sourcedoccd like '11___') or
                                    (sd.sourcedoccd like '16___') or
                                    (sd.sourcedoccd like '17___') or
                                    (sd.sourcedoccd like '21___')");
            Iterate();
            fbm.ExecuteNonQuery(@"update sourcedoc sd
                                set sd.paymethodcd = 7
                                where sd.sourcedoccd >= 51000 and sd.sourcedoccd < 94000");
            Iterate();
            fbm.ExecuteNonQuery(@"update sourcedoc sd
                                set sd.paymethodcd = 8
                                where (sd.sourcedoccd like '3___') or
                                    (sd.sourcedoccd like '5___') or
                                    (sd.sourcedoccd like '15___') or
                                    (sd.sourcedoccd like '25___') or
                                    (sd.sourcedoccd like '40___')");
            Iterate();
            fbm.ExecuteNonQuery(@"update sourcedoc sd
                                set sd.paymethodcd = 6
                                where (sd.sourcedoccd like '31___') or (sd.sourcedoccd like '38___')");

            Iterate();
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (801, 'Сбербанк', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (802, 'ВТБ 24', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (805, 'Почтамп', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (806, 'ПРИО-ВНЕШТОРБАНК', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (807, 'ООО МКБ им.Живаго', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (808, 'ОАО АКБ ""Пробизнесбанк""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (809, 'КБ ""СПЕЦСЕТЬСТРОЙБАНК""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (840, 'ООО ""БиПлат""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (803, 'ООО ""РГМЭК""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (852, 'ООО ""ПРОМЖИЛСТРОЙ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (854, 'ООО ""Мера""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (856, 'И.П. КАРАКУЦ В В', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (858, 'ООО ""ПРОМСТРОЙМОНТАЖ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (853, 'ЗАО ""РЯЗАНСКАЯ РАДИОЭЛЕКТРОННАЯ КОМПАНИЯ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (855, 'АППАРАТ РЯЗАНСКОЙ ОБЛАСТНОЙ ДУМЫ', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (857, 'ООО ""АССЕЛЬ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (859, 'ООО ""Новый день""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (860, 'ООО ""ОПТИМ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (861, 'И.П. ""ПЕВНЫЙ Б.Г.""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (862, 'ООО ""Призма""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (863, 'И.П. ВАСИЛЕНКО А.Н.', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (864, 'ООО ""Инар""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (865, 'И.П. ЦАРЕВ А Н', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (866, 'И.П. ДАНИЛКИН А В', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (867, 'И.П. ТРУБИЦЫН И.В.', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (868, 'И.П. АЛЕКСЕЕВА Т.Ю.', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (869, 'ООО ""ВЕРНИСАЖ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (870, 'ООО ""КРЕЗ""', 0, 1, 0, 0, 0);");
            fbm.ExecuteNonQuery(@"INSERT INTO extorgspr(EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, EQUIPMENTSALE, EQUIPMENTMAKE) VALUES (871, 'ООО ""Набатпром""', 0, 1, 0, 0, 0);");

            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 801 where (sourcedoccd like '1___') or (sourcedoccd like '11___') or (sourcedoccd like '21___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 802 where (sourcedoccd like '2___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 805 where (sourcedoccd like '5___') or (sourcedoccd like '15___') or (sourcedoccd like '25___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 806 where (sourcedoccd like '6___') or (sourcedoccd like '16___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 807 where (sourcedoccd like '7___') or (sourcedoccd like '17___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 808 where (sourcedoccd like '8___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 809 where (sourcedoccd like '9___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 1 where (sourcedoccd like '13___') or (sourcedoccd like '33___') or (sourcedoccd like '93___') or (sourcedoccd like '51___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 840 where (sourcedoccd like '40___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 803 where (sourcedoccd like '3___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 852 where (sourcedoccd like '52___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 854 where (sourcedoccd like '54___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 856 where (sourcedoccd like '56___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 858 where (sourcedoccd like '58___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 853 where (sourcedoccd like '53___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 855 where (sourcedoccd like '55___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 857 where (sourcedoccd like '57___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 859 where (sourcedoccd like '59___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 860 where (sourcedoccd like '60___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 861 where (sourcedoccd like '61___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 862 where (sourcedoccd like '62___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 863 where (sourcedoccd like '63___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 864 where (sourcedoccd like '64___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 865 where (sourcedoccd like '65___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 866 where (sourcedoccd like '66___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 867 where (sourcedoccd like '67___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 868 where (sourcedoccd like '68___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 869 where (sourcedoccd like '69___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 870 where (sourcedoccd like '70___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 871 where (sourcedoccd like '71___');");
            fbm.ExecuteNonQuery(@"update sourcedoc set EXTORGCD = 1 where (sourcedoccd like '31___') or (sourcedoccd like '38___');");
            StepFinish();
        }
    }

    public class TransferCitizens : KvcConvertCase
    {
        public TransferCitizens()
        {
            ConvertCaseName = "Перенос данных о гражданах";
            Position = 1080;
            IsChecked = false;

        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(3);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01800_CITIZENS", new []{"1"});
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01810_CITIZENRELATIONS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01820_CITIZENMIGRATION");
            StepFinish();
        }
    }

    public class TransferAddchars : KvcConvertCase
    {
        public TransferAddchars()
        {
            ConvertCaseName = "Перенос дополнительных характеристик";
            Position = 1090;
            IsChecked = false;
        }

        public override void DoKvcConvert()
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
            StepStart(2);
            fbm.ExecuteProcedure("CNV$CNV_00850_CHARSHOUSES", new []{"0", "0"});
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00855_LCHARSHOUSES", new []{"0", "0"});
            StepFinish();
        }
    }

    public class TransferGroupCounters : KvcConvertCase
    {
        public TransferGroupCounters()
        {
            ConvertCaseName = "Перенос данных о групповых счетчиках и показаниях";
            Position = 1110;
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

    public class TransferEnableTriggers : KvcConvertCase
    {
        public TransferEnableTriggers()
        {
            ConvertCaseName = "Включить триггеры изменений";
            Position = 3000;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("ALTER trigger ABONENTADDDOCUMENT active");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_INSERT active");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_CHANGE active");
            fbm.ExecuteNonQuery("ALTER trigger ABN_ADD_CHARS_DELETE active");
            fbm.ExecuteNonQuery("ALTER trigger LSHETS_CHANGE active");
            fbm.ExecuteNonQuery("ALTER trigger EQSTATUSES_INSCHANGEDOC active");
            fbm.ExecuteNonQuery("ALTER trigger CITYZENS_INSERT active");
            StepFinish();
        }
    }

    public class CalcRash : KvcConvertCase
    {
        public CalcRash()
        {
            ConvertCaseName = "Запустить служебный расчет";
            Position = 9999;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            Process.Start(@"D:\mk\abonents.exe", @"-calccurrentnachisl ""D:\mk\recount.cfg""");
            StepFinish();
        }
    }

    #endregion

    #region Очистка таблиц

    public class ClearAbonents : KvcConvertCase
    {
        public ClearAbonents()
        {
            ConvertCaseName = "CLEAR ABONENT - очистка данных об абонентах";
            Position = -2020;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
        }
    }

    public class ClearCchars : KvcConvertCase
    {
        public ClearCchars()
        {
            ConvertCaseName = "CLEAR CCHAR - очистка количественный характеристики";
            Position = -2030;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CHARS");
        }
    }

    public class ClearLchars : KvcConvertCase
    {
        public ClearLchars()
        {
            ConvertCaseName = "CLEAR LCHAR - очистка качественные характеристики";
            Position = -2039;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$LCHARS");
        }
    }

    public class ClearCounters : KvcConvertCase
    {
        public ClearCounters()
        {
            ConvertCaseName = "CLEAR COUNTERS - очистка информации о счетчиках";
            Position = -2040;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
        }
    }

    public class ClearCntind : KvcConvertCase
    {
        public ClearCntind()
        {
            ConvertCaseName = "CLEAR CNTINDS - очистка информации о показаниях";
            Position = -2045;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
        }
    }

    public class ClearCitizens : KvcConvertCase
    {
        public ClearCitizens()
        {
            ConvertCaseName = "CLEAR CITIZENS - очистка информации о гражданах";
            Position = -2046;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CITIZENS");
            BufferEntitiesManager.DropTableData("CNV$CITIZENRELATIONS");
        }
    }

    public class CleraNachopl : KvcConvertCase
    {
        public CleraNachopl()
        {
            ConvertCaseName = "CLEAR NACHOPL - очистка истории оплат и начислений";
            Position = -2050;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$NACH");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
        }
    }

    public class CleraAddchars : KvcConvertCase
    {
        public CleraAddchars()
        {
            ConvertCaseName = "CLEAR ADDCHARS - очистка дополнительных характеристик";
            Position = -2070;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$AADDCHAR");
        }
    }

    public class CleraHchars : KvcConvertCase
    {
        public CleraHchars()
        {
            ConvertCaseName = "CLEAR HCHARS - очистка характеристик домов";
            Position = -2080;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("cnv$charshouses");
            BufferEntitiesManager.DropTableData("cnv$lcharhouses");
            BufferEntitiesManager.DropTableData("cnv$haddchar");
        }
    }

    public class ClearPeni : KvcConvertCase
    {
        public ClearPeni()
        {
            ConvertCaseName = "CLEAR PENI - очистка пени";
            Position = -2100;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            BufferEntitiesManager.DropTableData("cnv$penisumma");
        }
    }

    public class SplitterClear : ConvertCase
    {
        public SplitterClear()
        {
            ConvertCaseName = "";
            Position = -1;
            IsChecked = false;
        }

        public override void DoConvert()
        {

        }
    }
    #endregion

    #region Доработки после конвертации

    public class SplitterMaintaince : ConvertCase
    {
        public SplitterMaintaince()
        {
            ConvertCaseName = "";
            Position = 10000;
            IsChecked = false;
        }

        public override void DoConvert()
        {

        }
    }

    public class FindMergedLs : KvcConvertCase
    {
        public FindMergedLs()
        {
            ConvertCaseName = "Найти слитые ЛС";
            Position = 10001;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(ConvertingDates.Length + 1);
            var mergedLs = new List<MergedLs>();
            ConvertRsnFiles(rsnAbonent =>
            {
                if (rsnAbonent.СостояниеЛС == 7)
                    mergedLs.Add(new MergedLs { Ls = rsnAbonent.LsKvc.CombinedLs, Date = CurrnetConvertDate });

            }, null, RsnFilePath, false, ConvertingDates, true);

            var sb = new StringBuilder("");
            for (int i = 0; i < mergedLs.Count; i++)
            {
                var merge = mergedLs[i];
                sb.AppendLine($"{merge.Ls}{merge.Date.ToString("d")}");
            }
            File.Delete("MergedLs.csv");
            File.WriteAllText("MergedLs.csv", sb.ToString());
        }

        private class MergedLs
        {
            public string Ls;
            public DateTime Date;
        }
    }

    public class GetNoCalcAvg : KvcConvertCase
    {
        public GetNoCalcAvg()
        {
            ConvertCaseName = "Получение признаков не начислять среднее";
            Position = 10002;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(99);
            var noCalcAvgList = new List<NoCalcAvg>();
            ConvertRsnFiles(rsnAbonent =>
            {
                for (int i = 0; i < rsnAbonent.ПризнакиНеНачислСреднее.Count; i++)
                {
                    noCalcAvgList.Add(new NoCalcAvg(rsnAbonent.ПризнакиНеНачислСреднее[i], rsnAbonent));
                }

            }, null, RsnFilePath, false, ConvertingDates, true);
            noCalcAvgList = noCalcAvgList.Distinct().ToList();
        }

        private struct NoCalcAvg
        {
            public LsKvc LsKvc;
            public byte Vid;
            public DateTime Date;

            public NoCalcAvg(Record_020 record, RsnAbonent abonent)
            {
                LsKvc = abonent.LsKvc;
                Date = abonent.FileDate;
                Vid = record.Вид;
            }
        }
    }

    public class ConvertGazCounters : KvcConvertCase
    {
        public ConvertGazCounters()
        {
            ConvertCaseName = "Конвертация счетчиков по газу";
            Position = 10003;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(4 + ConvertingDates.Length);
            var lc = new List<CNV_LCHAR>();
            ConvertRsnFiles(rsnAbonent =>
            {
                var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == 2);
                if (alg == null) return;

                string lshet = LsDic[rsnAbonent.LsKvc.Ls];
                long sortLs = long.Parse(lshet);
                lc.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = sortLs,
                    LCHARCD = 36,
                    VALUE_ = alg.Алгоритм >= 101 ? 1 : 0,
                    DATE_ = CurrnetConvertDate
                });
                int gazType;
                switch (rsnAbonent.ГазовоеОборудование)
                {
                    case 5:
                        gazType = 9;
                        break;
                    case 20:
                        gazType = 1;
                        break;
                    case 30:
                        gazType = 2;
                        break;
                    case 35:
                        gazType = 6;
                        break;
                    case 120:
                        gazType = 4;
                        break;
                    case 125:
                        gazType = 8;
                        break;
                    case 100:
                        gazType = 3;
                        break;
                    case 105:
                        gazType = 7;
                        break;
                    default:
                        gazType = 0;
                        break;
                }
                lc.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    SortLshet = sortLs,
                    LCHARCD = 10,
                    VALUE_ = gazType,
                    DATE_ = CurrnetConvertDate
                });
            }, () =>
            {
                if (ПроряжатьХарактеристики) lc = LcharsRecordUtils.ThinOutList(lc, true);
            }, RsnFilePath, false, ConvertingDates, true);

            StepStart(lc.Count);
            long lastLs = -1;
            int? lastChar = -1;
            for (int i = 0; i < lc.Count; i++)
            {
                var chr = lc[i];
                if (chr.VALUE_ == 0 && (chr.SortLshet != lastLs || chr.LCHARCD != lastChar))
                {
                    lc[i] = null;
                }
                else
                {
                    lastLs = chr.SortLshet;
                    lastChar = chr.LCHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lc.RemoveAll(c => c == null);
            StepFinish();

            SaveListInsertSQL(lc, InsertRecordCount);

            FreeListMemory(lc);
        }
    }

    public class ConvertNewSaldo : KvcConvertCase
    {
        public ConvertNewSaldo()
        {
            ConvertCaseName = "Новое сальдо";
            Position = 10004;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(3);
            var lsaldo = new List<CNV_NACHOPL>();
            ConvertRsnAbonent(rsnAbonent =>
            {
                var allSaldoVids = rsnAbonent.СальдоНаНачало.Select(s => s.Вид).ToArray();

                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;
                for (int j = 0; j < allSaldoVids.Length; j++)
                {
                    var vid = allSaldoVids[j];
                    string isPeni = "0";
                    if (RsnHelper.PeniResources.Contains(vid))
                    {
                        continue;
                        vid = PeniRecode[vid];
                        isPeni = "1";
                    }
                    int serviceCd;
                    var bsaldo = rsnAbonent.СальдоНаНачало.FirstOrDefault(s => s.Вид == vid);
                    var alg = rsnAbonent.Алгоритмы.FirstOrDefault(a => a.Вид == vid);
                    if (alg == null)
                    {
                        serviceCd = resourceRecode[vid];
                    }
                    else
                    {
                        var owner = alg.ХозяинВида;
                        var ownerRecode = OwnersRecode[vid][owner];
                        var resource = resourceRecode[vid];
                        serviceCd = resource * 10000 + ownerRecode;
                    }

                    lsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        SERVICECD = serviceCd,
                        SERVICENAME = isPeni,
                        MONTH_ = rsnAbonent.FileMonth,
                        MONTH2 = rsnAbonent.FileMonth,
                        YEAR_ = rsnAbonent.FileYear,
                        YEAR2 = rsnAbonent.FileYear,
                        BDEBET = bsaldo == null ? 0 : bsaldo.Сумма,
                        EDEBET = 0
                    });
                }
            }, 2017, 06, true, true);

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lsaldo, InsertRecordCount);
            FreeListMemory(lsaldo);
        }
    }

    public class FindDiffSaldo : KvcConvertCase
    {
        public FindDiffSaldo()
        {
            ConvertCaseName = "Найти различное сальдо";
            Position = 10005;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(5);
            var endsaldo = new List<CNV_NACHOPL>();
            var begsaldo = new List<CNV_NACHOPL>();

            ConvertRsnAbonent(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                for (int i = 0; i < rsnAbonent.СальдоНаКонец.Count; i++)
                {
                    var saldo = rsnAbonent.СальдоНаКонец[i];
                    if (RsnHelper.PeniResources.Contains(saldo.Вид)) continue;
                    int serviceCd = ConvertNachopl.CalcServiceCd(saldo.Вид, rsnAbonent);
                    endsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        YEAR_ = 2017,
                        MONTH_ = 04,
                        SERVICECD = serviceCd,
                        EDEBET = saldo.Сумма,
                        SERVICENAME = saldo.Вид.ToString()
                    });
                }
            }, 2017, 04, true, false);

            ConvertRsnAbonent(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                for (int i = 0; i < rsnAbonent.СальдоНаНачало.Count; i++)
                {
                    var saldo = rsnAbonent.СальдоНаНачало[i];
                    int serviceCd;
                    if (RsnHelper.PeniResources.Contains(saldo.Вид)) continue;
                    serviceCd = ConvertNachopl.CalcServiceCd(saldo.Вид, rsnAbonent);
                    begsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        YEAR_ = 2017,
                        MONTH_ = 05,
                        SERVICECD = serviceCd,
                        BDEBET = saldo.Сумма,
                        SERVICENAME = saldo.Вид.ToString()
                    });
                }
            }, 2017, 05, true, true);

            var diffSaldo = new List<CNV_NACHOPL>();

            StepStart(1);
            var abonentsServices = endsaldo
                .Select(es => new { es.LSHET, es.SERVICECD })
                .Union(begsaldo
                    .Select(bs => new { bs.LSHET, bs.SERVICECD }))
                .Distinct()
                .ToArray();

            var endSaldoDic = endsaldo
                .GroupBy(es => es.LSHET)
                .Select(g => new
                {
                    Lshet = g.Key,
                    Services = g.ToDictionary(es => es.SERVICECD, es => es.EDEBET)
                })
                .ToDictionary(es => es.Lshet, es => es.Services);
            var begSaldoDic = begsaldo
                .GroupBy(bs => bs.LSHET)
                .Select(g => new
                {
                    Lshet = g.Key,
                    Services = g.ToDictionary(bs => bs.SERVICECD, bs => bs.BDEBET)
                })
                .ToDictionary(bs => bs.Lshet, bs => bs.Services);
            FreeListMemory(endsaldo);
            FreeListMemory(begsaldo);
            StepFinish();

            StepStart(100);
            int stepCount = abonentsServices.Length / 100;
            int j = 0;
            foreach (var service in abonentsServices)
            {
                if (++j % stepCount == 0) Iterate();

                decimal esaldo = 0m;
                if (endSaldoDic.ContainsKey(service.LSHET))
                {
                    var saldoDic = endSaldoDic[service.LSHET];
                    if (saldoDic.ContainsKey(service.SERVICECD))
                        esaldo = saldoDic[service.SERVICECD];
                }
                decimal bsaldo = 0m;
                if (begSaldoDic.ContainsKey(service.LSHET))
                {
                    var saldoDic = begSaldoDic[service.LSHET];
                    if (saldoDic.ContainsKey(service.SERVICECD))
                        bsaldo = saldoDic[service.SERVICECD];
                }

                decimal diff = bsaldo - esaldo;
                if (Math.Abs(diff) < 0.01m) continue;
                diffSaldo.Add(new CNV_NACHOPL
                {
                    LSHET = service.LSHET,
                    YEAR_ = 2017,
                    MONTH_ = 05,
                    YEAR2 = 2017,
                    MONTH2 = 05,
                    SERVICECD = service.SERVICECD,
                    SERVICENAME = service.SERVICECD.ToString(),
                    BDEBET = bsaldo,
                    EDEBET = esaldo,
                    FNATH = 0,
                    PROCHL = bsaldo - esaldo,
                    OPLATA = 0
                });
            }
            StepFinish();

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(diffSaldo, InsertRecordCount);
            FreeListMemory(diffSaldo);
        }
    }

    public class FindDiffSaldoPeni : KvcConvertCase
    {
        public FindDiffSaldoPeni()
        {
            ConvertCaseName = "Найти различное сальдо по пене";
            Position = 10006;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(5);
            var endsaldo = new List<CNV_NACHOPL>();
            var begsaldo = new List<CNV_NACHOPL>();

            ConvertRsnAbonent(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                for (int i = 0; i < rsnAbonent.СальдоНаКонец.Count; i++)
                {
                    var saldo = rsnAbonent.СальдоНаКонец[i];
                    if (!RsnHelper.PeniResources.Contains(saldo.Вид)) continue;
                    int serviceCd = ConvertNachopl.CalcServiceCd(PeniRecode[saldo.Вид], rsnAbonent.Алгоритмы.First(a => a.Вид == saldo.Вид).ХозяинВида);
                    endsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        YEAR_ = 2017,
                        MONTH_ = 04,
                        SERVICECD = serviceCd,
                        EDEBET = saldo.Сумма,
                        SERVICENAME = saldo.Вид.ToString()
                    });
                }
            }, 2017, 04, true, false);

            ConvertRsnAbonent(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                for (int i = 0; i < rsnAbonent.СальдоНаНачало.Count; i++)
                {
                    var saldo = rsnAbonent.СальдоНаНачало[i];
                    int serviceCd;
                    if (!RsnHelper.PeniResources.Contains(saldo.Вид)) continue;
                    serviceCd = ConvertNachopl.CalcServiceCd(PeniRecode[saldo.Вид], rsnAbonent.Алгоритмы.First(a => a.Вид == saldo.Вид).ХозяинВида);
                    begsaldo.Add(new CNV_NACHOPL
                    {
                        LSHET = lshet,
                        YEAR_ = 2017,
                        MONTH_ = 05,
                        SERVICECD = serviceCd,
                        BDEBET = saldo.Сумма,
                        SERVICENAME = saldo.Вид.ToString()
                    });
                }
            }, 2017, 05, true, true);

            var diffSaldo = new List<CNV_NACHOPL>();

            StepStart(1);
            var abonentsServices = endsaldo
                .Select(es => new { es.LSHET, es.SERVICECD })
                .Union(begsaldo
                    .Select(bs => new { bs.LSHET, bs.SERVICECD }))
                .Distinct()
                .ToArray();

            var endSaldoDic = endsaldo
                .GroupBy(es => es.LSHET)
                .Select(g => new
                {
                    Lshet = g.Key,
                    Services = g.ToDictionary(es => es.SERVICECD, es => es.EDEBET)
                })
                .ToDictionary(es => es.Lshet, es => es.Services);
            var begSaldoDic = begsaldo
                .GroupBy(bs => bs.LSHET)
                .Select(g => new
                {
                    Lshet = g.Key,
                    Services = g.ToDictionary(bs => bs.SERVICECD, bs => bs.BDEBET)
                })
                .ToDictionary(bs => bs.Lshet, bs => bs.Services);
            FreeListMemory(endsaldo);
            FreeListMemory(begsaldo);
            StepFinish();

            StepStart(100);
            int stepCount = abonentsServices.Length / 100;
            int j = 0;
            foreach (var service in abonentsServices)
            {
                if (++j % stepCount == 0) Iterate();

                decimal esaldo = 0m;
                if (endSaldoDic.ContainsKey(service.LSHET))
                {
                    var saldoDic = endSaldoDic[service.LSHET];
                    if (saldoDic.ContainsKey(service.SERVICECD))
                        esaldo = saldoDic[service.SERVICECD];
                }
                decimal bsaldo = 0m;
                if (begSaldoDic.ContainsKey(service.LSHET))
                {
                    var saldoDic = begSaldoDic[service.LSHET];
                    if (saldoDic.ContainsKey(service.SERVICECD))
                        bsaldo = saldoDic[service.SERVICECD];
                }

                decimal diff = bsaldo - esaldo;
                if (Math.Abs(diff) < 0.01m) continue;
                diffSaldo.Add(new CNV_NACHOPL
                {
                    LSHET = service.LSHET,
                    YEAR_ = 2017,
                    MONTH_ = 05,
                    YEAR2 = 2017,
                    MONTH2 = 05,
                    SERVICECD = service.SERVICECD,
                    SERVICENAME = service.SERVICECD.ToString(),
                    BDEBET = bsaldo,
                    EDEBET = esaldo,
                    FNATH = 0,
                    PROCHL = bsaldo - esaldo,
                    OPLATA = 0
                });
            }
            StepFinish();

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(diffSaldo, InsertRecordCount);
            FreeListMemory(diffSaldo);
        }
    }

    public class GetCitizenWithEndDate : KvcConvertCase
    {
        public GetCitizenWithEndDate()
        {
            ConvertCaseName = "Получить граждан с датой снятия с регистрации";
            Position = 10007;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            var sb = new StringBuilder("");
            ConvertUniqueCCAbonents(ccAbonent =>
            {
                for (int i = 0; i < ccAbonent.Жители.Count; i++)
                {
                    var kvcCitizen = ccAbonent.Жители[i];

                    if (kvcCitizen.ДатаСнятияСРегистрации == null) continue;
                    string lshet = LsDic[ccAbonent.LsKvc.Ls];

                    sb.AppendLine($"{lshet.Substring(2, 6)}{kvcCitizen.НомерЖителя:D2}");
                }

            }, CcFileName, LsNotInLastFile, false, ConvertingDates, WithRsn3);
            File.Delete("CitsWithEndDate.txt");
            File.WriteAllText("CitsWithEndDate.txt", sb.ToString());
        }
    }

    public class GetCitizenNoReg : KvcConvertCase
    {
        public GetCitizenNoReg()
        {
            ConvertCaseName = "Получить с датаой снятия с регитсрации = 01.05.2017";
            Position = 10008;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            var sb = new StringBuilder("");
            ConvertUniqueCCAbonents(ccAbonent =>
            {
                for (int i = 0; i < ccAbonent.Жители.Count; i++)
                {
                    var c = ccAbonent.Жители[i];

                    if (c.ДатаСнятияСРегистрации.HasValue && c.ДатаСнятияСРегистрации.Value.Year == 2017 &&
                        c.ДатаСнятияСРегистрации.Value.Month == 05 && c.ДатаСнятияСРегистрации.Value.Day == 01)
                    {
                        sb.AppendLine($"{LsDic[ccAbonent.LsKvc.Ls].Substring(2, 6)}{c.НомерЖителя:D2}");
                    }
                }

            }, CcFileName, LsNotInLastFile, false, ConvertingDates, WithRsn3);
            File.Delete("CitsForNoReg.txt");
            File.WriteAllText("CitsForNoReg.txt", sb.ToString());
        }
    }

    public class GetCitizenWithDolya : KvcConvertCase
    {
        public GetCitizenWithDolya()
        {
            ConvertCaseName = "Получить граждан с долей собственности = 1";
            Position = 10009;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            var sb = new StringBuilder("");
            ConvertUniqueCCAbonents(ccAbonent =>
            {
                for (int i = 0; i < ccAbonent.Жители.Count; i++)
                {
                    var c = ccAbonent.Жители[i];

                    if (String.IsNullOrWhiteSpace(c.ДоляПлощади)) continue;
                    int dolya;
                    if (int.TryParse(c.ДоляПлощади, out dolya))
                        if (dolya == 1)
                            sb.AppendLine($"{LsDic[ccAbonent.LsKvc.Ls].Substring(2, 6)}{c.НомерЖителя:D2}");
                }

            }, CcFileName, LsNotInLastFile, false, ConvertingDates, WithRsn3);
            File.Delete("CitsWithDolya.txt");
            File.WriteAllText("CitsWithDolya.txt", sb.ToString());
        }
    }

    public class FindFilials : KvcConvertCase
    {
        public FindFilials()
        {
            ConvertCaseName = "Филиалы организаций";
            Position = 10010;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            var orgFlials = new Dictionary<int, int>();
            using (var firebird = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                firebird.Open();
                var cmd = firebird.CreateCommand();
                cmd.CommandText = "select eac.extorgcd, eac.significance from extorgadditionalchars eac where eac.additionalcharcd = 6299101";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orgFlials.Add(reader.GetInt32(0), reader.GetInt32(1));
                }
            }

            //var ownersRecode = new Dictionary<byte, Dictionary<ushort, int>>();
            //var mikroToMakro = File.ReadAllLines(aConverter_RootSettings.SourceDbfFilePath + "\\MikroToMakro.csv");
            //foreach (var s in mikroToMakro)
            //{
            //    string[] info = s.Split(';');
            //    byte vid = byte.Parse(info[0]);
            //    ushort mikro = ushort.Parse(info[1]);
            //    int makro = int.Parse(info[2]);

            //    Dictionary<ushort, int> vidRecode;
            //    if (ownersRecode.TryGetValue(vid, out vidRecode))
            //    {
            //        vidRecode.Add(mikro, makro);
            //        ownersRecode[vid] = vidRecode;
            //    }
            //    else
            //    {
            //        vidRecode = new Dictionary<ushort, int> { { mikro, makro } };
            //        ownersRecode.Add(vid, vidRecode);
            //    }
            //}
            //Console.WriteLine("Загружена перекодировка хозяев");

            var ownersAddSpr = spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);
            var ownerRecode = spr1.GetSubSpr(Spr1.SubSpr.ВходимостьХозяев);

            foreach (var ownerAdd in ownersAddSpr)
            {
                var fil = ownerAdd.R2;
                if (fil == 0) continue;

                var makro = ownerRecode.Any(or => or.R3 == ownerAdd.R1) ? ownerRecode.Where(or => or.R3 == ownerAdd.R1).Select(or => or.R2).Min() : ownerAdd.R1;

                if (!orgFlials.ContainsKey(makro))
                {
                    MessageBox.Show($"Не найден хозяин {makro}");
                    continue;
                }
                if (orgFlials[makro] != fil)
                {
                    MessageBox.Show($"Не соответствие филиалов справочник {fil} база {orgFlials[makro]}");
                    continue;
                }
            }
            MessageBox.Show("Конец");
        }
    }

    public class ConvertMissingHChars : KvcConvertCase
    {
        public ConvertMissingHChars()
        {
            ConvertCaseName = "Missing HCHARS - отсутсвующие характеристики домов";
            Position = 10011;
            IsChecked = false;
            //TotalConvertIterationCount = 5;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(8 - 1 + ConvertingDates.Length);

            var convertedHouses = new Dictionary<string, int>();
            var lsInHouse = new Dictionary<int, int>();
            var llc = new List<CNV_LCHARHOUSES>();
            var lcc = new List<CNV_CHARSHOUSES>();


            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<HouseExt>(@"select HOUSECD, EXTLSHET from cnv$abonent
                                                                union
                                                                select housecd, extlshet from abonents a
                                                                inner join extorgaccounts ea on ea.lshet = a.lshet");
                StepStart(sqlResult.Count);
                int lastHouse = 0;
                for (int i = 0; i < sqlResult.Count; i++)
                {
                    var house = sqlResult[i];
                    if (house.HOUSECD != lastHouse)
                    {
                        convertedHouses.Add(house.EXTLSHET, house.HOUSECD);
                        lastHouse = house.HOUSECD;
                        lsInHouse.Add(house.HOUSECD, sqlResult.Count(s => s.HOUSECD == house.HOUSECD));
                    }
                    Iterate();
                }
                StepFinish();
            }


            ConvertRsnFiles(rsnAbonent =>
            {
                int housecd;
                if (!convertedHouses.TryGetValue(rsnAbonent.LsKvc.CombinedLs, out housecd)) return;
                var date = new DateTime(rsnAbonent.FileYear, rsnAbonent.FileMonth, 1);

                Record_200 param;
                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 16);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31014,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });

                param = rsnAbonent.ПараметрыДома.FirstOrDefault(p => p.КодРеквезита == 17);
                lcc.Add(new CNV_CHARSHOUSES
                {
                    HOUSECD = housecd,
                    CHARCD = 31013,
                    VALUE_ = param == null ? 0 : param.Значение,
                    DATE_ = date
                });
            }, () =>
            {
                lcc = HcharsRecordUtils.ThinOutList(lcc);
                llc = HcharsRecordUtils.ThinOutList(llc);
            },
            RsnFilePath, false, ConvertingDates, WithRsn3);

            StepStart(lcc.Count);
            int? lastHc = -1;
            int? lastChar = -1;
            for (int i = 0; i < lcc.Count; i++)
            {
                var chr = lcc[i];
                if (chr.VALUE_ == 0 && (chr.HOUSECD != lastHc || chr.CHARCD != lastChar))
                {
                    lcc[i] = null;
                }
                else
                {
                    lastHc = chr.HOUSECD;
                    lastChar = chr.CHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc.RemoveAll(c => c == null);
            StepFinish();

            StepStart(llc.Count);
            lastHc = -1;
            lastChar = -1;
            for (int i = 0; i < llc.Count; i++)
            {
                var chr = llc[i];
                if (chr.VALUE_ == 0 && (chr.HOUSECD != lastHc || chr.LCHARCD != lastChar))
                {
                    llc[i] = null;
                }
                else
                {
                    lastHc = chr.HOUSECD;
                    lastChar = chr.LCHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            llc.RemoveAll(c => c == null);
            StepFinish();

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(llc, InsertRecordCount);

            FreeListMemory(lcc);
            FreeListMemory(llc);
        }

        public override void ActionAfterConvert()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteQuery(@"delete from cnv$charshouses c1
where c1.value_ = 0 and c1.date_ = (select min(c2.date_) from cnv$charshouses c2 where c2.housecd = c1.housecd and c2.charcd = c1.charcd);");
            fbm.ExecuteQuery(@"delete from cnv$lcharhouses c1
where c1.value_ = 0 and c1.date_ = (select min(c2.date_) from cnv$lcharhouses c2 where c2.housecd = c1.housecd and c2.lcharcd = c1.lcharcd);");
        }

        public class HouseExt
        {
            public int HOUSECD { get; set; }
            public string EXTLSHET { get; set; }
            public int LsCount;
        }
    }

    public class FixGroupCountersName : KvcConvertCase
    {
        public FixGroupCountersName()
        {
            ConvertCaseName = "GROUP COUNTERS FIX NAMES - исправить имена групповых счетчиков";
            Position = 10012;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(2 - 1 + ConvertingDates.Length);
            var lcc = new List<CNV_COUNTER>();
            var vids = spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var streets = spr1.GetSubSpr(Spr1.SubSpr.Улицы);
            var houses = spr1.GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир);

            ConvertUniqueRsnAbonents((rsnAbonent) =>
            {
                string lset = LsDic[rsnAbonent.LsKvc.Ls];

                for (int j = 0; j < rsnAbonent.ОПУ.Count; j++)
                {
                    var gcounter = rsnAbonent.ОПУ[j];

                    var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == gcounter.Вид);
                    if (alg == null || (alg.Вид == 6 && alg.Алгоритм != 2) || (alg.Вид != 6 && alg.Алгоритм != 101)) continue;
                    var houseInfo = houses.FirstOrDefault(h => h.R1 == rsnAbonent.LsKvc.Adr1 && h.R2 == 0);
                    string houseNo = houseInfo == null
                        ? "д." + rsnAbonent.LsKvc.HouseId + (rsnAbonent.LsKvc.KorpusId == 0
                            ? ""
                            : " корп." + rsnAbonent.LsKvc.KorpusId)
                        : houseInfo.Sr40;
                    string counterId = $"{rsnAbonent.LsKvc.Adr1:D8}{gcounter.Вид:D2}{gcounter.НомерСчетчика:D2}";
                    string counterName = $"ОПУ {vids.Single(v => v.R1 == gcounter.Вид).Sr40} №{gcounter.НомерСчетчика} {streets.First(s => s.R1 == rsnAbonent.LsKvc.StreetId).Sr40} {houseNo}";

                    var counter = new CNV_COUNTER
                    {
                        LSHET = lset,
                        COUNTERID = counterId,
                        COUNTER_LEVEL = 1,
                        NAME = counterName
                    };
                    lcc.Add(counter);
                }

            }, RsnFilePath, LsNotInLastFile, true, ConvertingDates);

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);

            FreeListMemory(lcc);
        }
    }

    public class ConvertNewPeniSaldo : KvcConvertCase
    {
        public ConvertNewPeniSaldo()
        {
            ConvertCaseName = "Сконвертировать сальдо по пене";
            Position = 10013;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(3);
            var saldoDate = new DateTime(2017, 06, 01);

            var peniSaldoList = new List<CNV_PENISUMMA>();
            ConvertRsnAbonent(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                rsnAbonent.Алгоритмы.Where(alg => RsnHelper.PeniResources.Contains(alg.Вид)).ToList().ForEach(alg =>
                {
                    decimal saldoSum = 0;
                    var saldo = rsnAbonent.СальдоНаНачало.FirstOrDefault(s => s.Вид == alg.Вид);
                    if (saldo != null) saldoSum = saldo.Сумма;

                    peniSaldoList.Add(new CNV_PENISUMMA
                    {
                        LSHET = lshet,
                        FYEAR = saldoDate.Year,
                        FMONTH = saldoDate.Month,
                        SERVICECD = resourceRecode[PeniRecode[alg.Вид]] * 10000 + OwnersRecode[alg.Вид][alg.ХозяинВида],
                        ABONENTSALDO = saldoSum
                    });
                });

            }, saldoDate.Year, saldoDate.Month, true, true);

            SaveListInsertSQL(peniSaldoList, InsertRecordCount);
            FreeListMemory(peniSaldoList);
        }
    }

    public class GetLastKvcInd : KvcConvertCase
    {
        public GetLastKvcInd()
        {
            ConvertCaseName = "Получить последние показания счетчиков";
            Position = 100014;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(2);
            byte vid = 3;
            var owners = new ushort[] {1330, 1333, 1340, 1341, 1342, 1343, 1344, 1345, 1346, 1347, 1348, 1349, 1350, 1351};
            var convertDate = new DateTime(2017, 05, 31);

            var notFoundedLs = new List<string>();
            var indList = new List<CNV_CNTRSIND>();
            ConvertRsnAbonent(rsnAbonent =>
            {
                if (!rsnAbonent.Алгоритмы.Any(alg => alg.Вид == vid && owners.Contains(alg.ХозяинВида))) return;

                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet))
                {
                    notFoundedLs.Add(rsnAbonent.LsKvc.Ls);
                    return;
                }

                rsnAbonent.УЗСнаКонМес.Where(ind => ind.Вид == vid).ToList().ForEach(ind =>
                {
                    indList.Add(new CNV_CNTRSIND
                    {
                        INDDATE = convertDate,
                        INDICATION = ind.Счетчик.Значение,
                        COUNTERID = $"{lshet.Substring(2, 6)}{ind.Вид:D2}{ind.Счетчик.Код:D2}"
                    });
                });
            }, convertDate.Year, convertDate.Month);

            Task.Factory.StartNew(() => MessageBox.Show($"Не найдено {notFoundedLs.Count} лс"));

            SaveListInsertSQL(indList, InsertRecordCount);
        }
    }

    public class GetCitizenMigrationExit : KvcConvertCase
    {
        public GetCitizenMigrationExit()
        {
            ConvertCaseName = "Получить миграцию граждан, которые выбыли после временного отсутствия";
            Position = 100015;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(3 - 1 + ConvertingDates.Length);
            var lcm = new List<CNV_CITIZENMIGRATION>();

            var exitedLs = new Dictionary<int, DateTime>();
            ConvertCcFiles(ccAbonent =>
            {
                string lshet = LsDic[ccAbonent.LsKvc.Ls];
                for (int i = 0; i < ccAbonent.Жители.Count; i++)
                {
                    var citizen = ccAbonent.Жители[i];
                    int citizenid = Int32.Parse($"{lshet.Substring(2, 6)}{citizen.НомерЖителя:D2}");

                    if (citizen.СтатусРегистрации == 3)
                    {
                        //lcm.Add(new CNV_CITIZENMIGRATION
                        //{
                        //    CITIZENID = citizenid,
                        //    DATE_ = CurrnetConvertDate,
                        //    DIRECTION = 2,
                        //    MIGRATIONTYPE = 3
                        //});

                        var curDate = citizen.ДатаОкончанияВыбытия != null
                            ? citizen.ДатаОкончанияВыбытия.Value.AddMonths(1)
                            : DateTime.MaxValue;
                        if (exitedLs.ContainsKey(citizenid)) exitedLs[citizenid] = curDate;
                        else exitedLs.Add(citizenid, curDate);
                    }
                    else
                    {
                        DateTime exitedDate;
                        if (exitedLs.TryGetValue(citizenid, out exitedDate))
                        {
                            int migrType;
                            if (exitedDate >= CurrnetConvertDate)
                            {
                                switch (citizen.СтатусРегистрации)
                                {
                                    case 0:
                                    case 9:
                                        migrType = 0;
                                        break;
                                    case 2:
                                        migrType = 2;
                                        break;
                                    case 1:
                                        continue;
                                    default:
                                        throw new Exception("Неизвестный тип регистрации");
                                }
                                lcm.Add(new CNV_CITIZENMIGRATION
                                {
                                    CITIZENID = citizenid,
                                    DATE_ = CurrnetConvertDate,
                                    DIRECTION = 1,
                                    MIGRATIONTYPE = migrType
                                });
                            }
                        }
                    }
                }
            }
            , () =>
            {
                lcm = lcm.GroupBy(cm => new { cm.CITIZENID, cm.DATE_ }).Select(gcm => gcm.Last()).ToList();
                lcm = CitizenRecordUtils.ThinOutList(lcm);
            }
            , CcFileName, false, ConvertingDates, WithRsn3);

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcm, InsertRecordCount);
        }
    }

    public class GetRemovedOwner : KvcConvertCase
    {
        public GetRemovedOwner()
        {
            ConvertCaseName = "Получить отключенных поставщиков";
            Position = 100016;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(4 + ConvertingDates.Length);
            var lchars = new List<CNV_LCHAR>();
            ConvertRsnFiles(rsnAbonent =>
            {
                var lshet = LsDic[rsnAbonent.LsKvc.Ls];
                var longls = Int64.Parse(lshet);
                foreach (var resource in resourceRecode)
                {
                    if (RsnHelper.PeniResources.Contains(resource.Key)) continue;
                    var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == resource.Key);
                    lchars.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        SortLshet = longls,
                        DATE_ = CurrnetConvertDate,
                        LCHARCD = resourceRecode[resource.Key] + 100,
                        VALUE_ = alg == null ? 0 : OwnersRecode[resource.Key][alg.ХозяинВида]
                    });
                }
            },
            () => lchars = LcharsRecordUtils.ThinOutList(lchars, true),
            RsnFilePath, false, ConvertingDates, false);

            /*  
            StepStart(lchars.Count);
            long lastLs = -1;
            int? lastChar = -1;
            for (int i = 0; i < lchars.Count; i++)
            {
                var chr = lchars[i];
                if (chr.VALUE_ == 0 && (chr.SortLshet != lastLs || chr.LCHARCD != lastChar))
                {
                    lchars[i] = null;
                }
                else
                {
                    lastLs = chr.SortLshet;
                    lastChar = chr.LCHARCD;
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lchars.RemoveAll(c => c == null);
            StepFinish();

            StepStart(1);
            lchars = lchars
                .GroupBy(lc => new {lc.LSHET, lc.LCHARCD})
                .Select(glc =>
                {
                    var maxDate = glc.Max(lc => lc.DATE_);
                    return glc.First(lc => lc.DATE_ == maxDate);
                })
                .ToList();
            StepFinish();
            */

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lchars);
            //SaveListInsertSQL(lchars, InsertRecordCount);
            StepFinish();

            FreeListMemory(lchars);
        }
    }

    public class AddMissingAddChars : KvcConvertCase
    {
        public AddMissingAddChars()
        {
            ConvertCaseName = "Дополнительные характеристики Специализированный ЖФ и Признак ТСЖ,ЖСК";
            Position = 100017;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            if (LsDic == null || !LsDic.Any()) throw new Exception("Необходимо заполнить список лицевых счетов");

            SetStepsCount(2 - 1 + ConvertingDates.Length);

            var lcc = new List<CNV_AADDCHAR>();
            ConvertUniqueRsnAbonents(rsnAbonent =>
            {
                string lshet;
                if (!LsDic.TryGetValue(rsnAbonent.LsKvc.Ls, out lshet)) return;

                lcc.Add(new CNV_AADDCHAR
                {
                    LSHET = lshet,
                    ADDCHARCD = 756,
                    VALUE = rsnAbonent.СпециализированныйЖилищныйФонд.ToString()
                });
                lcc.Add(new CNV_AADDCHAR
                {
                    LSHET = lshet,
                    ADDCHARCD = 757,
                    VALUE = rsnAbonent.ПризнакТсжЖск.ToString()
                });

            }, RsnFilePath, LsNotInLastFile, true, ConvertingDates, WithRsn3);

            //if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) BufferEntitiesManager.SaveDataToBufferIBScript(lcc);

            FreeListMemory(lcc);
        }
    }

    public class FindEmptyFlats : KvcConvertCase
    {
        public FindEmptyFlats()
        {
            ConvertCaseName = "Поиск адресов без квартир (Задача 27845)";
            Position = 100018;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            Spr[] flats = new Spr1(SprYearToConvert, SprMonthToConvert).GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир).Where(s => s.R2 != 0).ToArray();
            var fb = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            var badLs = new List<string>();
            using (var dt = fb.ExecuteQuery(@"select a.lshet, ea.extlshet, a.flatno, a.flatpostfix
from abonents a
inner join extorgaccounts ea on ea.lshet = a.lshet and ea.extorgcd = 1"))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var abonent = new CNV_ABONENT
                    {
                        LSHET = dr["lshet"].ToString(),
                        EXTLSHET = dr.IsNull("extlshet") ? null : dr["extlshet"].ToString(),
                        FLATNO = dr.IsNull("flatno") ? (int?)null : Convert.ToInt32(dr["flatno"]),
                        FLATPOSTFIX = dr.IsNull("flatpostfix") ? null : dr["flatpostfix"].ToString()
                    };
                    var lsKvc = new LsKvc(abonent.EXTLSHET, false);
                    var flatKvc = flats.FirstOrDefault(f => f.R1 == lsKvc.Adr1 && f.R2 == lsKvc.Adr2);
                    if (flatKvc == null) continue;
                    if (String.IsNullOrWhiteSpace(flatKvc.Sr40) && (abonent.FLATNO != null || !String.IsNullOrWhiteSpace(abonent.FLATPOSTFIX)))
                    {
                        badLs.Add(abonent.LSHET);
                    }
                }
            }
            Clipboard.SetText(String.Join("\r\n", badLs));
        }
    }

    public class FindBadGas : KvcConvertCase
    {
        public FindBadGas()
        {
            ConvertCaseName = "Поиск абонентов с газовым оборудованием, но без начислений (Задача 30230)";
            Position = 100019;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(4 + ConvertingDates.Length);
            var lsList = new List<string>();
            ConvertRsnFiles(rsnAbonent =>
            {
                var alg = rsnAbonent.Алгоритмы.FirstOrDefault(al => al.Вид == 2);
                if (alg == null) return;
                if (rsnAbonent.ГазовоеОборудование == 0) return;
                if (alg.Алгоритм > 0) return;
                lsList.Add(rsnAbonent.LsKvc.ToString());
            }, null, RsnFilePath, false, ConvertingDates, true);
            lsList = lsList.Distinct().ToList();
            Clipboard.SetText(String.Join("\r\n", lsList));
        }
    }

    public class FindRemovedCitizensAterNaem : KvcConvertCase
    {
        public FindRemovedCitizensAterNaem()
        {
            ConvertCaseName = "Поиск граждан, присутствующих при конвертации старого наема, но отсутствующих при последнее конвертации (Задача 30807, лс 99081116)";
            Position = 100020;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(3);
            var abonents = new Dictionary<string, CcAbonent[]>();
            ConvertUniqueCCAbonents(ccAbonent =>
            {
                ccAbonent.Жители.ForEach(c => c.ДопИнформация = CitizenComparer.GetCompareString(c.ФИО, c.ДатаРождения));
                abonents.Add(ccAbonent.LsKvc.Ls, new[] {ccAbonent, null});
            }, CcFileName, LsNotInLastFile, true, ConvertingDates.Where(d => d < new DateTime(2015, 09, 01)).ToArray(), false);

            ConvertUniqueCCAbonents(ccAbonent =>
            {
                if (!abonents.ContainsKey(ccAbonent.LsKvc.Ls)) return;
                ccAbonent.Жители.ForEach(c => c.ДопИнформация = CitizenComparer.GetCompareString(c.ФИО, c.ДатаРождения));
                abonents[ccAbonent.LsKvc.Ls][1] = ccAbonent;
            }, CcFileName, LsNotInLastFile, true, ConvertingDates.Where(d => d >= new DateTime(2015, 09, 01)).ToArray(), true);

            var removedCitizens = new List<CcAbonent.Citizen>();
            StepStart(abonents.Count + 1);
            foreach (var abonent in abonents)
            {
                Iterate();
                if (abonent.Value[1] == null) continue;
                var newCitizens = abonent.Value[1].Жители.Select(c => c.ДопИнформация).ToArray();
                abonent.Value[0].Жители.ForEach(c =>
                {
                    if (!newCitizens.Contains(c.ДопИнформация)) removedCitizens.Add(c);
                });
            }
            StepFinish();

            StepStart(1);
            var citizenComparer = new CitizenComparer(aConverter_RootSettings.FirebirdStringConnection);
            citizenComparer.LoadCitizens();
            StepFinish();

            StepStart(removedCitizens.Count + 1);
            removedCitizens.ForEach(c =>
            {
                Iterate();
                string lshet;
                if (!LsDic.TryGetValue(c.LsKvc.Ls, out lshet)) return;
                c.НомерЕГРП = lshet;
                var citizenid = citizenComparer.GetCitizenId(new CompareKey(lshet, c.ФИО, c.ДатаРождения));
                c.НомерПаспорта = citizenid == null ? 0 : (uint)citizenid;
            });
            removedCitizens.RemoveAll(c => c.НомерПаспорта == 0);
            StepFinish();

            StepStart(1);
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbManager.ExecuteNonQuery("DELETE FROM CNV$CITIZENS");
            BufferEntitiesManager.SaveDataToBufferIBScript(removedCitizens.Select(c => new CitizenIdOrm(c)));
            StepFinish();
        }

        private class CitizenIdOrm : IOrmRecord
        {
            private int _id;

            public CitizenIdOrm(CcAbonent.Citizen citizen)
            {
                _id = (int)citizen.НомерПаспорта;
            }

            public const string InsertSqlTemplate =
           "INSERT INTO CNV$CITIZENS(ID, ISMAINCITYZEN, HIDDEN, REGISTRATIONTYPE) VALUES({0}, 0, 0, 0);";

            public string InsertSql => string.Format(InsertSqlTemplate,
                ToSql(_id));
        }
    }

    #endregion
}