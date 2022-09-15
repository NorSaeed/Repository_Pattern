using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LibContext : DbContext
    {
        public LibContext()
        {
            
        }
        public LibContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var connectionStrings = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionStrings);
        }
    }
}