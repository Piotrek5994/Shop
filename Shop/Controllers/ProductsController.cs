using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 6;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.PageCount = categorySlug;
            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((double)_context.Products.Count() / pageSize);

                return View(await _context.Products.OrderByDescending(x => x.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            Category category = await _context.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null)
                return RedirectToAction("Index");

            var productsByCategory = _context.Products.Where(p => p.CategoryId == category.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((double)productsByCategory.Count() / pageSize);

            return View(await productsByCategory.OrderByDescending(x => x.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}
