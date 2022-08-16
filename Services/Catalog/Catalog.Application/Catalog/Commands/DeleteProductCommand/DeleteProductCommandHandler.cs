using Catalog.Persistence.Interfaces;
using MediatR;

namespace Catalog.Application.Catalog.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetAsync(command.Id, cancellationToken);

            productRepository.Delete(entity, cancellationToken);
            await productRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
