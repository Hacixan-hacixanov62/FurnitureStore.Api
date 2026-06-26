

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context) { }
        public async Task AddAsync(Basket basket)
        {
            await _context.Baskets.AddAsync(basket);
        }
        public async Task<Basket> GetByUserIdAsync(string userId)
        {
            return await _context.Baskets
                .Include(b => b.AppUser)
                .Include(b => b.BasketProducts).ThenInclude(c => c.Color)
                .Include(b => b.BasketProducts).ThenInclude(c => c.Product).ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);
        }

        public void Delete(BasketProduct basketProduct)
        {
            _context.BasketProducts.Remove(basketProduct);
        }
        public async Task DeleteProductByUserIdAsync(string userId)
        {
            var data= await _context.Baskets
                .Include(b => b.BasketProducts).ThenInclude(c => c.Color)
                .Include(b => b.BasketProducts).ThenInclude(c => c.Product).ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);
            _context.Baskets.Remove(data);
            await _context.SaveChangesAsync();  
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
