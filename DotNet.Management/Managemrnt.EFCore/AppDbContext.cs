using Management.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Entities;
using static Management.Domain.Permissions;
using System.Xml.Linq;

namespace Managemrnt.EFCore
{
    public class AppDbContext : DbContext
    {

        //public virtual ChangeTracker ChangeTracker
        //=> _changeTracker ??= InternalServiceProvider.GetRequiredService<IChangeTrackerFactory>().Create();

        private ICurrentUserContext? _currentUserContext;
        public ICurrentUserContext CurrentUserContext 
        {
            get
            {
                if(_currentUserContext == null && this is IInfrastructure<IServiceProvider> serviceProvider)
                {
                    _currentUserContext = serviceProvider.GetService<CurrentUserContext>();
                }
                return _currentUserContext;
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // TODO: soft delete query

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

            #region Seeder

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Code = UserManagement.GroupName, Name = "用户管理", SortOrder = 1 },
                new Permission { Code = UserManagement.Query, Name = "查询", SortOrder = 2,ParentCode = UserManagement.GroupName },
                new Permission { Code = UserManagement.Create, Name = "创建", SortOrder = 3, ParentCode = UserManagement.GroupName },
                new Permission { Code = UserManagement.Update, Name = "更新", SortOrder = 4, ParentCode = UserManagement.GroupName },
                new Permission { Code = UserManagement.Delete, Name = "删除", SortOrder = 5, ParentCode = UserManagement.GroupName },
                new Permission { Code = UserManagement.ChangeUserRole, Name = "设置用户角色", SortOrder = 6, ParentCode = UserManagement.GroupName },

                new Permission { Code = RoleManagement.GroupName, Name = "角色管理", SortOrder = 7 },
                new Permission { Code = RoleManagement.Query, Name = "查询", SortOrder = 8, ParentCode = RoleManagement.GroupName },
                new Permission { Code = RoleManagement.Create, Name = "创建", SortOrder = 9, ParentCode = RoleManagement.GroupName },
                new Permission { Code = RoleManagement.Update, Name = "更新", SortOrder = 10, ParentCode = RoleManagement.GroupName },
                new Permission { Code = RoleManagement.Delete, Name = "删除", SortOrder = 11, ParentCode = RoleManagement.GroupName },
                new Permission { Code = RoleManagement.ChangeRolePermission, Name = "设置角色权限", SortOrder = 12, ParentCode = RoleManagement.GroupName });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationProperties(entity);
                        break;
                    case EntityState.Modified:
                        SetModificationProperties(entity);
                        break;
                    case EntityState.Deleted:
                        SetDeletionProperties(entry);
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #region DbSet

        // public DbSet<User> Users => Set<User>(); 只读..
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion
        
        private void SetCreationProperties(object entity)
        {
            if (entity is ICreationAuditedObject creationAuditedObject)
            {
                creationAuditedObject.CreationTime = DateTime.Now;
                if (CurrentUserContext != null && CurrentUserContext.Id != null)
                {
                    creationAuditedObject.CreatorId = CurrentUserContext.Id;
                }
            }
        }

        private void SetModificationProperties(object entity)
        {
            if (entity is IAuditedObject auditedObject)
            {
                auditedObject.ModificationTime = DateTime.Now;
                if (CurrentUserContext != null && CurrentUserContext.Id != null)
                {
                    auditedObject.ModifierId = CurrentUserContext.Id;
                }
            }
        }

        private void SetDeletionProperties(EntityEntry entry)
        {
            if (entry is IFullAuditedObject fullAuditedObject && !fullAuditedObject.IsDeleted)
            {
                entry.State = EntityState.Modified;
                fullAuditedObject.IsDeleted = true;
                fullAuditedObject.DeletionTime = DateTime.Now;
                if (CurrentUserContext != null && CurrentUserContext.Id != null)
                {
                    fullAuditedObject.DeleterId = CurrentUserContext.Id;
                }
            }
        }
    }
}
