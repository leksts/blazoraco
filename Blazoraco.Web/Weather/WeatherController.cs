namespace Blazoraco.Web.Weather;

using Blazoraco.Contracts.Weather;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

public class WeatherController(IWeatherService service) : UmbracoApiController
{
    private readonly IWeatherService service = service;

    [HttpGet]
    public async Task<WeatherForecast[]?> GetForecastAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        return await service.GetForecastAsync(cancellationToken);
    }
}
