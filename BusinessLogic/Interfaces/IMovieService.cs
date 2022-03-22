using DataAccess.Models;
using DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<MovieResponse> GetById(int id, Account account);
        Task<Movie> Create(string imdbMovieId, int accountId);
        Task<bool> Delete(int id);
    }
}
