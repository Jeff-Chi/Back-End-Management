using Microsoft.OpenApi.Models;

namespace Magament.Host
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                // TODO: API 版本控制
                option.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "后台管理Api文档",
                    Version = "v1",
                    Description = "后台管理Api文档v1"
                });

                var file = Path.Combine(AppContext.BaseDirectory, "Management.Host.xml");
                option.IncludeXmlComments(file, true);
            });
        }

        public static void UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
