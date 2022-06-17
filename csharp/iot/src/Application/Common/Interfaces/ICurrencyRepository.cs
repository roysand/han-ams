using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ICurrencyRepository<T> : IRepository<T>
    {
        Task<Currency> FindAsync(DateTime date);
    }
}