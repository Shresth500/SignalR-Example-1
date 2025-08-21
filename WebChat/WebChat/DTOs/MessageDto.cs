namespace WebChat.DTOs;

public class MessageDto
{
    public int SenderId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.Now;
}
