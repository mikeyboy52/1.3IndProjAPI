using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnviromentController : ControllerBase
{
    private readonly IEnviromentRepository _enviromentRepository;
    private readonly ILogger<EnviromentController> _logger;

    public EnviromentController(IEnviromentRepository enviromentRepository, ILogger<EnviromentController> logger)
    {
        _enviromentRepository = enviromentRepository;
        _logger = logger;
    }

    [HttpGet("{Email}", Name = "ReadEnviromenstFromEmail")]
    [Authorize]
    public async Task<ActionResult<Enviroment>> Get(string email)
    {
        // verkrijgen van Enviroments voor een gebruiker via de email
        var enviroment = await _enviromentRepository.ReadEnviroment(email);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    [HttpGet(Name = "ReadEnviromentFromNameFromEmail")]
    [Authorize]
    public async Task<ActionResult<Enviroment>> Get([FromQuery] string email, [FromQuery] string name)
    {
        // verkrijgen van enviroment via naam, van 1 gebruiker
        var enviroment = await _enviromentRepository.ReadEnviroment(email, name);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    [HttpPost(Name = "CreateEnviroment")]
    [Authorize]
    public async Task<ActionResult> Add([FromBody] Enviroment Enviroment)
    {
        // maken van een nieuw enviroment
        Enviroment.Id = Guid.NewGuid();

        var enviroment = await _enviromentRepository.InsertEnviroment(Enviroment);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateEnviroment")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Id, [FromBody] Enviroment newEnviroment)
    {
        // updaten van een enviroment vanuit enviromentId
        var existingEnviroment = await _enviromentRepository.ReadEnviroment(Id);

        if (existingEnviroment == null)
            return NotFound();

        await _enviromentRepository.UpdateEnviroment(newEnviroment);

        return Ok(newEnviroment);
    }

    [HttpDelete("{Id}", Name = "DeleteEnviroment")]
    [Authorize]
    public async Task<IActionResult> Update(Guid Id)
    {
        // deleten van een enviroment vanuit enviromentId
        Console.WriteLine(Id);
        var existingEnviroment = await _enviromentRepository.ReadEnviroment(Id);

        if (existingEnviroment == null)
        {
            return NotFound(); 
        }
            else
            {
                await _enviromentRepository.DeleteEnviroment(Id);
                return Ok();
            }
    }
}
