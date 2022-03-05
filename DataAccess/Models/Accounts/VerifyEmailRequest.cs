using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}