using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class NachoplCalculationCorrectionCase: CorrectionCase
    {
        private string connectionString;
        private NachoplCorrectionType nachoplCorrectionType;

        public NachoplCalculationCorrectionCase(string AConnectionString, NachoplCorrectionType ANachoplCorrectionType)
        {
            this.CorrectionCaseName = "В некоторых строках NACHOPL.DBF значение EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)";
            
            this.connectionString = AConnectionString;
            nachoplCorrectionType = ANachoplCorrectionType;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (OleDbConnection dbConn = new OleDbConnection(connectionString))
            {
                dbConn.Open();
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    try
                    {
                        if (nachoplCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                        {
                            command.CommandText = "UPDATE NACHOPL SET BDEBET = EDEBET - (FNATH + PROCHL - OPLATA) " +
                                "WHERE BDEBET <> (EDEBET - (FNATH + PROCHL - OPLATA))"; 
                        }
                        else if (nachoplCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                        {
                            command.CommandText = "UPDATE NACHOPL SET EDEBET = BDEBET + FNATH + PROCHL - OPLATA " +
                                "WHERE EDEBET <> BDEBET + FNATH + PROCHL - OPLATA";
                        }
                        else if (nachoplCorrectionType == NachoplCorrectionType.Скорректировать_суммой_изменений)
                        {
                            command.CommandText = "UPDATE NACHOPL SET PROCHL = EDEBET - (BDEBET + FNATH - OPLATA) "+
                                "WHERE PROCHL <> (EDEBET - (BDEBET + FNATH - OPLATA))";
                        }
                        else
                        {
                            throw new Exception("Неизвестный тип корректировки сальдо");
                        }
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
