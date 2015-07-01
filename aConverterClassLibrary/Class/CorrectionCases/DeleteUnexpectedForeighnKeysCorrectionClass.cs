﻿using System;

namespace aConverterClassLibrary
{
    public class DeleteUnexpectedForeighnKeysCorrectionClass: CorrectionCase
    {
        private string primaryTable;
        private string primaryKey;
        private string foreignTable;
        private string foreignKey;

        public DeleteUnexpectedForeighnKeysCorrectionClass(string PrimaryTable, string PrimaryKey, 
            string ForeighnTable, string ForeignKey)
        {
            this.CorrectionCaseName = String.Format("Удалить записи, для которых {0}.{1} не расшифровывается в {2}.{3}",
                ForeighnTable, ForeignKey, PrimaryTable, PrimaryKey);
            
            this.primaryTable = PrimaryTable;
            this.primaryKey = PrimaryKey;
            this.foreignTable = ForeighnTable;
            this.foreignKey = ForeignKey;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (MySqlConnection dbConn = new MySqlConnection(aConverter_RootSettings.DestMySqlConnectionString))
            {
                dbConn.Open();
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    try
                    {
                        command.CommandText = String.Format(
                            "DELETE FROM {1} WHERE {0} NOT IN (SELECT {2} FROM {3})",
                            foreignKey, foreignTable, primaryKey, primaryTable);
                        command.CommandTimeout = 0;
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
