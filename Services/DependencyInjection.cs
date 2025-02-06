using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DependencyInjection
{
    public static void AddBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserManager<IdentityUser>>();
        serviceCollection.AddScoped<SignInManager<IdentityUser>>();
        serviceCollection.AddScoped<IFollowService, FollowService>();
        serviceCollection.AddScoped<ICommentService, CommentService>();
        serviceCollection.AddScoped<IUserJwtService, UserJwtService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IBlogService, BlogService>();
    }
}