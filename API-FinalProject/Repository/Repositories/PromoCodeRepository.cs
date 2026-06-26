using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class PromoCodeRepository : BaseRepository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(AppDbContext context) : base(context) { }
        public async Task AddAsync(PromoCode promoCode)
        {
            await _context.PromoCodes.AddAsync(promoCode);
            await _context.SaveChangesAsync();
        }

        public async Task<PromoCode?> GetByCodeAsync(string code)
        {
            return await _context.PromoCodes.FirstOrDefaultAsync(p => p.Code == code && p.IsActive);
        }

        public async Task UpdateAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Update(promoCode);
            await _context.SaveChangesAsync();
        }

        //Join ile et
        public async Task<List<AppUser>> GetAllMembersAsync()
        {
            return await (from user in _context.Users
                          join userRole in _context.UserRoles on user.Id equals userRole.UserId
                          join role in _context.Roles on userRole.RoleId equals role.Id
                          where role.Name == "Member"
                          select user).ToListAsync();
        }

        public async Task<List<PromoCode>> GetAllAsync()
        {
            return await _context.PromoCodes.ToListAsync();
        }

        public async Task DeleteAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Remove(promoCode);
            await _context.SaveChangesAsync();
        }

    }
}
