using Management.Domain;
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

        #region private methods

        private IQueryable<Role> Build(IQueryable<Role> query, GetRolesInput? input, bool page = false)
        {
            if (input == null)
            {
                return query;
            }

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Name), r => r.Name.Contains(input.Name!))
                .WhereIf(input.IsDisabled.HasValue, r => r.IsDisabled == input.IsDisabled)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Code), r => r.Code == input.Code)
                .OrderByDescending(r => r.CreationTime)
                .PageIf(page, input);

            return query;
        }

        #endregion
    }
}
