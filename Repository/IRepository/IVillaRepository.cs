using System.Linq.Expressions;
using villaApi.Models;

namespace villaApi.Repository.IRepository
{
	public interface IVillaRepository
	{
		Task <List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null!);
		Task <Villa> GetAsync(Expression<Func<Villa, bool>> filter = null!, bool tracked = true);
		Task CreateAsync(Villa entity);
		Task RemoveAsync(Villa entity);
		Task UpdateAsync(Villa entity);
		Task SaveAsync();
		
	}
}