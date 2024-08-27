using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceApp.Domain.Tasks;
using ServiceApp.Domain.ToDoItems;
using ServiceApp.Infrastructure.Repositories;

namespace ServiceApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

        return services;
    }
}
