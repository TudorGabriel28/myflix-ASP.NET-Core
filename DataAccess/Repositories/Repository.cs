using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
	{
		protected MyflixContext _context;

		public Repository(MyflixContext context)
		{
			_context = context;
		}
		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _context.Set<TEntity>().ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public TEntity GetById(int id)
		{
			var entity = _context.Set<TEntity>().Find(id);
			return entity;

		}

		public async Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false)
		{
			var entity = await _context.Set<TEntity>().FindAsync(id);
			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
			return entity;

		}
	}
}
