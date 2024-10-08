using Amazon.Runtime.Internal;
using BTG_core.Models;
using BTG_core.Models.Commons;
using BTG_core.Models.Dtos;
using BTG_core.Models.Products;
using BTG_core.Models.UserProducts;
using BTG_core.Models.Users;
using BTG_core.Repositorie.Commons;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons;
using BTG_core.Services.Core.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Core
{
    public class UserProductService : GenericService<UserProduct, UserProductRepository>, IUserProductService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public UserProductService(
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, 
            IProductService productService,
            IUserService userService)
        {
            GenericRepository = new UserProductRepository(new DBSettings()
            {
                CollectionName = "users_products",
                DatabaseName = "btg",
                ConnectionString = "mongodb+srv://faustperez22:ySvFkkJ31ZHqgnwg@cluster0.dofrl.mongodb.net/"
            });
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.productService = productService;
            this.userService = userService;
        }
        public async override Task<UserProduct> Update(ObjectId id, UserProduct request)
        {
            var filter = new BsonDocument("_id", id);
            await GenericRepository.EditAsync(filter, request);
            return request;
        }

        public async Task<UserProduct> GetByUserId(string userId)
        {
            var filter = new BsonDocument("user_id", ObjectId.Parse(userId));
            var userProductEntity = await GenericRepository.FindOneAsyncBy(filter);
            return userProductEntity;
        }

        public async Task<UserProduct> UpdateStateByProductId(string userId, string productId, StateUserProductDto stateUserProductDto)
        {
            var filter = new BsonDocument("user_id", ObjectId.Parse(userId));
            
            var userProductEntity = await GenericRepository.FindOneAsyncBy(filter);
            var product = userProductEntity.Products.Find((p) => p.Id == productId);
           
            if (userProductEntity != null && product.Record.Count > 0)
            {
                product.Active = stateUserProductDto.isActive;
                if (stateUserProductDto.isActive)
                {
                    await ValidateAmountUser(userId, productId, stateUserProductDto.OpeningAmount);
                    var record = new Record()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        DateCreated = DateTime.UtcNow,
                        OpeningAmount = stateUserProductDto.OpeningAmount
                    };
                    product.Record.Add(record);
                }
                else
                {
                    var record = new Record()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        DateCreated = product.Record[^1].DateCreated,
                        DateClosed = DateTime.UtcNow,
                        OpeningAmount = product.Record[^1].OpeningAmount
                    };
                    product.Record.Add(record);
                    await ReturnMoneyToUser(userId, product.Record[^1].OpeningAmount);
                }
            }
            var productIndex = userProductEntity.Products.FindIndex((p) => p.Id == productId);
            userProductEntity.Products[productIndex] = product;
            
            await Update(ObjectId.Parse(userProductEntity.Id), userProductEntity);
            return userProductEntity;
        }

        public async Task<UserProduct> AddProductById(string userId, string productId, StateUserProductDto stateUserProductDto)
        {
            await ValidateAmountUser(userId, productId, stateUserProductDto.OpeningAmount);
            var productEntity = await productService.GetOneById(ObjectId.Parse(productId));
            var filter = new BsonDocument("user_id", ObjectId.Parse(userId));
            var userProductEntity = await GenericRepository.FindOneAsyncBy(filter);
           
            var newProduct = new ProductItem()
            {
                Id = productId,
                Active = true,
                Record = new List<Record>
                {
                    new Record()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        OpeningAmount = stateUserProductDto.OpeningAmount,
                        DateCreated = DateTime.UtcNow
                    }
                }
            };
            userProductEntity.Products.Add(newProduct);
            await Update(ObjectId.Parse(userProductEntity.Id), userProductEntity);
            return userProductEntity;
        }

        private async Task ValidateAmountUser(string userId, string productId, double openingAmount)
        {
            var productEntity = await productService.GetOneById(ObjectId.Parse(productId));
            if (productEntity == null) throw new NotFoundException($"El fondo no existe");
            var userEntity = await userService.GetOneById(ObjectId.Parse(userId));
            if (openingAmount < productEntity.MinAmount) throw new NotFoundException($"No cumple el monto minimo de apertura");
            if (userEntity.FinanceData.Amount < openingAmount) throw new NotFoundException($"No tiene saldo disponible para vincularse al fondo {productEntity.Name}");

            userEntity.FinanceData.Amount = userEntity.FinanceData.Amount - openingAmount;
            await userService.Update(ObjectId.Parse(userEntity.Id), userEntity);
        }

        private async Task ReturnMoneyToUser(string userId, double amount)
        {
            var userEntity = await userService.GetOneById(ObjectId.Parse(userId));
            userEntity.FinanceData.Amount = userEntity.FinanceData.Amount + amount;
            await userService.Update(ObjectId.Parse(userEntity.Id), userEntity);
        }
    }
}