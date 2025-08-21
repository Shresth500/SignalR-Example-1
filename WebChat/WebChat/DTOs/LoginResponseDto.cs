namespace WebChat.DTOs;

public class LoginResponseDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken {  get; set; } = string.Empty;
    public string filePath { get; set; } = string.Empty;
}
