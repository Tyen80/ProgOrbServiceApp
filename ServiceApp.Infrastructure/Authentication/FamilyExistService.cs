using Microsoft.EntityFrameworkCore;
using ServiceApp.Application.Authentication;

namespace ServiceApp.Infrastructure.Authentication;
public class FamilyExistService : IFamilyExistService
{
    private readonly ApplicationDbContext _context;

    public FamilyExistService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> FamilyExists(string familyId)
    {
        return await _context.Users.AnyAsync(f => f.FamilyId == familyId);
    }
}
