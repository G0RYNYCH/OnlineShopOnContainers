using AutoMapper;
using Catalog.Domain;

namespace Catalog.Application.Catalog.Queries;
public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductModel>();
    }
}
