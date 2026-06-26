using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class LandingBannerRepository : BaseRepository<LandingBanner>, ILandingBannerRepository
    {
        public LandingBannerRepository(AppDbContext context) : base(context) { }
    }
}
