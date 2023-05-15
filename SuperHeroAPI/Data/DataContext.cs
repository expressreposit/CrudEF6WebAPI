using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    // run this cmd for EF core: dotnet tool install --global dotnet-ef --version 6.*
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
