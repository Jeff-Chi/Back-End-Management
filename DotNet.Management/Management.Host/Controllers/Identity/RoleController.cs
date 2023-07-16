using Management.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Host.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleAppService _roleAppService;
        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PageResultDto<RoleDto>>> GetListAsync(GetRolesInputDto dto)
        {
            return await _roleAppService.GetListAsync(dto);
        }

        /// <summary>
        /// 获取指定角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<RoleDto>> GetAsync(long id)
        {
            return await _roleAppService.GetAsync(id);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateAsync([FromBody] CreateRoleInputDto createDto)
        {
            return await _roleAppService.CreateAsync(createDto);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> UpdateAsync(long id, [FromBody] CreateRoleInputDto updateDto)
        {
            await _roleAppService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _roleAppService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionCodes"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:long}/permission")]
        public async Task UpdateRolePermissionAsync(int id,List<string> permissionCodes)
        {
            await _roleAppService.UpdateRolePermissionAsync(id, permissionCodes);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("permissions")]
        public async Task<List<PermissionDto>> GetPermissionsAsync()
        {
            return await _roleAppService.GetPermissionsAsync();
        }
    }
}
