using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    public class GithubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
