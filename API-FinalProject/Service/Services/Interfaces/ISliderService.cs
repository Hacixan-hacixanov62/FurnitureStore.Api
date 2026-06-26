using Service.DTO.Admin.Category;
using Service.DTO.Admin.Sliders;
using Service.Helpers;

namespace Service.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto model);
        Task EditAsync(SliderEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<SliderDto>> GetAllAsync();
        Task<SliderDto> GetByIdAsync(int id);
        Task<PaginationResponse<SliderDto>> GetPaginateAsync(int page, int take);
    }
}
