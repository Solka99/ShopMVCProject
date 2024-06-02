using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVCProject.Data;
using ShopMVCProject.Models;

namespace ShopMVCProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public ProductController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            List<Product> objProductList=_dbcontext.Products.ToList();
            
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _dbcontext.Categories
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product productObj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Add(productObj);
                _dbcontext.SaveChanges();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb= _dbcontext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product productObj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Update(productObj);
                _dbcontext.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _dbcontext.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj=_dbcontext.Products.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _dbcontext.Products.Remove(obj);
            _dbcontext.SaveChanges();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");
            
        }


    }
}
