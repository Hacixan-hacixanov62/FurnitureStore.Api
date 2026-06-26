using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IPromoCodeRepository : IBaseRepository<PromoCode>
    {
        Task AddAsync(PromoCode promoCode);
        Task<PromoCode?> GetByCodeAsync(string code);
        Task UpdateAsync(PromoCode promoCode);
        Task<List<AppUser>> GetAllMembersAsync();
        Task<List<PromoCode>> GetAllAsync();
        Task DeleteAsync(PromoCode promoCode);
    }
}
