using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class HouseCDCheckCase : CheckCase
    {
        public HouseCDCheckCase()
        {
            this.CheckCaseName = "Проверяется корректность заполнения поля HouseCD в таблице ABONENT.DBF";
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
                DataTable dt = tm.ExecuteQuery("select housecd, COUNT(*) " +
                    "from (select HOUSECD, TOWNSKOD, RAYONKOD, ULICAKOD, NDOMA, KORPUS from ABONENT group by 1, 2, 3, 4, 5, 6) b group by 1 having count(*) > 1");
                if (dt.Rows.Count > 0)
                {
                    HouseCDError er = new HouseCDError();
                    er.ParentCheckCase = this;

                    this.ErrorList.Add(er);
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
