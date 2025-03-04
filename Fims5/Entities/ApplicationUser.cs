using Microsoft.AspNetCore.Identity;

namespace Fims5.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? HangulName { get; set; }

        public string? EnglishName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }

}
