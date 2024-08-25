using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {

        public DBContext() : base() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
