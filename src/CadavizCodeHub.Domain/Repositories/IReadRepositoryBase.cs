using System.Threading.Tasks;
using System;
using CadavizCodeHub.Framework.Domain;
using System.Threading;

namespace CadavizCodeHub.Domain.Repositories
{
    public interface IReadRepositoryBase<T> : IDisposable
        where T : IEntity
    {
        virtual static string DatabaseName => "default";

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
