using System.ComponentModel.DataAnnotations.Schema;
using TODO.Interfaces.Common;
using TODO.Interfaces.User.Entities;

namespace TODO.Interfaces.Todo.Entity;

[Table("todos")]
public class TodoEntity : BaseEntity
{
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = "Default";

    [Column("complete_until")]
    public DateTime CompleteUntil { get; set; }

    [Column("is_completed")]
    public bool IsCompleted { get; set; } = false;

    [Column("author_id")]
    public Guid AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public UserEntity? Author { get; set; }
}