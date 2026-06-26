using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.BlogCategory;

namespace Service.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task CreateAsync(BlogCategoryCreateDto model);
        Task EditAsync(BlogCategoryEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<BlogCategoryDto>> GetAllAsync();
        Task<BlogCategoryDto> GetByIdAsync(int id);

        Task<Dictionary<string, int>> GetCategoryPostCountsAsync();
    }
}
