using System.CodeDom;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Versioning;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;

namespace _036_Izhevskoe
{
    public static class Consts
    {
        public static readonly DateTime StartDate = new DateTime(2005, 1, 1);

        public static readonly int CurrentMonth = 10;

        public static readonly int CurrentYear = 2015;

        public static string GetLs(long intls)
        {
            return String.Format("82{0:D6}", intls);
            // return String.Format("84{0:D6}", intls);
        }

        // public static readonly int StartYear = 2014;
        public static readonly bool FullHistory = true;

        public static readonly string RecodeTableFileName = @"C:\Work\aConverter_Data\036_Izhevskoe\Service\Таблица перекодировки.xls";

        public static readonly int RecodeTableOffset = 1;

        public static readonly int CommitStep = 1000;
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

        public uint CurrentLshet;

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("F_LITS");
            // DataTable dt = Tmsource.ExecuteQuery("SELECT N_Lits,  ")
            var lca = new List<CNV_ABONENT>();

            StepStart(dt.Rows.Count);
            var fLits = new F_litsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                fLits.ReadDataRow(dataRow);
                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(fLits.N_lits)),
                    EXTLSHET = fLits.N_lits.ToString(CultureInfo.InvariantCulture),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 1,
                    RAYONNAME = "Спасский район",
                    TOWNSKOD = 1,
                    TOWNSNAME = "Ижевское",
                    DUCD = 1,
                    DUNAME = "МУП 'Ижевское коммунальное хозяйство'",
                    ULICAKOD = (int) fLits.Kst,
                    ULICANAME = fLits.Street,
                    HOUSENO = fLits.N_dom.ToString(CultureInfo.InvariantCulture),
                    FLATNO = (int) fLits.N_kw,
                    ISDELETED = 0
                };
                // if (!String.IsNullOrEmpty(fLits.Prim)) a.PRIM_ = fLits.Prim;
                if (fLits.Drob != 0) a.HOUSEPOSTFIX += "/" + fLits.Drob.ToString(CultureInfo.InvariantCulture);
                if (!String.IsNullOrWhiteSpace(fLits.Lit))
                {
                    if (!String.IsNullOrEmpty(a.HOUSEPOSTFIX)) a.HOUSEPOSTFIX += " ";
                    a.HOUSEPOSTFIX += "лит." + fLits.Lit.ToString(CultureInfo.InvariantCulture);
                }
                string[] fio = Utils.SplitFio(fLits.Fio);
                a.F = fio[0];
                a.I = fio[1];
                a.O = fio[2];

                lca.Add(a);
             
                Iterate();
            }
            AbonentRecordUtils.SetUniqueHouseCd(lca, 1);
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

    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - данные о количественных характеристиках";
            Position = 30;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("F_LITS");
            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var fLits = new F_litsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                fLits.ReadDataRow(dataRow);
                if (fLits.N_jil != 0)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(fLits.N_lits),
                        CHARCD = 1,
                        CHARNAME = "Число проживающих",
                        DATE_ = Consts.StartDate,
                        VALUE_ = fLits.N_jil
                    };
                    lcc.Add(c);
                }
                if (fLits.Metr != 0)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(fLits.N_lits),
                        CHARCD = 2,
                        CHARNAME = "Общая площадь",
                        DATE_ = Consts.StartDate,
                        VALUE_ = fLits.Metr
                    };
                    lcc.Add(c);
                }

            }
            StepFinish();

            StepStart(1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(lcc);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - данные о параметрах потребления";
            Position = 40;
            IsChecked = true;
        }

        readonly Dictionary<long, List<CNV_LCHAR>> _rcDic = new Dictionary<long, List<CNV_LCHAR>>();

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");

            var llc = new List<CNV_LCHAR>();
            // var fLits = new F_litsRecord();
            DataTable rcTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист1");

            #region Получаем словарь для перекодировки

            foreach (DataRow recodeDataRow in rcTable.Rows)
            {
                int tarCode = Convert.ToInt32(recodeDataRow[0 + Consts.RecodeTableOffset]); // N_LGT
                // string tarName = Convert.ToString(recodeDataRow["NASV"]);
                if (!(recodeDataRow[3 + Consts.RecodeTableOffset] is DBNull))
                {
                    int lcharcd = Convert.ToInt32(recodeDataRow[3 + Consts.RecodeTableOffset]); // LCHARCD
                    string lcharName = Convert.ToString(recodeDataRow[4 + Consts.RecodeTableOffset]); // LCHARNAME
                    int lcharvalue = Convert.ToInt32(recodeDataRow[5 + Consts.RecodeTableOffset]); // LCHARVALUE
                    string lcharValueDesc = Convert.ToString(recodeDataRow[6 + Consts.RecodeTableOffset]); // LCHARVALUEDESC

                    var lc = new CNV_LCHAR()
                    {
                        DATE_ = Consts.StartDate,
                        LCHARCD = lcharcd,
                        LCHARNAME = lcharName,
                        VALUE_ = lcharvalue,
                        VALUEDESC = lcharValueDesc
                    };
                    List<CNV_LCHAR> currentList;
                    if (!_rcDic.TryGetValue(tarCode, out currentList))
                    {
                        currentList = new List<CNV_LCHAR>();
                        _rcDic.Add(tarCode, currentList);
                    }
                    currentList.Add(lc);
                }
            }

            #endregion

            DataTable dt = Tmsource.GetDataTable("F_LITS");
            StepStart(dt.Rows.Count);
            var fLits = new F_litsRecord();
            var globalLCharsList = new List<CNV_LCHAR>();

            foreach (DataRow dataRow in dt.Rows)
            {
                fLits.ReadDataRow(dataRow);
                string lshet = Consts.GetLs(fLits.N_lits);
                globalLCharsList.AddRange(GetCurrentList(fLits.Kvar, lshet));
                globalLCharsList.AddRange(GetCurrentList(fLits.Watr, lshet));
                globalLCharsList.AddRange(GetCurrentList(fLits.Kanal, lshet));
                globalLCharsList.AddRange(GetCurrentList(fLits.Mus, lshet));

                var lcPriv = new CNV_LCHAR()
                {
                    DATE_ = Consts.StartDate,
                    LCHARCD = 5,
                    LCHARNAME = "Приватизация",
                    LSHET = lshet,
                    VALUE_ = (int)fLits.Priv,
                    VALUEDESC = fLits.Priv == 0 ? "Муниципальное" : "Частное"
                };
                globalLCharsList.Add(lcPriv);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(LcharsRecordUtils.ThinOutList(globalLCharsList));
                acem.SaveChanges();
            }
            
            StepFinish();
        }

        private IEnumerable<CNV_LCHAR> GetCurrentList(long tarifcd, string lshet)
        {
            var currentList = new List<CNV_LCHAR>();
            if (tarifcd != 0)
            {
                var patternList = _rcDic[tarifcd];
                foreach (var patternLchar in patternList)
                {
                    var newLChar = new CNV_LCHAR()
                    {
                        DATE_ = patternLchar.DATE_,
                        LCHARCD = patternLchar.LCHARCD,
                        LCHARNAME = patternLchar.LCHARNAME,
                        VALUEDESC = patternLchar.VALUEDESC,
                        VALUE_ = patternLchar.VALUE_,
                        LSHET = lshet
                    };
                    currentList.Add(newLChar);
                }
            }
            return currentList;
        }
    }
   

    /// <summary>
    /// Конвертация истории оплат-начислений
    /// </summary>
    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL, OPLATA, NACH - конвертация истории оплат-начислений";
            Position = 50;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(Consts.FullHistory ? 10 : 8);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            var nom = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_конец);

            #region Сальдо
            // DataTable dt = Tmsource.GetDataTable("OSTN");
            DataTable dt = Tmsource.ExecuteQuery("SELECT DISTINCT * FROM OSTN");
            StepStart(dt.Rows.Count);
            var ostn = new OstnRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                ostn.ReadDataRow(dataRow);
                string lshet = Consts.GetLs(ostn.N_lits);
                //if (ostn.S_kvart != 0)
                //{
                    nom.RegisterBeginSaldo(lshet, Consts.CurrentMonth,
                        Consts.CurrentYear, 2, "Содержание жилья", ostn.S_kvart);
                //}
                //if ((ostn.S_water + ostn.Korov) != 0)
                //{
                    nom.RegisterBeginSaldo(lshet, Consts.CurrentMonth,
                        Consts.CurrentYear, 4, "Водоснабжение", (ostn.S_water + ostn.Korov));
                //}
                //if (ostn.S_kanal != 0)
                //{
                    nom.RegisterBeginSaldo(lshet, Consts.CurrentMonth,
                        Consts.CurrentYear, 8, "Водоотведение", ostn.S_kanal);
                //}
                //if (ostn.S_musor != 0)
                //{
                    nom.RegisterBeginSaldo(lshet, Consts.CurrentMonth,
                        Consts.CurrentYear, 6, "Вывоз ТБО", ostn.S_musor);
                //}
                Iterate();
            }
            StepFinish();
            #endregion

            #region Оплата
            ProcessOplata(nom, "FKT");
            ProcessOplata(nom, "ARX_FKT");
            if (Consts.FullHistory) ProcessOplata(nom, "FKT_98");
            #endregion

            #region Начисления
            ProcessNach(nom, "FPLAN");
            ProcessNach(nom, "ARX_PLAN");
            if (Consts.FullHistory) ProcessNach(nom, "ARX_PL98");
            #endregion

            #region Сохраняем сальдо
                SaveList(nom.NachoplRecords.Values);
            //var list = nom.NachoplRecords.Values.ToList();
            //int stepsCount = (list.Count / Consts.CommitStep) + 1;
            //StepStart(stepsCount);
            //while (list.Count > 0)
            //{
            //    int count = Math.Min(Consts.CommitStep, list.Count);
            //    var sublist = list.GetRange(0, count);
            //    list.RemoveRange(0, count);
            //    SaveContextPart(sublist);
            //    Iterate();
            //}
            //StepFinish();
            #endregion

            #region Сохраняем начисления
            SaveList(nom.NachRecords);
            //StepStart(nom.NachRecords.Count);
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    int i = 0;
            //    foreach (CNV_NACH n in nom.NachRecords)
            //    {
            //        context.Add(n);
            //        if ((i++ % Consts.CommitStep) == 0)
            //        {
            //            context.SaveChanges();
            //            context.ClearChanges();
            //        }
            //        Iterate();
            //    }
            //    context.SaveChanges();
            //}
            //StepFinish();
            #endregion

            #region Сохраняем оплату
            SaveList(nom.OplataRecords);
            //StepStart(nom.OplataRecords.Count);
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    int i = 0;
            //    foreach (CNV_OPLATA o in nom.OplataRecords)
            //    {
            //        context.Add(o);
            //        if ((i++ % Consts.CommitStep) == 0)
            //        {
            //            context.SaveChanges();
            //            context.ClearChanges();
            //        }
            //        Iterate();
            //    }
            //    context.SaveChanges();
            //}
            //StepFinish();
            #endregion
        }

        private void SaveList<T>(IEnumerable<T> listToSave)
        {
            var list = listToSave.ToList();
            int stepsCount = (list.Count / Consts.CommitStep) + 1;
            StepStart(stepsCount);
            while (list.Count > 0)
            {
                int count = Math.Min(Consts.CommitStep, list.Count);
                var sublist = new List<object>();
                for (int i = 0; i < count; i++)
                {
                    sublist.Add(list[i]);
                }
                list.RemoveRange(0, count);
                SaveContextPart(sublist);
                Iterate();
            }
            StepFinish();            
        }

        private void SaveContextPart(IEnumerable<object> entitiesList)
        {
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                context.Add(entitiesList);
                context.SaveChanges();
            }
        }

        private void ProcessOplata(NachoplManager nom, string tableName)
        {
            var odef = new CNV_OPLATA()
            {
                SOURCECD = 17,
                SOURCENAME = "Касса"
            };

            StepStart(1);
            using (var dr = Tmsource.ExecuteQueryToReader("SELECT RECNO() AS RECNO, * FROM " + tableName))
            {
                while (dr.Read())
                {
                    string mesPl = dr["Mes_pl"].ToString();
                    DateTime dat = Convert.ToDateTime(dr["Dat"]);
                    int recno = Convert.ToInt32(dr["Recno"]);
                    decimal sKvart = Convert.ToDecimal(dr["S_kvart"]);
                    decimal sWater = Convert.ToDecimal(dr["S_water"]);
                    decimal korov = Convert.ToDecimal(dr["Korov"]);
                    decimal sKanal = Convert.ToDecimal(dr["S_kanal"]);
                    decimal sMusor = Convert.ToDecimal(dr["S_musor"]);

                    string lshet = Consts.GetLs(Convert.ToInt32(dr["N_lits"]));
                    int currentYear = dat.Year;
                    DateTime za = Convert.ToDateTime("01 " + mesPl + " " + currentYear);
                    int zayear = currentYear;
                    if (za.Month > dat.Month) zayear--;
                    if (sKvart != 0)
                    {
                        odef.SERVICECD = 2;
                        odef.SERVICENAME = "Содержание жилья";
                        nom.RegisterOplata(odef, lshet, za.Month, zayear, sKvart, dat, dat, tableName + "_" + recno);
                    }
                    if ((sWater + korov) != 0)
                    {
                        odef.SERVICECD = 4;
                        odef.SERVICENAME = "Водоснабжение";
                        nom.RegisterOplata(odef, lshet, za.Month, zayear, sWater + korov, dat, dat, tableName + "_" + recno);
                    }
                    if (sKanal != 0)
                    {
                        odef.SERVICECD = 8;
                        odef.SERVICENAME = "Водоотведение";
                        nom.RegisterOplata(odef, lshet, za.Month, zayear, sKanal, dat, dat, tableName + "_" + recno);
                    }
                    if (sMusor != 0)
                    {
                        odef.SERVICECD = 6;
                        odef.SERVICENAME = "Вывоз ТБО";
                        nom.RegisterOplata(odef, lshet, za.Month, zayear, sMusor, dat, dat, tableName + "_" + recno);
                    }
                }
            }
            Iterate();
            StepFinish();
            
        }

        // Обрабатываем записи о начислениях
        private void ProcessNach(NachoplManager nom, string tableName)
        {
            var ndef = new CNV_NACH()
            {
                TYPE_ = 0, // По нормам
                VOLUME = 0,
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM " + tableName)));
            using (var dr = Tmsource.ExecuteQueryToReader("SELECT RECNO() AS RECNO, * FROM " + tableName))
            {
                while (dr.Read())
                {
                    string mesPl = dr["Mes_pl"].ToString();
                    int recno = Convert.ToInt32(dr["Recno"]);
                    decimal sKvart = Convert.ToDecimal(dr["S_kvart"]);
                    decimal sWater = Convert.ToDecimal(dr["S_water"]);
                    decimal korov = Convert.ToDecimal(dr["Korov"]);
                    decimal sKanal = Convert.ToDecimal(dr["S_kanal"]);
                    decimal sMusor = Convert.ToDecimal(dr["S_musor"]);

                    string lshet = Consts.GetLs(Convert.ToInt32(dr["N_lits"]));

                    int currentYear = Consts.CurrentYear;
                    if (tableName == "ARX_PL98")
                        currentYear = Convert.ToInt32(dr["GOD"]);
                    DateTime za = Convert.ToDateTime("01 " + mesPl + " " + currentYear);
                    if (sKvart != 0)
                    {
                        ndef.REGIMCD = 31;
                        ndef.REGIMNAME = "Содержание жилья, конвертация";
                        ndef.SERVICECD = 2;
                        ndef.SERVICENAME = "Содержание жилья";
                        nom.RegisterNach(ndef, lshet, za.Month, currentYear, sKvart, 0, za, tableName + "_" + recno);
                    }
                    if ((sWater + korov) != 0)
                    {
                        ndef.REGIMCD = 32;
                        ndef.REGIMNAME = "Холодная вода, конвертация";
                        ndef.SERVICECD = 4;
                        ndef.SERVICENAME = "Водоснабжение";
                        nom.RegisterNach(ndef, lshet, za.Month, currentYear, sWater + korov, 0, za,
                            tableName + "_" + recno);
                    }
                    if (sKanal != 0)
                    {
                        ndef.REGIMCD = 29;
                        ndef.REGIMNAME = "Водоотведение, конвертация";
                        ndef.SERVICECD = 8;
                        ndef.SERVICENAME = "Водоотведение";
                        nom.RegisterNach(ndef, lshet, za.Month, currentYear, sKanal, 0, za, tableName + "_" + recno);
                    }
                    if (sMusor != 0)
                    {
                        ndef.REGIMCD = 30;
                        ndef.REGIMNAME = "Вывоз ТБО, конвертация";
                        ndef.SERVICECD = 6;
                        ndef.SERVICENAME = "Вывоз ТБО";
                        nom.RegisterNach(ndef, lshet, za.Month, currentYear, sMusor, 0, za, tableName + "_" + recno);
                    }
                    Iterate();
                }
            }
            StepFinish();
        }
    }

    public class CountersConvert : ConvertCase
    {
        private List<CNV_CNTRSIND> _globalCntrsinds = new List<CNV_CNTRSIND>();
        private Dictionary<CounterKey, CNV_COUNTER> _globalCountersDic = new Dictionary<CounterKey, CNV_COUNTER>();

        public CountersConvert()
        {
            ConvertCaseName = "COUNTERS, CNTRSIND - счетчики и показания";
            Position = 60;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            if (Consts.FullHistory)
                SetStepsCount(6);
            else
                SetStepsCount(5);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");

            #region Формируем словаь с данными о счетчиках
            DataTable dt = Tmsource.ExecuteQuery("SELECT *, RECNO() AS RECNO FROM F_LITS");
            StepStart(dt.Rows.Count);
            
            foreach (DataRow dataRow in dt.Rows)
            {
                var fLits = new F_litsRecord();
                int recno = Convert.ToInt32(dataRow["RECNO"]);
                var dat = new DateTime(Consts.CurrentYear, Consts.CurrentMonth, 1);
                fLits.ReadDataRow(dataRow);
                string lshet = Consts.GetLs(fLits.N_lits);
                if (!String.IsNullOrWhiteSpace(fLits.N_shet4))
                {
                    string counterid = lshet + "_1";
                    var c = new CNV_COUNTER()
                    {
                        LSHET = lshet,
                        CNTNAME = "Счетчик холодной воды",
                        CNTTYPE = 106,
                        COUNTERID = counterid,
                        NAME = fLits.N_shet4,
                        SERIALNUM = fLits.N_shet4
                    };
                    _globalCountersDic.Add(new CounterKey() { Lshet = lshet, Code = 1 }, c);
                    if (fLits.Shet_4 != 0)
                    {
                        var ci = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid,
                            DOCUMENTCD = "F_LITS_" + recno,
                            INDDATE = dat,
                            INDICATION = fLits.Shet_4,
                            OB_EM = 0,
                            INDTYPE = 1
                        };
                        _globalCntrsinds.Add(ci);
                    }
                }
                if (!String.IsNullOrWhiteSpace(fLits.N_shet3))
                {
                    string counterid = lshet + "_2";
                    var c = new CNV_COUNTER()
                    {
                        LSHET = lshet,
                        CNTNAME = "Счетчик холодной воды",
                        CNTTYPE = 106,
                        COUNTERID = counterid,
                        NAME = fLits.N_shet3,
                        SERIALNUM = fLits.N_shet3
                    };
                    _globalCountersDic.Add(new CounterKey() { Lshet = lshet, Code = 2 }, c);
                    if (fLits.Shet_3 != 0)
                    {
                        var ci = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid,
                            DOCUMENTCD = "F_LITS_" + recno,
                            INDDATE = dat,
                            INDICATION = fLits.Shet_3,
                            OB_EM = 0,
                            INDTYPE = 1
                        };
                        _globalCntrsinds.Add(ci);
                    }
                }
                Iterate();
            }
            StepFinish();
            #endregion

            ProcessCounterIndication("FKT");
            ProcessCounterIndication("ARX_FKT");
            if (Consts.FullHistory) ProcessCounterIndication("FKT_98");

            CntrsindRecordUtils.RestoreHistory(ref _globalCntrsinds, RestoreHistoryType.Рассчитать_показания_на_начало_как_конечные_минус_объем);

            StepStart(_globalCntrsinds.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                int i = 0;
                foreach (CNV_CNTRSIND ci in _globalCntrsinds)
                {
                    acem.Add(ci);
                    if ((i++ % Consts.CommitStep) == 0)
                    {
                        acem.SaveChanges();
                        acem.ClearChanges();
                    }
                    Iterate();
                }
                acem.SaveChanges();
            }
            StepFinish();

            StepStart(_globalCountersDic.Values.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                int i = 0;
                foreach (CNV_COUNTER c in _globalCountersDic.Values)
                {
                    acem.Add(c);
                    if ((i++ % Consts.CommitStep) == 0)
                    {
                        acem.SaveChanges();
                        acem.ClearChanges();
                    }
                    Iterate();
                }
                acem.SaveChanges();
            }
            StepFinish();
        }

        private void ProcessCounterIndication(string tableName)
        {
            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM " + tableName + " WHERE Chet > 0")));
            using (var dr = Tmsource.ExecuteQueryToReader("SELECT RECNO() AS RECNO, * FROM " + tableName + " WHERE Chet > 0"))
            {
                while (dr.Read())
                {
                    DateTime dat = Convert.ToDateTime(dr["Dat"]);
                    int recno = Convert.ToInt32(dr["Recno"]);
                    int chet = Convert.ToInt32(dr["Chet"]);
                    int pokC = Convert.ToInt32(dr["Pok_c"]);
                    int kvs = Convert.ToInt32(dr["Kvs"]);

                    if (pokC != 0)
                    {
                        string lshet = Consts.GetLs(Convert.ToInt32(dr["N_lits"]));
                        var counterKey = new CounterKey() {Code = chet, Lshet = lshet};

                        // Пытаемся найти счетчик
                        CNV_COUNTER cc;
                        if (!_globalCountersDic.TryGetValue(counterKey, out cc))
                        {
                            cc = new CNV_COUNTER()
                            {
                                LSHET = lshet,
                                CNTNAME = "Счетчик холодной воды",
                                CNTTYPE = 106,
                                COUNTERID = lshet + "_" + chet.ToString(CultureInfo.InvariantCulture),
                                NAME = "Неизвестен",
                                SERIALNUM = lshet + "_" + chet.ToString(CultureInfo.InvariantCulture),
                            };
                            _globalCountersDic.Add(counterKey, cc);
                        }

                        var ci = new CNV_CNTRSIND()
                        {
                            COUNTERID = cc.COUNTERID,
                            DOCUMENTCD = tableName + "_" + recno,
                            INDDATE = dat,
                            INDICATION = pokC,
                            OB_EM = kvs,
                            INDTYPE = 0
                        };
                        _globalCntrsinds.Add(ci);
                    }
                    Iterate();
                }
            }
            StepFinish();
        }

        private struct  CounterKey
        {
            public string Lshet;
            public int Code;
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
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "1" });
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
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferCounters : ConvertCase
    {
        public TransferCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках";
            Position = 1040;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "1" });
            Iterate();
        }
    }

    public class TransferOplata : ConvertCase
    {
        public TransferOplata()
        {
            ConvertCaseName = "Перенос данных об оплате";
            Position = 1050;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            Iterate();
        }
    }

    public class TransferSaldo : ConvertCase
    {
        public TransferSaldo()
        {
            ConvertCaseName = "Перенос данных о сальдо";
            Position = 1060;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(),
                Consts.CurrentMonth.ToString() });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active"); 
            Iterate();
        }
    }

    public class TransferNachisl : ConvertCase
    {
        public TransferNachisl()
        {
            ConvertCaseName = "Перенос данных о начислениях";
            Position = 1070;
            IsChecked = false;

    }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
        }
    }
}
