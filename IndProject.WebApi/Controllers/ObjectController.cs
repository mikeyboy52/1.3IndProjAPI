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
    private readonly Object2DRepository _objectRepository;
    private readonly ILogger<Object2DController> _logger;

    public Object2DController(Object2DRepository objectRepository, ILogger<Object2DController> logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadObjects")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Object2D>>> Get()
    {
        var enviroments = await _objectRepository.ReadObjects();
        return Ok(enviroments);
    }

    [HttpGet("{EnviromentId}", Name = "ReadObjectsFromEnviroment")]
    [Authorize]
    public async Task<ActionResult<Object2D>> Get(Guid EnviromentId)
    {
        var enviroment = await _objectRepository.ReadObjectFromEnviroment(EnviromentId);
        if (enviroment == null)
            return NotFound();

        return Ok(enviroment);
    }

    //[HttpGet("{Id}", Name = "ReadObject")]
    //public async Task<ActionResult<Object2D>> Get(Guid Id)
    //{
    //    var Object = await _objectRepository.ReadObject(Id);
    //    if (Object == null)
    //        return NotFound();

    //    return Ok(Object);
    //}

    [HttpPost(Name = "CreateObject")]
    [Authorize]
    public async Task<ActionResult> Add([FromBody] Object2D object2D)
    {
        object2D.Id = Guid.NewGuid();
        var objects = await _objectRepository.InsertObject(object2D);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateObject")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Id, [FromBody] Object2D newObject)
    {
        var existingWeatherForecast = await _objectRepository.ReadObject(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _objectRepository.UpdateObject(newObject);

        return Ok(newObject);
    }

    [HttpDelete("{Id}", Name = "DeleteObject")]
    [Authorize]
    public async Task<IActionResult> Update(Guid Id)
    {
        var existingWeatherForecast = await _objectRepository.ReadObject(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _objectRepository.DeleteObject(Id);

        return Ok();
    }

}
