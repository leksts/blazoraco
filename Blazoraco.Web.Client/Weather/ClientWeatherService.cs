namespace Blazoraco.Web.Client;

using System.Net.Http.Json;
using Blazoraco.Contracts.Weather;

public class ClientWeatherService(HttpClient httpClient) : IWeatherService
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<WeatherForecast[]?> GetForecastAsync(CancellationToken cancellationToken)
    {
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>(
            "/umbraco/api/weather/getforecast",
            cancellationToken: cancellationToken
        );
    }
}
