using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Repository;

namespace VMS.Web.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public DeleteModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        [BindProperty]
      public Entities.User Users { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await dbContext.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (users == null)
            {
                return NotFound();
            }
            else 
            {
                Users = users;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await dbContext.Users.FindAsync(id);

            if (users != null)
            {
                Users = users;
                dbContext.Users.Remove(Users);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
