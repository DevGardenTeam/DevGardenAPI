using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    public class DevGardenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
