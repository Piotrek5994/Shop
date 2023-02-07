using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.Components
{
    public class CategoriesViewComponent:ViewComponent
    {

        private readonly DataContext _context;

        public CategoriesViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }


    }
}
