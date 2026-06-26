using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Task<Dictionary<string, int>> GetBrandProductCountsAsync();
    }
}
