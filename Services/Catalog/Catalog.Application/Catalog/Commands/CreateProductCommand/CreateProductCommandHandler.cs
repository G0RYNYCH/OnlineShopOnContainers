using Catalog.Domain;
using Catalog.Persistence.Interfaces;
using MediatR;

namespace Catalog.Application.Catalog.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price
            };

            productRepository.Create(product, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
