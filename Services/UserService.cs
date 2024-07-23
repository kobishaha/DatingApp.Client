using DatingApp.Client.Data;
using DatingApp.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Client.Services;

public class UserService(DataContext context) : IUserService
{
    private readonly DataContext _context = context;

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (user == null)
        {
            throw new KeyNotFoundException($"User with username '{username}' not found.");
        }

        return user;
    }


    public async Task<User> RegisterAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetOnlineUsersAsync()
    {
        return await _context.Users.ToListAsync(); // Replace with actual logic for online users
    }
}

public interface IUserService
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> RegisterAsync(User user);
    Task<List<User>> GetOnlineUsersAsync();
}
