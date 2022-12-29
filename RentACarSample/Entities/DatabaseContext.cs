using Microsoft.EntityFrameworkCore;

namespace RentACarSample.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
    }
}
