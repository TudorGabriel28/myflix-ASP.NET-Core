using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}