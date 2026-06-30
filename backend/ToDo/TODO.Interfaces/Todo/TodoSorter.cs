namespace TODO.Interfaces.Todo;

public record TodoSorter(
    TodoSortField Field = TodoSortField.CreatedAt,
    SortDirection Direction = SortDirection.Descending);