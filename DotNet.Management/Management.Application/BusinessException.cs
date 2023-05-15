using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class BusinessException: Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; set; } = string.Empty;
        public int? HttpStatusCode { get; set; }
    }
}
