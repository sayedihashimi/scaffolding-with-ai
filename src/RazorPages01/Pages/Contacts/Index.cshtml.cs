using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPages01.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Contact> Contacts { get; set; } = new List<Contact>();
        public async Task OnGetAsync()
        {
            Contacts = await _context.Contacts.AsNoTracking().ToListAsync();
        }
    }
}