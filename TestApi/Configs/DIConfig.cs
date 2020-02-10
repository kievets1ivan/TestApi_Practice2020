using Microsoft.Extensions.DependencyInjection;
using TestApi.BL.Services;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Storages;

namespace TestApi
{
    public static class DIConfig
    {
        public static void RegisterInjections(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductStorage, ProductStorage>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
