using Blazoraco.Contracts.Weather;
using Blazoraco.Web.Client;
using Blazoraco.Web.Client.Counters;
using Blazoraco.Web.Weather;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder().AddBackOffice().AddWebsite().AddDeliveryApi().AddComposers().Build();

builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddScoped<CounterState>();

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

if (!app.Environment.IsDevelopment())
{
    var policyCollection = new HeaderPolicyCollection()
        .AddContentSecurityPolicy(builder =>
        {
            builder.AddUpgradeInsecureRequests();
            builder.AddBaseUri().Self();
            builder.AddDefaultSrc().Self();
            builder
                .AddScriptSrc()
                .Self()
                .WasmUnsafeEval()
                // The following are needed for /umbraco backoffice
                .UnsafeInline()
                .UnsafeEval();
            builder
                .AddStyleSrc()
                .Self()
                // The following is needed for /umbraco backoffice and some of the demo templates
                .UnsafeInline();
            builder.AddImgSrc().Self().Data();
            builder.AddObjectSrc().None();
            builder.AddFrameAncestors().None();
            builder.AddFormAction().Self();
        })
        .AddReferrerPolicySameOrigin()
        .AddStrictTransportSecurityMaxAgeIncludeSubDomains()
        .AddContentTypeOptionsNoSniff()
        .AddPermissionsPolicy(builder =>
        {
            builder.AddAccelerometer().None();
            builder.AddAutoplay().None();
            builder.AddCamera().None();
            builder.AddEncryptedMedia().None();
            builder.AddFullscreen().None();
            builder.AddGeolocation().None();
            builder.AddGyroscope().None();
            builder.AddMagnetometer().None();
            builder.AddMicrophone().None();
            builder.AddMidi().None();
            builder.AddPayment().None();
            builder.AddPictureInPicture().None();
            builder.AddUsb().None();
        });

    app.UseSecurityHeaders(policyCollection);
}

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
