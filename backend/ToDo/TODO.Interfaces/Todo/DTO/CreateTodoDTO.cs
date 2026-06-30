namespace TODO.Interfaces.Todo.DTO;

public record CreateTodoDTO(
    string Title,
    string Description,
    string Category,
    DateTime CompleteUntil);
