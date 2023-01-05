using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using RentACarSample.Common;

namespace RentACarSample.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
#if DEBUG
            if (Database.CanConnect())
            {
                if (Members.Any() == false)
                {
                    Members.Add(new Member
                    {
                        Username = "admin",
                        Password = "admin123".MD5(),
                        Roles = new List<MemberRole> { new MemberRole { Name = Roles.Admin } }
                    });

                    SaveChanges();
                }
            }
#else
            if (Database.GetPendingMigrations().Count() > 0)
            {
                Database.Migrate();
            }
#endif
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SubBrand> SubBrands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }
}
