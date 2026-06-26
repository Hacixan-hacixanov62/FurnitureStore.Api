using Service.DTO.Admin.PromoCode;

namespace Service.Services.Interfaces
{
    public interface IPromoCodeService
    {
        Task CreateAsync(PromoCodeCreateDto dto);
        Task<PromoCodeResultDto?> GetByCodeAsync(string code);
        Task<PromoCodeCheckResultDto> CheckAndApplyAsync(string code);
        Task<bool> IncrementUsageCountAsync(string code);
        Task<List<PromoCodeDto>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
