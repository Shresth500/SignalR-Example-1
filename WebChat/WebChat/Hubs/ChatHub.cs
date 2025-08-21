using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebChat.Models;
using WebChat.Repositories;

namespace WebChat.Hubs;

[Authorize]
public class ChatHub(IMessageRepo repo,ILogger<ChatHub> logger):Hub
{
    public async Task SendMessage(int receiverId, string message)
    {
        var senderId = Context.UserIdentifier!;
        logger.LogInformation($"{senderId}");
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage",senderId, message);
        var newMessage = new ChatMessage
        {
            ReceiverId = receiverId,
            SenderId = int.Parse(senderId),
            Message = message,

        };
        await repo.AddMessage(newMessage);
    }
}
