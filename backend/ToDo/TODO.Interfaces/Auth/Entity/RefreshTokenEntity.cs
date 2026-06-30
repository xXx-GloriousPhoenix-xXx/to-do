using System.ComponentModel.DataAnnotations.Schema;
using TODO.Interfaces.Common;
using TODO.Interfaces.User.Entities;

namespace TODO.Interfaces.Auth.Entity;

[Table("refresh_tokens")]
public class RefreshTokenEntity : BaseEntity
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("token")]
    public required string Token { get; set; }

    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [Column("is_revoked")]
    public bool IsRevoked { get; set; } = false;

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }
}