using Microsoft.AspNetCore.Mvc;

namespace test_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqTestController() : ControllerBase
{
	[HttpGet]
	[Route("SendMessageToUfmInQueue")]
	public IActionResult SendMessageToUfmInQueue()
	{
		return Ok();
	}
}
