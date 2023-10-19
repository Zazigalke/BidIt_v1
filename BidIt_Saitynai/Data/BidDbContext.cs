using BidIt_Saitynai.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BidIt_Saitynai.Data
{
    public class BidDbContext : DbContext
    {
   
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Auction> Auctions { get; set; }

       

        public BidDbContext(DbContextOptions<BidDbContext> options) : base(options)
        {
               
        }

    }
}
