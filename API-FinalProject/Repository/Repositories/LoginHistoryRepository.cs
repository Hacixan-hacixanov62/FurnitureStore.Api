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
    public class LoginHistoryRepository : BaseRepository<LoginHistory>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(LoginHistory entity)
        {
            await _context.LoginHistories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LoginHistory>> GetAllAsync()
        {
            return await _context.LoginHistories
                .OrderByDescending(x => x.LoginTime)
                .ToListAsync();
        }

        public async Task<List<LoginHistory>> GetByUserIdAsync(string userId)
        {
            return await _context.LoginHistories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.LoginTime)
                .ToListAsync();
        }
    }
}
