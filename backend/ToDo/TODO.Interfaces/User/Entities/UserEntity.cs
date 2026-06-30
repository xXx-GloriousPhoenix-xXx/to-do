using System.ComponentModel.DataAnnotations.Schema;
using TODO.Interfaces.Common;

namespace TODO.Interfaces.User.Entities;

[Table("users")]
public class UserEntity : BaseEntity
{
    [Column("username")]
    public required string Username { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("password_hash")]
    public required string PasswordHash { get; set; }
}