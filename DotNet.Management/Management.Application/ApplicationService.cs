using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class ApplicationService: IApplicationService//:IScopedDependency
    {
        // TODO: 权限验证，抛出异常等公用代码
        private static int _index;

        protected int GenerateId()
        {
            return ++_index;
        }
    }
}
