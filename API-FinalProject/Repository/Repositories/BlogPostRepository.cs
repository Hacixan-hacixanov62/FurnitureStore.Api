using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(AppDbContext context) : base(context) { }

        public IQueryable<BlogPost> ApplyIncludes()
        {
            return _context.BlogPosts
                           .Include(x => x.Images) 
                           .Include(x => x.BlogCategory); 
        }
        public async Task<BlogPost> GetByIdWithIncludesAsync(int id)
        {
            return await _context.BlogPosts.Include(x => x.Images).Include(x => x.BlogCategory)
                                           .FirstOrDefaultAsync(x => x.Id == id); 
        }

        public IQueryable<BlogPost> GetAllWithExpression(Expression<Func<BlogPost, bool>> predicate)
        {
            predicate ??= x => true;
            return _context.BlogPosts
             .Include(x=>x.BlogCategory)
             .Include(x=>x.Images)
             .Where(predicate);
        }

        public async Task<IEnumerable<BlogPost>> FilterAsync(string? categoryName)
        {
            var query = ApplyIncludes();

            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p => p.BlogCategory.Name.ToLower() == categoryName.ToLower());
            }
            return await query.ToListAsync();
        }
        public async Task<BlogPost> GetPreviousAsync(int currentId)
        {
            return await _context.BlogPosts.Include(x=>x.BlogCategory)
                .Include(x=>x.Images)
                .Where(b => b.Id < currentId)
                .OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<BlogPost> GetNextAsync(int currentId)
        {
            return await _context.BlogPosts.Include(x => x.BlogCategory)
                .Include(x => x.Images)
                .Where(b => b.Id > currentId)
                .OrderBy(b => b.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllWithIncludeAsync()
        {
                return await _context.BlogPosts.Include(x => x.BlogCategory)
                                               .Include(x => x.Images).ToListAsync();
        }
    }
}
