using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DataAccess.Models.Accounts
{
    public class CreateRequest
    {

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nickname { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Enter a password with minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords must coincide")]
        public string ConfirmPassword { get; set; }
    }
}