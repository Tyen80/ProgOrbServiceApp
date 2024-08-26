using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ServiceApp.WebUI.Client.Services.TaskService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ITaskService, TaskService>();

await builder.Build().RunAsync();
