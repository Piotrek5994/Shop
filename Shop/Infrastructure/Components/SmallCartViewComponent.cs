using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Models.ViewModels;

namespace Shop.Infrastructure.Components
{
    public class SmallCartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("cart");
            SmallCartViewModel smallCartVM;

            if (cart == null || cart.Count == 0)
            {
                smallCartVM = null;
            }
            else
            {
                smallCartVM = new ()
                {
                    NumberOfitems = cart.Sum(x=> x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(smallCartVM);

        }
    }
}
