using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ProductColorRepository : BaseRepository<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(AppDbContext context) : base(context) { }

		public async Task DeleteProductColor(ProductColor color)
		{
			_context.ProductColors.Remove(color);
			
		}
	}
}
