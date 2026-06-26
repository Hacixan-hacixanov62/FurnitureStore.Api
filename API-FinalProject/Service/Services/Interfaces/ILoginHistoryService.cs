using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTO.Admin.LoginHistory;

namespace Service.Services.Interfaces
{
    public interface ILoginHistoryService
    {
        Task CreateAsync(LoginHistoryCreateDto dto);
        Task<List<LoginHistoryListDto>> GetAllAsync();
        Task<List<LoginHistoryListDto>> GetByUserIdAsync(string userId);
    }
}
