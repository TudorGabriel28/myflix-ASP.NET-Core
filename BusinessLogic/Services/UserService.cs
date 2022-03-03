using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Add(User user)
        {
            if (user == null)
            {
                return null;
            }

            return await _repository.AddAsync(user);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                return null;
            }

            return await _repository.UpdateAsync(user);
        }
    }
}
