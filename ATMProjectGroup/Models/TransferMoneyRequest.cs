namespace ATMProjectGroup.Models;

public class TransferMoneyRequest
{
    public Account Sender { get; set; } = new Account();
    public Account Receiver { get; set; } = new Account();
    public decimal Amount { get; set; } = 0;
}