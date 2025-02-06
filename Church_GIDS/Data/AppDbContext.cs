using Church_GIDS.Model;
using Microsoft.EntityFrameworkCore;

namespace Church_GIDS.Data
{
    public class AppDbContext : DbContext

    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Church> Churches { get; set; }
        public DbSet<Person> Persons { get; set; }


    }
}
