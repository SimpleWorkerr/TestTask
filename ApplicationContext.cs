using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace TestTask
{
    public class ApplicationContext: DbContext
    {
        public DbSet<ArrayDb> Arrays { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword");
        }
    }
}
