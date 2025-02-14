using CadavizCodeHub.Framework.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Domain.Repositories
{
    public interface ICrudRepositoryBase<T> : IReadRepositoryBase<T>
        where T : IEntity
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
