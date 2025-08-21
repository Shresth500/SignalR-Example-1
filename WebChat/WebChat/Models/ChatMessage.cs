namespace WebChat.Models;

public class ChatMessage
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.Now;
    public User Sender { get; set; }
    public User Receiver { get; set; }
}
