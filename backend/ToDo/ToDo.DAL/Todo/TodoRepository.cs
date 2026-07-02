using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDo.DAL.Common;
using TODO.Interfaces.Common;
using TODO.Interfaces.Todo;
using TODO.Interfaces.Todo.Entity;

namespace ToDo.DAL.Todo;

public class TodoRepository(TodoDBContext context)
    : BaseRepository<TodoEntity>(context), ITodoRepository
{
    private static readonly Dictionary<TodoSortField, Expression<Func<TodoEntity, object>>> SortExpressions = new()
    {
        [TodoSortField.Title] = td => td.Title,
        [TodoSortField.CompleteUntil] = td => td.CompleteUntil,
        [TodoSortField.CreatedAt] = td => td.CreatedAt,
    };

    public async Task<PagedResponse<TodoEntity>> GetByUserIdAsync(
        Guid userId,
        TodoFilter? filter = null,
        TodoSorter? sorter = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var query = _context.Todos
            .AsNoTracking()
            .Include(td => td.Author)
            .Where(td => td.AuthorId == userId);

        if (filter is not null)
        {
            filter.Deconstruct(
                out var category, 
                out var isCompleted, 
                out var completeUntilFrom, 
                out var completeUntilTo);

            if (category is not null)
                query = query.Where(td => td.Category == category);

            if (isCompleted is not null)
                query = query.Where(td => td.IsCompleted == isCompleted);

            if (completeUntilFrom is not null)
            {
                var utcFrom = DateTime.SpecifyKind(completeUntilFrom.Value, DateTimeKind.Utc);
                query = query.Where(td => td.CompleteUntil >= utcFrom);
            }

            if (completeUntilTo is not null)
            {
                var utcTo = DateTime.SpecifyKind(completeUntilTo.Value, DateTimeKind.Utc);
                query = query.Where(td => td.CompleteUntil <= utcTo);
            }
        }

        if (sorter is not null)
        {
            sorter.Deconstruct(out var field, out var direction);
            var sortExpression = SortExpressions.GetValueOrDefault(field, td => td.CreatedAt);

            query = direction == SortDirection.Ascending
                ? query.OrderBy(sortExpression)
                : query.OrderByDescending(sortExpression);
        }
        else
        {
            query = query.OrderByDescending(td => td.CreatedAt);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<TodoEntity>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<string>> GetCategoriesByUserIdAsync(Guid userId)
    {
        return await _context.Todos
            .AsNoTracking()
            .Where(td => td.AuthorId == userId)
            .Select(td => td.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }
}