using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IExchangeRateRepository<T> : IRepository<T>
    {
        Task<ExchengeRate> FindAsync(DateTime date);
        Task<ExchengeRate> FindNewestAsync();
    }
}