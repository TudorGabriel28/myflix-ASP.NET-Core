using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Parameters
{
    public class MovieParameters : QueryStringParameters
    {
        public string? Genre { get; set; }
        public string? Title { get; set; }
    }
}
