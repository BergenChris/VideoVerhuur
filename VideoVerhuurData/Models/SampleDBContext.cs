using Microsoft.EntityFrameworkCore;
using VideoVerhuurVDAB.Models;

namespace VideoVerhuurData.Models
{
    public class SampleDBContext : DbContext
    {


        public DbSet<Verhuringen> Verhuringen { get; set; }
        public DbSet<Klanten> KlantenItems { get; set; }

        public DbSet<Films> FilmsItems { get; set; }

        public DbSet<Genres> GenresItems { get; set; }

        public SampleDBContext() { }
        public SampleDBContext(DbContextOptions options) : base(options) { }

       
    }
}
