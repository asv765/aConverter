using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aConverterClassLibrary
{
    public class CheckCaseFactory
    {
        public static List<CheckCase> GenerateCheckCases()
        {
            var checkCaseList = new List<CheckCase>();

            var ccLshetlength = new CheckCase()
            {
                Description =
                    "Проверка соответствия формата лицевого счета (является ли лицевой счет в таблице ABONENT строкой одинаковой длины)",
                NormalRows = 1,
                AnalyzeQuery = "SELECT CHAR_LENGTH(LSHET) AS LSHETLENGTH, COUNT(*) AS COUNT_ " +
                               "FROM CNV$ABONENT " +
                               "GROUP BY CHAR_LENGTH(LSHET) " +
                               "ORDER BY CHAR_LENGTH(LSHET)"
            };
            checkCaseList.Add(ccLshetlength);


            var ccNotUniqueLshet = new CheckCase()
            {
                Description = "Проверка на уникальность лицевых счетов в таблице CNV$ABONENT",
                NormalRows = 0,
                AnalyzeQuery = "select lshet, count(*) as count_ from cnv$abonent group by lshet having count(*) > 1",
                DependOn = ccLshetlength
            };
            checkCaseList.Add(ccNotUniqueLshet);

            var ccFio = new CheckCase()
            {
                StoredProcName = "CNV$CC_FIO",
                Description = "Проверяется, заполнены ли надлежащим образом поля F, I и O",
                NormalRows = 0,
                TestQuery = "SELECT * FROM CNV$CC_FIO(0)",
                AnalyzeQuery = "SELECT * FROM CNV$CC_FIO(1)"
            };
            checkCaseList.Add(ccFio);

            var ccNotUniqueNachoplSaldo = new CheckCase()
            {
                StoredProcName = "CNV$CC_NOTUNIQUENACHOPLSALDO",
                Description = "Проверка, встречаются ли в таблице CNV$NACHOPL несколько записей по одному лицевому счету в одном месяце по одной услуге",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NOTUNIQUENACHOPLSALDO(1)",
                FixCommand = "EXEQUTE PROCEDURE CNV$CC_NOTUNIQUENACHOPLSALDO(2)",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccNotUniqueNachoplSaldo);

            var ccSaldoHistoryGap = new CheckCase()
            {
                StoredProcName = "CNV$CC_SALDOHISTORYGAP",
                Description = "Проверка на \"пропуски\" в истории оплат/начислений",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_SALDOHISTORYGAP(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_SALDOHISTORYGAP(2)",
                DependOn = ccNotUniqueNachoplSaldo
            };
            checkCaseList.Add(ccSaldoHistoryGap);

            var ccOldNewSaldoMesmatch = new CheckCase()
            {
                StoredProcName = "CNV$CC_OLDNEWSALDOMISMATCH",
                Description = "Проверка, что для всех записей в истории оплат/начислений сальдо на конец месяца предыдущего равно сальдо на начало следующего",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_OLDNEWSALDOMISMATCH(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH(2)",
                DependOn = ccSaldoHistoryGap
            };
            checkCaseList.Add(ccOldNewSaldoMesmatch);

            var ccFirstSaldoIsNotNull = new CheckCase()
            {
                Description = "Проверка, что сальдо первой строки в истории оплат/начислений равно 0",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * " +
                               "FROM cnv$nachopl n1 " +
                               "WHERE year_*100 + month_ = (SELECT MIN(year_*100+month_) FROM cnv$nachopl n2 WHERE n1.lshet = n2.lshet AND n1.servicecd = n2.servicecd) and " +
                               "bdebet <> 0",
                DependOn = ccNotUniqueNachoplSaldo
            };
            checkCaseList.Add(ccFirstSaldoIsNotNull);

            var ccNachoplOplataNotFound = new CheckCase()
            {
                StoredProcName = "CNV$CC_NACHOPLOPLATANOTFOUND",
                Description = "Проверка, что все суммы оплат в таблице CNV$NACHOPL расшифровываются в CNV$OPLATA",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLOPLATANOTFOUND(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLOPLATANOTFOUND(2)"
            };
            checkCaseList.Add(ccNachoplOplataNotFound);

            var ccNachoplOplataMismatch = new CheckCase()
            {
                StoredProcName = "CNV$CC_NACHOPLOPLATAMISMATCH",
                Description = "Проверка, что суммы оплат в таблице CNV$NACHOPL и CNV$OPLATA совпадают",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLOPLATAMISMATCH(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH(2)"
            };
            checkCaseList.Add(ccNachoplOplataMismatch);

            var ccOplataNotFoundInNachopl = new CheckCase()
            {
                StoredProcName = "CNV$CC_OPLATANOTFOUNDINNACHOPL",
                Description = "Проверка, что суммы оплат из таблицы CNV$OPLATA присутствуют в CNV$NACHOPL",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_OPLATANOTFOUNDINNACHOPL(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL(2)"
            };
            checkCaseList.Add(ccOplataNotFoundInNachopl);

            var ccNachoplNachMismatch = new CheckCase()
            {
                StoredProcName = "CNV$CC_NACHOPLNACHMISMATCH",
                Description = "Проверка, что суммы начислений и перерасчетов в таблицах CNV$NACHOPL и CNV$NACH совпадают",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLNACHMISMATCH(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLNACHMISMATCH(2)"
            };
            checkCaseList.Add(ccNachoplNachMismatch);

            var ccNachoplNachNotFound = new CheckCase()
            {
                StoredProcName = "CNV$CC_NACHOPLNACHNOTFOUND",
                Description = "Проверка, что ненулевые суммы начислений и перерасчетов в таблице CNV$NACHOPL расшифровываются в таблице CNV$NACH",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLNACHNOTFOUND(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND(2)"
            };
            checkCaseList.Add(ccNachoplNachNotFound);

            var ccNachNotFoundInNachopl = new CheckCase()
            {
                StoredProcName = "CNV$CC_NACHNOTFOUNDINNACHOPL",
                Description = "Проверка, что суммы оплат из таблицы CNV$NACH присутствуют в CNV$NACHOPL",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHNOTFOUNDINNACHOPL(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL(2)"
            };
            checkCaseList.Add(ccNachNotFoundInNachopl);

            var ccNachoplCalculation = new CheckCase()
            {
                Description = "Проверка, что в таблице CNV$NACHOPL выполняется арифметика: EDEBET = BDBET + FNATH + PROCHL - OPLATA",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACHOPL WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)"
            };
            checkCaseList.Add(ccNachoplCalculation);

            var ccOplataYearMonth = new CheckCase()
            {
                Description = "Проверка, что в таблице CNV$OPLATA значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$OPLATA "+
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))"
            };
            checkCaseList.Add(ccOplataYearMonth);

            var ccNachYearMonth = new CheckCase()
            {
                Description = "Проверка, что в таблице CNV$NACH значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACH " +
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))"
            };
            checkCaseList.Add(ccNachYearMonth);

            var ccNachoplYearMonth = new CheckCase()
            {
                Description = "Проверка, что в таблице CNV$NACHOPL значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACHOPL " +
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))"
            };
            checkCaseList.Add(ccNachoplYearMonth);


            ////NachoplNachCheckCase2 nncc2 = new NachoplNachCheckCase2();
            ////checkCaseList.Add(nncc2);

            //var nuscc = new NotUniqueCounteridCheckCase();
            //checkCaseList.Add(nuscc);

            //var cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACHOPL", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACH", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "OPLATA", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "CHARS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LCHARS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("COUNTERS", "COUNTERID", "CNTRSIND", "COUNTERID");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "COUNTERS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "DOGOVOR", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "EQUIPMNT", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LGOTA", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "AADDCHAR", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "AADDCHAR", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("AADDCHAR", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //var ucc = new UncertaintyCheckCase("ABONENT", "ULICAKOD", "ULICANAME", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //var nucvcc = new NotUniqueCharsValuesCheckCase();
            //checkCaseList.Add(nucvcc);

            //var nulcvcc = new NotUniqueLcharsValuesCheckCase();
            //checkCaseList.Add(nulcvcc);

            //var hccc = new HouseCDCheckCase();
            //checkCaseList.Add(hccc);

            //ucc = new UncertaintyCheckCase("ABONENT", "RayonKod", "RayonName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("ABONENT", "TownsKod", "TownsName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("CHARS", "CharCD", "CharName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LCHARS", "LCharCD", "LCharName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LCHARS", "Convert(LCharCD,char) + Convert(Value,char)", "ValueDesc", UncertaintyCDType.Строка);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LGOTA", "Lgota", "LgotaName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("OPLATA", "SourceCD", "SourceName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //var efkcc = new ExternalForeighnKeyCheckCase("CHARS", "CHARCD", "CCHARSLIST", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD", "LCHARSLIST", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD*10000+Value", "LOGICVALUES", "KOD*10000+Significance");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("COUNTERS", "CNTTYPE", "COUNTERSTYPES", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("NACHOPL", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("OPLATA", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("NACH", "REGIMCD", "RESOURCESREGIMSLIST", "KODREGIM");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("ABONENT", "DUCD", "INFORMATIONOWNERS", "OWNERID");
            //checkCaseList.Add(efkcc);

            //var clccc = new CheckLshetCorrespondenceCheckCase();
            //checkCaseList.Add(clccc);

            //var urpcc = new UnknownRegimPresentCheckCase();
            //checkCaseList.Add(urpcc);

            //var mocc = new MainOrganizationCheckCase();
            //checkCaseList.Add(mocc);

            //var uepcc = new UnknownEmployeePresentCheckCase();
            //checkCaseList.Add(uepcc);

            //// Здесь добавляются свои варианты для проверки и заполнения следующих справочников
            //// 1. Справочник районов области
            ////RegionDistrictCheckCase rdcc = new RegionDistrictCheckCase();
            ////checkCaseList.Add(rdcc);

            //// 2. Справочник населенных пунктов.
            ////PunktCheckCase pcc = new PunktCheckCase();
            ////checkCaseList.Add(pcc);

            //// 3. Справочник улиц.
            ////StreetCheckCase scc = new StreetCheckCase();
            ////checkCaseList.Add(scc);

            //// 4. Справочник районов города.
            ////DistrictCheckCase dcc = new DistrictCheckCase();
            ////checkCaseList.Add(dcc);

            //// 5. Справочник источников оплаты
            ////SourceDocCheckCase sdcc = new SourceDocCheckCase();
            ////checkCaseList.Add(sdcc);

            ////// 6. Справочник групповых счетчиков
            ////CapCheckCase ccc = new CapCheckCase();
            ////checkCaseList.Add(ccc);

            return checkCaseList;
        }
    }
}
