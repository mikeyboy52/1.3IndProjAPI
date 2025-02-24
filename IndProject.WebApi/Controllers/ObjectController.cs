using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("Objects")]
public class ObjectController : ControllerBase
{
    private readonly ObjectRepository _objectRepository;
    private readonly ILogger<ObjectController> _logger;

    public ObjectController(ObjectRepository objectRepository, ILogger<ObjectController> logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadObjects")]
    public async Task<ActionResult<IEnumerable<Object2D>>> Get()
    {
        var enviroments = await _objectRepository.ReadObjects();
        return Ok(enviroments);
    }

    [HttpGet("{Id}", Name = "ReadObject")]
    public async Task<ActionResult<Object2D>> Get(Guid Id)
    {
        var Object = await _objectRepository.ReadObject(Id);
        if (Object == null)
            return NotFound();

        return Ok(Object);
    }

    [HttpPost(Name = "CreateObject")]
    public async Task<ActionResult> Add(Object2D object2D)
    {
        object2D.Id = Guid.NewGuid();

        var objects = await _objectRepository.InsertObject(object2D);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateObject")]
    public async Task<ActionResult> Update(Guid Id, Object2D newObject)
    {
        var existingWeatherForecast = await _objectRepository.ReadObject(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _objectRepository.UpdateObject(newObject);

        return Ok(newObject);
    }

    [HttpDelete("{Id}", Name = "DeleteObject")]
    public async Task<IActionResult> Update(Guid Id)
    {
        var existingWeatherForecast = await _objectRepository.ReadObject(Id);

        if (existingWeatherForecast == null)
            return NotFound();

        await _objectRepository.DeleteObject(Id);

        return Ok();
    }

}
