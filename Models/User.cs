using System.ComponentModel.DataAnnotations;

namespace DatingApp.Client.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NickName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; } // Add this line
}
