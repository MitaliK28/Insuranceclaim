using Microsoft.AspNetCore.Mvc;

namespace insuranceclaimproject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
