using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Movie> Movies { get; set; }
    }
}
