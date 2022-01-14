using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class DetailRepository : GenericRepository<Detail> , IDetailRepository<Detail>
    {
        public DetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}