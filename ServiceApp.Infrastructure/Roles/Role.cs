using Microsoft.AspNetCore.Identity;
using ServiceApp.Domain.Roles;

namespace ServiceApp.Infrastructure.Roles;
public class Role : IdentityRole, IRole
{
}
