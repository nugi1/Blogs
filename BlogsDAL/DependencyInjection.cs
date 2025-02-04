using BlogsDAL.Interfaces;
using BlogsDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogsDAL;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserJwtRepository, UserJwtRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddDbContext<BlogDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}