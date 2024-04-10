namespace ATMProjectGroup.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = default!;
    public Guid FromAccountId { get; set; }
    public virtual Account FromAccount { get; set; } = default!;
    public Guid ToAccountId { get; set; }
    public virtual Account ToAccount { get; set; } = default!;
    public TransactionType Type { get; set; }
}