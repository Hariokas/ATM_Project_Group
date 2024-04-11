using ATMProjectGroup.Models;
using ATMProjectGroup.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATMProjectGroup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    // GET: api/<UserController>
    // GET: api/<AccountController>
    [HttpGet("GetUsers")]
    public async Task<IEnumerable<User>> Get()
    {
        return await userService.GetAllUsersAsync();
    }

    // GET api/<AccountController>/5
    [HttpGet("GetUserByGuid")]
    public async Task<User> Get(Guid id)
    {
        return await userService.GetUserByIdAsync(id);
    }

    // POST api/<AccountController>
    [HttpPost("AddUser")]
    public void Post([FromBody] UserDto user)
    {
        userService.AddUserAsync(user);
    }

    // PUT api/<AccountController>/5
    [HttpPut("UpdateUser")]
    public void Put(Guid id, [FromBody] UserDto user)
    {
        userService.UpdateUserAsync(user);
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("DeleteUser")]
    public void Delete(Guid id)
    {
        userService.DeleteUserAsync(id);
    }
}