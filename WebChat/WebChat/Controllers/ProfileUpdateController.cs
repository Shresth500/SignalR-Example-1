using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebChat.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AccessTokenOnly")]
public class ProfileUpdateController : ControllerBase
{
}
