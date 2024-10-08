using BTG_core.Models.Commons;
using BTG_core.Models.UserProducts;
using BTG_core.Models.Users;
using BTG_core.Repositorie.Commons;
using BTG_core.Repositorie.Core.Interfaces;

namespace BTG_core.Repositorie.Core
{
    public class UserProductRepository : GenericRepository<UserProduct>, IUserProductRepository
    {
        public UserProductRepository(IDBSettings settings) : base(settings) { }
    }
}
