using System;
using System.Collections.Generic;

namespace MediRenew.ConsoleApp.DatabaseFirstEntities;

public partial class Authentication
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
