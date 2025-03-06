using CadavizCodeHub.Framework.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Domain.Repositories
{
    public interface IReadRepositoryBase<T>
        where T : IEntity
    {
        virtual static string DatabaseName => "default";

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
