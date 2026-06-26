using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class SubscribeImgRepository : BaseRepository<SubscribeImg>, ISubscribeImgRepository
    {
        public SubscribeImgRepository(AppDbContext context) : base(context) { }
    }
}
