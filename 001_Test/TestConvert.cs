using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001_Test
{
    public static class Consts
    {
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

    public class ReCreateProcedures : ConvertCase
    {
        public ReCreateProcedures()
        {
            ConvertCaseName = "Пересоздать хранимые процедуры";
            Position = 15;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    public class ClearTables : ConvertCase
    {
        public ClearTables()
        {
            ConvertCaseName = "Очищаем таблицы CNV$NACHOPL, CNV$NACH, CNV$OPLATA";
            Position = 17;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");
        }
    }

    public class TestCcNachoplOplata : ConvertCase
    {
        public TestCcNachoplOplata()
        {
            ConvertCaseName = "Добавляем данные для тестирования CC_NACHOPLOPLATANOTFOUND, CC_NACHOPLOPLATAMISMATCH, CC_OPLATANOTFOUNDINNACHOPL";
            Position = 20;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            var cno1 = new CNV_NACHOPL()
            {
                LSHET = "000000001",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                OPLATA = 100
            };

            var cno2 = new CNV_NACHOPL()
            {
                LSHET = "000000002",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                OPLATA = 200
            };


            var co1 = new CNV_OPLATA()
            {
                LSHET = "000000002",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                SUMMA = 100,
                DATE_ = new DateTime(2015,7,10),
                DATE_VV = new DateTime(2015, 7, 10),
                SOURCECD = 1,
                SOURCENAME = "Тестирование",
                DOCUMENTCD = "1"
            };

            var co2 = new CNV_OPLATA()
            {
                LSHET = "000000002",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                SUMMA = 50,
                DATE_ = new DateTime(2015, 7, 10),
                DATE_VV = new DateTime(2015, 7, 10),
                SOURCECD = 1,
                SOURCENAME = "Тестирование",
                DOCUMENTCD = "2"
            };

            var co3 = new CNV_OPLATA()
            {
                LSHET = "000000003",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                SUMMA = 300,
                DATE_ = new DateTime(2015, 7, 10),
                DATE_VV = new DateTime(2015, 7, 10),
                SOURCECD = 1,
                SOURCENAME = "Тестирование",
                DOCUMENTCD = "3"
            };

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(cno1);
                acem.Add(cno2);
                acem.Add(co1);
                acem.Add(co2);
                acem.Add(co3);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

    public class TestCcNachoplNach : ConvertCase
    {
        public TestCcNachoplNach()
        {
            ConvertCaseName = "Добавляем данные для тестирования CC_NACHOPLNACHNOTFOUND, CC_NACHOPLNACHMISMATCH, CC_NACHNOTFOUNDINNACHOPL";
            Position = 30;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            // Данные не расшифровываются
            var cno1 = new CNV_NACHOPL()
            {
                LSHET = "000000010",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 100,
                PROCHL = 0
            };

            // Данные не расшифровываются
            var cno2 = new CNV_NACHOPL()
            {
                LSHET = "000000011",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 0,
                PROCHL = 150
            };

            // Данные не расшифровываются
            var cno3 = new CNV_NACHOPL()
            {
                LSHET = "000000012",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 200,
                PROCHL = 250
            };

            // Не совпадают данные по FNATH
            var cno4 = new CNV_NACHOPL()
            {
                LSHET = "000000013",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 300,
                PROCHL = 0
            };

            // Не совпадают данные по PROCHL
            var cno5 = new CNV_NACHOPL()
            {
                LSHET = "000000014",
                MONTH_ = 7,
                YEAR_ = 2015,
                MONTH2 = 7,
                YEAR2 = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 0,
                PROCHL = 350
            };

            // Не совпадают данные по FNATH
            var nc1 = new CNV_NACH()
            {
                LSHET = "000000013",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 301,
                PROCHL = 0,
                DATE_VV = new DateTime(2015, 7, 10),
                DOCUMENTCD = "4",
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            // Не совпадают данные по PROCHL
            var nc2 = new CNV_NACH()
            {
                LSHET = "000000014",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 0,
                PROCHL = 351,
                DATE_VV = new DateTime(2015, 7, 10),
                DOCUMENTCD = "4",
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            // Данные отсутствуют в NACHOPL
            var nc3 = new CNV_NACH()
            {
                LSHET = "000000015",
                MONTH_ = 7,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 400,
                PROCHL = 450,
                DATE_VV = new DateTime(2015, 7, 10),
                DOCUMENTCD = "4",
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(cno1);
                acem.Add(cno2);
                acem.Add(cno3);
                acem.Add(cno4);
                acem.Add(cno5);
                acem.Add(nc1);
                acem.Add(nc2);
                acem.Add(nc3);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

    public class TestCcOplataYearMonth : ConvertCase
    {
        public TestCcOplataYearMonth()
        {
            ConvertCaseName = "Добавляем данные для тестирования CC_OPLATAYEARMONTH";
            Position = 40;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            var co1 = new CNV_OPLATA()
            {
                LSHET = "000000002",
                MONTH_ = 7,
                YEAR_ = 1999,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                SUMMA = 100,
                DATE_ = new DateTime(2015, 7, 10),
                DATE_VV = new DateTime(2015, 7, 10),
                SOURCECD = 1,
                SOURCENAME = "Тестирование",
                DOCUMENTCD = "1"
            };

            var co2 = new CNV_OPLATA()
            {
                LSHET = "000000002",
                MONTH_ = 0,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                SUMMA = 100,
                DATE_ = new DateTime(2015, 7, 10),
                DATE_VV = new DateTime(2015, 7, 10),
                SOURCECD = 1,
                SOURCENAME = "Тестирование",
                DOCUMENTCD = "1"
            };

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(co1);
                acem.Add(co2);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

    public class TestCcNachYearMonth : ConvertCase
    {
        public TestCcNachYearMonth()
        {
            ConvertCaseName = "Добавляем данные для тестирования CC_NACHYEARMONTH";
            Position = 50;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            // Не совпадают данные по FNATH
            var nc1 = new CNV_NACH()
            {
                LSHET = "000000013",
                MONTH_ = 7,
                YEAR_ = 1999,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 301,
                PROCHL = 0,
                DATE_VV = new DateTime(2015, 7, 10),
                DOCUMENTCD = "4",
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            // Не совпадают данные по PROCHL
            var nc2 = new CNV_NACH()
            {
                LSHET = "000000014",
                MONTH_ = 13,
                YEAR_ = 2015,
                SERVICECD = 10,
                SERVICENAME = "Для тестирования",
                FNATH = 0,
                PROCHL = 351,
                DATE_VV = new DateTime(2015, 7, 10),
                DOCUMENTCD = "4",
                REGIMCD = 10,
                REGIMNAME = "Неизвестен"
            };

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(nc1);
                acem.Add(nc2);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }
    }

}
