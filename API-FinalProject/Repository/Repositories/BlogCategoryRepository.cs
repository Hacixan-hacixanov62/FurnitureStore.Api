using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(AppDbContext context) : base(context) { }

        public async Task<Dictionary<string, int>> GetCategoryPostCountsAsync()
        {
            return await _context.BlogCategories
                .Select(c => new { c.Name, PostCount = c.BlogPosts.Count })
                .ToDictionaryAsync(c => c.Name, c => c.PostCount);
        }
    }
}
