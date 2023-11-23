using BidIt_Saitynai.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace BidIt_Saitynai.Data.Entities
{
    public class Company
    {
         public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public required string UserId { get; set; }
        public ForumRestUser AuthUser { get; set; }

    }
}
