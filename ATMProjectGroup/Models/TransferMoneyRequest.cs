namespace ATMProjectGroup.Models;

public class TransferMoneyRequest
{
    public Guid SenderAccountId { get; set; } = default!;
    public Guid ReceiverAccountId { get; set; } = default!;
    public decimal Amount { get; set; } = 0;
}