using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository
    {
        T Add(T entity);
        Task<T> GetByKey(Guid MeasurementId, CancellationToken cancellationToken);
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
