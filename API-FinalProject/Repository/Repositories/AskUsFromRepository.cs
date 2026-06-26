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
    public class AskUsFromRepository : BaseRepository<AskUsFrom>, IAskUsFromRepository
    {
        public AskUsFromRepository(AppDbContext context) : base(context) { }
    }
}
