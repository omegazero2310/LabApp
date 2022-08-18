using CommonClass.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiLab.DatabaseContext
{
    public class LabDbContext : DbContext
    {
        public DbSet<AdminStaff> AdminStaffs { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public LabDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
