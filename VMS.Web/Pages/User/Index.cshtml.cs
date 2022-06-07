using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VMS.Repository;

namespace VMS.Web.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly VMSDatabaseContext dbContext;

        public IndexModel(VMSDatabaseContext vmsDatabaseContext)
        {
            this.dbContext = vmsDatabaseContext;
        }

        public IList<Entities.User> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await dbContext.Users.ToListAsync();
        }
    }
}
