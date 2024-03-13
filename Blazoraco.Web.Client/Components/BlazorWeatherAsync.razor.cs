namespace Blazoraco.Web.Client.Components;

using Blazoraco.Contracts.Weather;
using Microsoft.AspNetCore.Components;

public sealed partial class BlazorWeatherAsync
{
    private WeatherForecast[]? forecasts;

    [Inject]
    public required IWeatherService WeatherService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OnAfterFirstRenderAsync();
        }
    }

    private async Task OnAfterFirstRenderAsync()
    {
        forecasts = await WeatherService.GetForecastAsync(CancellationToken.None);
        StateHasChanged();
    }
}
