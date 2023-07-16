using Management.Application;
using Management.Infrastructure.FileUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Management.Host.Controllers.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileUploadOptions _fileUploadOptions;
        public FileController(IOptions<FileUploadOptions> options, IHostEnvironment hostEnvironment, IWebHostEnvironment webEnvironment)
        {
            _fileUploadOptions = options.Value;
            if (string.IsNullOrWhiteSpace(_fileUploadOptions.PhysicalStoragePath))
            {
                _fileUploadOptions.PhysicalStoragePath = Path.Combine(hostEnvironment.ContentRootPath, _fileUploadOptions.StoreRootDirName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            if (formFile == null)
            {
                return BadRequest();
            }

            string fileName = formFile.FileName.Trim('\"', '/').Trim();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("文件名不能为空!");
            }

            string extStr =  Path.GetExtension(fileName).ToLower();

            if (string.IsNullOrWhiteSpace(extStr))
            {
                throw new Exception("文件扩展名不合法!");
            }

            // TODO:检查支持的文件类型 扩展名,文件大小等
            string directory = Path.Combine(_fileUploadOptions.PhysicalStoragePath, _fileUploadOptions.RootDirName, _fileUploadOptions.NormalDirName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            fileName = Guid.NewGuid().ToString();
            string filePath = Path.Combine(directory,fileName);

            string relativeURL = Path.Combine("/" + _fileUploadOptions.RootDirName, _fileUploadOptions.NormalDirName, fileName);
            relativeURL = relativeURL.Replace('\\', '/');

            if (!System.IO.File.Exists(filePath))
            {
                // TODO:验证扩展名
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return Ok(new FileUploadResultDto { FileUrl = relativeURL });
        }
    }
}
