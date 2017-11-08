using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using FirebirdSql.Data.FirebirdClient;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.CcChange;
using _045_KvcChangesImport.ChangeFiles.GGMMChanges;
using _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType;
using _045_KvcChangesImport.ChangeFiles.IdomChange;
using _049_Zheu18;
using static _049_Zheu18.Consts;
using static _045_KvcChangesImport.ChangesConsts;

namespace _045_KvcChangesImport
{
    public static class ChangesConsts
    {
        public static readonly string KodGraphFilePath = aConverter_RootSettings.SourceDbfFilePath + @"\Graph.xlsx";
        public static readonly string AlgChangeRecodeFilePath = aConverter_RootSettings.SourceDbfFilePath + @"\LcharsMonthRmp.xlsx";

        public static GraphInfo[] AllGraphInfo;

        public static int ImportYear = 0;
        public static int ImportMonth = 0;
    }
    
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = -10100;
            IsChecked = false;
        }

        public override void DoConvert()
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

            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbc.Open();
                var cmd = fbc.CreateCommand();
                cmd.CommandText = SQL.KvcChangesCitizensImport;
                cmd.ExecuteNonQuery();
                cmd.CommandText = SQL.KvcChangesHouseCchImport;
                cmd.ExecuteNonQuery();
                cmd.CommandText = SQL.KvcChangesHouseLchImport;
                cmd.ExecuteNonQuery();
                cmd.CommandText = SQL.KvcChangesHouseOdnCounters;
                cmd.ExecuteNonQuery();
                cmd.CommandText = SQL.KvcChangesCitizenRelations;
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class FillConverter : ConvertCase
    {
        public FillConverter()
        {
            ReadRsnForm.RsnFilePath = aConverter_RootSettings.SourceDbfFilePath;
            FullSpr.SPRFilePath = aConverter_RootSettings.SourceDbfFilePath + @"\SPR";

            ConvertCaseName = "Подготовиться к конвертации";
            Position = -10099;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            LsDic = new Dictionary<string, string>();
            resourceRecode = new Dictionary<byte, int>();
            RegimRecode = new Dictionary<string, Dictionary<DateTime, Dictionary<byte, int>>>();

            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select ea.extlshet, ea.lshet from extorgaccounts ea where ea.extorgcd = 1";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var lskvc = new LsKvc(uint.Parse(reader.GetString(0).Substring(0, 8)), uint.Parse(reader.GetString(0).Substring(8, 6)));
                    if (!LsDic.ContainsKey(lskvc.Ls))                     LsDic.Add(lskvc.Ls, reader.GetString(1));
                }
            }

            spr0 = new Spr0(SprYearToConvert, SprMonthToConvert);
            spr1 = new Spr1(SprYearToConvert, SprMonthToConvert);
            spr2 = new Spr2(SprYearToConvert, SprMonthToConvert);
            spr4 = new Spr4(SprYearToConvert, SprMonthToConvert);

            DataTable dt = Utils.ReadExcelFile(aConverter_RootSettings.SourceDbfFilePath + @"\ResAll.xlsx", "Лист1");
            foreach (DataRow dr in dt.Rows)
            {
                resourceRecode.Add(Convert.ToByte(dr[1]), Convert.ToInt32(dr[2]));
            }

            OwnersRecode = new Dictionary<byte, Dictionary<ushort, int>>();
            var mikroToMakro = File.ReadAllLines(aConverter_RootSettings.SourceDbfFilePath + @"\MikroToMakro.csv");
            foreach (var s in mikroToMakro)
            {
                string[] info = s.Split(';');
                byte vid = byte.Parse(info[0]);
                ushort mikro = ushort.Parse(info[1]);
                int makro = int.Parse(info[2]);

                Dictionary<ushort, int> vidRecode;
                if (OwnersRecode.TryGetValue(vid, out vidRecode))
                {
                    vidRecode.Add(mikro, makro);
                    OwnersRecode[vid] = vidRecode;
                }
                else
                {
                    vidRecode = new Dictionary<ushort, int> { { mikro, makro } };
                    OwnersRecode.Add(vid, vidRecode);
                }
            }

            AllGraphInfo = GraphInfo.GetFullGraphInfo(KodGraphFilePath, "Лист1");
        }
    }

    public class DigitChangesImport : KvcChangesCase
    {
        public DigitChangesImport()
        {
            ConvertCaseName = "Импорт числовых изменений";
            Position = -10098;
            IsChecked = false;
        }

        /// <summary>
        /// Объединенный лс квц - ресурс = услуга
        /// </summary>
        public static Dictionary<string, Dictionary<int, int>> AbonentServices;
        /// <summary>
        /// Объединенный лс кц - ресурс = инфа о счетчиках
        /// </summary>
        public static Dictionary<string, Dictionary<int, AbonentCounterOrm>> AbonentCounters;
        /// <summary>
        /// Объединенный лс кц - активность
        /// </summary>
        public static Dictionary<string, AbonentActiveServices> AbonentsActiveServices;

        public static List<CNV_CHAR> CcharsList;
        public static List<CNV_LCHAR> LcharsList;
        public static List<CNV_AADDCHAR> AddCharsList;
        public static List<CNV_NACH> NachList;
        public static List<CNV_OPLATA> PayList;
        public static List<CNV_COUNTER> CounterList;
        public static List<CNV_CNTRSIND> CntIndList;

        public static ErrorLog ErrorLog;

        public static Spr[] VidSpr;
        public static Spr[] CntSpr;
        public static Spr[] AlgSpr;
        public static Dictionary<string, int> CounterTags;

        protected override int AdditionalSteps => 11;
        protected override string FilesMask => "Числовые изменения|???????.chn|Изменения из водоканала|IZM????.xlsx";
        protected override string FileDialogTitle => "Выберите файлы с числовыми изменениями";
        protected override void DoKvcChangesConvert()
        {
            VidSpr = spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            CntSpr = spr4.GetSubSpr(Spr4.SubSpr.КодСчетчика);
            AlgSpr = spr4.GetSubSpr(Spr4.SubSpr.Алгоритмы);
            ErrorLog = new ErrorLog();

            CcharsList = new List<CNV_CHAR>();
            LcharsList = new List<CNV_LCHAR>();
            AddCharsList = new List<CNV_AADDCHAR>();
            NachList = new List<CNV_NACH>();
            PayList = new List<CNV_OPLATA>();
            CounterList = new List<CNV_COUNTER>();
            CntIndList = new List<CNV_CNTRSIND>();
            StepStart(1);
            AbonentServices = FillAbonentServices();
            AbonentCounters = FillAbonentsCounters();
            AbonentsActiveServices = GetAbonentsActiveServices();

            StepFinish();

            StepStart(1);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                string date = new DateTime(ImportYear, ImportMonth, 1).AddMonths(1).ToString("dd.MM.yyyy");
                var sqlResult = context.ExecuteQuery<HouseChangesImport.CounterTagOrm>(
$@"select pq.equipmentid as CounterId, pq.importtag as ImportTag from parentequipment pq
inner join resourcecounters rc on rc.kod = pq.equipmentid
where pq.importtag is not null and rc.counter_level = 0
    and 1 = (select first 1 eqs.statuscd from eqstatuses eqs
                where eqs.equipmentid = rc.kod
                    and eqs.statusdate < cast('{date}' as date)
                order by eqs.statusdate desc)");

                CounterTags = sqlResult.GroupBy(c => c.ImportTag).ToDictionary(c => c.Last().ImportTag, c => c.Last().CounterId);
            }
            StepFinish();

            var notFoundedLs = new List<string>();

            ConvertChanges<GGMMChangeFactory>(changeRecord =>
            {
                var record = (GGMMChangeRecord)changeRecord;

                string lshet;
                if (!Consts.LsDic.TryGetValue(record.LsKvc.Ls, out lshet))
                {
                    notFoundedLs.Add(record.LsKvc.Ls);
                    string error = "Не найден лс " + record.LsKvc.Ls;
                    ErrorLog.Add(record, error);
                    return;
                }

                try
                {
                    var changeType = ChangeTypeSelection.GetChangeTypeByFactory(record);
                    changeType.Convert(record);
                }
                catch (Exception ex)
                {
                    string error = record.LsKvc.Ls + "\r\n" + ex.ToString();
                    Task.Factory.StartNew(() => MessageBox.Show(error));
                    ErrorLog.Add(record, error);
                }
            });

            if (notFoundedLs.Any())
                Task.Factory.StartNew(()=>MessageBox.Show($"Не найдено {notFoundedLs.Distinct().Count()} лицевых счетов. См файл ошибок"));

            StepStart(1);
            var changedCounters = AbonentCounters
                .SelectMany(a => a.Value.Values)
                .Where(c => (c.CounterCount != c.NewCount) && ((c.NewCount > 0) != c.IsCounter))
                .ToArray();
            foreach (var changedCounter in changedCounters)
            {
                var newIsCounter = changedCounter.NewCount > 0;
                if (changedCounter.NewCount == changedCounter.CounterCount ||
                    (newIsCounter) == changedCounter.IsCounter) 
                    continue;
                int lcharcd = 0;
                switch (changedCounter.ResourceId)
                {
                    case 9:
                        lcharcd = 21;
                        break;
                    case 4:
                        lcharcd = 18;
                        break;
                    case 5:
                        lcharcd = 19;
                        break;
                }
                var existedLchar = LcharsList.FirstOrDefault(l => l.LSHET == changedCounter.AbonentLs && l.LCHARCD == lcharcd && l.DATE_ == changedCounter.NewDate);
                if (existedLchar != null)
                {
                    existedLchar.VALUE_ = newIsCounter ? 1 : 0;
                    existedLchar.VALUEDESC = "Смена типа учета";
                }
                else
                {
                    LcharsList.Add(new CNV_LCHAR
                    {
                        LSHET = changedCounter.AbonentLs,
                        LCHARCD = lcharcd,
                        VALUE_ = newIsCounter ? 1 : 0,
                        DATE_ = changedCounter.NewDate,
                        VALUEDESC = "Смена типа учета"
                    });
                }
            }
            StepFinish();

            StepStart(1);
            LcharsList = LcharsList
                .GroupBy(l => new {l.LSHET, l.LCHARCD, l.DATE_})
                .Select(g =>
                {
                    if (g.Count() == 1) return g.First();
                    else if (g.Any(l => l.VALUE_ > 0)) return g.First(l => l.VALUE_ > 0);
                    else return g.First();
                })
                .ToList();
            StepFinish();        

            if (ИнсертитьВоВременныеТаблицы)
            {
                SaveListInsertSQL(CcharsList, InsertRecordCount);
                SaveListInsertSQL(LcharsList, InsertRecordCount);
                SaveListInsertSQL(AddCharsList, InsertRecordCount);
                SaveListInsertSQL(NachList, InsertRecordCount);
                SaveListInsertSQL(PayList, InsertRecordCount);
                SaveListInsertSQL(CounterList, InsertRecordCount);
                SaveListInsertSQL(CntIndList, InsertRecordCount);
            }

            FreeListMemory(CcharsList);
            FreeListMemory(LcharsList);
            FreeListMemory(AddCharsList);
            FreeListMemory(NachList);
            FreeListMemory(PayList);
            FreeListMemory(CounterList);
            FreeListMemory(CntIndList);

            ErrorLog.WriteLogToFile("GGMMChangeErrors.txt");
        }

        public static Dictionary<string, Dictionary<int, int>> FillAbonentServices()
        {            
            Dictionary<string, Dictionary<int, int>> dic;
            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                string date = new DateTime(ImportYear, ImportMonth, 1).AddMonths(1).ToString("dd.MM.yyyy");
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
$@" select * from
(select T.lskvc as CombinedLsKvc,
    T.lchar - 100 as Resource,
    (select first 1 la2.significance + (la2.kodlcharslist - 100) * 10000
    from lcharsabonentlist la2
    where la2.lshet = T.ls and la2.kodlcharslist = T.lchar
    and la2.abonentlchardate < '{date}'
    order by la2.abonentlchardate desc) as ServiceId
from (
select distinct ea.lshet as ls, ea.extlshet as lskvc, la.kodlcharslist as lchar from extorgaccounts ea
inner join lcharsabonentlist la on la.lshet = ea.lshet
where (la.kodlcharslist >= 100 and la.kodlcharslist <= 130)
and ea.extorgcd = 1) as T) as TT
where TT.Serviceid is not null";
                var reader = cmd.ExecuteReader();
                var tempList = new List<AbonentServiceOrm>();
                while (reader.Read())
                {
                    tempList.Add(new AbonentServiceOrm
                    {
                        CombinedLsKvc = reader.GetString(0),
                        Resource = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2)
                    });
                }
                dic = tempList
                    .GroupBy(a => a.CombinedLsKvc)
                    .Select(g => new
                    {
                        LsKvc = g.Key,
                        Services = g.ToDictionary(s => s.Resource, s => s.ServiceId)
                    })
                    .ToDictionary(a => a.LsKvc, a => a.Services);
                FreeListMemory(tempList);
            }
            return dic;
        }

        public class AbonentServiceOrm
        {
            public string CombinedLsKvc { get; set; }
            public int Resource { get; set; }
            public int ServiceId { get; set; }
        }

        private static Dictionary<string, Dictionary<int, AbonentCounterOrm>> FillAbonentsCounters()
        {
            Dictionary<string, Dictionary<int, AbonentCounterOrm>> dic;

            string date = new DateTime(ImportYear, ImportMonth, 1).AddMonths(1).ToString("dd.MM.yyyy");

            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
$@"select ea.extlshet,
    a.lshet,
    rl.kod,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and
        ((rl.kod = 9 and la.kodlcharslist = 21) or
        (rl.kod = 4 and la.kodlcharslist = 18) or
        (rl.kod = 5 and la.kodlcharslist = 19))
        and la.abonentlchardate < cast('{date}' as date)
    order by la.abonentlchardate desc) as isCounter,
    (select count(0) from abonentsequipment ae
    inner join resourcecounters rc on ae.equipmentid = rc.kod
    inner join counterstypes cts on cts.kod = rc.kodcounterstypes
    inner join eqpmstypes et on et.kod = cts.equipmenttypeid
    where ae.lshet = a.lshet and rc.counter_level = 0 and et.kodresourceslist = rl.kod
        and 1 = (select first 1 eqs.statuscd from eqstatuses eqs
                where eqs.equipmentid = ae.equipmentid
                    and eqs.statusdate < cast('{date}' as date)
                order by eqs.statusdate desc)) as counterCount
from abonents a
inner join resourceslist rl on rl.kod in (9, 4, 5)
inner join extorgaccounts ea on ea.lshet = a.lshet and ea.extorgcd = 1";
                var reader = cmd.ExecuteReader();
                var tempList = new List<AbonentCounterOrm>();
                while (reader.Read())
                {
                    tempList.Add(new AbonentCounterOrm
                    {
                        CombinedLsKvc = reader.GetString(0),
                        AbonentLs = reader.GetString(1),
                        ResourceId = reader.GetInt32(2),
                        IsCounter = reader.GetString(3) == "1",
                        CounterCount = reader.GetInt32(4),
                        NewCount = reader.GetInt32(4)
                    });
                }
                dic = tempList
                    .GroupBy(a => a.CombinedLsKvc)
                    .Select(g => new
                    {
                        LsKvc = g.Key,
                        Counters = g.ToDictionary(s => s.ResourceId)
                    })
                    .ToDictionary(a => a.LsKvc, a => a.Counters);
                FreeListMemory(tempList);
            }
            return dic;
        }

        public class AbonentCounterOrm
        {
            public string CombinedLsKvc;
            public string AbonentLs;
            public int ResourceId;
            public bool IsCounter;
            public int CounterCount;
            public int NewCount;
            public DateTime NewDate;
        }

        public static Dictionary<string, AbonentActiveServices> GetAbonentsActiveServices()
        {
            string date = new DateTime(ImportYear, ImportMonth, 1).ToString("dd.MM.yyyy");
            Dictionary<string, AbonentActiveServices> dic;
            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
$@"select
    ea.extlshet,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and la.kodlcharslist = 31
    and la.abonentlchardate <= '{date}'
    order by la.abonentlchardate desc) as is05Active,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and la.kodlcharslist = 45
    and la.abonentlchardate <= '{date}'
    order by la.abonentlchardate desc) as is06Active,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and la.kodlcharslist = 27
    and la.abonentlchardate <= '{date}'
    order by la.abonentlchardate desc) as is07Active,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and la.kodlcharslist = 12
    and la.abonentlchardate <= '{date}'
    order by la.abonentlchardate desc) as is03Active,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = a.lshet and la.kodlcharslist = 10
    and la.abonentlchardate <= '{date}'
    order by la.abonentlchardate desc) as is02Active
from abonents a
inner join extorgaccounts ea on ea.lshet = a.lshet and ea.extorgcd = 1";
                var reader = cmd.ExecuteReader();
                var tempList = new List<AbonentActiveServices>();
                while (reader.Read())
                {
                    var activeServices = new AbonentActiveServices
                    {
                        Lshet = reader.GetString(0)
                    };

                    var serviceInfo = reader.GetString(1);
                    if (String.IsNullOrWhiteSpace(serviceInfo) || int.Parse(serviceInfo.ToString()) == 0)
                        activeServices.Is05Active = false;
                    else activeServices.Is05Active = true;

                    serviceInfo = reader.GetString(2);
                    if (String.IsNullOrWhiteSpace(serviceInfo) || int.Parse(serviceInfo.ToString()) == 0)
                        activeServices.Is06Active = false;
                    else activeServices.Is06Active = true;

                    serviceInfo = reader.GetString(3);
                    if (String.IsNullOrWhiteSpace(serviceInfo) || int.Parse(serviceInfo.ToString()) == 0)
                        activeServices.Is07Active = false;
                    else activeServices.Is07Active = true;

                    serviceInfo = reader.GetString(4);
                    if (String.IsNullOrWhiteSpace(serviceInfo) || int.Parse(serviceInfo.ToString()) == 0)
                        activeServices.Is03Active = false;
                    else activeServices.Is03Active = true;

                    serviceInfo = reader.GetString(5);
                    if (String.IsNullOrWhiteSpace(serviceInfo) || int.Parse(serviceInfo.ToString()) == 0)
                        activeServices.Is02Active = false;
                    else activeServices.Is02Active = true;

                    tempList.Add(activeServices);
                }
                dic = tempList.ToDictionary(a => a.Lshet);
                FreeListMemory(tempList);
            }
            return dic;
        }

        public class AbonentActiveServices
        {
            public string Lshet;
            public bool Is05Active;
            public bool Is06Active;
            public bool Is07Active;
            public bool Is03Active;
            public bool Is02Active;
        }

        public static bool IsServiceActive(string lsKvc, byte vid)
        {
            var services = AbonentsActiveServices[lsKvc];
            switch (vid)
            {
                case 2: return services.Is02Active;
                case 3: return services.Is03Active;
                case 5: return services.Is05Active;
                case 6: return services.Is06Active;
                case 7: return services.Is07Active;
                default: return true;
            }
        }
    }

    public class CitizenChangesImport : KvcChangesCase
    {
        public CitizenChangesImport()
        {
            ConvertCaseName = "Импорт изменений граждан";
            Position = -10097;
            IsChecked = false;
        }
        public static ErrorLog ErrorLog;
        protected override int AdditionalSteps => 8;
        protected override string FilesMask => "Изменения граждан|c???????|DBF|*.dbf.xlsx|Excel|*.xls;*.xlsx|All files|*.*";
        protected override string FileDialogTitle => "Выберите файлы с изменениями граждан";
        protected override void DoKvcChangesConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CITIZENS");
            BufferEntitiesManager.DropTableData("CNV$CITIZENMIGRATION");
            BufferEntitiesManager.DropTableData("CNV$CITIZENRELATIONS");

            var lc = new List<CNV_CITIZEN>();
            var lcr = new List<CNV_CITIZENRELATIONS>();
            var lcm = new List<CNV_CITIZENMIGRATION>();
            ErrorLog = new ErrorLog();

            ConvertCitizens.NotFoundedRelations = new List<ConvertCitizens.ExtOrgCd>();
            ConvertCitizens.NotFoundedCitizenShip = new List<string>();
            DataTable dt = Utils.ReadExcelFile(CitizenRecode, "ship");
            object[] dr = dt.Rows[0].ItemArray;
            ConvertCitizens.recodeShip = new ConvertCitizens.RecodeShip[dr.Length];
            StepStart(dt.Rows.Count);
            for (int i = 0; i < dr.Length; i++)
            {
                ConvertCitizens.recodeShip[i] = new ConvertCitizens.RecodeShip
                {
                    Id = Int32.Parse(dr[i].ToString()),
                    Names = new List<string>()
                };
                if (ConvertCitizens.recodeShip[i].Id == 0) ConvertCitizens.recodeShip[i].Id = null;
            }
            Iterate();
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i].ItemArray;
                for (int j = 0; j < dr.Length; j++)
                {
                    string name = dr[j].ToString();
                    if (String.IsNullOrWhiteSpace(name)) continue;
                    ConvertCitizens.recodeShip[j].Names.Add(name.Trim());
                }
                Iterate();
            }
            StepFinish();
            string[] relationsRecodeLines = File.ReadAllLines(CitizenRecodeRel);
            ConvertCitizens.RelationsRecode = new ConvertCitizens.RecodeRelations[relationsRecodeLines.Length];
            for (int i = 0; i < relationsRecodeLines.Length; i++)
            {
                string[] recodeRelStr = relationsRecodeLines[i].Split('\t');
                ConvertCitizens.RelationsRecode[i] = new ConvertCitizens.RecodeRelations
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
                ConvertCitizens.extOrgsPass = context.ExecuteQuery<ConvertCitizens.ExtOrgCd>(@"select es.extorgcd, es.extorgnm from extorgspr es where es.canissuepassp = 1").ToArray();
                maxOrgCd = context.ExecuteQuery<int>(@"select gen_id(extorgspr_g, 0) from rdb$database")[0];
            }
            StepFinish();

            var notFoundedLs = new List<string>();

            ConvertChanges<CcChangeFactory>(changeRecord =>
            {
                var record = (CcChangeRecord) changeRecord;              

                string lshet;
                if (!Consts.LsDic.TryGetValue(record.LsKvc.Ls, out lshet))
                {
                    notFoundedLs.Add(record.LsKvc.Ls);
                    string error = "Не найден лс " + record.LsKvc.Ls;
                    ErrorLog.Add(record, error);
                    return;
                }

                try
                {
                    ConvertCitizens.ConvertCitizen(record, ref lc, ref lcr);
                }
                catch (KeyNotFoundException ex)
                {
                    Task.Factory.StartNew(() => MessageBox.Show(ex.Message));
                    return;
                }

                for (int i = 0; i < record.Жители.Count; i++)
                {
                    var citizen = record.Жители[i];

                    int citizenid = Int32.Parse($"{lshet.Substring(2, 6)}{citizen.НомерЖителя:D2}");

                    if (citizen.СтатусРегистрации == 3)
                        lcm.Add(new CNV_CITIZENMIGRATION
                        {
                            CITIZENID = citizenid,
                            DATE_ = new DateTime(record.FileYear, record.FileMonth, 1),
                            DIRECTION = 2,
                            MIGRATIONTYPE = 3
                        });
                    if (citizen.ДатаОкончанияВыбытия != null)
                    {
                        var enterDate = citizen.ДатаОкончанияВыбытия.Value.AddMonths(1);
                        int regType = 0;
                        if (citizen.СтатусРегистрации == 1 || citizen.СтатусРегистрации == 3) regType = 1;
                        if (citizen.ДатаОкончанияВремРегистрации != null || citizen.СтатусРегистрации == 2) regType = 2;
                        var cnv = new CNV_CITIZENMIGRATION
                        {
                            CITIZENID = citizenid,
                            DATE_ = enterDate,
                            DIRECTION = 1,
                            MIGRATIONTYPE = regType
                        };
                        lcm.Add(cnv);
                    }
                }
            });

            if (notFoundedLs.Any())
                Task.Factory.StartNew(() => MessageBox.Show($"Не найдено {notFoundedLs.Distinct().Count()} лицевых счетов. См файл ошибок"));

            StepStart(lc.Count);
            lc.ForEach(c =>
            {
                c.UNIQUECITIZENID = CitizenCompare.GetCitizenId(c);
                Iterate();
            });
            StepFinish();

            StepStart(3);
            //lc = lc.GroupBy(c => c.CITIZENID).Select(c => c.First()).ToList();
            Iterate();
            lcr = lcr.GroupBy(c => new {c.CITIZENIDFROM, c.CITIZENIDTO}).Select(c => c.First()).ToList();
            Iterate();
            lcm = lcm.GroupBy(c => new {c.CITIZENID, c.DATE_}).Select(c => c.First()).ToList();
            StepFinish();

            StepStart(1);
            CitizenRecordUtils.SetUniqueDorgcd(lc, maxOrgCd);
            StepFinish();

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcr, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcm, InsertRecordCount);

            FreeListMemory(lc);
            FreeListMemory(lcr);
            FreeListMemory(lcm);

            ErrorLog.WriteLogToFile("CcChangeErrors.txt");
        }
    }

    public class HouseChangesImport : KvcChangesCase
    {
        public HouseChangesImport()
        {
            ConvertCaseName = "Импорт изменений домов";
            Position = -10096;
            IsChecked = false;
        }

        protected override int AdditionalSteps => 6;
        protected override string FilesMask => "Изменения домов|Idom????.???";
        protected override string FileDialogTitle => "Выберите файлы с изменениями домов";
        protected override void DoKvcChangesConvert()
        {
            var convertedHouses = new Dictionary<uint, int>();
            var lastIndDic = new Dictionary<int, decimal?>();
            var counterTags = new Dictionary<string, int>();

            var lcch = new List<CNV_CHARSHOUSES>();
            var llch = new List<CNV_LCHARHOUSES>();
            var lc = new List<CNV_COUNTER>();
            var lci = new List<CNV_CNTRSIND>();

            var vids = spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var streets = spr1.GetSubSpr(Spr1.SubSpr.Улицы);
            var houses = spr1.GetSubSpr(Spr1.SubSpr.ОтклоненияДомовИКвартир);

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<ConvertHChars.HouseExt>(@"select HOUSECD, EXTLSHET from cnv$abonent
                                                                            union
                                                                            select housecd, extlshet from abonents a
                                                                            inner join extorgaccounts ea on ea.lshet = a.lshet and ea.extorgcd = 1");
                StepStart(sqlResult.Count);
                for (int i = 0; i < sqlResult.Count; i++)
                {
                    var house = sqlResult[i];

                    uint adr1 = uint.Parse(house.EXTLSHET.Substring(0, 8));
                    int housecd;
                    if (!convertedHouses.TryGetValue(adr1, out housecd))
                        convertedHouses.Add(adr1, house.HOUSECD);
                    Iterate();
                }
                StepFinish();
            }

            StepStart(1);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<LastIndOrm>(
@"select T.cntId as CounterId,
    (select first 1 cgv.indication from countergroupvolums cgv
    where cgv.kod = T.cntId
    order by cgv.nyear desc, cgv.nmonth desc) as LastInd
from
(select distinct cgv.kod as cntId from countergroupvolums cgv) as T");

                lastIndDic = sqlResult.ToDictionary(i => i.CounterId, i => i.LastInd);
            }
            StepFinish();

            StepStart(1);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<CounterTagOrm>(
@"select pq.equipmentid as CounterId, pq.importtag as ImportTag from parentequipment pq
inner join resourcecounters rc on rc.kod = pq.equipmentid
where pq.importtag is not null and rc.counter_level = 1");

                counterTags = sqlResult.ToDictionary(c => c.ImportTag, c => c.CounterId);
            }
            StepFinish();

            ConvertChanges<IdomChangeFactory>(changeRecord =>
            {
                var record = (IdomChangeRecord)changeRecord;

                int housecd;
                if (!convertedHouses.TryGetValue(record.Адр1, out housecd))
                {
                    new Task(() => MessageBox.Show($"Не удалось сопоставить дом с адр1 = {record.Адр1} в файле {record.FileName}")).Start();
                    return;
                }

                #region Параметры дома
                foreach (var param in record.ПараметрыДома)
                {
                    if (param.КодРеквезита == 1) //год постройки
                    {
                        var buildingYear = param.Значение;
                        if (buildingYear > 0)
                        {
                            if (buildingYear < 20) buildingYear += 2000;
                            else if (buildingYear < 100) buildingYear += 1900;
                        }
                        lcch.Add(new CNV_CHARSHOUSES
                        {
                            HOUSECD = housecd,
                            CHARCD = 101026,
                            VALUE_ = buildingYear,
                            DATE_ = param.ДатаИзменения
                        });
                        lcch.Add(new CNV_CHARSHOUSES
                        {
                            HOUSECD = housecd,
                            CHARCD = -1,
                            VALUE_ = buildingYear,
                            DATE_ = param.ДатаИзменения
                        });
                    }
                    else if (param.КодРеквезита == 6)
                    {
                        string houseEqVal = $"{int.Parse(Convert.ToString(Convert.ToInt32(param.Значение), 2)):D6}";

                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990007,
                            VALUE_ = houseEqVal[0] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990006,
                            VALUE_ = houseEqVal[1] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990005,
                            VALUE_ = houseEqVal[2] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990004,
                            VALUE_ = houseEqVal[3] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990003,
                            VALUE_ = houseEqVal[4] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                        llch.Add(new CNV_LCHARHOUSES
                        {
                            HOUSECD = housecd,
                            LCHARCD = 62990002,
                            VALUE_ = houseEqVal[5] - '0',
                            DATE_ = param.ДатаИзменения
                        });
                    }
                    else if (RsnHelper.NotImportingHouseChars.Contains(param.КодРеквезита)) continue;
                    else
                    {
                        int charcd;
                        if (!RsnHelper.HouseCcharRecode.TryGetValue(param.КодРеквезита, out charcd))
                        {
                            if (!RsnHelper.HouseLcharRecode.TryGetValue(param.КодРеквезита, out charcd))
                            {
                                new Task(() => MessageBox.Show($"Не удалось сопоставить параметр дома с кодом {param.КодРеквезита} для дома {record.Адр1} из файла {record.FileName}")).Start();
                                continue;
                            }
                            else
                                llch.Add(new CNV_LCHARHOUSES
                                {
                                    HOUSECD = housecd,
                                    LCHARCD = charcd,
                                    VALUE_ = (int)param.Значение,
                                    DATE_ = param.ДатаИзменения
                                });
                        }
                        else
                            lcch.Add(new CNV_CHARSHOUSES
                            {
                                HOUSECD = housecd,
                                CHARCD = charcd,
                                VALUE_ = param.Значение,
                                DATE_ = param.ДатаИзменения
                            });
                    }
                }
                #endregion

                #region Потребление по нежилым и отсутствующим

                foreach (var unknownVolume in record.ПотреблениеПоНежилым)
                {
                    lcch.Add(new CNV_CHARSHOUSES
                    {
                        HOUSECD = housecd,
                        CHARCD = resourceRecode[unknownVolume.Вид] + 13000,
                        VALUE_ = unknownVolume.Потребление,
                        DATE_ = unknownVolume.ДатаИзменения
                    });
                }
                #endregion

                return;

                #region Потребление по общедомовым счетчикам
                if (record.ПоказанияОПУ.Count > record.ПотреблениеОПУ.Count)
                {
                    for (int i = 0; i < record.ПоказанияОПУ.Count; i++)
                    {
                        var ind = record.ПоказанияОПУ[i];
                        string counterTag = $"{record.Адр1:D8}{ind.Вид:D2}{ind.НомерСчетчика:D2}";

                        int indType;
                        decimal? lastInd = null;
                        string resultCounterId;
                        int counterId;
                        if (counterTags.TryGetValue(counterTag, out counterId))
                        {
                            lastIndDic.TryGetValue(counterId, out lastInd);
                            resultCounterId = counterId.ToString();
                            indType = 101;
                        }
                        else
                        {
                            resultCounterId = counterTag;
                            indType = -101;
                            record.ОПУ.Add(new Record_201
                            {
                                Вид = ind.Вид,
                                Разрядность = 0,
                                Коэффициент = 1,
                                СтатусСчетчика = 1,
                                НаличиеСчетчика = 1,
                                НомерСчетчика = ind.НомерСчетчика
                            });
                        }


                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = resultCounterId,
                            INDICATION = ind.Значение,
                            INDDATE = record.ДатаПотребления[i].ДатаИзменения,
                            INDTYPE = indType,
                            OB_EM = record.ПотреблениеОПУ.Count > i ? record.ПотреблениеОПУ[i].Значение : (decimal?)null,
                            OLDIND = lastInd,
                            DOCUMENTCD = "import_gcnt",
                        });
                    }
                }
                else
                {
                    for (int i = 0; i < record.ПотреблениеОПУ.Count; i++)
                    {
                        var vol = record.ПотреблениеОПУ[i];
                        string counterTag = $"{record.Адр1:D8}{vol.Вид:D2}{vol.НомерСчетчика:D2}";

                        int indType;
                        decimal? lastInd = null;
                        string resultCounterId;
                        int counterId;
                        if (counterTags.TryGetValue(counterTag, out counterId))
                        {
                            lastIndDic.TryGetValue(counterId, out lastInd);
                            resultCounterId = counterId.ToString();
                            indType = 101;
                        }
                        else
                        {
                            resultCounterId = counterTag;
                            indType = -101;
                            record.ОПУ.Add(new Record_201
                            {
                                Вид = vol.Вид,
                                Разрядность = 0,
                                Коэффициент = 1,
                                СтатусСчетчика = 1,
                                НаличиеСчетчика = 1,
                                НомерСчетчика = vol.НомерСчетчика
                            });
                        }

                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = resultCounterId,
                            OB_EM = vol.Значение,
                            INDDATE = record.ДатаПотребления[i].ДатаИзменения,
                            INDTYPE = indType,
                            INDICATION = record.ПоказанияОПУ.Count > i ? record.ПоказанияОПУ[i].Значение : (decimal?)null,
                            OLDIND = lastInd,
                            DOCUMENTCD = "import_gcnt"
                        });
                    }
                }
                #endregion

                #region Сами групповые счетчики
                foreach (var gcounter in record.ОПУ)
                {
                    int cntType, setupPlace;
                    int? serviceid = GetServiceCd(record.Адр1, gcounter.Вид);
                    try
                    {
                        ConvertGroupCounters.ConvertCounterType(gcounter, out cntType, out setupPlace);
                    }
                    catch (Exception ex)
                    {
                        Task.Factory.StartNew(()=>MessageBox.Show($"Неизвестный тип счетчика. Вид {gcounter.Вид} Номер {gcounter.НомерСчетчика}.\r\nЛс{record.Адр1}"));
                        continue;
                    }
                    var houseInfo = houses.FirstOrDefault(h => h.R1 == record.Адр1 && h.R2 == 0);
                    string houseNo = houseInfo == null
                        ? "д." + record.LsKvc.HouseId + (record.LsKvc.KorpusId == 0
                            ? ""
                            : " корп." + record.LsKvc.KorpusId)
                        : houseInfo.Sr40;
                    string counterTag = $"{record.Адр1:D8}{gcounter.Вид:D2}{gcounter.НомерСчетчика:D2}";
                    var counter = new CNV_COUNTER
                    {
                        CNTTYPE = cntType,
                        SETUPPLACE = setupPlace,
                        COUNTER_LEVEL = 1,
                        GROUPCOUNTERMODULEID = 23,
                        TARGETBALANCE_KOD = serviceid,
                        TARGETNEGATIVEBALANCE_KOD = serviceid,
                        STATUSID = ConvertGroupCounters.ConvertStatus(gcounter),
                        STATUSDATE = new DateTime(record.РасчетныйГод, record.РасчетныйМесяц, 1),
                        NAME = $"ОПУ {vids.Single(v => v.R1 == gcounter.Вид).Sr40} №{gcounter.НомерСчетчика} {streets.First(s => s.R1 == record.LsKvc.StreetId).Sr40} {houseNo}",
                        TAG = counterTag
                    };

                    int counterId;
                    if (!counterTags.TryGetValue(counterTag, out counterId))
                    {
                        using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            string sql;
                            if (gcounter.Вид == 06)
                                sql =
$@"select a.lshet from abonents a
inner join lcharsabonentlist la on la.lshet = a.lshet
where a.housecd = {housecd} and
la.kodlcharslist = 3 and la.significance = 2 and la.abonentlchardate =
(select max(la2.abonentlchardate) from lcharsabonentlist la2 where la2.lshet = la.lshet and la2.kodlcharslist = la.kodlcharslist)";
                            else
                                sql = $"select a.lshet from abonents a where a.housecd = {housecd}";
                            var sqlResult = context.ExecuteQuery<string>(sql);


                            foreach (var ls in sqlResult)
                            {
                                lc.Add(new CNV_COUNTER
                                {
                                    CNTTYPE = counter.CNTTYPE,
                                    SETUPPLACE = counter.SETUPPLACE,
                                    COUNTER_LEVEL = counter.COUNTER_LEVEL,
                                    GROUPCOUNTERMODULEID = counter.GROUPCOUNTERMODULEID,
                                    TARGETBALANCE_KOD = counter.TARGETBALANCE_KOD,
                                    TARGETNEGATIVEBALANCE_KOD = counter.TARGETNEGATIVEBALANCE_KOD,
                                    STATUSID = counter.STATUSID,
                                    STATUSDATE = counter.STATUSDATE,
                                    NAME = counter.NAME,
                                    TAG = counter.TAG,
                                    COUNTERID = null,
                                    LSHET = ls
                                });
                            }
                        }
                    }
                    else
                    {
                        counter.LSHET = null;
                        counter.COUNTERID = counterId.ToString();
                        lc.Add(counter);
                    }
                }
                #endregion
            });

            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lcch, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(llch, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lc, InsertRecordCount);
            if (ИнсертитьВоВременныеТаблицы) SaveListInsertSQL(lci, InsertRecordCount);

            FreeListMemory(lcch);
            FreeListMemory(llch);
            FreeListMemory(lc);
            FreeListMemory(lci);
        }

        public class LastIndOrm
        {
            public int CounterId { get; set; }
            public decimal? LastInd { get; set; }
        }

        private static int? GetServiceCd(uint adr1, byte vid)
        {
            int resource = resourceRecode[vid];
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var sqlResult = context.ExecuteQuery<LastSupplierOrm>(
$@"select ea.lshet as Lshet,
    (select first 1 la.significance from lcharsabonentlist la
    where la.lshet = ea.lshet and la.kodlcharslist = {resource + 100}
    order by la.abonentlchardate desc) as Supplier
from extorgaccounts ea
where ea.extlshet like '{adr1:D8}%' and ea.extorgcd = 1");
                return sqlResult.FirstOrDefault(r => r.Supplier != null)?.Supplier.Value + resource * 10000;
            }
        }

        private class LastSupplierOrm
        {
            public string Lshet { get; set; }
            public int? Supplier { get; set; }
        }

        public class CounterTagOrm
        {
            public int CounterId { get; set; }
            public string ImportTag { get; set; }
        }
    }

    public class Splitter : KvcChangesCase
    {
        public Splitter()
        {
            ConvertCaseName = "";
            Position = -10000;
            IsChecked = false;
        }

        protected override string FilesMask { get; }
        protected override string FileDialogTitle { get; }

        protected override void DoKvcChangesConvert()
        {
            return;
        }
    }
}
