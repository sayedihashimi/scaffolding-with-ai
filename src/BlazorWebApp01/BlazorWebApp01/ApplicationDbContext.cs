using Microsoft.EntityFrameworkCore;
using BlazorWebApp01.Models;

namespace BlazorWebApp01 {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
