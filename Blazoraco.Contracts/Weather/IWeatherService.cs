namespace Blazoraco.Contracts.Weather;

public interface IWeatherService
{
    Task<WeatherForecast[]?> GetForecastAsync(CancellationToken cancellationToken);
}
