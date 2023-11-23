using BidIt_Saitynai.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace BidIt_Saitynai.Data.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public Book Book { get; set; }
        public Auction Auction { get; set; }

        [Required]
        public required string UserId { get; set; }
        public ForumRestUser AuthUser { get; set; }

    }
}
