using Management.Application;
using Management.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Management.Host.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public AccountController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PageResultDto<UserDto>>> GetListAsync(GetUsersInputDto dto)
        {
            return await _userAppService.GetListAsync(dto);
        }

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<UserDto>> GetAsync(long id)
        {
            return await _userAppService.GetAsync(id);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] CreateUserInputDto createDto)
        {
            return await _userAppService.CreateAsync(createDto);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpPut("{id:long}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateUserInputDto updateDto)
        {
             await _userAppService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task DeleteAsync(int id)
        {
            await _userAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<JwtTokenDto> LoginAsync(UserLoginDto loginDto)
        {
            return await _userAppService.LoginAsync(loginDto);
        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id:long}/roles")]
        public async Task<List<RoleDto>> GetRolesAsync(long id)
        {
            return await _userAppService.GetRolesAsync(id);
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPut("{id:long}/role")]
        public async Task<ActionResult> UpdateUserRolesAsync(long id, [FromBody] List<long> roleIds)
        {
            return NoContent();
        }
    }
}
