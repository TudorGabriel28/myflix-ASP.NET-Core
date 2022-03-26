﻿using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Accounts;
using DataAccess.Models.Parameters;
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

        public async Task<PagedList<Account>> GetAllAsync(AccountParameters accountParameters)
        {
            return await PagedList<Account>.ToPagedListAsync(_context.Accounts, accountParameters.PageNumber, accountParameters.PageSize);
        }

        public async Task<Account> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Accounts
                .Include(x => x.WishList)
                    .ThenInclude(x => x.PrimaryImage)
                .Include(x => x.WatchedList)
                    .ThenInclude(x => x.PrimaryImage)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetByEmailWithDetailsAsync(string email)
        {
            return await _context.Accounts
                .Include(x => x.WishList)
                .Include(x => x.WatchedList)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<int> CountAccountsAsync()
        {
            return await _context.Accounts.CountAsync();
        }

        public async Task<Account> GetByVerificationTokenAsync(string verificationToken)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.VerificationToken == verificationToken);
        }

        public async Task<Account> GetByValidResetTokenAsync(string resetToken)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x =>
                x.ResetToken == resetToken &&
                x.ResetTokenExpires > DateTime.UtcNow);
        }

        public async Task<Account> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Accounts.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        }

    }
}
