using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VMS.Entities;
using VMS.Repository;

namespace VMS.Web.Pages.Roles
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

        [BindProperty] public Role Role { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            dbContext.Roles.Add(Role);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}