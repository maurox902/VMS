using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VMSRazorTest.Models;

namespace VMSRazorTest.Data
{
    public class VMSRazorTestContext : DbContext
    {
        public VMSRazorTestContext (DbContextOptions<VMSRazorTestContext> options)
            : base(options)
        {
        }

        public DbSet<VMSRazorTest.Models.Users>? Users { get; set; }

        public DbSet<VMSRazorTest.Models.Role>? Role { get; set; }
    }
}
