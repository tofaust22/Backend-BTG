using BTG_core.Models.Commons;
using BTG_core.Models.UserProducts;
using BTG_core.Models.Users;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons;
using BTG_core.Services.Core.Interfaces;
using MongoDB.Bson;

namespace BTG_core.Services.Core
{
    public class UserService : GenericService<User, UserRepository>, IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public UserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            GenericRepository = new UserRepository(new DBSettings()
            {
                CollectionName = "users",
                DatabaseName = "btg",
                ConnectionString = "mongodb+srv://faustperez22:ySvFkkJ31ZHqgnwg@cluster0.dofrl.mongodb.net/"
            });
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }
        public async override Task<User> Update(ObjectId id, User request)
        {
            var filter = new BsonDocument("_id", id);
            await GenericRepository.EditAsync(filter, request);
            return request;
        }
    }
}
