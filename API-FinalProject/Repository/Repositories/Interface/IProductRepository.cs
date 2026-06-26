

using Domain.Entities;
using Repository.Repositories.Interface;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IQueryable<Product> GetAllWithIncludes();
        IQueryable<Product> GetAllWithExpression(Expression<Func<Product, bool>> predicate);
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task<Product> GetByIdImagesWithIncludesAsync(int id);
        Task<IEnumerable<Product>> FilterAsync(string categoryName, string colorName, string tagName, string brandName);
        Task DeleteProductImage(ProductImage image);
        Task<IEnumerable<Product>> GetAllTakenAsync(int take, int? skip = null);
        Task<int> GetProductsCount();
        Task<Product> GetProductWithColorsAsync(int productId);
        Task<IEnumerable<Product>> SortedProductsAsync(string sortType);
        IEnumerable<Product> GetComparisonProducts(int categoryId, int selectedProductId, int count = 3);
        Task<ProductImage> GetByIdAsync(int id);
        void Delete(ProductImage image);
        Task SaveChangesAsync();
        Task<decimal> GetMaxPriceAsync();
        Task<List<Product>> FilterByPriceAsync(decimal? minPrice, decimal? maxPrice);
    }
}
