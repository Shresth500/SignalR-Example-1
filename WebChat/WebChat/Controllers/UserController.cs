using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebChat.DTOs;
using WebChat.Repositories;

namespace WebChat.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Policy = "AccessTokenOnly")]
[Authorize]
public class UserController(IUserRepo repo,IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserDataAsync()
    {
        int userid = int.Parse(User!.FindFirst("Id")!.Value);
        var data = await repo.GetUserListAsync(userid);
        var result = mapper.Map<List<UserDto>>(data);
        return Ok(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var data = await repo.GetUserByIdAsync(id);
        var result = mapper.Map<UserDto>(data);
        return Ok(result);
    }
}
