using Microsoft.AspNetCore.Identity;

namespace BidIt_Saitynai.Auth.Model
{
    public class ForumRestUser : IdentityUser
    {
        public bool ForceRelogin { get; set; }

    }
}
