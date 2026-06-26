using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.Color;
using Service.DTO.Admin.Tag;

namespace Service.Services.Interfaces
{
    public interface IColorService
    {
        Task CreateAsync(ColorCreateDto model);
        Task EditAsync(ColorEditDto model, int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<ColorDto>> GetAllAsync();
        Task<ColorDto> GetByIdAsync(int id);
    }
}
