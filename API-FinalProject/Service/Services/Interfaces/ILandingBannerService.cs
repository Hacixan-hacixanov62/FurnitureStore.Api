using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.Category;
using Service.DTO.Admin.LandingBanner;
using Service.DTO.Admin.Sliders;
using Service.Helpers;

namespace Service.Services.Interfaces
{
    public interface ILandingBannerService
    {
        Task CreateAsync(LandingBannerCreateDto model);
        Task EditAsync(LandingBannerEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<LandingBannerDto>> GetAllAsync();
        Task<LandingBannerDto> GetByIdAsync(int id);
        Task<PaginationResponse<LandingBannerDto>> GetPaginateAsync(int page, int take);
    }
}
