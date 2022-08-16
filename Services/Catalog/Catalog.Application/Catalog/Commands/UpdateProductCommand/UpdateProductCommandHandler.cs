using Catalog.Persistence;
using Catalog.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Catalog.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetAsync(command.Id, cancellationToken);
            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Price = command.Price;
            
            await productRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}