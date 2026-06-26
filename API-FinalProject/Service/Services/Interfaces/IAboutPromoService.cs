using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.AboutPromo;
using Service.DTO.Admin.Sliders;

namespace Service.Services.Interfaces
{
    public interface IAboutPromoService
    {
        Task CreateAsync(AboutPromoCreateDto model);
        Task EditAsync(AboutPromoEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<AboutPromoDto>> GetAllAsync();
        Task<AboutPromoDto> GetByIdAsync(int id);
    }
}
