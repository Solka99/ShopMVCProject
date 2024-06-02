using Microsoft.AspNetCore.Mvc;

namespace ShopMVCProject.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
