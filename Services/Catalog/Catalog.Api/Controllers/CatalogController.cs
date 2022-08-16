using AutoMapper;
using Catalog.Api.Models;
using Catalog.Application.Catalog.Commands;
using Catalog.Application.Catalog.Commands.DeleteProductCommand;
using Catalog.Application.Catalog.Commands.UpdateProductCommand;
using Catalog.Application.Catalog.Queries;
using Catalog.Domain;
using EventBus.RabbitMq;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IMessageProducer messageProducer;

        public CatalogController(IMapper mapper, IMediator mediator, IMessageProducer messageProducer) : base(mediator)
        {
            this.mapper = mapper;
            this.messageProducer = messageProducer;
        }

        /// <summary>Creates the product asynchronous.</summary>
        /// <param name="createProductModel">The create product model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateProductAsync([FromBody] CreateProductModel createProductModel, CancellationToken cancellationToken)
        {
            logger.LogInformation("Trying to create a product");

            var command = mapper.Map<CreateProductCommand>(createProductModel);
            var productId = await Mediator.Send(command, cancellationToken);

            logger.LogInformation($"Returning the product {productId}");

            return Ok(productId);
        }

        /// <summary>Gets the product asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetProductQuery
            {
                Id = id
            };
            var viewModel = await Mediator.Send(query, cancellationToken);

            return Ok(viewModel);
        }

        /// <summary>Updates the product asynchronous.</summary>
        /// <param name="updateProductModel">The update product model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductModel updateProductModel, CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateProductCommand>(updateProductModel);
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>Deletes the product asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(OrderModel orderModel, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                Name = orderModel.Name,
                Price = orderModel.Price,
                Quantity = orderModel.Quantity
            };

            //TODO: Order and SendMessage methods are not implemented yet
            //productRepository.Order.Add(order);
            //await productRepository.SaveChangesAsync(cancellationToken);

            return Ok(new { id = order.Id });
        }

        [HttpGet]
        public IActionResult SendMessage<T>(T message)
        {
            messageProducer.SendMessage(message);

            return Ok();
        }
    }
}
