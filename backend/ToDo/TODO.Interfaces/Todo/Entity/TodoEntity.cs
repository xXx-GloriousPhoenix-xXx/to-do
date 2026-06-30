using TODO.Interfaces.Common;
using TODO.Interfaces.User.Entities;

namespace TODO.Interfaces.Todo.Entity;

public class TodoEntity : BaseEntity
{
    public UserEntity? Author { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "Default";
    public DateTime CompleteUntil { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Guid AuthorId { get; set; }
}