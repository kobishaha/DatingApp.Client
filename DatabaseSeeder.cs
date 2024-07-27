using Bogus;
using DatingApp.Client.Data;
using DatingApp.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Client
{
    public class DatabaseSeeder
    {
        private readonly DataContext _context;

        public DatabaseSeeder(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.NickName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword("password123"))
                .RuleFor(u => u.Username, (f, u) => u.Email);

            var users = faker.Generate(count);

            _context.Users.AddRange(users);
            _context.SaveChanges();
        }
    }
}
