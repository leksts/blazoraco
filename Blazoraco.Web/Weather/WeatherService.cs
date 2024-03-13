namespace Blazoraco.Web.Weather;

using Blazoraco.Contracts.Weather;

public class WeatherService : IWeatherService
{
    public async Task<WeatherForecast[]?> GetForecastAsync(CancellationToken cancellationToken)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        var forecasts = Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast
                    {
                        Date = startDate.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    }
            )
            .ToArray();

        // Simulate async task and return the forecasts
        await Task.CompletedTask;
        return forecasts;
    }
}
