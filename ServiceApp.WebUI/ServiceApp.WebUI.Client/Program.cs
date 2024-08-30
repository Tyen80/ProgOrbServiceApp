using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ServiceApp.WebUI.Client;
using ServiceApp.WebUI.Client.Services.TaskService;
using ServiceApp.WebUI.Client.Services.ToDoItemService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();

await builder.Build().RunAsync();
