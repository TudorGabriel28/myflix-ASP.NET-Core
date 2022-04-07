using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Movies
{
    public class CreateMovieRequest
    {
        [Required]
        public string ImdbId { get; set; }
    }
}