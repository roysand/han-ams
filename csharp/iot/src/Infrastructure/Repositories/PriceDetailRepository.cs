using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class PriceDetailRepository : GenericRepository<PriceDetail> , IPriceDetailRepository<PriceDetail>
    {
        public PriceDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}