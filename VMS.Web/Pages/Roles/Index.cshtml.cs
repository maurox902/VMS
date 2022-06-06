using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Web.Data;
using VMSRazorTest.Models;

namespace VMSRazorTest.Pages.User.Roles
{
    public class IndexModel : PageModel
    {
        private readonly VMSRazorTestContext _context;

        public IndexModel(VMSRazorTestContext context)
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
