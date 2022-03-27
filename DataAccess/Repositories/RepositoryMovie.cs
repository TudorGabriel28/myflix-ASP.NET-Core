using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Models.Entities;
using DataAccess.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RepositoryMovie : Repository<Movie>, IRepositoryMovie
	{
		public RepositoryMovie(MyflixContext context) : base(context) { }

		public async Task<PagedList<Movie>> GetAllWithDetailsAsync(MovieParameters movieParameters)
		{
			var movies = _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.OrderBy(m => m.Title)
				.AsNoTracking();
			if(!string.IsNullOrEmpty(movieParameters.Genre))
            {
				movies = movies.Where(m => m.Genres.Any(g => g.Name == movieParameters.Genre));
			}

			if (!string.IsNullOrWhiteSpace(movieParameters.Title))
            {
				movies = movies.Where(m => m.Title.ToLower().Contains(movieParameters.Title.Trim().ToLower()));
			}

			return await PagedList<Movie>.ToPagedListAsync(movies, movieParameters.PageNumber, movieParameters.PageSize);
		}

		public async Task<Movie?> GetByImdbIdWithDetailsAsync(string imdbId)
        {
			var movie = await _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.SingleOrDefaultAsync(x => x.ImdbId == imdbId);
			return movie;
		}

		public async Task<Movie> GetByIdWithDetailsAsync(int id)
		{
			var movie = await _context.Movies.Where(m => m.Id == id)
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.SingleOrDefaultAsync();
			return movie;
		}

		public async Task<PagedList<Movie>> GetWishListAsync(MovieParameters movieParameters, int accountId)
		{
			var wishlist = _context.Movies.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.Where(m => m.WishListAccount.Any(a => a.Id == accountId));

			return await PagedList<Movie>.ToPagedListAsync(wishlist, movieParameters.PageNumber, movieParameters.PageSize);
		}

		public async Task<PagedList<Movie>> GetWatchedListAsync(MovieParameters movieParameters, int accountId)
		{
			var watchedlist = _context.Movies
				.Include(m => m.Episodes)
				.Include(m => m.Genres)
				.Include(m => m.PrimaryImage)
				.Include(m => m.MeterRanking)
				.Where(m => m.WatchedListAccount.Any(a => a.Id == accountId));

			return await PagedList<Movie>.ToPagedListAsync(watchedlist, movieParameters.PageNumber, movieParameters.PageSize);
		}

	}
}
