using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razer_Page.Data;
using Razer_Page.Models;

namespace Razer_Page.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
                _context = context;
        }
        public void OnGet()
        {
            CategoryList= _context.categories.ToList();
        }
    }
}
