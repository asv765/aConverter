using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class CodePageCorrectionCase: CorrectionCase
    {
        private string tableName;

        public CodePageCorrectionCase(Type ARecordType)
        {
            // this.recordType = ARecordType;
            this.tableName = String.Format("{0}\\{1}.DBF",
                aConverter_RootSettings.DestDBFFilePath,
                TableManager.GetTableName(ARecordType));
            this.CorrectionCaseName = String.Format("Установить кодовую страницу 866 DOS Russian для файла {0}", tableName);
        }

        /// <summary>
        /// Корректируем кодовую страницу
        /// </summary>
        public override void Correct()
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(this.tableName));
                bw.Seek(29, SeekOrigin.Begin);
                bw.Write(Convert.ToByte(101));
                bw.Close();
                this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
                this.Message = "Корректировка завершилась успешно";
            }
            catch (Exception ex)
            {
                this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }
}
