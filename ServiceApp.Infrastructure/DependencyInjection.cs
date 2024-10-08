﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceApp.Application.Authentication;
using ServiceApp.Domain.Email;
using ServiceApp.Domain.Tasks;
using ServiceApp.Domain.ToDoItems;
using ServiceApp.Domain.Users;
using ServiceApp.Infrastructure.Authentication;
using ServiceApp.Infrastructure.Email;
using ServiceApp.Infrastructure.Repositories;
using ServiceApp.Infrastructure.Users;

namespace ServiceApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserRepository, UserRepository>();

        AddAuthentication(services);

        return services;
    }

    private static void AddAuthentication(this IServiceCollection services)
    {
        services.AddScoped<ILoginUserService, LoginUserService>();
        services.AddScoped<IRegisterUserService, RegisterUserService>();
        services.AddScoped<IEmailConfirmService, EmailConfirmService>();
        services.AddScoped<IFamilyExistService, FamilyExistService>();
        services.AddScoped<IEmailManagerService, EmailManagerService>();
        services.AddScoped<IUserRolesService, UserRolesService>();
        services.AddScoped<ISendFamilyInviteService, SendFamilyInviteService>();
        services.AddScoped<ISendChildInviteService, SendChildInviteService>();

        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAuthenticatedUser", policy =>
                policy.RequireAuthenticatedUser());
        });
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();

        services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

    }
}
