using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class CorrectStructureCorrectionCase: CorrectionCase
    {
        private Type recordType;
        List<DbfField> modifiedField;
        List<DbfField> addedField;

        public CorrectStructureCorrectionCase(Type ARecordType, List<DbfField> AModifiedField, List<DbfField> AAddedField)
        {
            this.recordType = ARecordType;
            string tableName = TableManager.GetTableName(ARecordType);
            this.CorrectionCaseName = String.Format("Привести структуру таблицы {0}\\{1}.DBF в соответствие со стандартной",
                aConverter_RootSettings.DestDBFFilePath,tableName);
            modifiedField = AModifiedField;
            addedField = AAddedField;
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
                tm.ModifyStructure(recordType, modifiedField, addedField);
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
