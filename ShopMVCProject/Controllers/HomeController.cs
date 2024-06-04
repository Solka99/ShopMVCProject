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
        private ShoppingCart _shoppingCart;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
           /* var newShoppingCart = new ShoppingCart
            {
               // Id = 1
            };
            _dbcontext.ShoppingCarts.Add(newShoppingCart);
            _dbcontext.SaveChanges();
           

            _shoppingCart = newShoppingCart;
           */
        
        }

        public IActionResult Index()
        {
           
            IEnumerable<Product> productList= _dbcontext.Products.Include(u => u.Category).ToList();
            return View(productList);
        }
        public IActionResult AddToCart(int productId)
        {
            //do zmiany
            var productFromDb = _dbcontext.Products.Include(u => u.Category).FirstOrDefault(p => p.Id == productId);
            if (productFromDb == null)
            {
                return NotFound();
            }
            var newItem = new Item
            {
                ProductId = productFromDb.Id,
                Quantity = 1,
                ShoppingCartId = 1
            };

            _dbcontext.Items.Add(newItem);
            _dbcontext.SaveChanges();

            _shoppingCart = _dbcontext.ShoppingCarts.Where(x => x.Id == 1).FirstOrDefault();
            _shoppingCart.Items.Add(newItem);
            _dbcontext.SaveChanges();


            //return RedirectToAction("Index", "ShoppingCart",_shoppingCart);
            return RedirectToAction("Index","ShoppingCart");
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
