using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class LoginAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public void Clear() => Username = Password = null;
    }
    
    public class LoginResult
    {
        public string accessToken { get; set; }
		public string refreshToken { get; set; }
	}
}
