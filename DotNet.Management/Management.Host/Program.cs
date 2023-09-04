using Magament.Host;
using Management.Application;
using Management.Domain;
using Management.Host;
using Management.Host.Extensions;
using Management.Host.Middlewares;
using Management.Infrastructure;
using Management.Infrastructure.FileUpload;
using Managemrnt.EFCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext();
});

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(builder.Configuration["MySql:Version"]));

    //AutoDetect
    //options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();

// 配置模型校验返回错误格式
builder.Services.ConfigureApiBehaviorOptions();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllCrosDomainsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSwaggerService();

#region options

builder.Services.Configure<FileUploadOptions>(builder.Configuration.GetSection(FileUploadOptions.SectionName));

builder.Services.Configure<JwtTokenOptions>(builder.Configuration.GetSection(JwtTokenOptions.SectionName));

builder.Services.Configure<AuditlogOptions>(builder.Configuration.GetSection(AuditlogOptions.SectionName));

#endregion

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // https 默认为true
        //options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtToken:Issuer"],
            ValidAudience = builder.Configuration["JwtToken:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SecurityKey"]!)),
            ClockSkew = TimeSpan.FromMinutes(1) // 偏差
        };

        // options.Events 事件处理
    });

#endregion

#region 批量服务注入

builder.Services.RegisterServices();

// 注入泛型服务
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

#endregion

#region Authorization

builder.Services.AddSingleton<IAuthorizationPolicyProvider, AppAuthorizationPolicyProvider>();

builder.Services.AddScoped<IAuthorizationHandler, AppAuthorizationHandler>();

#endregion

//builder.Services.AddScoped<ICurrentUserContext, CurrentUserContext>();

#region aes

// TODO: Key放到 appsettings
builder.Services.AddSingleton<IAesProtector>(sp => new AesProtector("12346iid882uabcd"));

#endregion

// auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMemoryCache();

builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());

// builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();
}

app.UseSwaggerMiddleware();


//读取图片的中间件
app.UseDownFilesMiddleware(Directory.GetCurrentDirectory());

// cors
app.UseCors("AllCrosDomainsPolicy");

// static files
app.UseMultipleStaticFiles(app.Environment);

//app.UseHttpsRedirection();

//app.UseSerilogRequestLogging();

// audit lot middleware
app.UseMiddleware<AuditLogMiddleware>();

app.UseAppExceptionHandler(app.Environment);

app.UseAuthentication();

app.UseCurrentUser();

app.UseAuthorization();

app.MapControllers();

// ef core auto save.
app.UseMiddleware<EFCoreAutoSaveChangeMiddleware>();


using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var seederService = services.GetRequiredService<IAppSeederService>();
    await seederService.SeedAsync();
}

app.Run();
