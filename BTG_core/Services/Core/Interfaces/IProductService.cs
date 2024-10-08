using BTG_core.Models.Products;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons.Interfaces;

namespace BTG_core.Services.Core.Interfaces
{
    public interface IProductService : IGenericService<Product,ProductRepository>
    {
    }
}
