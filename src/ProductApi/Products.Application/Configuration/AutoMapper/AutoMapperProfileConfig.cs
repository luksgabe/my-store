using AutoMapper;
using Products.Application.Categories.Map;

namespace Products.Application.Configuration.AutoMapper
{
    public class AutoMapperProfileConfig : Profile
    {
        private readonly CategoryMappingProfile _categoryMap;

        public AutoMapperProfileConfig()
        {
            _categoryMap = new CategoryMappingProfile();
        }
    }
}
