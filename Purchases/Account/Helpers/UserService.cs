using System;

namespace Account.Helpers
{
    public class UserService
    {
        public int GetUserIdFromAuthToken(string authToken)
        {
            var userRepository = new UserRepository();

            var user = userRepository.Get(authToken);

            if (user.TokenExpiration < DateTime.Now)
            {
                throw new Exception("Token has expired");
            }

            return user.UserId;
        }
    }
}
