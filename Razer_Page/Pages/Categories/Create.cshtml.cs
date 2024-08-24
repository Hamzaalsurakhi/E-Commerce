using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razer_Page.Data;
using Razer_Page.Models;

namespace Razer_Page.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost() 
        { 
            _context.categories.Add(Category);
            _context.SaveChanges();
            return RedirectToPage("Index");
        
        }
    }
}
