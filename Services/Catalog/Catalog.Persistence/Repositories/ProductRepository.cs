using Catalog.Domain;
using Catalog.Persistence.Interfaces;

namespace Catalog.Persistence.Repositories;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {

    }
}