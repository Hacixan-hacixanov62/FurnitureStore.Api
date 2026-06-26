using Service.DTO.Admin.Instagram;
using Service.DTO.Admin.SubscribeImg;

namespace Service.Services.Interfaces
{
    public interface IInstagramService
    {
        Task CreateAsync(InstagramCreateDto model);
        Task EditAsync(InstagramEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<InstagramDto>> GetAllAsync();
        Task<InstagramDto> GetByIdAsync(int id);
    }
}
