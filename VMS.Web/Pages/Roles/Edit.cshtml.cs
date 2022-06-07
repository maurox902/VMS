using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Entities;
using VMS.Repository;

namespace VMS.Web.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public EditModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        [BindProperty]
        public Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || dbContext.Roles == null)
            {
                return NotFound();
            }

            var role =  await dbContext.Roles.FirstOrDefaultAsync(m => m.RolId == id);
            if (role == null)
            {
                return NotFound();
            }
            Role = role;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            dbContext.Attach(Role).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(Role.RolId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoleExists(int id)
        {
          return (dbContext.Roles?.Any(e => e.RolId == id)).GetValueOrDefault();
        }
    }
}
