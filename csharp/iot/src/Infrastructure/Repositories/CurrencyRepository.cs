using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository<Currency>
    {
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Currency> FindAsync(DateTime date)
        {
            var currency = await (from c in _context.CurrencySet
                where c.CurrencyPeriod == date
                select c).FirstOrDefaultAsync();

            return currency;
        }
    }
}