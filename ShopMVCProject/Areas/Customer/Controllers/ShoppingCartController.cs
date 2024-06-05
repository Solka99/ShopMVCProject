using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Data;

namespace ShopMVCProject.Areas.Customer.Controllers
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
            //nie działa
            var shoppingCart = _dbcontext.ShoppingCarts
            .Include(sc => sc.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault();

            return View(shoppingCart);
        }

        [HttpPost]
        public IActionResult RemoveItem(int itemId)
        {
            var item = _dbcontext.Items.Where(i => i.ShoppingCartId == 1).Where(j => j.ItemId == itemId).FirstOrDefault();
            if (item != null)
            {
                _dbcontext.Items.Remove(item);
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Checkout(int Id)
        {
            var shoppingCart = _dbcontext.ShoppingCarts.Find(Id);
            _dbcontext.Remove(shoppingCart);
            _dbcontext.SaveChanges();
            return View();
        }
        
    }
}
