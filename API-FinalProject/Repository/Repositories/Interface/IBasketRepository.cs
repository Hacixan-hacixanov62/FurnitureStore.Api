using Domain.Entities;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Task AddAsync(Basket basket);
        void Delete(BasketProduct basketProduct);
        Task<Basket> GetByUserIdAsync(string userId);
        Task DeleteProductByUserIdAsync(string userId);
        Task SaveChangesAsync();
    }
}
