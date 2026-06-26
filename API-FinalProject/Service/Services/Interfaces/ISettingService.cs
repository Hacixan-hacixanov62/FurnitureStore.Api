using Service.DTOs.Admin.Settings;

namespace Service.Services.Interfaces
{
    public interface ISettingService
    {
        //Task CreateAsync(SettingCreateDto model);
        Task EditAsync(int id, SettingEditDto model);
        //Task DeleteAsync(int id);
        Task<IEnumerable<SettingDto>> GetAllAsync();
        Task<SettingDto> GetByIdAsync(int id);
    }
}
