using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Repository;

namespace VMS.Web.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public DetailsModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        public Entities.User Users { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || dbContext.Users == null)
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
    }
}
