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
        Task<PageResultDto<UserDto>> GetListAsync(QueryUsersDto input);

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto?> GetAsync(long id);

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
        Task UpdateAsync(long id, CreateUserInputDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);
    }
}
