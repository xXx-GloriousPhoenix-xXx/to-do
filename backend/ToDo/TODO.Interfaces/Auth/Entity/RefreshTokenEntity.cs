using System.ComponentModel.DataAnnotations.Schema;
using TODO.Interfaces.Common;

namespace TODO.Interfaces.Auth.Entity;

[Table("refresh_tokens")]
public class RefreshTokenEntity : BaseEntity
{
    [Column("user_id")]
    public required Guid UserId { get; set; }

    [Column("token")]
    public required string Token { get; set; }

    [Column("expires_at")]
    public required DateTime ExpiresAt { get; set; }

    [Column("is_revoked")]
    public bool IsRevoked { get; set; } = false;
}