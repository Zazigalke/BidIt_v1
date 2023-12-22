using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class RegisterAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Clear() => Email = Password = UserName = FirstName = LastName = null;
    }
    public class RegisterResult
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
