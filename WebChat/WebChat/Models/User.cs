using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChat.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Email {  get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Role {  get; set; } = string.Empty;
    [NotMapped]
    public IFormFile? Image { get; set; }
    public string FileExtension { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public List<ChatMessage> SentMessages { get; set; } = new();
    public List<ChatMessage> ReceivedMessages { get; set; } = new();

}
