using DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepositoryMovie : IRepository<Movie>
    {
        Task<Movie?> GetByImdbIdAsync(string imdbId);
        new Task<IEnumerable<Movie>> GetAllAsync();
        new Task<Movie> GetByIdAsync(int id, bool asNoTracking = false);

    }
}
