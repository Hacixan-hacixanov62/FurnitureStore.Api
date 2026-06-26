using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class AboutPromoRepository : BaseRepository<AboutPromo>, IAboutPromoRepository
    {
        public AboutPromoRepository(AppDbContext context) : base(context) { }
    }
}
