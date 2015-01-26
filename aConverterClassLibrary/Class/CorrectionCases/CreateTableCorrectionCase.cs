using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class CreateTableCorrectionCase: CorrectionCase
    {
        private Type recordType;

        public CreateTableCorrectionCase(Type ARecordType)
        {
            this.recordType = ARecordType;
            string tableName = TableManager.GetTableName(ARecordType);
            this.CorrectionCaseName = String.Format("Создать таблицу {0}\\{1}.DBF",
                aConverter_RootSettings.DestDBFFilePath,tableName);
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            TableManager tm = new TableManager(aConverter_RootSettings.DestDBFFilePath);
            try
            {
                tm.Init();
                tm.CreateTable(recordType);
                this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
                this.Message = "Корректировка завершилась успешно";
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
