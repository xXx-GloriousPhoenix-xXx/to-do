using TODO.Interfaces.Common;
using TODO.Interfaces.Todo.DTO;

namespace TODO.Interfaces.Todo;

public interface ITodoService
{
    Task<PagedResponse<GetTodoDTO>> GetAllAsync(
        Guid userId,
        TodoFilter? filter = null,
        TodoSorter? sorter = null,
        int pageNumber = 1,
        int pageSize = 10);
    Task<GetTodoDTO?> GetByIdAsync(Guid userId, Guid todoId);
    Task<GetTodoDTO> CreateAsync(Guid userId, CreateTodoDTO dto);
    Task<bool> UpdateAsync(Guid userId, Guid todoId, UpdateTodoDTO dto);
    Task<bool> DeleteAsync(Guid userId, Guid todoId);
}
