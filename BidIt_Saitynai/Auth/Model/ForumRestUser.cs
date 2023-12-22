using Microsoft.AspNetCore.Identity;

namespace BidIt_Saitynai.Auth.Model
{
    public class ForumRestUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ForceRelogin { get; set; }

    }
}
