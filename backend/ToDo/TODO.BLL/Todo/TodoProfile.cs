using AutoMapper;
using TODO.Interfaces.Common;
using TODO.Interfaces.Todo.DTO;
using TODO.Interfaces.Todo.Entity;

namespace TODO.BLL.Todo;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<CreateTodoDTO, TodoEntity>();
        CreateMap<TodoEntity, GetTodoDTO>();

        CreateMap<UpdateTodoDTO, TodoEntity>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PagedResponse<TodoEntity>, PagedResponse<GetTodoDTO>>();
    }
}