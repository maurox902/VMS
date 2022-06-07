using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VMS.Repository;

namespace VMS.Web.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public CreateModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Entities.User Users { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            dbContext.Users.Add(Users);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
