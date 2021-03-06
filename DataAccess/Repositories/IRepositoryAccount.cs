using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Accounts;
using DataAccess.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepositoryAccount : IRepository<Account>
    {
        Task<PagedList<Account>> GetAllAsync(AccountParameters accountParameters);
        Task<Account> GetByEmailAsync(string email);
        Task<Account> GetByIdWithMovieListsAsync(int acccountId);
        Task<int> CountAccountsAsync();
        Task<Account> GetByVerificationTokenAsync(string verificationToken);
        Task<Account> GetByValidResetTokenAsync(string resetToken);
        Task<Account> GetByRefreshTokenAsync(string refreshToken);
    }
}
