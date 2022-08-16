using MediatR;

namespace Catalog.Application.Catalog.Queries
{
    public class GetProductQuery : IRequest<ProductModel>
    {
        public int Id { get; set; }
    }
}
