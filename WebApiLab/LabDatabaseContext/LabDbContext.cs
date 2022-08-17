using CommonClass.Enums;
using CommonClass.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiLab.LabDatabaseContext
{
    public class LabDbContext : DbContext
    {
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public IConfiguration _configuration;
        public LabDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasKey("UserName");
            modelBuilder.Entity<UserInfo>().HasIndex("Email").IsUnique();
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.Property(e => e.UserName)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired(true);

                entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(true);

                entity.Property(e => e.Gender)
                .IsRequired(true);

                entity.Property(e => e.CreatedDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(true);

                entity.Property(e => e.Status)
                .HasDefaultValue(AccountStatusOption.Using)
                .IsRequired(true);

                entity.Property(e => e.IsNew)
                .HasDefaultValue(true)
                .IsRequired(true);

                entity.Property(e => e.TotalTrips)
                .HasDefaultValue(0)
                .IsRequired(true);

            });

            modelBuilder.Entity<UserLogin>().HasKey("UserName");
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.HashedPassword)
                    .HasMaxLength(64)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.Salt)
                    .HasMaxLength(64)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.IsResetPassword)
                    .HasDefaultValue(false)
                    .IsRequired(true);

                entity.Property(e => e.DateModified)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired(true);
            });

            modelBuilder.Entity<UserNotification>().HasKey("NotificationId");
            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.Property(e => e.NotificationId)
                    .IsRequired(true);

                entity.Property(e => e.UserName)
                    .IsRequired(true)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired(true);

                entity.Property(e => e.NotificationType)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue(NotificationTypeOption.System)
                    .IsRequired(true);

                entity.Property(e => e.Title)
                    .HasDefaultValue(NotificationTypeOption.System.ToString())
                    .HasMaxLength(500)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.Content)
                    .HasDefaultValue(NotificationTypeOption.System.ToString())
                    .HasMaxLength(8000)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity.Property(e => e.IsReaded)
                    .HasDefaultValue(false)
                    .IsRequired(true);

            });
        }
    }
}
