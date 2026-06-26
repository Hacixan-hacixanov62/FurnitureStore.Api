using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class AboutBannerImgRepository : BaseRepository<AboutBannerImg>, IAboutBannerImgRepository
    {
        public AboutBannerImgRepository(AppDbContext context) : base(context) { }
    }
}
