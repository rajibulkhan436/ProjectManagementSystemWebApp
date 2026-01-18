using ProjectManagementSystem.Core.NotificationHub;
using ProjectManagementSystemWebApp.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();


app.UseRouting();

app.UseCors("MyPolicy");
app.UseAuthorization();
app.UseWebSockets();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
