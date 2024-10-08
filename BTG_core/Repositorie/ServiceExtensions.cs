using BTG_core.Repositorie.Core;
using BTG_core.Repositorie.Core.Interfaces;

namespace BTG_core.Repositorie
{
    public static class ServiceExtensions
    {
        public static void AddRepositoryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserProductRepository, UserProductRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
