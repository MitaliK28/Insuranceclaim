using Microsoft.AspNetCore.Mvc;

namespace insuranceclaimproject.Controllers
{
	[Route("Tickets")]
	public class TicketsUIController : Controller
	{
		[HttpGet("Create")]
		public IActionResult Create()
		{
			return Redirect("/support");
		}
	}
}