using WebChat.Models;

namespace WebChat.Repositories;

public interface IMessageRepo
{
    Task<List<ChatMessage>> GetChatMessagesAsync(int SenderId,int RecieverId); 
    Task AddMessage(ChatMessage message);
}
