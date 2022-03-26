using DataAccess.Helpers;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IWatchedListService
    {
        Task<PagedList<Movie>> Get(MovieParameters movieParameters, int AccountId);
        Task<bool> Add(int AccountId, int MovieId);
        Task<bool> Remove(int AccountId, int MovieId);
    }
}
