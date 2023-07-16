using Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class ApplicationService: IApplicationService
    {
        protected long GenerateId()
        {
           return IdGenerator.GenerateNewId();
        }

        protected static void ValidateNotNull(object? value, string? message = null)
        {
            if (value == null)
            {
                throw new BusinessException(message ?? "Target Not Found");
            }
        }

        protected static void ForbiddonError(string? message = null)
        {
            // Permission Denied
            throw new UnauthorizedAccessException(message ?? "Forbiddon Error");
        }
    }
}
