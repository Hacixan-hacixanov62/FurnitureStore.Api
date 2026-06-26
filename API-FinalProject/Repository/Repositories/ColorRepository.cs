using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }

        public async Task<bool> GetWithIncludes(Expression<Func<Color, bool>> predicate = null)
        {
            return predicate == null ? false : await _context.Colors.Include(c => c.ProductColors)
                                                                    .AnyAsync(predicate);
        }
    }
}
