using TODO.Interfaces.Common;

namespace TODO.Interfaces.User.Entities;

public class UserEntity : BaseEntity
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}