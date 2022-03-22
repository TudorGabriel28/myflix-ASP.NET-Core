﻿using AutoMapper;
using BusinessLogic.Interfaces;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class WishListService :IWishListService
    {
        private readonly IRepositoryAccount _repositoryAccount;
        private readonly IRepositoryMovie _repositoryMovie;
        public WishListService(IRepositoryAccount repositoryAccount, IRepositoryMovie repositoryMovie)
        {
            _repositoryAccount = repositoryAccount;
            _repositoryMovie = repositoryMovie;
        }

        public async Task<IEnumerable<Movie>> Get(int AccountId)
        {
            var account = await _repositoryAccount.GetByIdAsync(AccountId);
            if(account == null)
            {
                throw new AppException("Account does not exist");
            }
            return account.WishList;
        }

        public async Task<bool> Add(int AccountId, int MovieId)
        {
            var movie = await _repositoryMovie.GetByIdAsync(MovieId);
            if (movie == null)
            {
                throw new AppException("Movie does not exist anymore");
            }
            var account = await _repositoryAccount.GetByIdAsync(AccountId);
            if(account == null)
            {
                throw new AppException("Account does not exist");
            }
            if (account.WishList.Contains(movie))
            {
                throw new AppException("Movie already in the wishlist");
            }
            account.WishList.Add(movie);
            var result = await _repositoryAccount.UpdateAsync(account);
            if(result == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Remove(int AccountId, int MovieId)
        {
            var movie = await _repositoryMovie.GetByIdAsync(MovieId);
            if (movie == null)
            {
                throw new Exception("Movie does not exist anymore");
            }
            var account = await _repositoryAccount.GetByIdAsync(AccountId);
            if (account == null)
            {
                throw new AppException("Account does not exist");
            }
            if (!account.WishList.Contains(movie))
            {
                throw new Exception("Movie does not exist in wishlist");
            }
            account.WishList.Remove(movie);
            var result = await _repositoryAccount.UpdateAsync(account);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
