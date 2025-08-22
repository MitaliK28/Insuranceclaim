using Microsoft.AspNetCore.Mvc;

namespace insuranceclaimproject.Controllers
{
	[Route("support")]
	public class SupportTicketUIController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View("Index");
		}
	}
}