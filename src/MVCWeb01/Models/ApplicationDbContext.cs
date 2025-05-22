using Microsoft.EntityFrameworkCore;

namespace MVCWeb01.Models {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}