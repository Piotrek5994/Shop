﻿using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure;
using Shop.Models;
using Shop.Models.ViewModels;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart") ?? new List<CartItem>();

               CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Add(long id)
        {

            Product product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));

            }
            else
            {
                cartItem.Quantity++;
            }

            HttpContext.Session.SetJson("cart", cart);

            TempData["Success"] = "Product added to cart successfully!";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(long id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart");

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;

            }
            else
            {
                cart.RemoveAll(x=>x.ProductId==id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetJson("cart", cart);
            }

            TempData["Success"] = "Product has been Removed";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart");

            cart.RemoveAll(x => x.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetJson("cart", cart);
            }

            TempData["Success"] = "Product has been Removed";
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index");
        }
    }
}
