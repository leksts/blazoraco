using Blazoraco.Contracts.Weather;
using Blazoraco.Web.Client;
using Blazoraco.Web.Client.Counters;
using Blazoraco.Web.Weather;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder().AddBackOffice().AddWebsite().AddDeliveryApi().AddComposers().Build();

builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddScoped<CounterState>();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

app.MapRazorComponents<App>().AddInteractiveWebAssemblyRenderMode();

await app.RunAsync();
