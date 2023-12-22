using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Clear() => Name = null;
    }
    public class CompanyCreate
    {
        public string Name { get; set; }
        public void Clear() => Name = null;
    }
    public class CompanyEdit
    {
        public string Name { get; set; }
        public void Clear() => Name = null;
    }
}
