namespace Management.Host.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDownFilesMiddleware(this IApplicationBuilder app, string directoryPath)
        {
            return app.UseMiddleware<DownFilesMiddleware>(directoryPath);
        }

        public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CurrentUserMiddleware>();
        }
    }
}
