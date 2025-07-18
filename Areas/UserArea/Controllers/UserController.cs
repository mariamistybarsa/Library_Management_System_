using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
