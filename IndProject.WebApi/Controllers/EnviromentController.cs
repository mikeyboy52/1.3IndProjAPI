using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("Users")]
public class EnviromentController : ControllerBase
{
    private readonly EnviromentRepository _enviromentRepository;
    private readonly ILogger<EnviromentController> _logger;

    public EnviromentController(EnviromentRepository enviromentRepository, ILogger<EnviromentController> logger)
    {
        _enviromentRepository = enviromentRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadUsers")]
    public async Task<ActionResult<IEnumerable<Enviroment>>> Get()
    {
        var enviroments = await _enviromentRepository.ReadEnviroments();
        return Ok(enviroments);
    }

    [HttpGet("{Id}", Name = "ReadUser")]
    public async Task<ActionResult<Enviroment>> Get(Guid Id)
    {
        var enviroment = await _enviromentRepository.ReadEnviroment(Id);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Add(Enviroment Enviroment)
    {
        Enviroment.Id = Guid.NewGuid();

        var enviroment = await _enviromentRepository.InsertEnviroment(Enviroment);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateUser")]
    public async Task<ActionResult> Update(Guid Id, Enviroment newEnviroment)
    {
        var existingWeatherForecast = await _enviromentRepository.ReadEnviroment(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _enviromentRepository.UpdateEnviroment(newEnviroment);

        return Ok(newEnviroment);
    }

    [HttpDelete("{Id}", Name = "DeleteUser")]
    public async Task<IActionResult> Update(Guid Id)
    {
        var existingWeatherForecast = await _enviromentRepository.ReadEnviroment(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _enviromentRepository.DeleteEnviroment(Id);

        return Ok();
    }

}
