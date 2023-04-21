using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.FileUpload
{
    public class FileUploadTypeSetting
    {
        public string Name { get; set; } = string.Empty;
        public string FileTypes { get; set; } = string.Empty;
        public string MaxSize { get; set; } = string.Empty;
    }
}
