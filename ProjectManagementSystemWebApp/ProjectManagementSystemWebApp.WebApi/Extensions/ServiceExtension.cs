using Microsoft.AspNetCore.Authorization;
using ProjectManagementSystem.Core.Cache;
using ProjectManagementSystem.Core.Contracts;
using ProjectManagementSystem.Core.NotificationHub;
using ProjectManagementSystem.DataAccess;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.Services.Background;
using ProjectManagementSystem.Services.Services.Helper;
using ProjectManagementSystem.Services.Services.Notification;
using ProjectManagementSystem.Services.Services.Tokens;
using ProjectManagementSystemWebApp.WebApi.Authorization.Policy;

namespace ProjectManagementSystemWebApp.WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProjectTaskManager, ProjectTaskManager>();
            services.AddScoped<IRedisFactory, RedisFactory>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IAuthorizationHandler, RolePolicy.Handler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PolicyProvider>();
            services.AddHostedService<BackgroundWorkerService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddScoped<INotificationService,NotificationService>();
            services.AddScoped<IFileManager,FileManager>();
            services.AddScoped<NotificationHub>();
        }
    }
}
