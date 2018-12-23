using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Account.Helpers;
using Account.LegacyFormatting;

namespace Account.Accounts
{
    public static class AccountFunction
    {
        [FunctionName("AccountFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var userId = AuthenticationService.GetUserId(req);

            var accountController = new AccountController();
            var result = accountController.Get(userId);

            return result.EnableLegacyFormatting(req);
        }
    }
}
