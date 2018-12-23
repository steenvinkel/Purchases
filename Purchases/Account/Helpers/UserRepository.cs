using Account.DatabaseAccess;
using Account.Models;
using System;

namespace Account.Helpers
{
    public class UserRepository
    {
        public User Get(string authToken)
        {
            var sql = $"select user_id, UNIX_TIMESTAMP(auth_expire) as auth_expire from user where auth_token = '{authToken}'";

            return DatabaseHelper.Read(sql, MapToUser);
        }

        private User MapToUser(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            int count = reader.FieldCount;
            if (count != 2)
            {
                throw new Exception("Cannot authenticate using the token");
            }

            reader.Read();

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds((long)reader.GetValue(1));

            return new User
            {
                UserId = (int)reader.GetValue(0),
                TokenExpiration = dateTime,
            };
        }
    }
}
