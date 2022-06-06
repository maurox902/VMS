using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Web.Data;
using VMS.Web.Models;

namespace VMS.Web.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly VMSDataContext _context;

        public DetailsModel(VMSDataContext context)
        {
            _context = context;
        }

      public Role Role { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Role == null)
            {
                return NotFound();
            }

            var role = await _context.Role.FirstOrDefaultAsync(m => m.role_id == id);
            if (role == null)
            {
                return NotFound();
            }
            else 
            {
                Role = role;
            }
            return Page();
        }
    }
}
