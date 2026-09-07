using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly IHubContext<ChatHub, IChatClient> _hub;

    public AnnouncementController(IHubContext<ChatHub, IChatClient> hub)
    {
        _hub = hub;
    }

    [HttpPost]
    public async Task<IActionResult> PostAnnouncement([FromBody] AnnouncementRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest();
        }

        await _hub.Clients.All.SystemNotification($"[Server Announcement]: {request.Message}");
        return Ok(new { success = true });
    }
}
