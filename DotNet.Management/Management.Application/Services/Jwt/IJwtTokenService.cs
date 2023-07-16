using Management.Domain;
using System.Security.Claims;

namespace Management.Application
{
    public interface IJwtTokenService: ISingletonDependency
    {
        /// <summary>
        /// create token
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="claims">claims</param>
        /// <returns></returns>
        JwtTokenDto CreateToken(long userId,IEnumerable<Claim> claims);
        /// <summary>
        /// get user id by refresh token
        /// </summary>
        /// <param name="refreshToken">refreshToken</param>
        /// <returns></returns>
        long GetUserIdByRefreshToken(string refreshToken);
    }
}
