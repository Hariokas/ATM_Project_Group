namespace ATMProjectGroup.Models
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
