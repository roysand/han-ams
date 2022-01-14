using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class RawRepository : GenericRepository<RawData>, IRawRepository<RawData>
    {
        public RawRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}