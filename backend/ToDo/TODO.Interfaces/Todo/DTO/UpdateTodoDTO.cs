namespace TODO.Interfaces.Todo.DTO;

public record UpdateTodoDTO(
    string? Title,
    string? Description,
    string? Category,
    DateTime? CompleteUntil,
    bool? IsCompleted);