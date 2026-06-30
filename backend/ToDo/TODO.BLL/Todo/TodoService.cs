using AutoMapper;
using TODO.Interfaces.Common;
using TODO.Interfaces.Todo;
using TODO.Interfaces.Todo.DTO;
using TODO.Interfaces.Todo.Entity;
using TODO.Interfaces.User;
using TODO.Interfaces.Common.CustomException;

namespace TODO.BLL.Todo;

public class TodoService(
    IUserRepository userRepository,
    ITodoRepository todoRepository,
    IMapper mapper)
    : ITodoService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<GetTodoDTO> CreateAsync(Guid authorId, CreateTodoDTO dto)
    {
        var entity = _mapper.Map<TodoEntity>(dto);
        entity.AuthorId = authorId;

        var created = await _todoRepository.AddAsync(entity);
        return _mapper.Map<GetTodoDTO>(created);
    }

    public async Task DeleteAsync(Guid authorId, Guid todoId)
    {
        _ = await _userRepository.GetByIdAsync(authorId)
            ?? throw new NotFoundException("User not found");

        var todo = await _todoRepository.GetByIdAsync(todoId)
            ?? throw new NotFoundException("ToDo not found");

        if (todo.AuthorId != authorId)
            throw new ForbiddenException("You don't have access to this ToDo");

        await _todoRepository.DeleteAsync(todo);
    }

    public async Task<PagedResponse<GetTodoDTO>> GetAllAsync(
        Guid authorId,
        TodoFilter? filter = null,
        TodoSorter? sorter = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        _ = await _userRepository.GetByIdAsync(authorId)
            ?? throw new NotFoundException("User not found");

        var paged = await _todoRepository.GetByUserIdAsync(
            authorId, filter, sorter, pageNumber, pageSize);
        return _mapper.Map<PagedResponse<GetTodoDTO>>(paged);
    }

    public async Task<GetTodoDTO> GetByIdAsync(Guid authorId, Guid todoId)
    {
        _ = await _userRepository.GetByIdAsync(authorId)
            ?? throw new NotFoundException("User not found");

        var entity = await _todoRepository.GetByIdAsync(todoId)
            ?? throw new NotFoundException("ToDo not found");

        if (entity.AuthorId != authorId)
            throw new ForbiddenException("You don't have access to this ToDo");

        return _mapper.Map<GetTodoDTO>(entity);
    }

    public async Task UpdateAsync(Guid authorId, Guid todoId, UpdateTodoDTO dto)
    {
        _ = await _userRepository.GetByIdAsync(authorId)
            ?? throw new NotFoundException("User not found");

        var entity = await _todoRepository.GetByIdAsync(todoId)
            ?? throw new NotFoundException("ToDo not found");

        if (entity.AuthorId != authorId)
            throw new ForbiddenException("You don't have access to this ToDo");

        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _todoRepository.UpdateAsync(entity);
    }
}