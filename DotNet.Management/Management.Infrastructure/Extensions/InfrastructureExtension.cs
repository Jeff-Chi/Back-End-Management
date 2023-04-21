using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Management.Infrastructure.FileUpload;

namespace Management.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddUplodFile(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.SectionName));
            return services;
        }
    }
}
