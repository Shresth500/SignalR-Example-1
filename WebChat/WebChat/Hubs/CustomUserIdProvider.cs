using Microsoft.AspNetCore.SignalR;

namespace WebChat.Hubs;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("Id")?.Value!;
    }
}
