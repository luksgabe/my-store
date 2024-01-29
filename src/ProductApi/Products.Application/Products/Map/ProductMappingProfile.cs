using AutoMapper;
using Products.Application.Products.Events;
using Products.Application.Products.Responses;
using Products.Domain.Entities;

namespace Products.Application.Products.Map
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductRegisterEvent, Product>()
                .ConstructUsing(pEvent =>
                    new Product(pEvent.Id, pEvent.Name, pEvent.Description, pEvent.Color, pEvent.Size, pEvent.IdCategory, pEvent.Genre))
                .ReverseMap();

            CreateMap<ProductUpdateEvent, Product>()
                .ConstructUsing(pEvent =>
                    new Product(pEvent.Id, pEvent.Name, pEvent.Description, pEvent.Color, pEvent.Size, pEvent.IdCategory, pEvent.Genre))
                .ReverseMap();

            CreateMap<Product, ProductResponse>();
        }
    }
}
