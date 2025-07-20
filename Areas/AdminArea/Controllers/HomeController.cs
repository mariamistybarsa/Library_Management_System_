using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Areas.AdminArea.Controllers
{
    public class HomeController : Controller
    {
        [Area("AdminArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
