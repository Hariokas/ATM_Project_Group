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
        public async Task<ActionResult<TransactionResultDto>> TransferMoney([FromBody] TransferMoneyRequest request)
        {
            var result = await transactionService.TransferMoney(request.SenderAccountId, request.ReceiverAccountId, request.Amount);
            if (result == null) return BadRequest("Transfer failed.");
            return Ok(result);
        }

        [HttpPost("DepositMoney")]
        public async Task<ActionResult<TransactionResultDto>> DepositMoney([FromBody] DepositWithdrawRequest request)
        {
            var result = await transactionService.DepositMoney(request.AccountId, request.Amount);
            if (result == null) return BadRequest("Deposit failed.");
            return Ok(result);
        }

        [HttpPost("WithdrawMoney")]
        public async Task<ActionResult<TransactionResultDto>> WithdrawMoney([FromBody] DepositWithdrawRequest request)
        {
            var result = await transactionService.WithdrawMoney(request.AccountId, request.Amount);
            if (result == null) return BadRequest("Withdrawal failed.");
            return Ok(result);
        }

    }
}
