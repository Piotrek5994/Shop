using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.Components
{
    public class CategoriesViewComponents:ViewComponent
    {
        private readonly DataContext _context;

        public CategoriesViewComponents(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());
        
    }
}
