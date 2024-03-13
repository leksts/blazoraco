using Blazoraco.Contracts.Weather;
using Blazoraco.Web.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);
builder.Services.AddSingleton<IWeatherService, ClientWeatherService>();

await builder.Build().RunAsync();
