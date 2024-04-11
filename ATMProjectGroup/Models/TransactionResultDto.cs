namespace ATMProjectGroup.Models;

public class TransactionResultDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = default!;
    public TransactionType Type { get; set; }
}