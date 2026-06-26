using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IWishlistRepository : IBaseRepository<Wishlist>
    {
        Task AddAsync(Wishlist wishlist);
        Task UpdateAsync(Wishlist wishlist);
        Task DeleteAsync(int id);
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<Wishlist> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task RemoveProductFromWishlistAsync(string userId, int productId);
    }
}
