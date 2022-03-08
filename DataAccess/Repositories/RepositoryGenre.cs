using DataAccess.Models;
using DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryGenre : Repository<Genre>, IRepositoryGenre
    {
        public RepositoryGenre(MyflixContext context) : base(context) { }

        public async Task<Genre> GetByNameAsync(string name)
        {
            return await _context.Genres.SingleOrDefaultAsync(x => x.Name == name);
        }
    
    }
}
