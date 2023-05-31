using Management.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Managemrnt.EFCore
{
    public class RoleRepository : Repository<long, Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CountAsync(GetRolesInput input)
        {
            return await Build(DbSet, input).CountAsync();
        }

        public async Task<List<Role>> GetListAsync(GetRolesInput input)
        {
            return await Build(DbSet, input, true).ToListAsync();
        }

        public async Task<List<Role>> GetListAsync(long userId)
        {
            return await Context.Set<UserRole>()
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role!).ToListAsync();
        }

        public async Task<Role?> GetAsync(long id, GetRoleDetailsInput input)
        {
            var query = DbSet.Where(r => r.Id == id);
            if (input != null)
            {
                query = query.IncludeIf(input.IncludeRolePermission, r => r.RolePermissions);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return await Context.Permissions.ToListAsync();
        }

        public async Task<List<Permission>> GetPermissionsAsync(List<long> roleIds)
        {
            var query = from a in DbSet
                        join b in Context.RolePermissions
                        on a.Id equals b.RoleId
                        join c in Context.Permissions
                        on b.PermissionCode equals c.Code
                        where roleIds.Contains(a.Id)
                        orderby c.SortOrder
                        select c;
            return await query.ToListAsync();
        }


        #region private methods

        private IQueryable<Role> Build(IQueryable<Role> query, GetRolesInput? input, bool page = false)
        {
            if (input == null)
            {
                return query;
            }

            query = query.IncludeIf(input.IncludeRolePermission, r => r.RolePermissions)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), r => r.Name.Contains(input.Name!))
                .WhereIf(input.IsDisabled.HasValue, r => r.IsDisabled == input.IsDisabled)
                .WhereIf(!input.Code.IsNullOrWhiteSpace(), r => r.Code == input.Code)
                .WhereIf(!input.Ids.IsNullOrEmpty(), r => input.Ids!.Contains(r.Id))
                .OrderByDescending(r => r.CreationTime)
                .PageIf(page, input);

            return query;
        }

        #endregion
    }
}
