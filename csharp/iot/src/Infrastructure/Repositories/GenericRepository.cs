using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public Task<T> GetByKey(Guid MeasurementId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}