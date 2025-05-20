using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages01.Data;
using RazorPages01;
using System.Threading.Tasks;

namespace RazorPages01.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Contact? Contact { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (Contact == null)
                return NotFound();
            return Page();
        }
    }
}
