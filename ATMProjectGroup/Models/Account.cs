namespace ATMProjectGroup.Models;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = default!;
    public decimal Balance { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual ICollection<Transaction> OutgoingTransactions { get; set; } = default!;
    public virtual ICollection<Transaction> IncomingTransactions { get; set; } = default!;
}