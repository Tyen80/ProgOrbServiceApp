using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.Tasks;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Infrastructure;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<TaskToDo> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskToDo>()
            .Property(t => t.Amount)
            .HasColumnType("decimal(9,2)");

        // Other configurations...
    }
}
