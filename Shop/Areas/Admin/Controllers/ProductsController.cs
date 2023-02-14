﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_context.Products.Count() / pageSize);
            return View(await _context.Products.OrderByDescending(x => x.Id)
                                                     .Include(p=>p.Category)
                                                     .Skip((p - 1) * pageSize)
                                                     .Take(pageSize).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id" , "Name",product.CategoryId);

            if(ModelState.IsValid)
            { 
                product.Slug = product.Name.ToLower().Replace("","-");

                var slug = await _context.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "The Product already exist");
                    return View(product);
                }

                
                if(product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString()+"_"+ product.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                     fs.Close();

                    product.Image = imageName;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been Created!";
                return RedirectToAction ("Index");
            }
            return View(product);
        }

    }
}
