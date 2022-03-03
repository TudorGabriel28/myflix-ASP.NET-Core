using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
	{
		IEnumerable<TEntity> GetAll();

		Task<IEnumerable<TEntity>> GetAllAsync();

		TEntity GetById(int id);

		Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false);

		Task<TEntity> AddAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task<bool> DeleteAsync(int id);
	}
}
