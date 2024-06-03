using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Data;
using ShopMVCProject.Models;
using System.Diagnostics;

namespace ShopMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList= _dbcontext.Products.Include(u => u.Category).ToList();
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            var productFromDb = _dbcontext.Products.Include(u => u.Category).FirstOrDefault(p => p.Id == productId);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
