using BTG_core.Models.Commons;
using BTG_core.Models.Users;
using BTG_core.Repositorie.Commons;
using BTG_core.Repositorie.Core.Interfaces;

namespace BTG_core.Repositorie.Core
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(IDBSettings settings): base(settings) {}
    }
}
