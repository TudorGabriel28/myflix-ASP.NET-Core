using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Models.Movies;
using DataAccess.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        Task<PagedList<Movie>> GetAll(MovieParameters movieParameters);
        Task<MovieResponse> GetById(int movieId, int accountId);
        Task<Movie> Create(CreateMovieRequest movieRequest, int accountId);
        Task<bool> Delete(int id);
    }
}
