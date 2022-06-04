using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMSRazorTest.Data;
using VMSRazorTest.Models;

namespace VMSRazorTest.Pages.User.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly VMSRazorTest.Data.VMSRazorTestContext _context;

        public DetailsModel(VMSRazorTest.Data.VMSRazorTestContext context)
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
