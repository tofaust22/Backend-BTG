using BTG_core.Models.Users;
using BTG_core.Repositorie.Core;
using BTG_core.Services.Commons.Interfaces;

namespace BTG_core.Services.Core.Interfaces
{
    public interface IUserService : IGenericService<User,UserRepository>
    {
    }
}
