using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<IEnumerable<Review>> GetAllByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task<Review> GetByIdAsync(int id);
    }
}
