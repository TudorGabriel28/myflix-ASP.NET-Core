using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models.Entities
{
    public class MeterRanking : BaseEntity
    {
        public int CurrentRank { get; set; }
        public string ChangeDirection { get; set; }
        public int Difference { get; set; }
        public Movie Movie { get; set; }
        [JsonIgnore]
        public int MovieId { get; set; }
    }
}
