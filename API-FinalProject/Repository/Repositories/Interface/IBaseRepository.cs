using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Repository.Repositories.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithExpressionAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllForPagination(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> FindAllWithIncludes();
    }
}
