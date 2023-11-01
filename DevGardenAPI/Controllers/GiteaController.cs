using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    public class GiteaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
