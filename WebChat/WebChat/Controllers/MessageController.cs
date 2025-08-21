using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebChat.DTOs;
using WebChat.Models;
using WebChat.Repositories;

namespace WebChat.Controllers;

[Route("api/[controller]")]
[ApiController]

[Authorize]
public class MessageController(IMessageRepo repo,IMapper mapper) : ControllerBase
{
    [HttpGet("{ReceiverId:int}")]
    public async Task<IActionResult> GetMessageList([FromRoute] int ReceiverId)
    {
        var senderId = User!.FindFirst("Id")!.Value;
        var data = await repo.GetChatMessagesAsync(int.Parse(senderId),ReceiverId);
        var chats = mapper.Map<List<MessageDto>>(data);
        return Ok(chats);
    }
}
