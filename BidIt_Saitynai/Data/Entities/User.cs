using FluentValidation;
using static BidIt_Saitynai.Data.Entities.User;

namespace BidIt_Saitynai.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
    
}
