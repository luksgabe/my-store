using AutoMapper;
using Products.Application.Categories.Map;
using Products.Application.Products.Map;

namespace Products.Application.Configuration.AutoMapper
{
    public class AutoMapperProfileConfig : Profile
    {
        private readonly CategoryMappingProfile _categoryMap;
        private readonly ProductMappingProfile _productMapping;

        public AutoMapperProfileConfig()
        {
            _categoryMap = new CategoryMappingProfile();
            _productMapping = new ProductMappingProfile();
        }
    }
}
