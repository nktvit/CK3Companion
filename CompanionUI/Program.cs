using CompanionData;
using CompanionData.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CompanionUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register services with connection string
// builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
// builder.Services.AddTransient<ITraitRepository, TraitRepository>();

await builder.Build().RunAsync();