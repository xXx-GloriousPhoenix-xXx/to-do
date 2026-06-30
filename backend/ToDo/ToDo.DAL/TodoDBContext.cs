using Microsoft.EntityFrameworkCore;
using TODO.Interfaces.Auth.Entity;
using TODO.Interfaces.Todo.Entity;
using TODO.Interfaces.User.Entities;

namespace ToDo.DAL;
public class TodoDBContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TodoEntity> Todos { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}