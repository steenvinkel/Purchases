using System;

namespace Account.Models
{
    public class User
    {
        public int UserId { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
