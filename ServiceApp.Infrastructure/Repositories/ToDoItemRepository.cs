using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Infrastructure.Repositories;
public class ToDoItemRepository : IToDoItemRepository
{
    private readonly ApplicationDbContext _context;

    public ToDoItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoItem>> GetAllAsync()
    {
        return await _context.ToDoItems.ToListAsync();
    }

    public Task<List<ToDoItem>> GetAllActiveByFamilyId(string FamilyId)
    {
        return _context.ToDoItems.Include(t => t.Task).Where(x => x.IsComplete == false && x.FamilyId == FamilyId).ToListAsync();
    }

    public Task<List<ToDoItem>> GetAllActiveByUserId(string UserId)
    {
        return _context.ToDoItems.Include(t => t.Task).Where(x => x.IsComplete == false && x.UserId == UserId).ToListAsync();
    }

    public async Task<List<ToDoItem>> GetAllActiveAsync()
    {
        return await _context.ToDoItems.Include(t => t.Task).Where(x => x.IsComplete == false).ToListAsync();
    }

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        var toDoItem = await _context.ToDoItems.Include(t => t.Task).FirstOrDefaultAsync(t => t.Id == id);
        return toDoItem;
    }

    public async Task<ToDoItem> CreateAsync(ToDoItem entity)
    {
        _context.ToDoItems.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ToDoItem?> UpdateAsync(ToDoItem entity)
    {
        var toDoItemToUpdate = await GetByIdAsync(entity.Id);
        if (toDoItemToUpdate == null)
        {
            throw new Exception("ToDoItem not found");
        }

        toDoItemToUpdate.TaskId = entity.TaskId;
        toDoItemToUpdate.IsComplete = entity.IsComplete;
        toDoItemToUpdate.DateUpdated = DateTime.Now;
        await _context.SaveChangesAsync();
        return toDoItemToUpdate;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var toDoItemToDelete = await GetByIdAsync(id);
        if (toDoItemToDelete == null)
        {
            return false;
        }
        _context.ToDoItems.Remove(toDoItemToDelete);
        await _context.SaveChangesAsync();
        return true;
    }


}
