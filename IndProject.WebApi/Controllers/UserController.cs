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
    public async Task<ActionResult<User>> Get(string Username)
    {
        var user = await _userRepository.ReadUser(Username);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Add(User user)
    {
        var createdUser = await _userRepository.InsertUser(user);
        return Created();
    }

    [HttpPut("{username}", Name = "UpdateUser")]
    public async Task<ActionResult> Update(string username, User newUser)
    {
        var existingUser = await _userRepository.ReadUser(username);

        if (existingUser == null)
            return NotFound();

        await _userRepository.UpdateUser(newUser);

        return Ok(newUser);
    }

    [HttpDelete("{Id}", Name = "DeleteUser")]
    public async Task<IActionResult> Update(string Username)
    {
        var existingUser = await _userRepository.ReadUser(Username);

        if (existingUser == null)
            return NotFound();

        await _userRepository.DeleteUser(Username);

        return Ok();
    }

}
