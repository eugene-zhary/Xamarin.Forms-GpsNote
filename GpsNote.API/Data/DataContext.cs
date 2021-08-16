using GpsNote.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GpsNote.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PinModel> Pins { get; set; }
    }
}
