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
            // var data = await (from detail in _context.DetailSet
            //         where detail.TimeStamp.Date == date.Date && detail.ValueNum != -1
            //         select detail.ValueNum)
            //     .AverageAsync(cancellationToken);

            var powerByHourByDay = await (from detail in _context.DetailSet
                where detail.ObisCode == "1-0:1.7.0.255" && detail.TimeStamp.Date == DateTime.Now.Date
                group detail by new { date = detail.TimeStamp.Date, hour = detail.TimeStamp.Hour }
                into g
                select new HourTotalVm()
                {
                    Date = new DateTime(g.Key.date.Year, g.Key.date.Month, g.Key.date.Day,g.Key.hour,0,0),
                    Value = g.Average(x => x.ValueNum),
                    Description = "Hour Total",
                    Unit = "kW"
                }).ToListAsync(cancellationToken);
            
            return new DailyTotalVm()
            {
                Date = date,
                Value = powerByHourByDay.Sum(s => s.Value),
                Description = "Daily sum",
                Unit = "kW",
                HoursTotal = powerByHourByDay.OrderBy(o => o.Date).ToList()
            };
        }
    }
}