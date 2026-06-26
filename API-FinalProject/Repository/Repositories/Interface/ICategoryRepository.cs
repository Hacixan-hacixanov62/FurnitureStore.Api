
using System.Linq.Expressions;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IQueryable<Category> GetAllWithExpression(Expression<Func<Category, bool>> predicate);
        Task<Dictionary<string, int>> GetCategoryProductCountsAsync();
    }
}
