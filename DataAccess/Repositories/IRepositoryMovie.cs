using DataAccess.Helpers;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepositoryMovie : IRepository<Movie>
    {
        Task<Movie> GetByIdWithDetailsAsync(int id);
        Task<Movie?> GetByImdbIdWithDetailsAsync(string imdbId);
        Task<PagedList<Movie>> GetAllWithDetailsAsync(MovieParameters movieParameters);
        Task<PagedList<Movie>> GetWishListAsync(MovieParameters movieParameters, int accountId);
        Task<PagedList<Movie>> GetWatchedListAsync(MovieParameters movieParameters, int accountId);
    }
}
