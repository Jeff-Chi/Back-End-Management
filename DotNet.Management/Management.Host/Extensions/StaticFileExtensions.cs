using Management.Infrastructure.FileUpload;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Management.Host.Extensions
{
    public static class StaticFileExtensions
    {
        public static IApplicationBuilder UseMultipleStaticFiles(this IApplicationBuilder app)
        {

            FileUploadOptions fileUploadOptions = app.ApplicationServices.GetService<IOptions<FileUploadOptions>>()!.Value;

            List<(string, string)> staticFileList = new List<(string, string)>();

            staticFileList.Add(ValueTuple.Create(Path.Combine(fileUploadOptions.StoreRootDirName, fileUploadOptions.RootDirName), $"/{fileUploadOptions.RootDirName}"));
            foreach (var item in staticFileList)
            {
                Directory.CreateDirectory(item.Item1);

                var options = new StaticFileOptions();
                options.FileProvider = new PhysicalFileProvider(item.Item1);
                if (item.Item2 != null)
                {
                    options.RequestPath = item.Item2;
                }

                app.UseStaticFiles(options);
            }

            // 添加默认的wwwroot静态文件
            app.UseStaticFiles();

            return app;
        }
    }
}
