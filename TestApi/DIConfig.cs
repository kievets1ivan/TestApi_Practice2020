using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Services;
using TestApi.Storages;

namespace TestApi
{
    public static class DIConfig
    {
        public static void RegisterInjections(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductStorage, ProductStorage>();
        }
    }
}
