using BTG_core.Models.Dtos;
using BTG_core.Models.UserProducts;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Core.Interfaces
{
    public interface IUserProductService : IGenericService<UserProduct, UserProductRepository>
    {
        Task<UserProduct> UpdateStateByProductId(string userId, string productId, StateUserProductDto stateUserProductDto);
        Task<UserProduct> AddProductById(string userId, string productId, StateUserProductDto stateUserProductDto);
        Task<UserProduct> GetByUserId(string userId);
    }
}
