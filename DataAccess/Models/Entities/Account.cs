

using DataAccess.Models;
using DataAccess.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Account : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname  { get; set; }
        public bool AcceptTerms { get; set; }
        public Role Role { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
        [InverseProperty("WatchedListAccount")]
        public virtual ICollection<Movie>? WatchedList { get; set; }
        [InverseProperty("WishListAccount")]
        public virtual ICollection<Movie>? WishList { get; set; }


        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }

    }
}
