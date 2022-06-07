using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Entities;
using VMS.Repository;

namespace VMS.Web.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public IndexModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        public IList<Role> Role { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Role = await dbContext.Roles.ToListAsync();
        }
    }
}
