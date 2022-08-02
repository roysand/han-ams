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
        private readonly string GenHourStatistics_SQL =
            @"declare @startDate as DateTime = (select max(timestamp) from hour)
            declare @minMeasDate as datetime = (select min(timestamp) from minute)
            declare @maxMeasDate as datetime = (select max(timestamp) from minute)
            declare @stopTime DateTime = (select max(timestamp) from minute)

            set @stopTime = DateAdd(hour, -1, @stopTime)
            set @stopTime = DATETIMEFROMPARTS(year(@stopTime), month(@stopTime), day(@stopTime),DATEPART(hour , @stopTime) ,59,59,0)

            if (@startDate is null)
            begin
                set @startDate = @minMeasDate
            end
            else
            begin
                set @startDate = DateAdd(hour, +1, @startDate)
            end

            set @startDate = DATETIMEFROMPARTS(year(@startDate), month(@startDate), day(@startDate),DATEPART(hour , @startDate) ,0,0,0)

            insert into hour
            select DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
                   , m.Location, 'kW' Unit, avg(m.valuenum) ValueNum, count(*) count
            from minute m
            where
                m.TimeStamp between @startDate and @stopTime
            group by
                m.location,DATETIMEFROMPARTS(year(m.TimeStamp), month(m.TimeStamp), day(m.TimeStamp),DATEPART(hour , m.timestamp) ,0,0,0)
            order by
                1 desc, 2 desc";
        
        private readonly string GenMinuteStatistics_SQL =
            @"declare @startDate as DateTime
            declare @stopDate as datetime
            declare @minMeasDate as datetime
            declare @maxMeasDate as datetime

            set @minMeasDate = (select min(timestamp) from detail)
            set @maxMeasDate = (select max(timestamp) from detail)
            set @startDate = (select max(timestamp) from minute)

            if (@startDate is null)
                begin
            set @startDate = @minMeasDate
            end
            else
            begin
                set @startDate = DateAdd(minute, +1, @startDate)
            end

            set @stopDate = DateAdd(minute, -1,@maxMeasDate)
            set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate),DATEPART(hour , @stopDate) ,DATEPART(minute , @stopDate),0,0)

            insert into minute
                select DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0) Timestamp
                , d.Location, 'kW' Unit, avg(d.valueNum)  ValueNum, count(*) count
                from dbo.detail d
            where d.ObisCodeId = 1 and d.TimeStamp between @startDate and @stopDate
            group by
                d.Location,
                DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp),DATEPART(hour , d.timestamp) ,DATEPART(minute , d.TimeStamp),0,0)--cast(d.TimeStamp as date), DATEPART(hour, d.timestamp), DATEPART(minute , d.timestamp)
            order by 1 desc,2 asc, 3 asc;";
        
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
            var now = DateTime.Now.Date;
            var powerByHourByDay = await (from hour in _context.HourSet
                where hour.TimeStamp.Date == now
                select new HourTotalVm()
                {
                    Date = hour.TimeStamp.Date,
                    Value = hour.ValueNum,
                    Location = hour.Location,
                    Description = "Hour total",
                    Unit = "kW"
                }).OrderBy(o => o.Location).ToListAsync(cancellationToken);
            
            var result = new DailyTotalVm()
            {
                Date = date,
                Value = powerByHourByDay.Sum(s => s.Value),
                Description = "Daily sum",
                Unit = "kW",
                //HoursTotal = powerByHourByDay.OrderBy(o => o.Date).ToList()
            };
            
            
            return result;
        }

        public async Task GeneratePowerUsageStatistics(CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlRawAsync(GenMinuteStatistics_SQL, cancellationToken);
            await _context.Database.ExecuteSqlRawAsync(GenHourStatistics_SQL, cancellationToken);
            
            return;
        }
    }
}