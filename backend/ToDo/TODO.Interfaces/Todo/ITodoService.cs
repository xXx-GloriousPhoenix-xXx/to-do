using TODO.Interfaces.Common;
using TODO.Interfaces.Todo.DTO;

namespace TODO.Interfaces.Todo;

public interface ITodoService
{
    Task<PagedResponse<GetTodoDTO>> GetAllAsync(
        Guid authorId,
        TodoFilter? filter = null,
        TodoSorter? sorter = null,
        int pageNumber = 1,
        int pageSize = 10);
    Task<GetTodoDTO> GetByIdAsync(Guid authorId, Guid todoId);
    Task<GetTodoDTO> CreateAsync(Guid authorId, CreateTodoDTO dto);
    Task UpdateAsync(Guid authorId, Guid todoId, UpdateTodoDTO dto);
    Task DeleteAsync(Guid authorId, Guid todoId);
}
