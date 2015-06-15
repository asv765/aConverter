using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System;

namespace _033_YrupinskConvert
{
    public static class Consts
    {
        public static DateTime StartDate = new DateTime(2010, 1, 1);

        public static int CurrentYear = 2015;

        public static int CurrentMonth = 6;

        public static DateTime CurrentDateTime = new DateTime(CurrentYear, CurrentMonth, 1);

        public static int LastYear = CurrentMonth == 1 ? CurrentYear - 1 : CurrentYear;

        public static int LastMonth = CurrentMonth == 1 ? 12 : CurrentMonth - 1;

        public const string SourceDir = @"C:\Work\aConverter_Data\033_Yrupinsk\Source";

        public static string GetLs(long intls)
        {
            return String.Format("01{0:D6}", intls);
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

            BufferEntitiesManager.CreateAllEntities();

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
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
            tms.Init();

            StepStart(1);

            var a = new ABONENT()
            {
                LSHET = "123456"
            };

            var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection);
            
            acem.Add(a);
            acem.SaveChanges();

            StepFinish();
        }
    }

    //        #endregion

    //        #region 2. Грузим справочник домов

    //        var dsprdom = new Dictionary<long, SprdomRecord>();
    //        dt = tms.GetDataTable("SPRDOM");
    //        StepStart(dt.Rows.Count);
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            var sprdom = new SprdomRecord();
    //            sprdom.ReadDataRow(dr);
    //            dsprdom.Add(sprdom.Codc, sprdom);
    //            Iterate();
    //        }
    //        StepFinish();

    //        #endregion

    //        #region 3. Заполняем справочник абонентов
    //        var labonent = new List<abonent>();
    //        var dhaddchar = new Dictionary<string, haddchar>();
    //        var dstreet = new Dictionary<string, int>();
    //        StepStart(dsprrab.Count);
    //        foreach (SprrabRecord sr in dsprrab.Values)
    //        {
    //            var ar = new abonent();
    //            ar.LSHET = Consts.GetLs(sr.Ls);
    //            ar.EXTLSHET = sr.Ls.ToString(CultureInfo.InvariantCulture);
    //            ar.F = sr.Fam;
    //            ar.I = sr.Imia;
    //            ar.O = sr.Otch;

    //            ar.DISTKOD = ar.RAYONKOD = ar.TOWNSKOD = 1;
    //            ar.DISTNAME = ar.RAYONNAME = ar.TOWNSNAME = "Ряжск";

    //            ar.HOUSECD = (int)sr.Addres;

    //            var sd = dsprdom[sr.Addres];
    //            ar.NDOMA = sd.Domc.ToString(CultureInfo.InvariantCulture) + sd.Dom_alpha;
    //            ar.KORPUS = 0;

    //            if (sd.Sob != 0)
    //            {
    //                haddchar hac;
    //                string key = String.Format("{0}_{1}", ar.HOUSECD, 206001);
    //                if (!dhaddchar.TryGetValue(key, out hac))
    //                {
    //                    hac = new haddchar()
    //                    {
    //                        ADDCHARCD = 206001,
    //                        HOUSECD = ar.HOUSECD,
    //                        VALUE = sd.Sob.ToString(CultureInfo.InvariantCulture)
    //                    };
    //                    dhaddchar.Add(key, hac);
    //                }
    //            }
    //            if (sd.Sscale != 0)
    //            {
    //                haddchar hac;
    //                string key = String.Format("{0}_{1}", ar.HOUSECD, 32010);
    //                if (!dhaddchar.TryGetValue(key, out hac))
    //                {
    //                    hac = new haddchar()
    //                    {
    //                        ADDCHARCD = 32010,
    //                        HOUSECD = ar.HOUSECD,
    //                        VALUE = sd.Sscale.ToString(CultureInfo.InvariantCulture)
    //                    };
    //                    dhaddchar.Add(key, hac);
    //                }
    //            }

    //            int ulicakod;
    //            if (!dstreet.TryGetValue(sd.Ulic, out ulicakod))
    //            {
    //                if (dstreet.Count == 0)
    //                    ulicakod = 1;
    //                else
    //                    ulicakod = dstreet.Values.Max() + 1;
    //                dstreet.Add(sd.Ulic, ulicakod);
    //            }
    //            ar.ULICAKOD = ulicakod;
    //            ar.ULICANAME = sd.Ulic;

    //            ar.DUCD = (int)sd.Uchac;
    //            ar.DUNAME = String.Format("Участок {0}", sd.Uchac);
    //            ar.KVARTIRA = sr.Kvar.ToString(CultureInfo.InvariantCulture) + sr.Kvar_a;
    //            ar.ISDELETED = sr.Close ? 1 : 0;
    //            ar.PRIM_ = sr.Close ? "Закрыт " + sr.Datclose.ToString(CultureInfo.InvariantCulture) : "";

    //            labonent.Add(ar);
    //            Iterate();
    //        }
    //        StepFinish();
    //        #endregion

    //        #region 4. Сохраняем справочник абонентов в базу
    //        StepStart(labonent.Count);
    //        using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
    //        {
    //            testcontext.ExecuteStoreCommand("TRUNCATE TABLE ABONENT");
    //            foreach (var abonentRecord in labonent)
    //            {
    //                testcontext.abonents.AddObject(abonentRecord);
    //                Iterate();
    //            }
    //            testcontext.SaveChanges();
    //            StepFinish();
    //        }
    //        #endregion

    //        #region 5. Сохраняем справочник дополнительных характеристик дома
    //        StepStart(dhaddchar.Count);
    //        using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
    //        {
    //            testcontext.ExecuteStoreCommand("TRUNCATE TABLE HADDCHAR");
    //            foreach (var haddchar in dhaddchar.Values)
    //            {
    //                testcontext.haddchars.AddObject(haddchar);
    //                Iterate();
    //            }
    //            testcontext.SaveChanges();
    //            StepFinish();
    //        }
    //        #endregion

    //    }
    //}

    ///// <summary>
    ///// Конвертирует данные о количественных характеристиках
    ///// </summary>
    //public class ConvertChars : ConvertCase
    //{

    //    public ConvertChars()
    //    {
    //        ConvertCaseName = "CHARS - данные о количественных характеристиках";
    //        Position = 30;
    //        IsChecked = true;
    //    }

    //    public uint CurrentLshet;

    //    public override void DoConvert()
    //    {
    //        SetStepsCount(5);

    //        var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
    //        tms.Init();

    //        #region 1. Грузим справочник абонентов (для конвертации площади)
    //        var dsprrab = new Dictionary<long, SprrabRecord>();
    //        var dt = tms.GetDataTable("SPRRAB");
    //        StepStart(dt.Rows.Count);
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            var sprrab = new SprrabRecord();
    //            sprrab.ReadDataRow(dr);
    //            dsprrab.Add(sprrab.Ls, sprrab);
    //            Iterate();
    //        }
    //        StepFinish();
    //        #endregion

    //        #region 2. Заполняем справочник площадей и текущее значение числа проживающих
    //        var lchars = new List<@char>();
    //        StepStart(dsprrab.Values.Count);
    //        foreach (SprrabRecord dr in dsprrab.Values)
    //        {
    //            var c = new @char
    //            {
    //                CHARCD = 2,
    //                CHARNAME = "Общая площадь",
    //                DATE = Consts.StartDate,
    //                LSHET = Consts.GetLs(dr.Ls),
    //                VALUE = dr.S2
    //            };
    //            lchars.Add(c);

    //            c = new @char
    //            {
    //                CHARCD = 1,
    //                CHARNAME = "Число проживающих",
    //                DATE = Consts.CurrentDateTime,
    //                LSHET = Consts.GetLs(dr.Ls),
    //                VALUE = dr.Kol
    //            };
    //            lchars.Add(c);
    //            Iterate();
    //        }
    //        StepFinish();
    //        #endregion

    //        #region 3. Грузим справочник ArcSr и заполняем по нему данные в списке характеристик
    //        dt = tms.GetDataTable("ARCSR");
    //        StepStart(dt.Rows.Count);
    //        var asr = new ArcsrRecord();
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            asr.ReadDataRow(row);
    //            string year = asr.Dat.Substring(0, 4);
    //            string month = asr.Dat.Substring(4, 2);
    //            var c = new @char
    //            {
    //                CHARCD = 1,
    //                CHARNAME = "Число проживающих",
    //                LSHET = Consts.GetLs(asr.Ls),
    //                DATE = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1),
    //                VALUE = asr.Kol
    //            };
    //            lchars.Add(c);
    //            Iterate();
    //        }
    //        StepFinish();
    //        #endregion

    //        #region 4. Прореживаем список характеристик
    //        StepStart(1);
    //        var lcharsto = CharsRecordUtils.ThinOutList(lchars);
    //        StepFinish();
    //        #endregion

    //        #region 5. Сохраняем справочник количественных характеристик в базу
    //        StepStart(lcharsto.Count);
    //        using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
    //        {
    //            testcontext.ExecuteStoreCommand("TRUNCATE TABLE CHARS");
    //            foreach (var charRecord in lcharsto)
    //            {
    //                testcontext.chars.AddObject(charRecord);
    //                Iterate();
    //            }
    //            testcontext.SaveChanges();
    //            StepFinish();
    //        }
    //        #endregion
    //    }
    //}

    ///// <summary>
    ///// Конвертирует данные о качественных характеристиках
    ///// </summary>
    //public class ConvertLChars : ConvertCase
    //{
    //    public ConvertLChars()
    //    {
    //        ConvertCaseName = "LCHARS - данные о качественных характеристиках";
    //        Position = 40;
    //        IsChecked = true;
    //    }

    //    public override void DoConvert()
    //    {
    //        SetStepsCount(2);

    //        var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
    //        tms.Init();

    //        #region 1. Проходим по справочнику абонентов
    //        var llchars = new List<@lchar>();
    //        var dt = tms.GetDataTable("SPRRAB");
    //        StepStart(dt.Rows.Count);
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            var sprrab = new SprrabRecord();
    //            sprrab.ReadDataRow(dr);
    //            string lshet = Consts.GetLs(sprrab.Ls);
    //            for (int i = 0; i < 5; i++)
    //            {
    //                string tarif = sprrab.Tarifi.Substring(i * 3, 3);
    //                if (!String.IsNullOrWhiteSpace(tarif))
    //                    llchars.AddRange(GetLcharListByTarif(Convert.ToInt32(tarif), lshet));
    //            }
    //            Iterate();
    //        }
    //        StepFinish();
    //        #endregion

    //        #region 2. Сохраняем справочник качественных характеристик в базу

    //        llchars = LcharsRecordUtils.ThinOutList(llchars);
    //        StepStart(llchars.Count);
    //        using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
    //        {
    //            testcontext.ExecuteStoreCommand("TRUNCATE TABLE LCHARS");
    //            foreach (var lcharRecord in llchars)
    //            {
    //                testcontext.lchars.AddObject(lcharRecord);
    //                Iterate();
    //            }
    //            testcontext.SaveChanges();
    //            StepFinish();
    //        }
    //        #endregion
    //    }

    //    public List<lchar> GetLcharListByTarif(int tarifCd, string lshet)
    //    {
    //        var llchar = new List<lchar>();
    //        if (tarifCd == 4)
    //        {
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 11,
    //                LCHARNAME = "Режим центрального отопления",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 1,
    //                VALUEDESC = "Наличие центрального отопления"
    //            });
    //        }
    //        else if (tarifCd == 36 || tarifCd == 38)
    //        {
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 6,
    //                LCHARNAME = "ЦГВС",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 1,
    //                VALUEDESC = "ГВС включено"
    //            });
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 19,
    //                LCHARNAME = "Счетчик горячей воды",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 0,
    //                VALUEDESC = "Без счетчика"
    //            });
    //        }
    //        else if (tarifCd == 37 || tarifCd == 39)
    //        {
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 6,
    //                LCHARNAME = "ЦГВС",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 1,
    //                VALUEDESC = "ГВС включено"
    //            });
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 19,
    //                LCHARNAME = "Счетчик горячей воды",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 1,
    //                VALUEDESC = "По счетчику"
    //            });
    //        }
    //        else if (tarifCd == 40)
    //        {
    //            llchar.Add(new lchar()
    //            {
    //                LCHARCD = 118,
    //                LCHARNAME = "ГВС общедомовые нужды",
    //                LSHET = lshet,
    //                DATE = Consts.StartDate,
    //                VALUE = 1,
    //                VALUEDESC = "ГВС ОДН"
    //            });
    //        }
    //        return llchar;
    //    }
    //}
}
