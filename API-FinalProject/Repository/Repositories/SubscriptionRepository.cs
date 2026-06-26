using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context) { }

        public async Task<bool> RemoveAsync(string email)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Email == email);
            if (subscription == null) return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Subscription> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var normalizedEmail = email.Trim().ToLower();

            return await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Email.Trim().ToLower() == normalizedEmail);
        }
    }
}
