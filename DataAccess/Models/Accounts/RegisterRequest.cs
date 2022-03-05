using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Accounts
{
    public class RegisterRequest
    {

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength (255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Enter a password with minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true")]
        public bool AcceptTerms { get; set; }
    }
}