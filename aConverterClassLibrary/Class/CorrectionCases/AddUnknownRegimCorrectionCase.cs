using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class AddUnknownRegimCorrectionCase: CorrectionCase
    {
        public AddUnknownRegimCorrectionCase()
        {
            this.CorrectionCaseName = String.Format("Добавить режим с кодом 10 (Неизвестен)");
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = String.Format("INSERT INTO RESOURCESREGIMSLIST (KODREGIM, KODRESOURCESGROUPSREGIMSLIST, NAME, REGIMPRIORITY, ISCOUNTERLCHARCD, USERFILTERID) VALUES (10, NULL, 'Неизвестен', NULL, NULL, NULL);");
                        command.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                        this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
                    }
                }
            }
        }
    }
}
