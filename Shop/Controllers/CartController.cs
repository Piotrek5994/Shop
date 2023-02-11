using Microsoft.AspNetCore.Mvc;
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
    }
}
