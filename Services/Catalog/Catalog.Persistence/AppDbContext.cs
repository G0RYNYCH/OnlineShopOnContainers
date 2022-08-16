using Catalog.Domain;
using Catalog.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateFields();
       
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateFields();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
           
        builder.ApplyConfiguration(new CatalogConfiguration());
    }

    private void UpdateFields()
    {
        var currentDateTime = DateTimeOffset.Now;

        var entityEntries = ChangeTracker.Entries<BaseEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entityEntries)
        {
            var entity = entityEntry.Entity;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entity.CreatedOn = currentDateTime;
                    entity.UpdatedOn = currentDateTime;

                    break;
                case EntityState.Modified:
                    entity.UpdatedOn = currentDateTime;

                    break;
            }
        }
    }
}