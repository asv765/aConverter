using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class FioCheckCase : CheckCase
    {
        public FioCheckCase()
        {
            this.CheckCaseName = "Проверяется, заполненны ли надлежащим образом поля F, I и O";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            TableManager tm = new TableManager(aConverter_RootSettings.DestDBFFilePath);
            tm.Init();
            try
            {
                int fcount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(F)"));
                int icount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(I)"));
                int ocount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(O)"));

                if (fcount != 0 && icount == 0 && ocount == 0)
                {
                    FioErrorClass ec = new FioErrorClass(fcount);
                    ec.ParentCheckCase = this;
                    this.ErrorList.Add(ec);
                    this.Result = CheckCaseStatus.Выявлена_ошибка;
                }
            }
            finally
            {
                tm.Dispose();
            }
        }
    }
}
