namespace ATMProjectGroup.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}