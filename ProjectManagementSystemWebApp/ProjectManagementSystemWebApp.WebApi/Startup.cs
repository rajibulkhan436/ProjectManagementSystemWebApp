using ProjectManagementSystem.Contexts;
using ProjectManagementSystemWebApp.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using ProjectManagementSystemWebApp.WebApi.Authentication;
using Mapster;
using ProjectManagementSystem.Services.TypeAdapter;

namespace ProjectManagementSystemWebApp.WebApi
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddServices();

            //Mapster services Added
            services.AddMapsterServices();

            //SignalR services added
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            }).AddJsonProtocol();

            //Mediator
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IMediatorInterface>());

            //Db context
            var connectionString = configuration.GetConnectionString("MyDb");
            services.AddDbContext<DatabaseContext>(Options => {
                Options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            //services.Configure<AppSetting>(configuration.GetSection("ConnectionString"));

            //Redis services
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
           

            services.ConfigureAuthenticationAndAuthorization(configuration);
        }

        private static void ConfigureAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            string? mySecretKey = configuration["Jwt:SecretKey"];

            services.AddAuthentication("CustomAuthScheme")
                    .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomAuthScheme", null);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(mySecretKey)
                            ),
                        }
                    );
            services.AddAuthorization();

        }

        private static void AddMapsterServices(this IServiceCollection services)
        {
            services.AddMapster();
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingAdapter).Assembly);
            services.AddSingleton(config);
        }

    }
}