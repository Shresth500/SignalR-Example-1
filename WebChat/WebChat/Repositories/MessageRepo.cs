using Microsoft.EntityFrameworkCore;
using WebChat.Data;
using WebChat.Models;

namespace WebChat.Repositories;

public class MessageRepo(ApplicationDbContext context) : IMessageRepo
{
    public async Task AddMessage(ChatMessage message)
    {
        await context.ChatMessages.AddAsync(message);
        await context.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetChatMessagesAsync(int SenderId, int RecieverId)
    {
        var data = await context
                    .ChatMessages
                    .Where(x => (x.SenderId == SenderId && x.ReceiverId == RecieverId) || 
                    (x.SenderId == RecieverId && x.ReceiverId == SenderId))
                    .OrderBy(x => x.SentAt)
                    .ToListAsync();
        return data;
    }
}
