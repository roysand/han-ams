using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        
        public new virtual async Task<Price> GetByKey(Guid id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<Price>(new object[] {id}, cancellationToken);
        }

        public Price FindMaxPricePeriod()
        {
            var price = (from p in _context.PriceSet
                where p.PricePeriod == (_context.PriceSet.Select(u => u.PricePeriod).Max())
                select p).FirstOrDefault();

            return price;
        }
    }
}