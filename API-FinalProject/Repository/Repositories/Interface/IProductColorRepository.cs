using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories.Interfaces
{
    public interface IProductColorRepository : IBaseRepository<ProductColor>
    {
		Task DeleteProductColor(ProductColor color);
	}
}
