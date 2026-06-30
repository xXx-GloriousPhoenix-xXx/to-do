namespace TODO.Interfaces.Todo;

public record TodoFilter(
    string? Category = null,
    bool? IsCompleted = null,
    DateTime? CompleteUntilFrom = null,
    DateTime? CompleteUntilTo = null);