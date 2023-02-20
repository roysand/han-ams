using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IHourRepository<T> : IRepository<T>
{
    Task<IList<Hour>> PowerUsageForHours(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
    Task<IList<Hour>> PowerUsageForDays(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
}