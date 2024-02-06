using AutoMapper;
using Products.Application.Categories.Map;
using Products.Application.Products.Map;
using Products.Application.Users.Map;

namespace Products.Application.Configuration.AutoMapper
{
    public class AutoMapperProfileConfig : Profile
    {
        private readonly CategoryMappingProfile _categoryMapping;
        private readonly ProductMappingProfile _productMapping;
        private readonly UserMappingProfile _userMapping;

        public AutoMapperProfileConfig()
        {
            _categoryMapping = new CategoryMappingProfile();
            _productMapping = new ProductMappingProfile();
            _userMapping = new UserMappingProfile();
        }
    }
}
