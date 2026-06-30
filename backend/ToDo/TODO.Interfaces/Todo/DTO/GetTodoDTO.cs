namespace TODO.Interfaces.Todo.DTO;

public record GetTodoDTO(
    Guid Id,
    Guid AuthorId,
    string AuthorUsername,
    string Title,
    string Description,
    string Category,
    DateTime CompleteUntil);
