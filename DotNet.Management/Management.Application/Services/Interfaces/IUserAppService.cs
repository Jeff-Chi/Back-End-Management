using Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<UserDto>> GetListAsync(GetUsersInputDto input);

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetAsync(long id);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> CreateAsync(CreateUserInputDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(long id, UpdateUserInputDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        Task<JwtTokenDto> LoginAsync(UserLoginDto userLoginDto);

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleDto>> GetRolesAsync(long userId);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task UpdateUserRolesAsync(long userId,List<long> roleIds);

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="currentUserContext">当前用户context</param>
        /// <returns></returns>
        Task<CurrentUserDto> GetCurrentUserAsync(CurrentUserContext currentUserContext);

    }
}
