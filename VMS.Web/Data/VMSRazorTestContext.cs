using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VMSRazorTest.Models;

namespace VMS.Web.Data
{
    public class VMSRazorTestContext : DbContext
    {
        public VMSRazorTestContext(DbContextOptions<VMSRazorTestContext> options)
            : base(options)
        {
        }

        public DbSet<Users>? Users { get; set; }

        public DbSet<Role>? Role { get; set; }
    }
}
