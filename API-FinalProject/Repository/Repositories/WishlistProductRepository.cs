using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class WishlistProductRepository : BaseRepository<WishlistProduct>, IWishlistProductRepository
	{
        public WishlistProductRepository(AppDbContext context) : base(context) { }       
    }
}
