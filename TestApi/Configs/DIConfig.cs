using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TestApi.BL.Services;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Storages;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi
{
    public static class DIConfig
    {
        public static void RegisterInjections(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductStorage, ProductStorage>();
            services.AddTransient<IProductTransactionService, ProductTransactionService>();
            services.AddTransient<IProductTransactionStorage, ProductTransactionStorage>();
            services.AddTransient<IDbTransactionService, DbTransactionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserStorage, UserStorage>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
