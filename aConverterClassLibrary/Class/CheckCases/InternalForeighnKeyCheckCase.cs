using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class InternalForeighnKeyCheckCase: CheckCase
    {
        private string primaryTable;
        private string primaryKey;
        private string foreignTable;
        private string foreignKey;

        public InternalForeighnKeyCheckCase(string PrimaryTable, string PrimaryKey, 
            string ForeighnTable, string ForeignKey)
        {
            this.CheckCaseName = String.Format("Проверяется, что {0}.{1} расшифровывается в {2}.{3}",
                ForeighnTable, ForeignKey, PrimaryTable, PrimaryKey);
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
            
            this.primaryTable = PrimaryTable;
            this.primaryKey = PrimaryKey;
            this.foreignTable = ForeighnTable;
            this.foreignKey = ForeignKey;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format(
                        "SELECT {0} FROM {1} WHERE {0} NOT IN (SELECT {2} FROM {3})",
                        foreignKey, foreignTable, primaryKey, primaryTable);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        return;
                    else
                    {
                        InternalIntegrityErrorClass iiec = new InternalIntegrityErrorClass(foreignTable, foreignKey, primaryTable, primaryKey);
                        iiec.ParentCheckCase = this;
                        //iiec.ErrorName = String.Format(
                        //    "{0} значений поля {1}.{2} не расшифровываются в {3}.{4}",
                        //    dt.Rows.Count, foreignTable, foreignKey, primaryTable, primaryKey);
                        this.ErrorList.Add(iiec);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
