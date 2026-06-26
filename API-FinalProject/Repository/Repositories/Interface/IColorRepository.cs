using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IColorRepository : IBaseRepository<Color>
    {
        Task<bool> GetWithIncludes(Expression<Func<Color, bool>> predicate = null);
    }
}
