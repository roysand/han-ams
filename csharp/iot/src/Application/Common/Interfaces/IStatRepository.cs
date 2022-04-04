using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IStatRepository<T> : IRepository<T>
    {
        Task<DailyTotalVm> DailyTotal(DateTime date, CancellationToken cancellationToken);
        Task GeneratePowerUsageStatistics(CancellationToken cancellationToken);
    }
}