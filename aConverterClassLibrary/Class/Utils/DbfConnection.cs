using System;
using System.Data;
using System.Data.OleDb;

namespace aConverterClassLibrary.Class.Utils
{
    /// <summary>
    /// Класс для работы с DBF файлами
    /// </summary>
    public class DbfManager
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Создает объект для работы с DBF файлами из указанной папки
        /// </summary>
        /// <param name="filesDirectory">Путь к папке с DBF файлами</param>
        public DbfManager(string filesDirectory)
        {
            _connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filesDirectory};Extended Properties = dBASE IV";
        }

        /// <summary>
        /// Создает и открывает покдлючение
        /// </summary>
        /// <returns>Подключение</returns>
        public OleDbConnection OpenConnection()
        {
            var connection = new OleDbConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Выполняет указанное действтие для каждой строки результата запроса. Результат в память не заносится, что позволяет эконопить память
        /// </summary>
        /// <param name="query">Запрос, результаты которого необходимо обработать</param>
        /// <param name="drAction">Действие, которое необходимо выполнить для каждой строки результата запроса</param>
        public void ExecuteQueryByRow(string query, Action<DataRow> drAction)
        {
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand(query))
            using (var reader = cmd.ExecuteReader())
            using (var readerToDataRow = new ReaderToDataRow(reader, ReaderToDataRow.NullValuesHandledRegime.DbfLikeRegime))
            {
                while (reader.Read())
                {
                    drAction(readerToDataRow.GetDataRow(reader));
                }
            }
        }

        /// <summary>
        /// Выполняет указанное действтие для каждой строки результата запроса. Результат в память не заносится, что позволяет эконопить память
        /// </summary>
        /// <param name="query">Запрос, результаты которого необходимо обработать</param>
        /// <param name="readerAction">Действие, которое необходимо выполнить для каждой строки результата запроса</param>
        public void ExecuteQueryByReader(string query, Action<OleDbDataReader> readerAction)
        {
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand(query))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    readerAction(reader);
                }
            }
        }
    }
}
