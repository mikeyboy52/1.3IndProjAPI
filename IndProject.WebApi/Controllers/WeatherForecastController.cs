using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("WeatherForecasts")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastRepository _weatherForecastRepository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(WeatherForecastRepository weatherForecastRepository, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadUsers")]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var weatherForecasts = await _weatherForecastRepository.ReadAsync();
        return Ok(weatherForecasts);
    }

    [HttpGet("{Id}", Name = "ReadUser")]
    public async Task<ActionResult<User>> Get(Guid UserId)
    {
        var weatherForeCast = await _weatherForecastRepository.ReadAsync(UserId);
        if (weatherForeCast == null)
            return NotFound();

        return Ok(weatherForeCast);
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Add(User user)
    {
        user.Id = Guid.NewGuid();

        var createdWeatherForecast = await _weatherForecastRepository.InsertAsync(user);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateUser")]
    public async Task<ActionResult> Update(Guid Id, User newUser)
    {
        var existingWeatherForecast = await _weatherForecastRepository.ReadAsync(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _weatherForecastRepository.UpdateAsync(newUser);

        return Ok(newUser);
    }

    [HttpDelete("{Id}", Name = "DeleteUser")]
    public async Task<IActionResult> Update(Guid Id)
    {
        var existingWeatherForecast = await _weatherForecastRepository.ReadAsync(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _weatherForecastRepository.DeleteAsync(Id);

        return Ok();
    }

}
