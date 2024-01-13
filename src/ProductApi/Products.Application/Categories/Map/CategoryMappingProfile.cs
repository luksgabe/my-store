using AutoMapper;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Responses;
using Productss.Domain.Entities;

namespace Products.Application.Categories.Map
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            //Commands Map
            CreateMap<RegisterCategoryCommand, Category>()
                .ConstructUsing(c => new Category(c.Id, c.Name));
            CreateMap<UpdateCategoryCommand, Category>()
                .ConstructUsing(c => new Category(c.Id, c.Name));

            //Response Map
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}
