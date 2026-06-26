using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.Sliders;
using Service.DTO.Admin.SubscribeImg;

namespace Service.Services.Interfaces
{
    public interface ISubscribeImgService
    {
        Task CreateAsync(SubscribeImgCreateDto model);
        Task EditAsync(SubscribeImgEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<SubscribeImgDto>> GetAllAsync();
        Task<SubscribeImgDto> GetByIdAsync(int id);
    }
}
