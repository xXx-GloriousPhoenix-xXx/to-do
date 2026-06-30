using TODO.Interfaces.Auth.Entity;
using TODO.Interfaces.Common;

namespace TODO.Interfaces.Auth;

public interface IRefreshTokenRepository : IBaseRepository<RefreshTokenEntity>
{
    Task<RefreshTokenEntity?> GetByTokenAsync(string token);
    Task RevokeAsync(string token);
    Task RevokeAllForUserAsync(Guid userId);
}
