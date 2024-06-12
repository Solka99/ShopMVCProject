using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopMVCProject.Data;
using ShopMVCProject.Models;
using ShopMVCProject.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace ShopMVCProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        private ShoppingCart _shoppingCart;

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
        public IActionResult AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



            var productFromDb = _dbcontext.Products.Include(u => u.Category).FirstOrDefault(p => p.Id == productId);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _shoppingCart = _dbcontext.ShoppingCarts.Include(sc => sc.Items).Where(x => x.ApplicationUserId == userId).FirstOrDefault();
            if (_shoppingCart == null)
            {
                var newShoppingCart = new ShoppingCart
                {
                    //id is set automatically
                    Items = new List<Item>(),
                    ApplicationUserId=userId
                };
                _shoppingCart = newShoppingCart;
                _dbcontext.ShoppingCarts.Add(newShoppingCart);
                _dbcontext.SaveChanges();
            }
             
            var itemExists = _shoppingCart.Items.Any(i => i.ProductId == productId);
            var existingItem = _shoppingCart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemExists == true)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new Item
                {
                    ProductId = productFromDb.Id,
                    Quantity = 1,
                    ShoppingCartId = _shoppingCart.Id
                };
                _shoppingCart.Items.Add(newItem); 
            }
            _dbcontext.SaveChanges();


            return RedirectToAction("Index","ShoppingCart", new { Id = _shoppingCart.Id, Items = _shoppingCart.Items, ApplicationUserId=_shoppingCart.ApplicationUserId });
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
