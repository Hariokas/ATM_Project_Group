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
        [HttpPost("TransferMoney")]
        public async Task<Transaction> TransferMoney(TransferMoneyRequest request)
        {
            return await transactionService.TransferMoney(request.Sender, request.Receiver, request.Amount);
        }

        [HttpPost("DepositMoney")]
        public async Task<Transaction> DepositMoney([FromBody] Account account, decimal amount)
        {
            return await transactionService.DepositMoney(account, amount);
        }

        [HttpPost("WithdrawMoney")]
        public async Task<Transaction> WithdrawMoney([FromBody] Account account, decimal amount)
        {
            return await transactionService.WithdrawMoney(account, amount);
        }
    }
}
