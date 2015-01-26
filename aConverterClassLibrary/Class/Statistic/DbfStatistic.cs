using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Xml.Serialization;

namespace aConverterClassLibrary
{
    [Serializable]
    public class DbfStatistic: Statistic
    {
        public DbfStatistic() { }

        public DbfStatistic(string AName, string ASql, List<string> AFieldRecodeList):
            base(AName, ASql, AFieldRecodeList)
        {
        }

        public override System.Data.DataTable GenerateStatistic()
        {
            {
                using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
                {
                    using (OleDbCommand command = dbConn.CreateCommand())
                    {
                        command.CommandText = Sql;
                        DataTable dt = new DataTable();
                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
