using CadavizCodeHub.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Core.Domain.Repositories
{
    public interface IReadRepositoryBase<T>
        where T : IEntity
    {
        virtual string DatabaseName => "default";

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
