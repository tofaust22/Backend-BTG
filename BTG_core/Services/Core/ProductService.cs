using BTG_core.Models.Commons;
using BTG_core.Models.Products;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons;
using BTG_core.Services.Core.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Core
{
    public class ProductService : GenericService<Product, ProductRepository>, IProductService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        
        public ProductService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            GenericRepository = new ProductRepository(new DBSettings()
            {
                CollectionName = "products",
                DatabaseName = "btg",
                ConnectionString = "mongodb+srv://faustperez22:ySvFkkJ31ZHqgnwg@cluster0.dofrl.mongodb.net/"
            }); 
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }
        public async override Task<Product> Update(ObjectId id, Product request)
        {
            var filter = new BsonDocument("_id", id);
            await GenericRepository.EditAsync(filter, request);
            return request;
        }
    }
}
