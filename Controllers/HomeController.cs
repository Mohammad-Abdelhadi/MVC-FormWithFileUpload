using Microsoft.AspNetCore.Mvc;

namespace MVC_CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
