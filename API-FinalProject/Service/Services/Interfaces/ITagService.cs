using Service.DTO.Admin.Category;
using Service.DTO.Admin.Tag;
using Service.Helpers;

namespace Service.Services.Interfaces
{
    public interface ITagService
    {
        Task CreateAsync(TagCreateDto model);
        Task EditAsync(TagEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto> GetByIdAsync(int id);
        Task<PaginationResponse<TagDto>> GetPaginateAsync(int page, int take);
    }
}
