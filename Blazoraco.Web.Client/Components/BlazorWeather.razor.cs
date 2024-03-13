namespace Blazoraco.Web.Client.Components;

using Blazoraco.Contracts.Weather;
using Microsoft.AspNetCore.Components;

public sealed partial class BlazorWeather : IDisposable
{
    private WeatherForecast[]? forecasts;
    private PersistingComponentStateSubscription persistingSubscription;

    [Inject]
    public required IWeatherService WeatherService { get; set; }

    [Inject]
    public required PersistentComponentState ApplicationState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        persistingSubscription = ApplicationState.RegisterOnPersisting(() =>
        {
            ApplicationState.PersistAsJson(nameof(forecasts), forecasts);
            return Task.CompletedTask;
        });

        forecasts = await GetForecasts(
            async () => await WeatherService.GetForecastAsync(CancellationToken.None)
        );
    }

    private async Task<WeatherForecast[]?> GetForecasts(Func<Task<WeatherForecast[]?>> action)
    {
        if (
            ApplicationState.TryTakeFromJson<WeatherForecast[]>(
                nameof(forecasts),
                out var restoredForecasts
            )
        )
        {
            forecasts = restoredForecasts;
        }
        else
        {
            forecasts = await action();
        }

        return forecasts;
    }

    void IDisposable.Dispose() => persistingSubscription.Dispose();
}
