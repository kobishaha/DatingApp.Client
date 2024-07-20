using Microsoft.EntityFrameworkCore;
using DatingApp.Client.Models;

namespace DatingApp.Client.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
    }
}
