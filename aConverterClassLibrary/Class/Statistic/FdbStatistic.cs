using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Xml.Serialization;

namespace aConverterClassLibrary
{
    [Serializable]
    public class FdbStatistic: Statistic
    {
        public FdbStatistic() { }

         public FdbStatistic(string AName, string ASql, List<string> AFieldRecodeList):
            base(AName, ASql, AFieldRecodeList)
        {
        }

        public override System.Data.DataTable  GenerateStatistic()
        {
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = Sql;
                    DataTable dt = new DataTable();
                    FbDataAdapter fda = new FbDataAdapter(command);
                    fda.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
