using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO;
using WebChat.DTOs;
using WebChat.Models;

namespace WebChat.Repositories;

public interface IAuthRepo
{
    Task Register(UserRegisterRequestDto user,CancellationToken token);
    Task<bool> findEmailAsync(string email);
    Task<bool> findUserNameAsync(string userName);
    string GenerateAccessToken(int Id, string userName, string email, string role, TimeSpan lifetime, bool isRefreshToken = false);
    Task<User?> findUserByEmail(string email);
    PasswordVerificationResult VerifyPassword(User user,string password);
    int checkRefreshTokenValidity(string RefreshToken);
    TokensDto GenerateTokenByRefreshToken(string refreshToken);
}
