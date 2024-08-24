using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razer_Page.Data;
using Razer_Page.Models;

namespace Razer_Page.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category=_context.categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            Category? obj = _context.categories.Find(Category.Id);
            if (obj == null)
            {
                return NotFound();
            }
            
                _context.Remove(obj);
                _context.SaveChanges();
                /// TempData["success"]="Category Update Successfully";
                return RedirectToPage("Index");
            
        }
    }
}
