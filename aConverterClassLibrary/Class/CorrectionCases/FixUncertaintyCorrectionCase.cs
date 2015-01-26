using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class FixUncertaintyCorrectionCase: CorrectionCase
    {
        private string tableName;
        private string fieldCD;
        private string fieldDescription;
        private UncertaintyCDType uncertaintyCDType;

        public FixUncertaintyCorrectionCase(string ATableName, string AFieldCD, string AFieldDescription, UncertaintyCDType AUncertaintyCDType)
        {
            tableName = ATableName;
            fieldCD = AFieldCD;
            fieldDescription = AFieldDescription;
            uncertaintyCDType = AUncertaintyCDType;

            this.CorrectionCaseName = String.Format("Для кода {0}.{1} установить значение {0}.{2} в то, которое встречается наиболее часто",
                tableName, fieldCD, fieldDescription);
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                dbConn.Open();
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    try
                    {
                        command.CommandText = String.Format(
                            "select {1} as cd, {2} as desc, count(*) as cnt " +
                            "from {0} c " +
                            "where {1} in ( " +
                            "select cd " +
                            "from " +
                            "(select {1} as cd, {2} as desc " +
                            "from {0} a " +
                            "group by 1,2 " +
                            ") b " +
                            "group by 1 " +
                            "having count(*) > 1 " +
                            ") " +
                            "group by 1, 2 " +
                            "order by 1, cnt DESCENDING",
                            tableName, fieldCD, fieldDescription);
                        DataTable dt = new DataTable();
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        da.Fill(dt);

                        string oldCD = "---";
                        foreach (DataRow dr in dt.Rows)
                        {
                            string currentCD = Convert.ToString(dr["cd"]); 
                            if (oldCD != currentCD)
                            {
                                string currentDescription = Convert.ToString(dr["desc"]);
                                using (OleDbCommand command2 = dbConn.CreateCommand())
                                {
                                    if (uncertaintyCDType == UncertaintyCDType.Число)
                                    {
                                        command2.CommandText = String.Format(
                                            "UPDATE {0} SET {2} = '{3}' WHERE {1} = {4}",
                                            tableName, fieldCD, fieldDescription, currentDescription.Trim(), currentCD);
                                    }
                                    else if (uncertaintyCDType == UncertaintyCDType.Строка)
                                    {
                                        command2.CommandText = String.Format(
                                            "UPDATE {0} SET {2} = '{3}' WHERE {1} = '{4}'",
                                            tableName, fieldCD, fieldDescription, currentDescription.Trim(), currentCD);
                                    }
                                    command2.ExecuteNonQuery();
                                }
                                oldCD = currentCD;
                            }
                        }
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
