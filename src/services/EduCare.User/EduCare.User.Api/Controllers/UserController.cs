using System.ComponentModel.DataAnnotations;
using EduCare.User.Core.Abstract.Repository;
using EduCare.User.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EduCare.User.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(int page = 0, int count = 10)
    {
        var users = await _userRepository.GetAll(page * count, count);
        return Ok(users);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userRepository.Find(id);
        if (user == null) return NotFound(new { message = $"User specified Id: {id} does not exist" });
        return Ok(user);
    }

    [HttpGet("{email}", Name = "GetUserByEmail")]
    public async Task<IActionResult> GetUserByEmail([EmailAddress] string email)
    {
        var user = await _userRepository.FindByEmail(email);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserDto userDto)
    {
        // check user does exist
        var userExist = await _userRepository.IsExist(userDto.Email);
        if (userExist) return BadRequest(new { message = $"{userDto.Email} does exist." });
        // create user if user does not exist
        var user = new User.Core.Entities.User
        {
            Email = userDto.Email,
            FullName = userDto.FullName
        };
        // save user  
        await _userRepository.Add(user);
        var result = await _userRepository.SaveChangesAsync();
        // check result
        if (result <= 0) return BadRequest();

        return CreatedAtRoute(nameof(GetUserByEmail), new { email = user.Email }, userDto);
    }
}