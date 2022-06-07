using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Entities;
using VMS.Repository;

namespace VMS.Web.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public DeleteModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        [BindProperty] public Role Role { get; set; } = default!;

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

            Role = role;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await dbContext.Roles.FindAsync(id);

            if (role != null)
            {
                Role = role;
                dbContext.Roles.Remove(Role);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}