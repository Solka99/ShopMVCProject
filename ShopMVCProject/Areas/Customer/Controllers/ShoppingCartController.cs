using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopMVCProject.Data;
using ShopMVCProject.Models;
using ShopMVCProject.Utility;
using System.Security.Claims;

namespace ShopMVCProject.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        [ActionName("Index")]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingCart = _dbcontext.ShoppingCarts
            .Include(sc => sc.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault(x=>x.ApplicationUserId==userId);

             return View(shoppingCart);

        }

        [HttpPost]
        [ActionName("RemoveItemOrDecreaseQuantity")]
        public IActionResult RemoveItemOrDecreaseQuantity(int itemId)
        {
            var item = _dbcontext.Items.Where(j => j.ItemId == itemId).FirstOrDefault();
            if (item.Quantity == 1)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //var item = _dbcontext.Items.Where(i => i.ShoppingCart.ApplicationUserId == userId).Where(j => j.ItemId == itemId).FirstOrDefault();
                if (item != null)
                {
                    _dbcontext.Items.Remove(item);
                    _dbcontext.SaveChanges();
                }
                
            }
            else
            {
                item.Quantity--;
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

       /* [HttpPost]
        public IActionResult DecreaseQuantity(int itemId)
        {
            var item = _dbcontext.Items.Where(j => j.ItemId == itemId).FirstOrDefault();

            if (item != null)
            {
                if (item.Quantity == 1)
                {
                    return RedirectToAction("RemoveItem", new { itemId = itemId });
                }
                item.Quantity--;
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        } */

        [HttpPost]
        public IActionResult IncreaseQuantity(int itemId)
        {
            var item = _dbcontext.Items.Where(j => j.ItemId == itemId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity++;
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
