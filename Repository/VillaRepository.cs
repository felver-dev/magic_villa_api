using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using villaApi.Models;
using villaApi.Repository.IRepository;
using villaApi.Services;

namespace villaApi.Repository
{
	public class VillaRepository : IVillaRepository
	{
		public readonly VillaDBContext Context;
		public VillaRepository(VillaDBContext context)
		{
			Context = context;
		}
		public async Task CreateAsync(Villa entity)
		{
			await Context.Villas.AddAsync(entity);
			await SaveAsync();
		}

		public async  Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
		{
			IQueryable<Villa> query = Context.Villas;
			
			if (!tracked)
			{
				query = query.AsNoTracking();
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
		{
			IQueryable<Villa> query = Context.Villas;
			
			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.ToListAsync();
		}

		public async Task RemoveAsync(Villa entity)
		{
			Context.Villas.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await Context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Villa entity)
		{
			Context.Villas.Update(entity);
			await SaveAsync();
		}
	}
}