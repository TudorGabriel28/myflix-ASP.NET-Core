using DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MovieResponse
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public bool IsSeries { get; set; }
        public bool IsEpisode { get; set; }
        public float AggregateRating { get; set; }
        public int VoteCount { get; set; }
        public Episodes? Episodes { get; set; }
        public PrimaryImage PrimaryImage { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RuntimeSeconds { get; set; }
        public MeterRanking MeterRanking { get; set; }
        public string Plot { get; set; }
        public int UploadedById { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool InWishList { get; set; } = false;
        public bool InWatchedList { get; set; } = false;

    }
}
