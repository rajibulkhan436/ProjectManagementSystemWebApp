using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Core.NotificationHub;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystem.Services.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IHubContext<NotificationHub> hubContext,ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task BroadcastNotification(string message)
        {
            try
            {
                Console.WriteLine("Broadcasting message");
                _logger.LogInformation("Broadcasting message");
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public async Task SendNotification(string message, string userId)
        {
            await _hubContext.Clients.Group(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
