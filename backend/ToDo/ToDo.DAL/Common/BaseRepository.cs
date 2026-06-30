using Microsoft.EntityFrameworkCore;
using TODO.Interfaces.Common;

namespace ToDo.DAL.Common;

public class BaseRepository<T>(TodoDBContext context)
    : IBaseRepository<T> where T : BaseEntity
{
    protected readonly TodoDBContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        var affected = await _context.SaveChangesAsync();
        return affected > 0;
    }
}