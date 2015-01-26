using aConverterClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DbfClassLibrary;

namespace aConverterTestProject
{
    /// <summary>
    ///Это класс теста для AbstractRecordTest, в котором должны
    ///находиться все модульные тесты AbstractRecordTest
    ///</summary>
    [TestClass()]
    public class AbstractRecordTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual AbonentRecordForTest CreateRecord()
        {
            // TODO: создайте экземпляр подходящего конкретного класса.
            AbonentRecordForTest target = new AbonentRecordForTest();
            return target;
        }

        /// <summary>
        ///Тест для CheckData
        ///</summary>
        [TestMethod()]
        public void CheckDataTest()
        {
            AbonentRecordForTest target = CreateRecord(); // TODO: инициализация подходящего значения
            try
            {
                target.Lshet = "12345678901";
                Assert.Fail("Должно возникнуть исключение на превышение максимальной длины строки!");
            }
            catch (ArgumentException) {};
            target.Lshet = "1234567890";

            try
            {
                target.DistKod = 1234;
                Assert.Fail("Должно возникнуть исключение на превышение максимальной длины поля!");
            }
            catch (ArgumentException) { };
            target.DistKod = 123;

            try
            {
                target.Saldo = 1123.45M;
                Assert.Fail("Должно возникнуть исключение на превышение максимальной длины поля!");
            }
            catch (ArgumentException) { };
            try
            {
                target.Saldo = 123.456M;
                Assert.Fail("Должно возникнуть исключение на превышение максимальной длины дробной части!");
            }
            catch (ArgumentException) { };
            target.Saldo = 123.45M;

            target.IsDeleted = false;
            target.D_Dog = DateTime.Now;
        }
    }

    /// <summary>
    /// Класс-запись для проверки AbstractRecord
    /// </summary>
    public class AbonentRecordForTest : AbstractRecord
    {
        private string lshet;
        /// <summary>
        /// Лицевой счет
        /// </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckData("Lshet", value); lshet = value; }
        }

        private Int32 distKod;
        /// <summary>
        /// Код 
        /// </summary>
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(3)]
        public Int32 DistKod
        {
            get { return distKod; }
            set { CheckData("DistKod", value); distKod = value; }
        }

        private int distName;
        [FieldName("DISTNAME"), FieldType('C'), FieldWidth(40)]
        public int DistName
        {
            get { return distName; }
            set { CheckData("DistName", value); distName = value; }
        }

        private decimal saldo;
        /// <summary>
        /// Код 
        /// </summary>
        [FieldName("SALDO"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Saldo
        {
            get { return saldo; }
            set { CheckData("Saldo", value); saldo = value; }
        }

        private bool isDeleted;
        /// <summary>
        /// Код 
        /// </summary>
        [FieldName("ISDELETED"), FieldType('L')]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { CheckData("IsDeleted", value); isDeleted = value; }
        }

        private DateTime d_dog;
        /// <summary>
        /// Код 
        /// </summary>
        [FieldName("D_DOG"), FieldType('D')]
        public DateTime D_Dog
        {
            get { return d_dog; }
            set { CheckData("D_Dog", value); d_dog = value; }
        }
    }
}
