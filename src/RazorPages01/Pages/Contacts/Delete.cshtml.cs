using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages01.Data;
using RazorPages01;
using System.Threading.Tasks;

namespace RazorPages01.Pages.Contacts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Contact? Contact { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (Contact == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Contact == null)
                return NotFound();
            var contact = await _context.Contacts.FindAsync(Contact.Id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
