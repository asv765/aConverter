using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace aConverterClassLibrary.Class.CheckCases
{
    public class KoneksiMariaDB
    {
        public MySqlConnection mon = new MySqlConnection();
        public KoneksiMariaDB()
        {
            string StrKon = "server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'";
            mon = new MySqlConnection(StrKon);
        }

    }
}
