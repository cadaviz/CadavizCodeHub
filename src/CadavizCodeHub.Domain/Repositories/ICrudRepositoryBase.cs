using System;
using System.Threading.Tasks;
using CadavizCodeHub.Framework.Domain;

namespace CadavizCodeHub.Domain.Repositories
{
    public interface ICrudRepositoryBase<T> : IReadRepositoryBase<T>
        where T : IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}
