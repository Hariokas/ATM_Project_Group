namespace ATMProjectGroup.Models;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public TransactionType Type { get; set; }
}