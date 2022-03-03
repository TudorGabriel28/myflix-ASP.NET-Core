

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Enter a password with minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords must coincide")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nickname  { get; set; }
        public bool? IsActive { get; set; } = false;
        [RegularExpression("viewer|admin")]
        public string? Role { get; set; } = "viewer";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
