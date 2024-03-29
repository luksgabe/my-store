﻿using Products.Application.Categories.Map;
using Products.Application.Configuration.AutoMapper;

namespace Products.WebApi.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperProfileConfig));

        }
    }
}
