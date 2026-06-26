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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public IQueryable<Category> GetAllWithExpression(Expression<Func<Category, bool>> predicate)
        {
            predicate ??= x => true;
            return _context.Categories.Include(m => m.Products).Where(predicate);
        }

        public async Task<Dictionary<string, int>> GetCategoryProductCountsAsync()
        {
            return await _context.Categories
                .Select(c => new { c.Name, ProductCount = c.Products.Count })
                .ToDictionaryAsync(c => c.Name, c => c.ProductCount);
        }
    }
}
