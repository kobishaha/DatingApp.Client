namespace DatingApp.Client.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}