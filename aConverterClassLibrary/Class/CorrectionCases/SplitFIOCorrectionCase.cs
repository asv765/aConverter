using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class SplitFIOCorrectionCase : CorrectionCase
    {
        public SplitFIOCorrectionCase()
        {
            this.CorrectionCaseName = String.Format("Разбить поле ФИО в поле F таблицы ABONENT.DBF на Фамилию (F), Имя (I) и Отчество (O)");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";
            TableManager tm = new TableManager(aConverter_RootSettings.DestDBFFilePath);
            tm.Init();
            try
            {
                DataTable dt = tm.GetDataTable("ABONENT");
                foreach (DataRow dr in dt.Rows)
                {
                    string lshet = dr["LSHET"].ToString();
                    string f = dr["F"].ToString();

                    string i = Regex.Match(f, @"(?<=^\w+\s+)\w*").Value;
                    string o = Regex.Match(f, @"(?<=^\w+\s+\w+\s+)\w*").Value;
                    f = Regex.Match(f, @"^\w*").Value;

                    string update = String.Format("UPDATE ABONENT SET F = '{0}', I = '{1}', O = '{2}' WHERE LSHET = '{3}'", f, i, o, lshet);

                    tm.ExecuteNonQuery(update);
                }
            }
            catch (Exception ex)
            {
                this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
            finally
            {
                tm.Dispose();
            }

        }
    }
}
