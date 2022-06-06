using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Web.Data;
using VMS.Web.Models;

namespace VMS.Web.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly VMSDataContext _context;

        public IndexModel(VMSDataContext context)
        {
            _context = context;
        }

        public IList<Role> Role { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Role != null)
            {
                Role = await _context.Role.ToListAsync();
            }
        }
    }
}
