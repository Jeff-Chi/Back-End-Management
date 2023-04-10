using Management.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Managemrnt.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identity

            modelBuilder.Entity<User>(b =>
            {
                // properties
                b.Property(u => u.UserName).HasMaxLength(Constants.MaxNameLength).IsRequired();
                b.Property(u => u.NickName).HasMaxLength(Constants.MaxNameLength).IsRequired();
                b.Property(u => u.Password).HasMaxLength(Constants.MaxNameLength).IsRequired();
                b.Property(u => u.Avatars).HasMaxLength(Constants.MaxPathLength);
                b.Property(u => u.MobilePhone).HasMaxLength(Constants.MaxMobilePhoneLength);
                b.Property(u => u.Email).HasMaxLength(Constants.MaxEmailLength);

                // relations

                // indexs
                b.HasIndex(u => u.UserName);
                b.HasIndex(u => u.Email);
            });

            modelBuilder.Entity<Role>(b =>
            {
                // properties
                b.Property(r => r.Name).HasMaxLength(Constants.MaxNameLength).IsRequired();
                b.Property(r => r.Code).HasMaxLength(Constants.MaxCode).IsRequired();
                b.Property(r => r.Description).HasMaxLength(Constants.MaxDescriptionLength);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                // Key
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                // properties
                b.Property(ur => ur.UserId).IsRequired();
                b.Property(ur => ur.RoleId).IsRequired();

                // relations
                b.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                b.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<Permission>(b =>
            {
                b.HasKey(p => p.Code);

                // properties
                b.Property(u => u.Name).HasMaxLength(Constants.MaxNameLength).IsRequired();
                b.Property(u => u.Code).HasMaxLength(Constants.MaxCode).IsRequired();
                b.Property(u => u.ParentCode).HasMaxLength(Constants.MaxCode);
                b.Property(u => u.SortOrder).IsRequired();
            });

            modelBuilder.Entity<RolePermission>(b =>
            {
                // Key
                b.HasKey(rp => new { rp.RoleId, rp.PermissionCode });

                // relations
                b.HasOne(up => up.Role).WithMany(r => r.RolePermissions).HasForeignKey(up => up.RoleId);
                b.HasOne(up => up.Permission).WithMany().HasForeignKey(up => up.PermissionCode);
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }


        #region DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
