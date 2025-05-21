using Microsoft.EntityFrameworkCore;
using WebApiControllers.Models;

namespace WebApiControllers {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        // ...other DbSets...
    }
}
