using aConverterClassLibrary.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace aConverterTestProject
{
    
    
    /// <summary>
    ///Это класс теста для CntrsindRecordUtilsTest, в котором должны
    ///находиться все модульные тесты CntrsindRecordUtilsTest
    ///</summary>
    [TestClass()]
    public class CntrsindRecordUtilsTest
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


        /// <summary>
        ///Тест для RestoreHistory
        ///</summary>
        [TestMethod()]
        public void RestoreHistoryTest()
        {
            List<CntrsindRecord> cirl = new List<CntrsindRecord>();
            cirl.Add(new CntrsindRecord() { Counterid = "1", Indication = 1000, Inddate = new DateTime(2013, 1, 10) });
            cirl.Add(new CntrsindRecord() { Counterid = "1", Indication = 1500, Inddate = new DateTime(2013, 3, 12) });
            cirl.Add(new CntrsindRecord() { Counterid = "1", Indication = 1200, Inddate = new DateTime(2013, 2, 11) });
            cirl.Add(new CntrsindRecord() { Counterid = "2", Indication = 10, Inddate = new DateTime(2013, 1, 10) });
            cirl.Add(new CntrsindRecord() { Counterid = "2", Indication = 12, Inddate = new DateTime(2013, 2, 11) });
            cirl.Add(new CntrsindRecord() { Counterid = "2", Indication = 15, Inddate = new DateTime(2013, 3, 12) });

            RestoreHistoryType restoreHistoryType = RestoreHistoryType.С_конца_по_конечным_показаниям;

            CntrsindRecordUtils.RestoreHistory(ref cirl, restoreHistoryType);

            Assert.AreEqual(cirl[0].Counterid, "2");
            Assert.AreEqual(cirl[1].Counterid, "2");
            Assert.AreEqual(cirl[2].Counterid, "2");
            Assert.AreEqual(cirl[3].Counterid, "1");
            Assert.AreEqual(cirl[4].Counterid, "1");
            Assert.AreEqual(cirl[5].Counterid, "1");

            Assert.AreEqual(cirl[0].Inddate, new DateTime(2013, 3, 12));
            Assert.AreEqual(cirl[1].Inddate, new DateTime(2013, 2, 11));
            Assert.AreEqual(cirl[2].Inddate, new DateTime(2013, 1, 10));
            Assert.AreEqual(cirl[3].Inddate, new DateTime(2013, 3, 12));
            Assert.AreEqual(cirl[4].Inddate, new DateTime(2013, 2, 11) );
            Assert.AreEqual(cirl[5].Inddate, new DateTime(2013, 1, 10) );

            Assert.AreEqual(cirl[0].Indication, 15);
            Assert.AreEqual(cirl[1].Indication, 12);
            Assert.AreEqual(cirl[2].Indication, 10);
            Assert.AreEqual(cirl[3].Indication, 1500);
            Assert.AreEqual(cirl[4].Indication, 1200);
            Assert.AreEqual(cirl[5].Indication, 1000);

            Assert.AreEqual(cirl[0].Oldind, 12);
            Assert.AreEqual(cirl[1].Oldind, 10);
            Assert.AreEqual(cirl[2].Oldind, 0);
            Assert.AreEqual(cirl[3].Oldind, 1200);
            Assert.AreEqual(cirl[4].Oldind, 1000);
            Assert.AreEqual(cirl[5].Oldind, 0);

            Assert.AreEqual(cirl[0].Ob_em, 3);
            Assert.AreEqual(cirl[1].Ob_em, 2);
            Assert.AreEqual(cirl[2].Ob_em, 0);
            Assert.AreEqual(cirl[3].Ob_em, 300);
            Assert.AreEqual(cirl[4].Ob_em, 200);
            Assert.AreEqual(cirl[5].Ob_em, 0);

            
        }
    }
}
