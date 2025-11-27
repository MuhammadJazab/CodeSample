

namespace Notification.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    [HttpGet]
    public IActionResult Status()
    {
        return Ok("Notification service is running");
    }
}
