using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StatRepository : GenericRepository<Detail> , IStatRepository<Detail>
    {
        public StatRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Detail Add(Detail entity)
        {
            throw new NotSupportedException("Not valid for this data ..");
        }

        public override Task<Detail> GetByKey(Guid MeasurementId, CancellationToken cancellationToken)
        {
            throw new NotSupportedException("Not valid for this data ..");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException(("Not valid for this data .."));
        }

        public async Task<DailyTotalVm> DailyTotal(DateTime date, CancellationToken cancellationToken)
        {
            var data = await (from detail in _context.DetailSet
                    where detail.TimeStamp.Date == date.Date && detail.ValueNum != -1
                    select detail.ValueNum)
                .AverageAsync(cancellationToken);
            
            return new DailyTotalVm()
            {
                Date = date,
                Value = data,
                Description = "Daily average",
                Unit = "kW"
            };
        }
    }
}