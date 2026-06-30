using TODO.BLL.Auth;
using TODO.BLL.Todo;
using TODO.BLL.User;
using TODO.Interfaces.Auth;
using TODO.Interfaces.Todo;
using TODO.Interfaces.User;

namespace TODO.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
