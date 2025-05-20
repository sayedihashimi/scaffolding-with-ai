using Microsoft.EntityFrameworkCore;
using MVCWeb01.Models;

namespace MVCWeb01.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}