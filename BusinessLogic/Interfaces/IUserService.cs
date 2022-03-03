using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Delete(int id);
    }
}
