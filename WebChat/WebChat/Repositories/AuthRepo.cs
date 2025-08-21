using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DTO;
using WebChat.Data;
using WebChat.DTOs;
using WebChat.Models;

namespace WebChat.Repositories;

public class AuthRepo(ApplicationDbContext context,IConfiguration configuration) : IAuthRepo
{
    public int checkRefreshTokenValidity(string RefreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(RefreshToken))
        {
            return 0;
        }
        var token = handler.ReadJwtToken(RefreshToken);
        if (token.ValidTo <= DateTime.Now)
        {
            return 1;
        }
        return 2;
    }

    public TokensDto GenerateTokenByRefreshToken(string refreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(refreshToken);
        var id = token.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        var email = token.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
        var role = token.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
        var userName = token.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
        if (id is null || email is null || role is null || userName is null)
            return new TokensDto();
        return new TokensDto
        {
            AccessToken = GenerateAccessToken(int.Parse(id),userName, email, role,TimeSpan.FromMinutes(15)),
            RefreshToken = GenerateAccessToken(int.Parse(id),userName,email, role,TimeSpan.FromDays(60))
        };
    }

    public async Task<bool> findEmailAsync(string email)
    {
        var data = await context.User.FirstOrDefaultAsync(x => x.Email == email);
        if(data is null) return false;
        return true;
    }

    public async Task<User?> findUserByEmail(string email)
    {
        var userData = await context.User.FirstOrDefaultAsync(x => x.Email == email);
        return userData;
    }

    public async Task<bool> findUserNameAsync(string userName)
    {
        var data = await context.User.FirstOrDefaultAsync(x => x.Username == userName);
        if (data is null) return false;
        return true;
    }

    public string GenerateAccessToken(int Id,string userName,string email, string role, TimeSpan lifetime, bool isRefreshToken = false)
    {
        var claims = new List<Claim>
        {
            new Claim("Id",Id.ToString()),
            new Claim("UserName",userName),
            new Claim ("Email",email),
            new Claim("Role",role),
            new Claim("token_type",isRefreshToken ? "refresh" : "access")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.Add(lifetime),
            signingCredentials: credentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(accessToken);
    }

    public async Task Register(UserRegisterRequestDto user, CancellationToken token)
    {
        var newUser = new User
        {
            Email = user.Email.ToLower(),
            Username = user.UserName,
            Role = user.Role,
        };
        var hashpassword = new PasswordHasher<User>().HashPassword(newUser, user.Password);
        newUser.HashPassword = hashpassword;
        await context.User.AddAsync(newUser, token);
        await context.SaveChangesAsync(token);
    }

    public PasswordVerificationResult VerifyPassword(User user,string password)
    {
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.HashPassword, password);
        return result;
    }
}
