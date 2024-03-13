namespace Blazoraco.Web.Client.Components;

using Blazoraco.Contracts.Weather;
using Microsoft.AspNetCore.Components;

public sealed partial class BlazorWeatherTable
{
    [Parameter]
    public required WeatherForecast[] Forecasts { get; set; }
}
