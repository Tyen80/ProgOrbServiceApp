using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.Tasks;

namespace ServiceApp.Infrastructure.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskToDo>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskToDo?> GetByIdAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return task;
    }
    public async Task<TaskToDo> CreateAsync(TaskToDo entity)
    {
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TaskToDo?> UpdateAsync(TaskToDo entity)
    {
        var taskToUpdate = await GetByIdAsync(entity.Id);
        if (taskToUpdate == null)
        {
            throw new Exception("Task not found");
        }
        taskToUpdate.Title = entity.Title;
        taskToUpdate.Description = entity.Description;
        taskToUpdate.Amount = entity.Amount;
        taskToUpdate.DateUpdated = DateTime.Now;
        await _context.SaveChangesAsync();
        return taskToUpdate;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var taskToDelete = await GetByIdAsync(id);
        if (taskToDelete == null)
        {
            return false;
        }
        _context.Tasks.Remove(taskToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}
