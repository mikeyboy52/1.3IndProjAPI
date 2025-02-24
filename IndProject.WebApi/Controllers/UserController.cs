using IndProject.WebApi.Models;
using IndProject.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IndProject.WebApi.Controllers;

[ApiController]
[Route("Users")]
public class UserController: ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(UserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadUsers")]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var Users = await _userRepository.ReadUsers();
        return Ok(Users);
    }

    [HttpGet("{Id}", Name = "ReadUser")]
    public async Task<ActionResult<User>> Get(Guid Id)
    {
        var user = await _userRepository.ReadUser(Id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Add(User user)
    {
        user.Id = Guid.NewGuid();

        var createdWeatherForecast = await _userRepository.InsertUser(user);
        return Created();
    }

    [HttpPut("{Id}", Name = "UpdateUser")]
    public async Task<ActionResult> Update(Guid Id, User newUser)
    {
        var existingUser = await _userRepository.ReadUser(Id);

        if (existingUser == null)
            return NotFound();

        await _userRepository.UpdateUser(newUser);

        return Ok(newUser);
    }

    [HttpDelete("{Id}", Name = "DeleteUser")]
    public async Task<IActionResult> Update(Guid Id)
    {
        var existingUser = await _userRepository.ReadUser(Id);

        if (existingUser == null)
            return NotFound();

        await _userRepository.DeleteUser(Id);

        return Ok();
    }

}
