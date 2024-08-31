using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.Users;

namespace ServiceApp.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<IUser>> GetFamilyMembersAsync(string familyId)
    {
        return await _context.Users.Where(u => u.FamilyId == familyId).ToListAsync<IUser>();
    }
}
