namespace DatingApp.Client.Models;

public class AuthResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; } // Add this line
}
