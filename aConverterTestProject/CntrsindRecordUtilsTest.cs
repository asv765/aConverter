using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
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
            List<CNTRSIND> cirl = new List<CNTRSIND>();
            cirl.Add(new CNTRSIND() { COUNTERID = "1", INDICATION = 1000, INDDATE = new DateTime(2013, 1, 10) });
            cirl.Add(new CNTRSIND() { COUNTERID = "1", INDICATION = 1500, INDDATE = new DateTime(2013, 3, 12) });
            cirl.Add(new CNTRSIND() { COUNTERID = "1", INDICATION = 1200, INDDATE = new DateTime(2013, 2, 11) });
            cirl.Add(new CNTRSIND() { COUNTERID = "2", INDICATION = 10, INDDATE = new DateTime(2013, 1, 10) });
            cirl.Add(new CNTRSIND() { COUNTERID = "2", INDICATION = 12, INDDATE = new DateTime(2013, 2, 11) });
            cirl.Add(new CNTRSIND() { COUNTERID = "2", INDICATION = 15, INDDATE = new DateTime(2013, 3, 12) });

            RestoreHistoryType restoreHistoryType = RestoreHistoryType.С_конца_по_конечным_показаниям;

            CntrsindRecordUtils.RestoreHistory(ref cirl, restoreHistoryType);

            Assert.AreEqual(cirl[0].COUNTERID, "2");
            Assert.AreEqual(cirl[1].COUNTERID, "2");
            Assert.AreEqual(cirl[2].COUNTERID, "2");
            Assert.AreEqual(cirl[3].COUNTERID, "1");
            Assert.AreEqual(cirl[4].COUNTERID, "1");
            Assert.AreEqual(cirl[5].COUNTERID, "1");

            Assert.AreEqual(cirl[0].INDDATE, new DateTime(2013, 3, 12));
            Assert.AreEqual(cirl[1].INDDATE, new DateTime(2013, 2, 11));
            Assert.AreEqual(cirl[2].INDDATE, new DateTime(2013, 1, 10));
            Assert.AreEqual(cirl[3].INDDATE, new DateTime(2013, 3, 12));
            Assert.AreEqual(cirl[4].INDDATE, new DateTime(2013, 2, 11) );
            Assert.AreEqual(cirl[5].INDDATE, new DateTime(2013, 1, 10) );

            Assert.AreEqual(cirl[0].INDICATION, 15);
            Assert.AreEqual(cirl[1].INDICATION, 12);
            Assert.AreEqual(cirl[2].INDICATION, 10);
            Assert.AreEqual(cirl[3].INDICATION, 1500);
            Assert.AreEqual(cirl[4].INDICATION, 1200);
            Assert.AreEqual(cirl[5].INDICATION, 1000);

            Assert.AreEqual(cirl[0].OLDIND, 12);
            Assert.AreEqual(cirl[1].OLDIND, 10);
            Assert.AreEqual(cirl[2].OLDIND, 0);
            Assert.AreEqual(cirl[3].OLDIND, 1200);
            Assert.AreEqual(cirl[4].OLDIND, 1000);
            Assert.AreEqual(cirl[5].OLDIND, 0);

            Assert.AreEqual(cirl[0].OB_EM, 3);
            Assert.AreEqual(cirl[1].OB_EM, 2);
            Assert.AreEqual(cirl[2].OB_EM, 0);
            Assert.AreEqual(cirl[3].OB_EM, 300);
            Assert.AreEqual(cirl[4].OB_EM, 200);
            Assert.AreEqual(cirl[5].OB_EM, 0);

            
        }
    }
}
