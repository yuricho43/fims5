using System.ComponentModel.DataAnnotations;


namespace Fims5.Data.Identity
{
    public class UserAuthInfoModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string HangulName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
    }
}
