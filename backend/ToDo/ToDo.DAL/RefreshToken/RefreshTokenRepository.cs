using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Common;
using TODO.Interfaces.Auth;
using TODO.Interfaces.Auth.Entity;

namespace ToDo.DAL.RefreshToken;

public class RefreshTokenRepository(TodoDBContext context)
    : BaseRepository<RefreshTokenEntity>(context), IRefreshTokenRepository
{
    public async Task<RefreshTokenEntity?> GetByTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task RevokeAsync(string token)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);
        if (entity is not null)
        {
            entity.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RevokeAllForUserAsync(Guid userId)
    {
        await _dbSet
            .Where(rt => rt.UserId == userId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(rt => rt.IsRevoked, true));
    }
}