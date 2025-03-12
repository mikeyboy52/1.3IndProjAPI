using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class Object2DController : ControllerBase
{
    private readonly IObjectRepository _objectRepository;
    private readonly ILogger<Object2DController> _logger;

    public Object2DController(IObjectRepository objectRepository, ILogger<Object2DController> logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
    }

    [HttpGet("{EnviromentId}", Name = "ReadObjectsFromEnviroment")]
    [Authorize]
    public async Task<ActionResult<Object2D>> Get(Guid EnviromentId)
    {
        // haalt de objecten van een enviroment op
        var enviroment = await _objectRepository.ReadObjectFromEnviroment(EnviromentId);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    [HttpPost(Name = "CreateObject")]
    [Authorize]
    public async Task<ActionResult> Add([FromBody] Object2D object2D)
    {
        // maakt een nieuw object aan
        object2D.Id = Guid.NewGuid();
        var objects = await _objectRepository.InsertObject(object2D);
        return Created();
    }
}
