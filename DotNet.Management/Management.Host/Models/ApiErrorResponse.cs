using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Host
{
    /// <summary>
    /// api error response
    /// </summary>
    public class ApiErrorResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public ValidationErrorInfo[]? ValidationErrors { get; set; }
    }
}
