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
    public class BlogReviewRepository : BaseRepository<BlogReview>, IBlogReviewRepository
    {
        public BlogReviewRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<BlogReview>> GetAllAsync()
        {
            return await _context.BlogReviews
                .Include(r => r.BlogPost)
                .Include(r => r.AppUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogReview>> GetByPostIdAsync(int postId)
        {
            return await _context.BlogReviews
                .Where(r => r.BlogPostId == postId)
                .Include(r => r.AppUser)
                .ToListAsync();
        }

        public async Task<BlogReview> GetByIdAsync(int id)
        {
            return await _context.BlogReviews
                .Include(r => r.AppUser)
                .Include(x => x.BlogPost)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<BlogReview>> GetAllByPostAsync(int postId)
        {
            return await _context.BlogReviews.Include(r => r.AppUser)
                .Include(x => x.BlogPost)
           .Where(r => r.BlogPostId == postId)
           .OrderByDescending(r => r.CreatedDate)
           .ToListAsync();
        }
    }
}
