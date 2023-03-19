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
    public class AppDbContext: DbContext
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
