using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ProjectManagementSystem.Core.NotificationHub
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            _logger.LogInformation($"Attempting to connect user: {user?.Identity?.Name}");

            if (user?.Identity?.IsAuthenticated == true)
            {
                
                var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(userRole))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
                    _logger.LogInformation($"User added to group {userRole}");
                }
            Console.WriteLine($"User added to group {userRole}");
            }
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(userRole))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userRole);
                _logger.LogInformation($"User {userId} disconnected from group {userRole}");
            }

            await base.OnDisconnectedAsync(exception);

        }

        public async Task BroadcastNotification(string message)
        {
            Console.WriteLine("Broadcasting message");
            _logger.LogInformation("Broadcasting message");
            try
            {
                
                if (Clients != null)
                {
                    await Clients.All.SendAsync("ReceiveMessage", message);
                }
                else
                {
                    _logger.LogError("Clients object is null. SignalR Hub is not initialized properly.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
