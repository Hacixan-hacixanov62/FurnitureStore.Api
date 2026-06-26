using Service.DTO.Admin.Category;
using Service.DTO.Admin.HelpSection;

namespace Service.Services.Interfaces
{
    public interface IHelpSectionService
    {
        Task CreateAsync(HelpSectionCreateDto model);
        Task EditAsync(HelpSectionEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<HelpSectionDto>> GetAllAsync();
        Task<HelpSectionDto> GetByIdAsync(int id);
    }
}
