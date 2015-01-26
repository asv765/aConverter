using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class LshetLengthMismatchCorrectionCase: CorrectionCase
    {
        private int lshetlength;

        public LshetLengthMismatchCorrectionCase(int Alshetlength)
        {
            this.CorrectionCaseName = String.Format("Установить значение переменной LSHET_DIGITS_COUNT равное {0}", Alshetlength);
            lshetlength = Alshetlength;
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
                        command.CommandText = String.Format("UPDATE SYSTEMVARIABLES SET VARIABLEVALUE = '{0}' WHERE VARIABLENAME = 'LSHET_DIGITS_COUNT'",
                            lshetlength);
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
