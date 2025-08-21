using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebChat.DTOs;
using WebChat.Repositories;

namespace WebChat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthRepo repo) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]UserRegisterRequestDto user, CancellationToken token)
    {
        user.Role = "User";
        var findEmail = await repo.findEmailAsync(user.Email);
        if (findEmail) return BadRequest("Email Already exists");
        var findUserName = await repo.findUserNameAsync(user.UserName);
        if (findUserName) return BadRequest("UserName already exists");
        await repo.Register(user,token);
        return Ok("User has been successfully registered!!!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto user,CancellationToken token)
    {
        var userData = await repo.findUserByEmail(user.Email);
        if (userData is null) return BadRequest("Email doesn't exist");
        var checkPasswordResult = repo.VerifyPassword(userData,user.Password);
        if (checkPasswordResult is PasswordVerificationResult.Failed) return BadRequest("Password Incorrect");
        var accessTokenTime = TimeSpan.FromMinutes(15);
        var accessToken = repo.GenerateAccessToken(userData.Id,userData.Username,userData.Email,userData.Role, accessTokenTime);
        var refreshTokenTime = TimeSpan.FromDays(60);
        var refreshToken = repo.GenerateAccessToken(userData.Id, userData.Username, userData.Email, userData.Role, refreshTokenTime, true);
        return Ok(new LoginResponseDto
        {
            Id = userData.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserName = userData.Username,
            Email = userData.Email,
            Role = userData.Role,
            filePath = userData.FilePath,
        });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] string refreshToken, CancellationToken token)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest("Token field is required");
        var validity = repo.checkRefreshTokenValidity(refreshToken);
        if (validity == 0)
            return BadRequest("Token Not Found");
        if (validity == 1)
            return Unauthorized("Token is expired");
        var tokens = repo.GenerateTokenByRefreshToken(refreshToken);
        return Ok(tokens);
    }

}
