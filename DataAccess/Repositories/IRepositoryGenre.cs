using DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepositoryGenre : IRepository<Genre>
    {
        Task<Genre?> GetByNameAsync(string imdbId);
       
    }
}
