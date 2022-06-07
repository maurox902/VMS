using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Entities;
using VMS.Repository;

namespace VMS.Web.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public DetailsModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

      public Role Role { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await dbContext.Roles.FirstOrDefaultAsync(m => m.RolId == id);
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
