using BTG_core.Models.Commons;
using BTG_core.Models.Products;
using BTG_core.Repositorie.Commons;
using BTG_core.Repositorie.Core.Interfaces;

namespace BTG_core.Repositorie.Core
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(IDBSettings settings) : base(settings) { }
    }
}
