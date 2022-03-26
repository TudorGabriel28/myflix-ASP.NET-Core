using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models.Entities
{
    public class PrimaryImage : BaseEntity
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Url { get; set; }
        public string? Caption { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        [JsonIgnore]
        public int MovieId { get; set; }
    }
}
