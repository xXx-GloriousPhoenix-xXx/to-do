using TODO.BLL.Todo;

namespace TODO.API.Extensions;

public static class MappingExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(TodoProfile));
        
        return services;
    }
}
