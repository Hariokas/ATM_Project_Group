using ATMProjectGroup.Models;
using ATMProjectGroup.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATMProjectGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        [HttpPost]
        public async Task<Transaction> TransferMoney(Account sender, Account receiver, decimal amount)
        {
            return await transactionService.TransferMoney(sender, receiver, amount);
        }

        [HttpPost]
        public async Task<Transaction> DepositMoney(Account account, decimal amount)
        {
            return await transactionService.DepositMoney(account, amount);
        }

        [HttpPost]
        public async Task<Transaction> WithdrawMoney(Account account, decimal amount)
        {
            return await transactionService.WithdrawMoney(account, amount);
        }
    }
}
