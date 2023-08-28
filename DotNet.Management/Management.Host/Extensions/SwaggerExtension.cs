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

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new [] { string.Empty }
                    }
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
