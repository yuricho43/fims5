using System.ComponentModel.DataAnnotations;

using static Fims5.Data.ModelConstants.Identity;


namespace Fims5.Data.Identity
{
    public class LoginRequestModel
    {
        [Required]
        [MinLength(MinUserNameLength)]
        [MaxLength(MaxUserNameLength)]
        public string UserName { get; set; } //login name

        // [Required]
        // [EmailAddress]
        // [MinLength(MinEmailLength)]
        // [MaxLength(MaxEmailLength)]
        // public string Email { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
