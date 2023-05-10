using Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public interface IJwtTokenService: ISingletonDependency
    {
        JwtTokenDto CreateToken(long userId,IEnumerable<Claim> claims);
    }
}
