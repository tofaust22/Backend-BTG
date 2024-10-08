using BTG_core.Services.Core;
using BTG_core.Services.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BTG_core.Services
{
    public static class ServiceExtensions
    {
        public static void AddBusinnesServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserProductService, UserProductService>();
            services.AddTransient<IProductService, ProductService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
