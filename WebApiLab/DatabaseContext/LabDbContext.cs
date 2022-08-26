using CommonClass.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiLab.DatabaseContext
{
    public class LabDbContext : DbContext
    {
        public DbSet<AdminStaff> AdminStaffs { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminParts> AdminParts { get; set; }
        public LabDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminParts>().HasData(
                new AdminParts { PartID = 1, PartName = "Nhân viên", DateCreated = DateTime.Now, DateModified = DateTime.Now, UserCreated = "Seed", UserModified = "Seed" },
                new AdminParts { PartID = 2, PartName = "Trưởng phòng", DateCreated = DateTime.Now, DateModified = DateTime.Now, UserCreated = "Seed", UserModified = "Seed" },
                new AdminParts { PartID = 3, PartName = "Giám đốc", DateCreated = DateTime.Now, DateModified = DateTime.Now, UserCreated = "Seed", UserModified = "Seed" }
                );
        }
    }
}
