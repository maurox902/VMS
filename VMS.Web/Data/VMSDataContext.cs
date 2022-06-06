using Microsoft.EntityFrameworkCore;
using VMS.Web.Models;

namespace VMS.Web.Data
{
    public class VMSDataContext : DbContext
    {
        public VMSDataContext(DbContextOptions<VMSDataContext> options)
            : base(options)
        {
        }

        public DbSet<Users>? Users { get; set; }

        public DbSet<Role>? Role { get; set; }
    }
}
