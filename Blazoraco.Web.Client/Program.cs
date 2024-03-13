using Blazoraco.Contracts.Weather;
using Blazoraco.Web.Client;
using Blazoraco.Web.Client.Counters;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);
builder.Services.AddSingleton<IWeatherService, ClientWeatherService>();
builder.Services.AddSingleton<CounterState>();

await builder.Build().RunAsync();
