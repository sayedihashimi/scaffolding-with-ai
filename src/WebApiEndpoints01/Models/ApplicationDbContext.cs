using Microsoft.EntityFrameworkCore;
using WebApiEndpoints01.Models;

namespace WebApiEndpoints01.Models {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; } = null!;
        // ...other DbSets...
    }
}
