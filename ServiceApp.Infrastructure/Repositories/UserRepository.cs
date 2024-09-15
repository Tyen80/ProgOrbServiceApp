using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Domain.Users;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<IUser>> GetAllUsersByFamilyIdWithRolesAsync()
    {
        return await _userManager.Users
            .Select(user => (IUser)user).ToListAsync();
    }

    public async Task<List<IUser>> GetFamilyMembersAsync(string familyId)
    {
        return await _context.Users.Where(u => u.FamilyId == familyId).ToListAsync<IUser>();
    }

    public async Task<List<IUser>> GetFamilyMembersByRoleAsync(string familyId)
    {
        return await _context.Users.Where(u => u.FamilyId == familyId)
            .ToListAsync<IUser>();
    }
}
