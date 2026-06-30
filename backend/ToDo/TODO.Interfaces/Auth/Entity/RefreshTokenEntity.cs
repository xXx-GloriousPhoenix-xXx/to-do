using TODO.Interfaces.Common;

namespace TODO.Interfaces.Auth.Entity;

public class RefreshTokenEntity : BaseEntity
{
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
}