namespace Management.Host.Middlewares
{
    public class DownFilesMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private string _directoryPath = string.Empty;
        public DownFilesMiddleware(RequestDelegate requestDelegate, string directoryPath)
        {
            _requestDelegate = requestDelegate;
            _directoryPath = directoryPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value!.Contains("upload/files"))
            {
                string fileUrl = $"{_directoryPath}{context.Request.Path.Value}";
                if (!File.Exists(fileUrl)) //如果文件不存在就继续往后的流程
                {
                    await _requestDelegate(context);
                }
                else
                {
                    context.Request.EnableBuffering();
                    context.Request.Body.Position = 0;
                    var responseStream = context.Response.Body;
                    using (FileStream newStream = new FileStream(fileUrl, FileMode.Open))
                    {
                        context.Response.Body = newStream;
                        newStream.Position = 0;
                        var responseReader = new StreamReader(newStream);
                        var responseContent = await responseReader.ReadToEndAsync();
                        newStream.Position = 0;
                        await newStream.CopyToAsync(responseStream);
                        context.Response.Body = responseStream;
                    }
                }
            }
            else
            {
                await _requestDelegate(context);
            }
        }
    }
}
