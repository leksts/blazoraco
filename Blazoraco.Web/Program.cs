using Blazoraco.Web.Client;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder().AddBackOffice().AddWebsite().AddDeliveryApi().AddComposers().Build();

builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

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
