using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace aConverterClassLibrary.Class
{
    public class FbManager
    {
        private string ConnectionString { get; set; }

        public FbManager(string aConnectionString)
        {
            ConnectionString = aConnectionString;
        }

        /// <summary>
        /// Возвращает таблицу с данными
        /// </summary>
        /// <param name="aTableName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string aTableName)
        {
            return ExecuteQuery("SELECT * FROM " + aTableName);
        }


        /// <summary>
        /// Возвращает результаты выполнения запроса
        /// </summary>
        public DataTable ExecuteQuery(string aQuery)
        {
            using (var connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = aQuery;
                    var dt = new DataTable();
                    var da = new FbDataAdapter(command);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Выполняет запрос и возвращает результат - скалярное значение
        /// </summary>
        /// <param name="aQuery"></param>
        /// <returns></returns>
        public object ExecuteScalar(string aQuery)
        {
            using (var connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = aQuery;
                    object o = command.ExecuteScalar();
                    return o;
                }
            }
        }

        /// <summary>
        /// Выполняет запрос, не возвращающий результат
        /// </summary>
        /// <param name="aQuery"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string aQuery)
        {
            using (var connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = aQuery;
                    return command.ExecuteNonQuery();
                }
            }
        }

        public FbDataReader ExecuteReader(string aQuery)
        {
            using (var connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = aQuery;
                    return command.ExecuteReader();
                }
            }
        }

    }
}
