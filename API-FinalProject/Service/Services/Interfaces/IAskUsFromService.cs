using Service.DTO.Admin.AskUsFrom;

namespace Service.Services.Interfaces
{
    public interface IAskUsFromService
    {
        Task CreateAsync(AskUsFromCreateDto model);
        Task DeleteAsync(int id);
        Task<IEnumerable<AskUsFromDto>> GetAllAsync();
        Task<AskUsFromDto> GetByIdAsync(int id);
        Task<IEnumerable<AskUsFromDto>> GetApprovedMessagesAsync();
        Task ApproveMessageAsync(int id);
    }
}
