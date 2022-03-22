using DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IWatchedListService
    {
        Task<IEnumerable<Movie>> Get(int AccountId);
        Task<bool> Add(int AccountId, int MovieId);
        Task<bool> Remove(int AccountId, int MovieId);
    }
}
