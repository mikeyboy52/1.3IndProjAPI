using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnviromentController : ControllerBase
{
    private readonly EnviromentRepository _enviromentRepository;
    private readonly ILogger<EnviromentController> _logger;

    public EnviromentController(EnviromentRepository enviromentRepository, ILogger<EnviromentController> logger)
    {
        _enviromentRepository = enviromentRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadEnviromentsForUser")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Enviroment>>> Get([FromBody] string Email)
    {
        var enviroments = await _enviromentRepository.ReadEnviroments(Email);
        return Ok(enviroments);
    }

    //[HttpGet(Name = "ReadEnviroments")]
    //[Authorize]
    //public async Task<ActionResult<IEnumerable<Enviroment>>> Get()
    //{
    //    var enviroments = await _enviromentRepository.ReadEnviroments();
    //    return Ok(enviroments);
    //}

    [HttpGet("{Id}", Name = "ReadEnviroment")]
    [Authorize]
    public async Task<ActionResult<Enviroment>> Get(Guid Id)
    {
        var enviroment = await _enviromentRepository.ReadEnviroment(Id);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    [HttpPost(Name = "CreateEnviroment")]
    [Authorize]
    public async Task<ActionResult> Add([FromBody] Enviroment Enviroment)
    {
        Enviroment.Id = Guid.NewGuid();

        var enviroment = await _enviromentRepository.InsertEnviroment(Enviroment);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateEnviroment")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Id, Enviroment newEnviroment)
    {
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
        var existingEnviroment = await _enviromentRepository.ReadEnviroment(Id);

        if (existingEnviroment == null)
            return NotFound();

        await _enviromentRepository.DeleteEnviroment(Id);

        return Ok();
    }
}
