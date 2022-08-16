using Catalog.Domain;
using Catalog.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T>
    where T : BaseEntity
{
    protected DbSet<T> DbSet;

    private readonly AppDbContext context;

    protected BaseRepository(AppDbContext context)
    {
        this.context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken) => 
        await DbSet.FirstOrDefaultAsync(x => x.Id == id);

    public void Create(T product, CancellationToken cancellationToken) =>
        DbSet.Add(product);

    public void Update(T product, CancellationToken cancellationToken) =>
        DbSet.Update(product);

    public void Delete(T product, CancellationToken cancellationToken) =>
        DbSet.Remove(product);

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync();
}