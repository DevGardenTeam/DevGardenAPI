using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    public class GitlabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
