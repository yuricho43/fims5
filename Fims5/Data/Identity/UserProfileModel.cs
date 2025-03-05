using System.ComponentModel.DataAnnotations;

using static Fims5.Data.ErrorMessages;
using static Fims5.Data.ModelConstants.Common;


namespace Fims5.Data.Identity
{
    public class UserProfileModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string HangulName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
    }
}