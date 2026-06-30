using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Common;
using TODO.Interfaces.User;
using TODO.Interfaces.User.Entities;

namespace ToDo.DAL.User;

public class UserRepository(TodoDBContext context)
    : BaseRepository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}