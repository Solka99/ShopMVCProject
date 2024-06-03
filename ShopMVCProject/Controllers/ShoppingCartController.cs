using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Data;

namespace ShopMVCProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            var shoppingCart = _dbcontext.ShoppingCarts
            .Include(sc => sc.Items)
            .FirstOrDefault();

            return View(shoppingCart);
        }
    }
}
