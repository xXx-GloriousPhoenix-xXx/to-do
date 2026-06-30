using TODO.Interfaces.Common;
using TODO.Interfaces.Todo.Entity;

namespace TODO.Interfaces.Todo;

public interface ITodoRepository : IBaseRepository<TodoEntity>
{
    Task<IEnumerable<TodoEntity>> GetByUserIdAsync(
        Guid userId,
        TodoFilter? filter = null,
        TodoSorter? sorter = null,
        int pageNumber = 1,
        int pageSuze = 10);
}
