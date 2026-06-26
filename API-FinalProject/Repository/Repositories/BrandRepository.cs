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
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public async Task<Dictionary<string, int>> GetBrandProductCountsAsync()
        {
            return await _context.Brands
                .Select(c => new { c.Name, ProductCount = c.Products.Count })
                .ToDictionaryAsync(c => c.Name, c => c.ProductCount);
        }
    }
}
