namespace TODO.Interfaces.Todo;

public enum TodoSortField
{
    CreatedAt,
    CompleteUntil,
    Title
}

public enum SortDirection
{
    Ascending,
    Descending
}

public record TodoSorter(
    TodoSortField Field = TodoSortField.CreatedAt,
    SortDirection Direction = SortDirection.Descending);