using Microsoft.EntityFrameworkCore;
using VideoVerhuur.Models;

namespace VideoVerhuur.Data
{
    public class SampleDBContext : DbContext
    {


        public DbSet<Verhuringen> Verhuringen { get; set; }
        public DbSet<Klanten> Klanten { get; set; }

        public DbSet<Films> Films { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public SampleDBContext() { }
        public SampleDBContext(DbContextOptions options) : base(options) { }


    }
}
