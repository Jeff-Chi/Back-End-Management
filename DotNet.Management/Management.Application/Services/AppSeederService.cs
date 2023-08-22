using Management.Domain;
using Management.Infrastructure;
using Managemrnt.EFCore;
using Microsoft.EntityFrameworkCore;
using static Management.Domain.Permissions;

namespace Management.Application
{
    public class AppSeederService : IAppSeederService
    {
        private readonly AppDbContext _dbContext;
        public AppSeederService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Test()
        {
            Console.WriteLine("this is test");
        }

        public async Task SeedAsync()
        {
            await InitRolesAsync();
            await InitUserAsync();
        }

        private async Task InitRolesAsync()
        {
            if (await _dbContext.Roles.AnyAsync())
            {
                return;
            }
            Role role = new Role(IdGenerator.GenerateNewId())
            {
                Name = "管理员",
                Code = "Admin",
                IsSuperAdmin = true
            };

            role.RolePermissions = new List<RolePermission>()
            {
                new RolePermission(role.Id,UserManagement.GroupName),
                new RolePermission(role.Id,UserManagement.Query),
                new RolePermission(role.Id,UserManagement.Create),
                new RolePermission(role.Id,UserManagement.Update),
                new RolePermission(role.Id,UserManagement.Delete),
                new RolePermission(role.Id,UserManagement.ChangeUserRole),
                new RolePermission(role.Id,RoleManagement.GroupName),
                new RolePermission(role.Id,RoleManagement.Query),
                new RolePermission(role.Id,RoleManagement.Create),
                new RolePermission(role.Id,RoleManagement.Update),
                new RolePermission(role.Id,RoleManagement.Delete),
                new RolePermission(role.Id,RoleManagement.ChangeRolePermission)
            };

            await _dbContext.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        private async Task InitUserAsync()
        {
            if (await _dbContext.Users.AnyAsync())
            {
                return;
            }

            User user = new User(IdGenerator.GenerateNewId());
            user.UserName = "admin";
            user.NickName = "admin";
            user.Password = EncryptHelper.MD5Encrypt("season.2023");
            user.Email = "asnotracking@gmail.com";

            var role = _dbContext.Roles.Where(r => r.Code == "Admin").FirstAsync();

            user.UserRoles = new List<UserRole>()
            {
                new UserRole(user.Id,role.Id)
            };

            await _dbContext.Users.AddAsync(user);
        }
    }
}
