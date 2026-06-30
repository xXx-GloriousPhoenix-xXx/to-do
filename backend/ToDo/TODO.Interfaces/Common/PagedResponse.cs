namespace TODO.Interfaces.Common;

public record PagedResponse<T>
{
    public T[] Items { get; init; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }

    public int PageCount => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNext => PageNumber < PageCount;
    public bool HasPrevious => PageNumber > 1;
}