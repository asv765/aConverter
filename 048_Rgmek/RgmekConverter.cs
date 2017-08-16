using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _048_Rgmek
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string LsRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\lsrecode.csv";
        public const string DuRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\durecode.csv";
        public const string HouseRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\houserecode.csv";
        public const string CnttypeRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\cnttyperecode.csv";
        public const string CounterIdRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\counteridrecode.csv";
        public const string IndtypeRecodeFileName = @"C:\Work\aConverter_Data\048_Rgmek\Docs\indtyperecode.csv";

        public static readonly int CurrentMonth = 03;

        public static readonly int CurrentYear = 2017;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";
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
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = new Dictionary<string, long>();
            long lastls = 6201000000;
            var durecode = new Dictionary<string, long>();
            long lastducd = 0;
            var houserecode = new Dictionary<string, long>();
            long lasthousecd = 0;

            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);

                var a = new CNV_ABONENT
                {
                    LSHET = Utils.GetValue(abonent.Lshet, lsrecode, ref lastls).ToString(),
                    EXTLSHET = abonent.Lshet.Trim(),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    PRIM_ = abonent.Address.Trim(),
                    POSTINDEX = abonent.Postindex.Trim()
                };

                a.TOWNSKOD = (int)abonent.Townskod;
                a.TOWNSNAME = abonent.Townsname.Trim();
                a.ULICAKOD = (int)abonent.Ulicakod;
                a.ULICANAME = abonent.Ulicaname.Trim();

                if (abonent.Townskod == 1000)
                {
                    a.RAYONKOD = 1;
                    a.RAYONNAME = "г.Рязань";
                }
                else if (abonent.Townskod == 107)
                {
                    a.RAYONKOD = 2;
                    a.RAYONNAME = "Рязанский район";
                }
                else
                {
                    a.RAYONKOD = 3;
                    a.RAYONNAME = "Неизвестен";
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
                int kvartira;
                if (!Int32.TryParse(abonent.Kvartira, out kvartira))
                    a.FLATNO = kvartira;
                a.ROOMNO = (short)abonent.Komnata;

                lca.Add(a);
                Iterate();
            }
            StepFinish();

            // StepStart(3);
            //AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            //StepFinish();

            Utils.SaveDictionary(lsrecode, Consts.LsRecodeFileName);
            Utils.SaveDictionary(durecode, Consts.DuRecodeFileName);
            Utils.SaveDictionary(houserecode, Consts.HouseRecodeFileName);

            StepStart(1);
            SaveList(lca, Consts.InsertRecordCount);
            Iterate();

            //StepStart(lca.Count);
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    foreach (var ca in lca)
            //    {
            //        context.Add(ca);
            //        context.SaveChanges();
            //        Iterate();
            //    }
            //}

            StepFinish();
        }
    }

    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - конвертация информации об количественных характеристиках";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = Utils.ReadDictionary(Consts.LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var cold = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                cold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(cold.Lshet, out lshet))
                {
                    var c = new CNV_CHAR()
                    {
                        LSHET = lshet.ToString(),
                        CHARCD = (int)cold.Charcd,
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
            var lccto = CharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lccto);
            StepFinish();
        }
    }

    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - конвертация информации о качественных характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.GetDataTable("LCHARS");
            var lcc = new List<CNV_LCHAR>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = Utils.ReadDictionary(Consts.LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var lcold = new LcharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lcold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                {
                    var c = new CNV_LCHAR()
                    {
                        LSHET = lshet.ToString(),
                        LCHARCD = (int)lcold.Lcharcd,
                        LCHARNAME = lcold.Lcharname.Trim(),
                        DATE_ = lcold.Date,
                        VALUE_ = (int)lcold.Value_,
                        VALUEDESC = lcold.Valuedesc
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            var lccto = LcharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lccto);
            StepFinish();
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - конвертация информации о качественных характеристиках";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            var lc = new List<CNV_COUNTER>();

            var cnttyperecode = new Dictionary<string, long>();
            long cnttypecd = 0;
            var counteridrecode = new Dictionary<string, long>();
            long counterid = 0;


            var lsrecode = Utils.ReadDictionary(Consts.LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var lcold = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lcold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                {
                    var c = new CNV_COUNTER()
                    {
                        LSHET = lshet.ToString(),
                        NAME = lcold.Name.Trim(),
                        SERIALNUM = lcold.Serialnum.Trim(),
                        CNTNAME = lcold.Cntname,
                        SETUPDATE = lcold.Setupdate,
                        DEACTDATE = lcold.Deactdate,
                        GUID_ = lcold.Counterid.Trim()
                    };
                    c.CNTTYPE = (int)Utils.GetValue(lcold.Cnttype, cnttyperecode, ref cnttypecd);
                    c.COUNTERID = Utils.GetValue(lcold.Counterid, counteridrecode, ref counterid).ToString();
                    if (lcold.Lastpov.Year > 1950)
                    {
                        c.LASTPOV = lcold.Lastpov;
                        if (lcold.Periodpov > 0)
                            c.NEXTPOV = lcold.Lastpov.AddMonths((int)lcold.Periodpov);
                    }
                    string prim = "";
                    if (lcold.Rgresid > 0)
                        prim += (prim == "" ? "" : ", ") + "RGRESID=" + lcold.Rgresid.ToString();
                    if (lcold.Precision > 0)
                        prim += (prim == "" ? "" : ", ") + "PRECISION=" + lcold.Precision.ToString().Replace(',','.');
                    if (!String.IsNullOrEmpty(lcold.Amperage))
                        prim += (prim == "" ? "" : ", ") + "AMPERAG=" + lcold.Amperage.Trim();
                    if (!String.IsNullOrEmpty(lcold.Instplace))
                        prim += (prim == "" ? "" : ", ") + "INSTPLACE=" + lcold.Instplace.Trim();
                    c.PRIM_ = prim;

                    //CounterSetupPlace 
                    // c.SETUPPLACE 
                    lc.Add(c);
                }
                Iterate();
            }
            Utils.SaveDictionary(cnttyperecode, Consts.CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, Consts.CounterIdRecodeFileName);
            StepFinish();

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }
    }

    public class ConvertCntrsind : ConvertCase
    {
        public ConvertCntrsind()
        {
            ConvertCaseName = "CNTRSIND - конвертация информации о показаниях счетчиков";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            var lc = new List<CNV_CNTRSIND>();

            var indtyperecode = new Dictionary<string, long>();
            long indtypecd = 0;
            var counteridrecode = Utils.ReadDictionary(Consts.CounterIdRecodeFileName);
            long counterid = 0;

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM CNTRSIND WHERE Indication > 0")));
            using (var reader = Tmsource.ExecuteQueryToReader("SELECT * FROM CNTRSIND WHERE Indication > 0"))
            {
                //StepStart(dt.Rows.Count);
                var cr = new CntrsindRecord();
                while(reader.Read())
                // foreach (DataRow dataRow in dt.Rows)
                {
                    //cr.ReadDataRow(dataRow);
                    cr.Counterid = reader["CounterID"].ToString().Trim();
                    cr.Doc = reader["Doc"].ToString().Trim();
                    cr.Date = Convert.ToDateTime(reader["Date"].ToString());
                    cr.Indication = Convert.ToDecimal(reader["Indication"]);
                    cr.Indtype = reader["Indtype"].ToString().Trim();

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid))
                    {
                        var c = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid.ToString(),
                            DOCUMENTCD = GetDocumentCd(cr.Doc),
                            INDDATE = cr.Date,
                            INDICATION = cr.Indication
                        };
                        c.INDTYPE = (int)Utils.GetValue(cr.Indtype, indtyperecode, ref indtypecd);
                        //CounterSetupPlace 
                        // c.SETUPPLACE 
                        lc.Add(c);
                    }
                    Iterate();
                }
            }
            Utils.SaveDictionary(indtyperecode, Consts.IndtypeRecodeFileName);
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

    public class ConvertMoney : ConvertCase
    {
        public ConvertMoney()
        {
            ConvertCaseName = "SUMS.DBF, PAYMENT.DBF, NACH.DBF - конвертация денег";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            var lc = new List<CNV_CNTRSIND>();

            var indtyperecode = new Dictionary<string, long>();
            long indtypecd = 0;
            var counteridrecode = Utils.ReadDictionary(Consts.CounterIdRecodeFileName);
            long counterid = 0;

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM CNTRSIND WHERE Indication > 0")));
            using (var reader = Tmsource.ExecuteQueryToReader("SELECT * FROM CNTRSIND WHERE Indication > 0"))
            {
                //StepStart(dt.Rows.Count);
                var cr = new CntrsindRecord();
                while (reader.Read())
                // foreach (DataRow dataRow in dt.Rows)
                {
                    //cr.ReadDataRow(dataRow);
                    cr.Counterid = reader["CounterID"].ToString().Trim();
                    cr.Doc = reader["Doc"].ToString().Trim();
                    cr.Date = Convert.ToDateTime(reader["Date"].ToString());
                    cr.Indication = Convert.ToDecimal(reader["Indication"]);
                    cr.Indtype = reader["Indtype"].ToString().Trim();

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid))
                    {
                        var c = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid.ToString(),
                            //DOCUMENTCD = GetDocumentCd(cr.Doc),  //TODO
                            INDDATE = cr.Date,
                            INDICATION = cr.Indication
                        };
                        c.INDTYPE = (int)Utils.GetValue(cr.Indtype, indtyperecode, ref indtypecd);
                        //CounterSetupPlace 
                        // c.SETUPPLACE 
                        lc.Add(c);
                    }
                    Iterate();
                }
            }
            Utils.SaveDictionary(indtyperecode, Consts.IndtypeRecodeFileName);
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
    }


        public class KillMe
    {
    }
}
