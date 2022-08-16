using AutoMapper;
using Catalog.Persistence.Interfaces;
using MediatR;

namespace Catalog.Application.Catalog.Queries;
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductModel>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id, cancellationToken);

        return mapper.Map<ProductModel>(product);
    }
}
