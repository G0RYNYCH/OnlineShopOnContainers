using Catalog.Domain;
using System.Data.Entity;

namespace Catalog.Persistence.Interfaces;

public interface IBaseRepository<T>
    where T : BaseEntity
{
    Task<T?> GetAsync(int id, CancellationToken cancellationToken);

    void Create(T product, CancellationToken cancellationToken);
    void Update(T entity, CancellationToken cancellationToken);
    void Delete(T entity, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}