using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class Password
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public void Clear() => CurrentPassword = NewPassword = null;
    }
}
