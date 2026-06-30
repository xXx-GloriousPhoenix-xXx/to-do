using ToDo.DAL.Common;
using ToDo.DAL.RefreshToken;
using ToDo.DAL.Todo;
using ToDo.DAL.User;
using TODO.Interfaces.Auth;
using TODO.Interfaces.Common;
using TODO.Interfaces.Todo;
using TODO.Interfaces.User;

namespace TODO.API.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}