using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IBlogReviewRepository : IBaseRepository<BlogReview>
    {
        Task<IEnumerable<BlogReview>> GetAllAsync();
        Task<IEnumerable<BlogReview>> GetAllByPostAsync(int postId);
        Task<IEnumerable<BlogReview>> GetByPostIdAsync(int postId);
        Task<BlogReview> GetByIdAsync(int id);
    }
}
