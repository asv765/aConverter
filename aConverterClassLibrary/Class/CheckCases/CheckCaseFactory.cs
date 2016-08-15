using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace aConverterClassLibrary
{
    public class CheckCaseFactory
    {
        public static List<CheckCase> GenerateCheckCases()
        {
            var checkCaseList = new List<CheckCase>();

            var ccLshetlength = new CheckCase
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

            var ccLshetlength2 = new CheckCase
            {
                Description =
                    "Проверка, что длина лицевого счета совпадает со значением переменной SYSTEMVARIABLES.LSHET_DIGITS_COUNT",
                NormalRows = 0,
                AnalyzeQuery = "select VARIABLENAME, VARIABLEVALUE, (SELECT MAX(CHAR_LENGTH(LSHET)) FROM CNV$ABONENT) AS MAX_LSHET_LENGTH " +
                               "from systemvariables "+
                               "where variablename = 'LSHET_DIGITS_COUNT' and  " +
                               "cast(variablevalue as INTEGER) <> (SELECT MAX(CHAR_LENGTH(LSHET)) FROM CNV$ABONENT)",
                AnalyzeResultDescription = "Значение переменной SYSTEMVARIABLES.LSHET_DIGITS_COUNT не сооветствует длине лицевого счета в CNV$ABONENT.LSHET",
                DependOn = ccLshetlength
            };
            checkCaseList.Add(ccLshetlength2);

            var ccNotUniqueLshet = new CheckCase
            {
                Description = "Проверка на уникальность лицевых счетов в таблице CNV$ABONENT",
                NormalRows = 0,
                AnalyzeQuery = "select lshet, count(*) as count_ from cnv$abonent group by lshet having count(*) > 1",
                DependOn = ccLshetlength
            };
            checkCaseList.Add(ccNotUniqueLshet);

            var ccLshetPrefix = new CheckCase
            {
                Description = "Проверка, что у лицевх счетов соответсвует префикс, заданные через системной переменной LSHET_PREFIX",
                NormalRows = 0,
                AnalyzeQuery = "select distinct lshet, (select SUBSTRING(variablevalue FROM 1 FOR 2) " +
                                   "from systemvariables where variablename = 'LSHET_PREFIX') as prefix " +
                               "from cnv$abonent " +
                               "where substring(lshet from 1 for 2) <> (select SUBSTRING(variablevalue FROM 1 FOR 2) " +
                                   "from systemvariables where variablename = 'LSHET_PREFIX')",
                AnalyzeResultDescription = "Лицевые счета, в которых первые символы не совпадают с префиксом, заданным системной переменной LSHET_PREFIX",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccLshetPrefix);

            var ccFio = new CheckCase
            {
                StoredProcName = "CNV$CC_FIO",
                Description = "Проверяется, заполнены ли надлежащим образом поля F, I и O",
                NormalRows = 0,
                TestQuery = "SELECT * FROM CNV$CC_FIO(0)",
                AnalyzeQuery = "SELECT * FROM CNV$CC_FIO(1)"
            };
            checkCaseList.Add(ccFio);

            var ccNotUniqueNachoplSaldo = new CheckCase
            {
                StoredProcName = "CNV$CC_NOTUNIQUENACHOPLSALDO",
                Description = "Проверка, встречаются ли в таблице CNV$NACHOPL несколько записей по одному лицевому счету в одном месяце по одной услуге",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NOTUNIQUENACHOPLSALDO(1)",
                FixCommand = "EXEQUTE PROCEDURE CNV$CC_NOTUNIQUENACHOPLSALDO(2)",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccNotUniqueNachoplSaldo);

            var ccSaldoHistoryGap = new CheckCase
            {
                StoredProcName = "CNV$CC_SALDOHISTORYGAP",
                Description = "Проверка на \"пропуски\" в истории оплат/начислений",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_SALDOHISTORYGAP(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_SALDOHISTORYGAP(2)",
                DependOn = ccNotUniqueNachoplSaldo
            };
            checkCaseList.Add(ccSaldoHistoryGap);

            var ccOldNewSaldoMesmatch = new CheckCase
            {
                StoredProcName = "CNV$CC_OLDNEWSALDOMISMATCH",
                Description = "Проверка, что для всех записей в истории оплат/начислений сальдо на конец месяца предыдущего равно сальдо на начало следующего",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_OLDNEWSALDOMISMATCH(1,0)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_OLDNEWSALDOMISMATCH(2,4)", 
                DependOn = ccSaldoHistoryGap
            };
            checkCaseList.Add(ccOldNewSaldoMesmatch);

            var ccFirstSaldoIsNotNull = new CheckCase
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

            var ccNachoplOplataNotFound = new CheckCase
            {
                StoredProcName = "CNV$CC_NACHOPLOPLATANOTFOUND",
                Description = "Проверка, что все суммы оплат в таблице CNV$NACHOPL расшифровываются в CNV$OPLATA",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLOPLATANOTFOUND(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLOPLATANOTFOUND(2)"
            };
            checkCaseList.Add(ccNachoplOplataNotFound);

            var ccNachoplOplataMismatch = new CheckCase
            {
                StoredProcName = "CNV$CC_NACHOPLOPLATAMISMATCH",
                Description = "Проверка, что суммы оплат в таблице CNV$NACHOPL и CNV$OPLATA совпадают",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLOPLATAMISMATCH(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLOPLATAMISMATCH(2)"
            };
            checkCaseList.Add(ccNachoplOplataMismatch);

            var ccOplataNotFoundInNachopl = new CheckCase
            {
                StoredProcName = "CNV$CC_OPLATANOTFOUNDINNACHOPL",
                Description = "Проверка, что суммы оплат из таблицы CNV$OPLATA присутствуют в CNV$NACHOPL",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_OPLATANOTFOUNDINNACHOPL(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_OPLATANOTFOUNDINNACHOPL(2)"
            };
            checkCaseList.Add(ccOplataNotFoundInNachopl);

            var ccNachoplNachMismatch = new CheckCase
            {
                StoredProcName = "CNV$CC_NACHOPLNACHMISMATCH",
                Description = "Проверка, что суммы начислений и перерасчетов в таблицах CNV$NACHOPL и CNV$NACH совпадают",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLNACHMISMATCH(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLNACHMISMATCH(2)"
            };
            checkCaseList.Add(ccNachoplNachMismatch);

            var ccNachoplNachNotFound = new CheckCase
            {
                StoredProcName = "CNV$CC_NACHOPLNACHNOTFOUND",
                Description = "Проверка, что ненулевые суммы начислений и перерасчетов в таблице CNV$NACHOPL расшифровываются в таблице CNV$NACH",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHOPLNACHNOTFOUND(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHOPLNACHNOTFOUND(2)"
            };
            checkCaseList.Add(ccNachoplNachNotFound);

            var ccNachNotFoundInNachopl = new CheckCase
            {
                StoredProcName = "CNV$CC_NACHNOTFOUNDINNACHOPL",
                Description = "Проверка, что суммы оплат из таблицы CNV$NACH присутствуют в CNV$NACHOPL",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$CC_NACHNOTFOUNDINNACHOPL(1)",
                FixCommand = "EXECUTE PROCEDURE CNV$CC_NACHNOTFOUNDINNACHOPL(2)",
                FixDescription = "Из таблицы CNV$NACH удаляются записи, для которых не найдена соответствующая итоговая запись в CNV$NACHOPL"
            };
            checkCaseList.Add(ccNachNotFoundInNachopl);

            var ccNachoplCalculation = new CheckCase
            {
                Description = "Проверка, что в таблице CNV$NACHOPL выполняется арифметика: EDEBET = BDBET + FNATH + PROCHL - OPLATA",
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACHOPL WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)",
                FixCommand = "UPDATE cnv$nachopl SET EDEBET = BDEBET + FNATH + PROCHL - OPLATA WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)",
                FixDescription = "Обновить сальдо на конец в тех записях, в которых не выполняется арифметика EDEBET = BDEBET + FNATH + PROCHL - OPLATA"
            };
            checkCaseList.Add(ccNachoplCalculation);

            var ccOplataYearMonth = new CheckCase
            {
                Description = "Проверка, что в таблице CNV$OPLATA значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$OPLATA "+
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))",
                AnalyzeResultDescription = "Записи из CNV$OPLATA в которых значение в поле MONTH_ не лежит в диапазоне от 1 до 12, либо значение в поле YEAR_ не лежит в диапазоне от 2000 до " + DateTime.Now.Year
            };
            checkCaseList.Add(ccOplataYearMonth);

            var ccNachYearMonth = new CheckCase
            {
                Description = "Проверка, что в таблице CNV$NACH значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACH " +
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))",
                AnalyzeResultDescription = "Записи из CNV$NACH в которых значение в поле MONTH_ не лежит в диапазоне от 1 до 12, либо значение в поле YEAR_ не лежит в диапазоне от 2000 до " + DateTime.Now.Year
            };
            checkCaseList.Add(ccNachYearMonth);

            var ccNachoplYearMonth = new CheckCase
            {
                Description = "Проверка, что в таблице CNV$NACHOPL значения в поле MONTH_ лежат в диапазоне от 1 до 12, а в поле YEAR_ - от 2000 до " + DateTime.Now.Year,
                NormalRows = 0,
                AnalyzeQuery = "SELECT * FROM CNV$NACHOPL " +
                               "WHERE (MONTH_ not BETWEEN 1 and 12) OR (YEAR_ not BETWEEN 2000 and EXTRACT(YEAR FROM cast('today' as date)))",
                AnalyzeResultDescription = "Записи из CNV$NACHOPL в которых значение в поле MONTH_ не лежит в диапазоне от 1 до 12, либо значение в поле YEAR_ не лежит в диапазоне от 2000 до " + DateTime.Now.Year
            };
            checkCaseList.Add(ccNachoplYearMonth);

            var ccNotUniqueCounterid = new CheckCase
            {
                Description = "Проверка на уникальность значений COUNTERID в таблице CNV$COUNTERS",
                NormalRows = 0,
                AnalyzeQuery = "select * from cnv$counters " +
                               "where counterid in (select counterid as count_ from cnv$counters group by counterid having count(*) > 1) " +
                               "order by counterid",
                AnalyzeResultDescription = "Записи из CNV$COUNTERS в которых значения в поле COUNTERID являются неуникальными",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccNotUniqueCounterid);

            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$NACHOPL", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$NACH", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$OPLATA", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$CHARS", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$LCHARS", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$COUNTERS", "COUNTERID", "CNV$CNTRSIND", "COUNTERID", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$COUNTERS", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$DOGOVOR", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$EQUIPMENT", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$CITIZENS", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$ABONENT", "LSHET", "CNV$AADDCHAR", "LSHET", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$CHARLST", "ADDCHARCD", "CNV$AADDCHAR", "ADDCHARCD", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$CHARLST", "ADDCHARCD", "CNV$CHARVALS", "ADDCHARCD", ccNotUniqueLshet));
            checkCaseList.Add(GenerateIntegrityCheckCase("CNV$AADDCHAR", "ADDCHARCD", "CNV$CHARVALS", "ADDCHARCD", ccNotUniqueLshet));

            checkCaseList.Add(GenerateIntegrityCheckCase("CCHARSLIST", "KOD", "CNV$CHARS", "CHARCD", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("LCHARSLIST", "KOD", "CNV$LCHARS", "LCHARCD", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("COUNTERSTYPES", "KOD", "CNV$COUNTERS", "CNTTYPE", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("BALANCESLIST", "BALANCE_KOD", "CNV$NACHOPL", "SERVICECD", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("BALANCESLIST", "BALANCE_KOD", "CNV$OPLATA", "SERVICECD", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("RESOURCESREGIMSLIST", "KODREGIM", "CNV$NACH", "REGIMCD", null));
            checkCaseList.Add(GenerateIntegrityCheckCase("INFORMATIONOWNERS", "OWNERID", "CNV$ABONENT", "DUCD", null));

            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$ABONENT", "ULICAKOD", "ULICANAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$ABONENT", "RAYONKOD", "RAYONNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$ABONENT", "TOWNSKOD", "TOWNSNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$ABONENT", "DUCD", "DUNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$CHARS", "CHARCD", "CHARNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$LCHARS", "LCHARCD", "LCHARNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$CITIZENS", "LGOTA", "LGOTANAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$OPLATA", "SOURCECD", "SOURCENAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$OPLATA", "SERVICECD", "SERVICENAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$NACH", "REGIMCD", "REGIMNAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$NACH", "SERVICECD", "SERVICENAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$NACHOPL", "SERVICECD", "SERVICENAME", null));
            checkCaseList.Add(GenerateUncertaintyCheckCase("CNV$COUNTERS", "CNTTYPE", "CNTNAME", null));

            var ccLcharsUncertainty = new CheckCase
            {
                Description = "Проверка в CNV$LCHARS что для комбинации значений в полях LCHARCD, VALUE_ встречается только один вариант расшифровки в полях LCHARNAME, VALUEDESC",
                NormalRows = 0,
                AnalyzeQuery = "select clc1.* " +
                               "from cnv$lchars clc1 " +
                               "inner join " +
                               "(select lcharcd, value_, count(*) as cnt " +
                               "from " +
                               "( " +
                               "select clc2.lcharcd, clc2.value_, clc2.lcharname, clc2.valuedesc " +
                               "from cnv$lchars clc2 " +
                               "group by clc2.lcharcd, clc2.value_, clc2.lcharname, clc2.valuedesc) " +
                               "group by lcharcd, value_ " +
                               "having count(*) > 1) clc3 " +
                               "on clc1.lcharcd = clc3.lcharcd and clc1.value_ = clc3.value_ " +
                               "order by clc1.lshet, clc1.lcharcd,  clc1.lcharname, clc1.value_, clc1.valuedesc",
                AnalyzeResultDescription = "Записи из CNV$LCHARS в которых для комбинации значений в полях LCHARCD, VALUE_ встречаются более одного варианта расшифровки в полях LCHARNAME, VALUEDESC",
                DependOn = ccNotUniqueLshet                
            };
            checkCaseList.Add(ccLcharsUncertainty);

            var ccNotUniqueCharsValues = new CheckCase
            {
                Description = "Проверка в CNV$CHARS, существуют ли записи с повторяющимися сочетанием LSHET, CHARCD, DATE_",
                NormalRows = 0,
                AnalyzeQuery = "select cc1.* " +
                               "from cnv$chars cc1 inner join " +
                               "(select cc2.lshet, cc2.charcd, cc2.date_ " +
                               "from cnv$chars cc2 " +
                               "group by cc2.lshet, cc2.charcd, cc2.date_ " +
                               "having count(*) > 1) cc3 " +
                               "on cc1.lshet = cc3.lshet and cc1.charcd = cc3.charcd and cc1.date_ = cc3.date_ " +
                               "order by cc1.lshet, cc1.charcd, cc1.date_",
                AnalyzeResultDescription = "Записи из CNV$CHARS c повторяющимся сочетанием LSHET, CHARCD, DATE_",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccNotUniqueCharsValues);

            var ccNotUniqueLcharsValues = new CheckCase
            {
                Description = "Проверка в CNV$LCHARS, существуют ли записи с повторяющимися сочетанием LSHET, LCHARCD, DATE_",
                NormalRows = 0,
                AnalyzeQuery = "select lc1.* " +
                               "from cnv$lchars lc1 inner join " +
                               "(select lc2.lshet, lc2.lcharcd, lc2.date_ " +
                               "from cnv$lchars lc2 " +
                               "group by lc2.lshet, lc2.lcharcd, lc2.date_ " +
                               "having count(*) > 1) lc3 " +
                               "on lc1.lshet = lc3.lshet and lc1.lcharcd = lc3.lcharcd and lc1.date_ = lc3.date_ "+
                               "order by lc1.lshet,  lc1.lcharcd, lc1.date_",
                AnalyzeResultDescription = "Записи из CNV$LCHARS c повторяющимся сочетанием LSHET, LCHARCD, DATE_",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccNotUniqueLcharsValues);

            var ccHouseCd = new CheckCase
            {
                Description = "Проверка в CNV$ABONENT уникальность значений HOUSECD для домов",
                NormalRows = 0,
                AnalyzeQuery = "select a1.* from cnv$abonent a1 " +
                               "where housecd in (select a4.housecd " +
                               "from (select a3.HOUSECD, a3.TOWNSKOD, a3.RAYONKOD, a3.ULICAKOD, a3.houseno, a3.housepostfix, a3.korpusno, a3.korpuspostfix, COUNT(*) as cnt " +
                               "from cnv$abonent a3 " +
                               "group by a3.HOUSECD, a3.TOWNSKOD, a3.RAYONKOD, a3.ULICAKOD, a3.houseno, a3.housepostfix, a3.korpusno, a3.korpuspostfix) a4 " +
                               "group by a4.housecd having count(*) > 1) " +
                               "order by a1.HOUSECD, a1.TOWNSKOD, a1.RAYONKOD, a1.ULICAKOD, a1.houseno, a1.housepostfix, a1.korpusno, a1.korpuspostfix",
                AnalyzeResultDescription = "Абоненты, у которых для разных домов (TOWNSKOD, RAYONKOD, ULICAKOD, HOUSENO, HOUSEPOSTFIX, KORPUSNO, KORPUSPOSTFIX) встречаются одинаковые значения в поле HOUSECD",
                DependOn = ccNotUniqueLshet
            };
            checkCaseList.Add(ccHouseCd);

            var ccMainOrganizationPresent = new CheckCase
            {
                Description = "Проверка, что в справочнике EXTORGSPR существует организация с кодом 1",
                NormalRows = 1,
                AnalyzeQuery = "select * from EXTORGSPR where EXTORGCD = 1",
                AnalyzeResultDescription = "Организация с кодом 1 отсутствует"
            };
            checkCaseList.Add(ccMainOrganizationPresent);

            var ccUnknownEmployeePresent = new CheckCase
            {
                Description = "Проверка, что в справочнике EMPLOYEES существует сотрудник, у которого TABNUMBER = 1",
                NormalRows = 1,
                AnalyzeQuery = "select * from EMPLOYEES e where e.tabnumber = 1",
                AnalyzeResultDescription = "Сотрудник с TABNUMBER = 1 отсутствует"
            };
            checkCaseList.Add(ccUnknownEmployeePresent);

            return checkCaseList;
        }

        private static CheckCase GenerateIntegrityCheckCase(string pkTable, string pkField, 
            string fkTable, string fkField, CheckCase dependOn)
        {
            var cc = new CheckCase
            {
                Description = String.Format("Проверка, что {0}.{1} расшифровывается в {2}.{3}", fkTable, fkField, pkTable, pkField),
                NormalRows = 0,
                AnalyzeQuery = String.Format("select t1.* from {0} t1 where (select t2.{3} from {2} t2 where t1.{1} = t2.{3}) is null", fkTable, fkField, pkTable, pkField),
                AnalyzeResultDescription = String.Format("Записи из {0}, поле {1} в которых не расшифровывается в {2}.{3}", fkTable, fkField, pkTable, pkField),
                FixCommand = String.Format("delete from {0} t1 where (select t2.{3} from {2} t2 where t1.{1} = t2.{3}) is null", fkTable, fkField, pkTable, pkField),
                FixDescription = String.Format("Из таблицы {0} удалить все записи, поле {1} в которых не расшифровывается в {2}.{3}", fkTable, fkField, pkTable, pkField),
                DependOn = dependOn
            };
            return cc;
        }

        private static CheckCase GenerateUncertaintyCheckCase(string tableName, string cdFieldName, string descFieldName,
            CheckCase dependOn)
        {
            var cc = new CheckCase
            {
                Description =
                    String.Format(
                        "Проверяется, что {0}.{1} имеет только одинаковые значения для всех уникальных значений {0}.{2}",
                        tableName, cdFieldName, descFieldName),
                NormalRows = 0,
                AnalyzeQuery = String.Format("select a.{1}, a.{2}, COUNT(*) AS CNT " +
                                             "from {0} a inner join (select cd " +
                                             "from (select {1} as cd, {2} " +
                                             "from {0} group by {1}, {2}) b " +
                                             "group by cd " +
                                             "having count(*) > 1) c " +
                                             "on a.{1} = c.cd " + 
                                             "group by a.{1}, a.{2}",
                    tableName, cdFieldName, descFieldName),
                AnalyzeResultDescription = String.Format("Записи из {0} в которых для одинаковых значений в поле {1} встречаются различные значения в поле {2}",
                    tableName, cdFieldName, descFieldName),
                DependOn = dependOn
            };
            return cc;
        }
    }



}
