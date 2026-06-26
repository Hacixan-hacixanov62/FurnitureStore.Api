

using Microsoft.AspNetCore.Http;
using Service.DTO.Admin.Products;
using Service.DTOs.Admin.Products;
using Service.Helpers;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto model);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
		Task<ProductDetailDto> DetailAsync(int id);
		Task DeleteAsync(int id);
        Task EditAsync(int id, ProductEditDto model);
        Task<IEnumerable<ProductDto>> SearchByCategoryAndName(string categoryOrProductName);
        Task<IEnumerable<ProductDto>> FilterAsync(string categoryName, string colorName, string tagName, string brandName);
        Task<bool> DeleteImageAsync(int productId, int productImageId);
        Task<PaginationResponse<ProductDto>> GetPaginateAsync(int page, int take);
        Task<IEnumerable<ProductDto>> GetAllTakenAsync(int take, int? skip = null);
        Task<int> GetProductsCountAsync();
        Task<IEnumerable<ProductDto>> GetSortedProductsAsync(string sortType);
        Task<IEnumerable<ProductDto>> GetComparisonProductsAsync(int categoryId ,int selectedProductId, int count = 3);
        Task<ProductWithImagesDto> GetByIdWithImagesAsync(int id);
        Task<List<ProductDto>> FilterByPriceAsync(ProductFilterDto filterDto);
        Task<IEnumerable<ProductDto>> SearchByName(string name);
        Task<decimal> GetMaxPriceAsync();

    }
}