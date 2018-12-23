using Account.DatabaseAccess;
using System.Collections.Generic;

namespace Account.Accounts
{
    public class AccountRepository
    {
        public List<Account> GetAccounts(int userId)
        {
            var sql = $"SELECT * FROM account WHERE user_id = {userId}";
            return DatabaseHelper.Read(sql, (reader) =>
            {
                var accounts = new List<Account>();
                while (reader.Read())
                {
                    var account = new Account
                    {
                        AccountId = (int)reader.GetValue(0),
                        UserId = (int)reader.GetValue(1),
                        Name = (string)reader.GetValue(2),
                        AccumulatedCategoryId = (int)reader.GetValue(3),
                    };
                    accounts.Add(account);
                }
                return accounts;
            });
        }
    }
}
