using MySql.Data.MySqlClient;
using System;

namespace Account.DatabaseAccess
{
    public static class DatabaseHelper
    {
        public static object Scalar(string sql)
        {
            return Call(sql, (command) =>
            {
                return command.ExecuteScalar();
            });
        }

        public static TResult Read<TResult>(string sql, Func<MySqlDataReader, TResult> readHandler)
        {
            return Call(sql, (command) =>
            {
                using (var reader = command.ExecuteReader())
                {
                    return readHandler(reader);
                }
            });
        }

        private static TResult Call<TResult>(string sql, Func<MySqlCommand, TResult> commandHandler)
        {
            var connectionString = Environment.GetEnvironmentVariable("sqldb_connection");
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    return commandHandler(command);
                }
            }
        }
    }
}
