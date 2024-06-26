﻿namespace ATMProjectGroup.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public ICollection<Account> Accounts { get; set; } = default!;
}