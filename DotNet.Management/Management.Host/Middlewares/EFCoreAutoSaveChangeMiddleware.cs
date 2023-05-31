using Managemrnt.EFCore;

namespace Management.Host
{
    public class EFCoreAutoSaveChangeMiddleware
    {
        private readonly RequestDelegate _next;
        public EFCoreAutoSaveChangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            await _next(context);

            if (dbContext.ChangeTracker.HasChanges())
            {
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
