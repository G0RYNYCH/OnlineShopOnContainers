using MediatR;

namespace Catalog.Application.Catalog.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
