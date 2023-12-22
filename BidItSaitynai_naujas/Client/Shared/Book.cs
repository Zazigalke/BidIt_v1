using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidItSaitynai_naujas.Shared
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Condition { get; set; }
        public int PageCount { get; set; }
        public double StartingPrice { get; set; }
    }
    public class BookRequest
    {

        public string Title { get; set; }
        public int Condition { get; set; }
        public int PageCount { get; set; }
        public double StartingPrice { get; set; }
        public int User { get; set; }
        public void Clear()
        {
            Title = null;
            Condition = 0;
            PageCount = 0;
            StartingPrice = 0;
        }

    }
    public class BookEdit
    {

        public string Title { get; set; }
        public int Condition { get; set; }
        public int PageCount { get; set; }
        public double StartingPrice { get; set; }
        public int User { get; set; }
        public void Clear()
        {
            Title = null;
            Condition = 0;
            PageCount = 0;
            StartingPrice = 0;
        }

    }
}
