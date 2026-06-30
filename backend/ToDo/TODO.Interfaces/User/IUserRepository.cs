using TODO.Interfaces.Common;
using TODO.Interfaces.User.Entities;

namespace TODO.Interfaces.User;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByUsernameAsync(string username);
}
