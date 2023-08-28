using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public interface IRoleAppService:IApplicationService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<RoleDto>> GetListAsync(GetRolesDto input);

        /// <summary>
        /// 获取指定角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleDto> GetAsync(long id);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RoleDto> CreateAsync(CreateRoleDto input);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(long id, CreateRoleDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionCodes"></param>
        /// <returns></returns>
        Task UpdateRolePermissionAsync(long id,List<string> permissionCodes);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionDto>> GetPermissionsAsync();
    }
}
