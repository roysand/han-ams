using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HourRepository : GenericRepository<Hour>, IHourRepository<Hour>
{
    public HourRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IList<Hour>> PowerUsageForHours(DateTime fromDate, DateTime toDate,
        CancellationToken cancellationToken)
    {
        var result = await (from hour in _context.HourSet
            where hour.TimeStamp >= fromDate && hour.TimeStamp <= toDate
            select hour).OrderBy(o => o.Location).ThenBy(o => o.TimeStamp).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<IList<Hour>> PowerUsageForDays(DateOnly fromDate, DateOnly toDate,
        CancellationToken cancellationToken)
    {

            /*
            where hour.TimeStamp.Date >= fromDate && hour.TimeStamp.Date <= toDate
            group hour by new {location = hour.Location, date = hour.TimeStamp.Date}
            into g
                select new DailyTotalVm()
                {
                    Date = g.Key.date,
                    Loca
                }*/
            throw new NotImplementedException();
    }
}