using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.AboutBannerImg;
using Service.DTO.Admin.Instagram;

namespace Service.Services.Interfaces
{
    public interface IAboutBannerImgService
    {
        Task CreateAsync(AboutBannerImgCreateDto model);
        Task EditAsync(AboutBannerImgEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<AboutBannerImgDto>> GetAllAsync();
        Task<AboutBannerImgDto> GetByIdAsync(int id);
    }
}
