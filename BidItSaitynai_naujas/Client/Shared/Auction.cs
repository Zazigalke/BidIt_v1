using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string Company { get; set; }
        
    }
    public class AuctionCreate
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public int? Company { get; set; }
        public void Clear() => Name = City = null;
    }
    public class AuctionEdit
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public int? Company { get; set; }
        public void Clear() => Name = City = null;
    }
}
