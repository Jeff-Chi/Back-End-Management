using Management.Domain;
using Management.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Management.Host
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly RequestResponseLoggerOption _options;
        //private readonly IRequestResponseLogger _logger;

        public AuditLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            // TODO: Options

            AuditLog log = new AuditLog(IdGenerator.GenerateNewId())
            {
                CreationTime = DateTime.Now,
            };

            var request = context.Request;

            string? ip = null;
            if (request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ip = context.Request.Headers["X-Forwarded-For"].ToString();
            }
            else
            {
                ip = context.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            }

            log.SourceIpAddress = ip;
            if (context.Request.Headers.TryGetValue("User-Agent", out var vals))
            {
                //userAgent = vals.ToString();
            }
        }
    }
}
