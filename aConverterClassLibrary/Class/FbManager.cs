using System;
using System.Collections.Generic;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using FirebirdSql.Data.Isql;

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
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = aQuery;
                        command.Transaction = transaction;
                        int result = command.ExecuteNonQuery();
                        command.Transaction.Commit();
                        return result;
                    }
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

        /// <summary>
        /// Выполняет скрипт
        /// </summary>
        /// <param name="script"></param>
        /// <param name="ignoreerror"></param>
        public void ExecuteScript(string script, bool ignoreerror = false)
        {
            if (script == null) throw new ArgumentNullException("script");
            var fs = new FbScript(script);
            fs.Parse();
            using (var connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                foreach (var result in fs.Results)
                {
                    var fbc = new FbCommand(result, connection);
                    if (!ignoreerror)
                        fbc.ExecuteNonQuery();
                    else
                    {
                        try
                        {
                            fbc.ExecuteNonQuery();
                        }
                        catch (FbException)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает элементы схемы
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="restrictionValues"></param>
        /// <returns></returns>
        public DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            using (var fc = new FbConnection(ConnectionString))
            {
                fc.Open();
                return fc.GetSchema(collectionName, restrictionValues);
            }
        }

        /// <summary>
        /// Возвращает элементы схемы
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchema()
        {
            using (var fc = new FbConnection(ConnectionString))
            {
                fc.Open();
                return fc.GetSchema();
            }
        }

        /// <summary>
        /// Возвращает элементы схемы
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public DataTable GetSchema(string collectionName)
        {
            using (var fc = new FbConnection(ConnectionString))
            {
                fc.Open();
                return fc.GetSchema(collectionName);
            }
        }

        public void ExecuteProcedure(string procedureName, string[] parameters = null )
        {
            string addstring = "";
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (addstring != "") addstring += ", ";
                    addstring += p;
                }
                addstring = "(" + addstring + ")";
            }
            string query = "EXECUTE PROCEDURE " + procedureName + addstring;
            ExecuteNonQuery(query);
        }
    }
}
