using Microsoft.AspNetCore.Http;
using System;

namespace Account.Helpers
{
    public static class AuthenticationService
    {
        public static int GetUserId(HttpRequest req)
        {
            string authToken = req.Headers["auth_token"];
            if (authToken == null)
            {
                throw new UnauthorizedAccessException("Missing authentication token");
            }

            var userService = new UserService();
            var userId = userService.GetUserIdFromAuthToken(authToken);
            return userId;
        }
    }
}
