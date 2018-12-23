using Microsoft.AspNetCore.Mvc;

namespace Account.Accounts
{
    public class AccountController
    {
        public ObjectResult Get(int userId)
        {
            var accountRepository = new AccountRepository();
            var accounts = accountRepository.GetAccounts(userId);

            return new OkObjectResult(accounts);
        }
    }
}
