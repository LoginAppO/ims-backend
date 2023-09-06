using System.Text;
using Destructurama;
using Ims.Api;
using Ims.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

const string everyoneCorsPolicyName = "Everyone";

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Destructure.UsingAttributes()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host
    .UseDefaultServiceProvider(
        (context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment() ||
                context.HostingEnvironment.IsEnvironment("Local");
            options.ValidateOnBuild = true;
        })
    .UseSerilog();

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureApp(app, builder.Configuration);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    _ = services.AddEndpointsApiExplorer();
    _ = services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        });
    });

    _ = services.AddSingleton(Log.Logger);

    _ = services.AddHttpContextAccessor();

    _ = services.AddApplication(configuration);

    services.AddRouteModules();

    _ = services.AddCors(options => options.AddPolicy(
        everyoneCorsPolicyName,
        policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
    _ = services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x => x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtSettings:Key"] ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        });

    _ = services.AddMvc();
}

static void ConfigureApp(WebApplication app, IConfiguration configuration)
{
    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
    {
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI();
    }

    _ = app.UseHttpsRedirection();
    _ = app.UseStaticFiles();

    app.UseRouteModules();

    _ = app.UseCors(everyoneCorsPolicyName);

    _ = app.UseAuthentication();
    _ = app.UseAuthorization();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ImsDbContext>();
        DbInitializer.Initialize(dbContext, configuration);
    }
}

public partial class Program
{
}
