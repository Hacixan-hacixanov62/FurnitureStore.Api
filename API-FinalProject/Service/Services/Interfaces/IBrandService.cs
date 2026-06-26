using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.Brand;
using Service.DTO.Admin.Category;

namespace Service.Services.Interfaces
{
    public interface IBrandService
    {
        Task CreateAsync(BrandCreateDto model);
        Task EditAsync(BrandEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIdAsync(int id);
        Task<Dictionary<string, int>> GetBrandProductCountsAsync();
    }
}
