using Management.Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace Management.Host
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuditlogOptions _options;

        public AuditLogMiddleware(RequestDelegate next, IOptions<AuditlogOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_options.IsEnabled)
            {
                await _next(context);
                return;
            }

            RequestResponseLogDto log = new RequestResponseLogDto()
            {
                RequestDateTime = DateTime.Now,
            };

            var request = context.Request;
            string? userAgent = null;
            string? ip = null;
            if (request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ip = request.Headers["X-Forwarded-For"].ToString();
            }
            else
            {
                ip = context.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            }

            log.SourceIpAddress = ip;
            if (request.Headers.TryGetValue("User-Agent", out var vals))
            {
                userAgent = vals.ToString();
            }

            string? controllerName = null;
            string? actionName = null;
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                ControllerActionDescriptor? descriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                controllerName = descriptor?.ControllerName;
                actionName = descriptor?.ActionName;
            }

            log.RequestMethod = request.Method;
            log.RequestPath = request.Path.ToString();
            log.RequestQuery = request.QueryString.ToString();
            log.RequestBody = await ReadBodyFromRequest(request);
            log.RequestScheme = request.Scheme;
            log.RequestHost = request.Host.ToString();
            log.RequestContentType = request.ContentType;
            log.Platform = userAgent;
            log.Controller = controllerName;
            log.Action = actionName;
            log.SourceIpAddress = ip;

            // Temporarily replace the HttpResponseStream, 
            // which is a write-only stream, with a MemoryStream to capture 
            // its value in-flight.
            HttpResponse response = context.Response;
            var originalResponseBody = response.Body;
            using var newResponseBody = new MemoryStream();
            response.Body = newResponseBody;


            // Call the next middleware in the pipeline
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                /*exception: but was not managed at app.UseExceptionHandler() 
                  or by any middleware*/
                LogError(log, exception);
            }

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText =
                await new StreamReader(response.Body).ReadToEndAsync();

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);

            /*response*/
            log.ResponseContentType = response.ContentType;
            log.ResponseStatus = response.StatusCode.ToString();
            //log.ResponseHeaders = FormatHeaders(response.Headers);
            log.ResponseBody = responseBodyText;
            log.ResponseDateTime = DateTime.Now;

            /*exception: but was managed at app.UseExceptionHandler() 
              or by any middleware*/
            var contextFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();
            if (contextFeature != null && contextFeature.Error != null)
            {
                Exception exception = contextFeature.Error;
                LogError(log, exception);
            }

            Console.WriteLine(log.ToString());

            //var jsonString = logCreator.LogString(); /*log json*/
        }

        private void LogError(RequestResponseLogDto log, Exception exception)
        {
            log.ExceptionMessage = exception.Message;
            log.ExceptionStackTrace = exception.StackTrace;
        }

        private Dictionary<string, string?> FormatHeaders(IHeaderDictionary headers)
        {
            Dictionary<string, string?> pairs = new Dictionary<string, string?>();
            foreach (var header in headers)
            {
                pairs.Add(header.Key, header.Value);
            }
            return pairs;
        }

        private List<KeyValuePair<string, string>> FormatQueries(string queryString)
        {
            List<KeyValuePair<string, string>> pairs =
                 new List<KeyValuePair<string, string>>();
            string key, value;
            foreach (var query in queryString.TrimStart('?').Split("&"))
            {
                var items = query.Split("=");
                key = items.Count() >= 1 ? items[0] : string.Empty;
                value = items.Count() >= 2 ? items[1] : string.Empty;
                if (!String.IsNullOrEmpty(key))
                {
                    pairs.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return pairs;
        }

        private async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times 
            // (for the next middlewares in the pipeline).
            request.EnableBuffering();
            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();
            // Reset the request's body stream position for 
            // next middleware in the pipeline.
            request.Body.Position = 0;
            return requestBody;
        }
    }
}
