using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IPriceRepository<T> : IRepository<T>
    {
        Price FindMaxPricePeriod();
    }
}