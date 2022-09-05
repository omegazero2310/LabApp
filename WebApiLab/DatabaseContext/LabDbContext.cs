using CommonClass.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiLab.DatabaseContext
{
    public class LabDbContext : DbContext
    {
        /// <summary>
        /// Bảng danh sách nhân viên
        /// </summary>
        /// <value>
        /// The admin staffs.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public DbSet<AdminStaff> AdminStaffs { get; set; }
        /// <summary>
        /// Bảng thông tin đăng nhập phần mềm
        /// </summary>
        /// <value>
        /// The admin users.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public DbSet<AdminUser> AdminUsers { get; set; }
        /// <summary>
        /// Bảng chức danh
        /// </summary>
        /// <value>
        /// The admin parts.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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
            var user = new AdminUser();
            user.Id = Guid.NewGuid();
            user.UserName = "Admin";
            user.HashedPassword = "j3oEIiMWevilRp5y5/mzfETFHoGnBgrj23oQuiTNtRM=";
            user.Salt = "kPg2GW/IblRkqFtMPrhmbw==";
            user.IsResetPassword = false;
            user.IsActive = true;
            user.AccountStatus = CommonClass.Enums.AccountStatusOptions.Normal;
            user.DateCreated = DateTime.Now;
            user.UserCreated = "SeedSystem";
            user.UserModified = "SeedSystem";
            user.DateModified = DateTime.Now;
            user.ProfilePictureName = "";
            user.PhoneNumber = "09781231234";
            user.DisplayName = "Administrator";
            modelBuilder.Entity<AdminUser>().HasData(
                user
                );
        }
    }
}
