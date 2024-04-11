namespace ATMProjectGroup.Models;

public class DepositWithdrawRequest
{
    public Guid AccountId { get; set; } = default!;
    public decimal Amount { get; set; } = 0;
}