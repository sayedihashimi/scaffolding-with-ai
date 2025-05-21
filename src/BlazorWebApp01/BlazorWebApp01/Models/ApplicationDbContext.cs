using Microsoft.EntityFrameworkCore;
using BlazorWebApp01.Models;
// ...existing code...
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Contact> Contacts { get; set; }
    // ...other DbSets...
}
// ...existing code...