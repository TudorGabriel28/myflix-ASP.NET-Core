using DataAccess.Models;
using DataAccess.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryAccount : Repository<Account>, IRepositoryAccount
    {
        public RepositoryAccount(MyflixContext context) : base(context) { } 

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<int> CountAccountsAsync()
        {
            return await _context.Accounts.CountAsync();
        }
    }
}
