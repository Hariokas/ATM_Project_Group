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
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await userService.GetAllUsersAsync();
    }

    // GET api/<AccountController>/5
    [HttpGet("{id}")]
    public async Task<User> Get(Guid id)
    {
        return await userService.GetUserByIdAsync(id);
    }

    // POST api/<AccountController>
    [HttpPost]
    public void Post([FromBody] User user)
    {
        userService.AddUserAsync(user);
    }

    // PUT api/<AccountController>/5
    [HttpPut("{id}")]
    public void Put(Guid id, [FromBody] User user)
    {
        userService.UpdateUserAsync(user);
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        userService.DeleteUserAsync(id);
    }
}