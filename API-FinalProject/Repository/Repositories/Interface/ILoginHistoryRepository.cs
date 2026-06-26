using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface ILoginHistoryRepository : IBaseRepository<LoginHistory>
    {
        Task AddAsync(LoginHistory entity);
        Task<List<LoginHistory>> GetAllAsync();
        Task<List<LoginHistory>> GetByUserIdAsync(string userId);
    }
}