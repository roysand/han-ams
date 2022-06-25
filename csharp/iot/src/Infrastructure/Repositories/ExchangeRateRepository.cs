using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExchangeRateRepository : GenericRepository<ExchengeRate>, IExchangeRateRepository<ExchengeRate>
    {
        public ExchangeRateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ExchengeRate> FindAsync(DateTime date)
        {
            var currency = await (from c in _context.CurrencySet
                where c.ExchangeRatePeriod == date
                select c).FirstOrDefaultAsync();

            return currency;
        }

        public async Task<ExchengeRate> FindNewestAsync()
        {
            var currency = await (from c in _context.CurrencySet
                where c.ExchangeRatePeriod == _context.CurrencySet.Select(u => u.ExchangeRatePeriod).Max()
                select c).FirstOrDefaultAsync();

            return currency;
        }
    }
}