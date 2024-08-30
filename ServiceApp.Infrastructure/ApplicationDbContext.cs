using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.Tasks;
using ServiceApp.Domain.ToDoItems;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<TaskToDo> Tasks { get; set; }


}
