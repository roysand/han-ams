using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<T> GetByKey(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken);
        Task<T> FindSingle(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int AddRange(List<T> entities);
    }
}
