using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository<Price>
    {
        public PriceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}