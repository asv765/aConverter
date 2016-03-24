using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _041_Sarai
{
    #region Скрипты
    public static class Scripts
    {
        public const string SelectAbonents = @"SELECT tblАдрес.АдресИД AS LSHET,
                                                       tblАбоненты.Абонент AS FIO,
                                                       tblАдрес.ПунктИД AS TOWNSKOD,
                                                       tblПункт.Пункт AS TOWNSNAME,
                                                       (tblАдрес.ПунктИД*200 + tblАдрес.НазваниеИД) AS ULICAKOD,
                                                       IIf([tblАдрес]![НазваниеИД]=1,'Неизвестна',[tblНазвание]![Название]) AS ULICANAME,
                                                       tblАдрес.АдресДом AS HOUSENO,
                                                       tblАдрес.АдресКорпус AS KORPUSNO,
                                                       tblАдрес.АдресКвартира AS FLATNO
                                                FROM tblПункт
                                                INNER JOIN (tblНазвание
                                                            INNER JOIN (tblГруппа
                                                                        INNER JOIN (tblАбоненты
                                                                                    INNER JOIN tblАдрес 
                                                                                    ON tblАбоненты.АбонентИД = tblАдрес.АбонентИД)
                                                                        ON tblГруппа.ГруппаИД = tblАдрес.ГруппаИД) 
                                                            ON tblНазвание.НазваниеИД = tblАдрес.НазваниеИД) 
                                                ON tblПункт.ПунктИД = tblАдрес.ПунктИД
                                                ORDER BY tblАдрес.АдресИД;";

        public const string SelectCChars = @"select АдресИД, Месяц, КоличествоЖильцов, ОбщаяПлощадь, ОтапливаемаяПлощадь, КоличествоСоток from tblАрхив 
                                            union all (select АдресИД, '01.01.2016' as Месяц, КоличествоЖильцов, ОбщаяПлощадь, ОтапливаемаяПлощадь, КоличествоСоток from tblАдрес)";

        public const string SelectCounterIndications = @"select p1.АдресИД, p1.ДатаПлатежа, p1.{{service}} from tblПлатежи p1
	                                                    where p1.{{service}} <>0 and p1.НомерПлатежа <> (select max(p2.НомерПлатежа) from tblПлатежи p2 where p2.АдресИД = p1.АдресИД)
                                                        order by 1,2";

        public const string SelectCounters = @"SELECT ind.АдресИД, ind.{{cntname}},
                                               (SELECT top 1 p.{{service}} 
                                               from tblПлатежи p
                                               where p.АдресИд = ind.АдресИД and p.НовыйМесяц = (SELECT MAX(p2.НовыйМесяц) from tblПлатежи p2 where p2.АдресИД = p.АдресИД)
                                               order by p.НомерПлатежа desc) as {{service}},
                                               (SELECT top 1 p.ДатаПлатежа 
                                               from tblПлатежи p
                                               where p.АдресИд = ind.АдресИД and p.НовыйМесяц = (SELECT MAX(p2.НовыйМесяц) from tblПлатежи p2 where p2.АдресИД = p.АдресИД)
                                               order by p.НомерПлатежа desc) as ДатаПлатежа
                                               from tblПокСчОпл ind
                                               where ind.АдресИД is not null
                                               order by 1";
    }
    #endregion

    public static class Consts
    {
        public const string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=D:\Work\C#\C#Projects\aConverter\041_Sarai\Sources\январь_2016.mdb";

        public const int InsertRecordCount = 1000;

        public static string GetLs(long intls)
        {
            return String.Format("88{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 02;

        public static readonly int CurrentYear = 2016;
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

     #region Конвертация

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            var fioRegex = new Regex(@"([а-яa-z-]+)[^а-яa-z()]*([а-яa-z-]+)?[^а-яa-z()]*([а-яa-z-]+)?\.?(.*)", RegexOptions.IgnoreCase);
            var numberRegex = new Regex(@"(\d+)(.*)");

            DataTable dt;
            StepStart(1);
            using (var connection = new OleDbConnection(Consts.ConnectionString))
            {
                connection.Open();
                var ad = new OleDbDataAdapter(Scripts.SelectAbonents, connection);
                var ds = new DataSet("ACCESS");
                ad.Fill(ds);
                dt = ds.Tables[0];
            }
            StepFinish();

            StepStart(dt.Rows.Count);
            var lca = new List<CNV_ABONENT>();
            foreach (DataRow dataRow in dt.Rows)
            {
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(dataRow["LSHET"])),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 2,
                    RAYONNAME = "Сараевский район",
                    DUCD = 1,
                    DUNAME = "",
                    ISDELETED = 0,
                    F = dataRow["FIO"].ToString(),
                    TOWNSKOD = Convert.ToInt32(dataRow["TOWNSKOD"]),
                    TOWNSNAME = dataRow["TOWNSNAME"].ToString(),
                    ULICAKOD = Convert.ToInt32(dataRow["ULICAKOD"]),
                    ULICANAME = dataRow["ULICANAME"].ToString(),
                    EXTLSHET = dataRow["LSHET"].ToString()
                };

                var match = fioRegex.Match(abonent.F);
                if (match.Groups.Count > 4) abonent.PRIM_ = match.Groups[4].Value;
                if (match.Groups.Count > 3) abonent.O = match.Groups[3].Value;
                if (match.Groups.Count > 2) abonent.I = match.Groups[2].Value;
                if (match.Groups.Count > 1) abonent.F = match.Groups[1].Value;

                string house = dataRow["HOUSENO"].ToString().Trim();
                match = numberRegex.Match(house);
                if (match.Groups.Count > 2) abonent.HOUSEPOSTFIX = match.Groups[2].Value;
                if (match.Groups.Count > 1)
                {
                    int houseno;
                    if (Int32.TryParse(match.Groups[1].Value, out houseno)) abonent.HOUSENO = match.Groups[1].Value;
                    else abonent.HOUSEPOSTFIX = match.Groups[0].Value;
                }

                string korpus = dataRow["KORPUSNO"].ToString().Trim();
                match = numberRegex.Match(korpus);
                if (match.Groups.Count > 2) abonent.KORPUSPOSTFIX = match.Groups[2].Value;
                if (match.Groups.Count > 1)
                {
                    int korpusno;
                    if (Int32.TryParse(match.Groups[1].Value, out korpusno)) abonent.KORPUSNO = Convert.ToInt32(match.Groups[1].Value);
                    else abonent.KORPUSPOSTFIX = match.Groups[0].Value;
                }

                string flat = dataRow["FLATNO"].ToString().Trim();
                match = numberRegex.Match(flat);
                if (match.Groups.Count > 2) abonent.FLATPOSTFIX = match.Groups[2].Value;
                if (match.Groups.Count > 1)
                {
                    int flatno;
                    if (Int32.TryParse(match.Groups[1].Value, out flatno)) abonent.FLATNO = flatno;
                    else abonent.FLATPOSTFIX = match.Groups[0].Value;
                }

                lca.Add(abonent);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    /// <summary>
    /// Конвертирует данные о количественных характеристиках
    /// </summary>
    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - данные о количественных характеристиках";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt;

            dynamic[] chars =
            {
                new {CharName = "КоличествоЖильцов", Charcd = 1},
                new {CharName = "ОбщаяПлощадь", Charcd = 2},
                //new {CharName = "ЖилаяПлощадь", Charcd = 14},
                new {CharName = "ОтапливаемаяПлощадь", Charcd = 4},
                new {CharName = "КоличествоСоток", Charcd = 15}
            };

            StepStart(1);
            using (var connection = new OleDbConnection(Consts.ConnectionString))
            {
                connection.Open();
                var ad = new OleDbDataAdapter(Scripts.SelectCChars, connection);
                var ds = new DataSet("ACCESS");
                ad.Fill(ds);
                dt = ds.Tables[0];
            }
            StepFinish();

            var lcc = new List<CNV_CHAR>();
            StepStart(dt.Rows.Count);
            foreach (DataRow dataRow in dt.Rows)
            {
                string lshet = Consts.GetLs(Convert.ToInt64(dataRow["АдресИД"]));

                foreach (var value in chars)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = lshet,
                        CHARCD = value.Charcd,
                        VALUE_ = (decimal) dataRow[value.CharName],
                        DATE_ = Convert.ToDateTime(dataRow["Месяц"])
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();
            SaveList(lcc, Consts.InsertRecordCount);
        }
    }

    /// <summary>
    /// Конвертация данных о показаниях счетчиков
    /// </summary>
    public class ConvertCountersAndInd : ConvertCase
    {
        public ConvertCountersAndInd()
        {
            ConvertCaseName = "COUNTERS & CNTRSIND - данные о счетчиках и их показаниях";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            //SetStepsCount(12);
            SetStepsCount(6);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            int maxCounterId = 1;
            long recno = 0;
            DataTable dt;

            //dynamic[] services =
            //{
            //    new {Name = "СчХолКуб", CNTTYPE = 1},
            //    new {Name = "СчГорКуб", CNTTYPE = 3},
            //    new {Name = "СчПолКуб", CNTTYPE = 14}
            //};
            //foreach (var service in services)
            //{
            dynamic service = new {Name = "СчХолКуб", CNTTYPE = 1, CNTNAME = "СчОплХол"};
            StepStart(1);
            using (var connection = new OleDbConnection(Consts.ConnectionString))
            {
                connection.Open();
                string script = Scripts.SelectCounters.Replace("{{cntname}}", service.CNTNAME);
                script = script.Replace("{{service}}", service.Name);
                var ad = new OleDbDataAdapter(script,connection);
                var ds = new DataSet("ACCESS");
                ad.Fill(ds);
                dt = ds.Tables[0];
            }
            StepFinish();

            StepStart(dt.Rows.Count);
            var lcc = new List<CNV_COUNTER>();
            var lci = new List<CNV_CNTRSIND>();
            foreach (DataRow dataRow in dt.Rows)
            {
                string lshet = Consts.GetLs(Convert.ToInt64(dataRow["АдресИД"]));
                maxCounterId++;
                var counter = new CNV_COUNTER
                {
                    LSHET = lshet,
                    SETUPDATE = new DateTime(2016,1,1),
                    COUNTERID = maxCounterId.ToString("D9"),
                    CNTTYPE = service.CNTTYPE
                };
                lcc.Add(counter);

                var cind = new CNV_CNTRSIND
                {
                    COUNTERID = counter.COUNTERID,
                   INDICATION = (decimal)dataRow[service.CNTNAME]
                };
                if (!dataRow.IsNull(service.Name))
                {
                    cind.OLDIND = cind.INDICATION - (decimal) dataRow[service.Name];
                    cind.DOCUMENTCD = String.Format("{0}_{1}", dataRow["АдресИД"], recno);
                    cind.INDDATE = (DateTime) dataRow["ДатаПлатежа"];
                    cind.INDTYPE = 0;
                    if (cind.OLDIND < 0) cind.OLDIND = 0;
                }
                else
                {
                    cind.OLDIND = cind.INDICATION;
                    cind.DOCUMENTCD = String.Format("{0}_{1}", dataRow["АдресИД"], recno);
                    cind.INDDATE = new DateTime(2016,03,01);
                    cind.INDTYPE = 0;
                }
                lci.Add(cind);
                recno++;
                Iterate();
            }

            StepStart(1);
            using (var connection = new OleDbConnection(Consts.ConnectionString))
            {
                connection.Open();
                var ad = new OleDbDataAdapter(Scripts.SelectCounterIndications.Replace("{{service}}", service.Name),
                    connection);
                var ds = new DataSet("ACCESS");
                ad.Fill(ds);
                dt = ds.Tables[0];
            }
            StepFinish();

            StepStart(dt.Rows.Count);
            foreach (DataRow dataRow in dt.Rows)
            {
                string lshet = Consts.GetLs(Convert.ToInt64(dataRow["АдресИД"]));
                var counter = lcc.Single(cnt => cnt.LSHET == lshet && cnt.CNTTYPE == service.CNTTYPE);
                var lastInd = lci.Last(ci => ci.COUNTERID == counter.COUNTERID);

                var cind = new CNV_CNTRSIND
                {
                    COUNTERID = counter.COUNTERID,
                    DOCUMENTCD = String.Format("{0}_{1}", dataRow["АдресИД"], recno),
                    INDDATE = (DateTime) dataRow["ДатаПлатежа"],
                    INDTYPE = 0,
                    INDICATION = lastInd.OLDIND,
                    OLDIND = lastInd.OLDIND - (decimal)dataRow[service.Name]
                };
                if (cind.OLDIND < 0) cind.OLDIND = 0;
                lci.Add(cind);
                recno++;
                Iterate();
            }
            StepFinish();

            SaveList(lcc, Consts.InsertRecordCount);
            SaveList(lci, Consts.InsertRecordCount);
            //}
        }
    }

    /// <summary>
    /// Конвертация данных истории начислений
    /// </summary>
    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL - данные истории начислений";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            dynamic[] services =
            {
                new {OriginName = "", Servicecd = 0},
                new {OriginName = "", Servicecd = 0},
                new {OriginName = "", Servicecd = 0},
                new {OriginName = "", Servicecd = 0},
                new {OriginName = "", Servicecd = 0},
                new {OriginName = "", Servicecd = 0},
            };
        }
    }

    #endregion
}
