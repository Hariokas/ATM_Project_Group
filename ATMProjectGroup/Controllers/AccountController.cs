using ATMProjectGroup.Models;
using ATMProjectGroup.Services;
using ATMProjectGroup.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATMProjectGroup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Post([FromBody] Account account)
    {
        return await _accountService.AddAccountAsync(account);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccountById(Guid id)
    {
        return await _accountService.GetAccountByIdAsync(id);
    }


    [HttpGet("{id}")]
    public async Task<IEnumerable<Account>> GetAccountsFromUser(Guid userId)
    {
        return await _accountService.GetAccountsFromUser(userId);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> Put(Guid id, [FromBody] Account account)
    {
        return await _accountService.UpdateAccountAsync(account);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> Delete(Guid id)
    {
        return await _accountService.DeleteAccountAsync(id);
    }


}