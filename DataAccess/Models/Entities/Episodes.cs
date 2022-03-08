using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models.Entities
{
    public class Episodes : BaseEntity
    {
        public int SeasonsNumber { get; set; }
        public int FirstYear { get; set; }
        public int LastYear { get; set; }
        public int TotalEpisodes { get; set; }
        public Movie Movie { get; set; }
        [JsonIgnore]
        public int MovieId { get; set; }
    }
}
