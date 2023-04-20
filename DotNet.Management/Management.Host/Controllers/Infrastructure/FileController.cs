using Management.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Management.Host.Controllers.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
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

            // TODO:检查支持的文件类型
            string directory = Path.Combine(Directory.GetCurrentDirectory(),"upload","files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            fileName = Guid.NewGuid().ToString();
            string filePath = Path.Combine(directory,fileName);

            string relativeURL = Path.Combine("/" + "upload", "files", fileName);
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
