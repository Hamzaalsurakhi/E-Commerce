using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;   
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList =_applicationDbContext.categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name== category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrser cannot match the Name");
            }

            if (ModelState.IsValid)
            {
                _applicationDbContext.Add(category);
                _applicationDbContext.SaveChanges();
                TempData["success"]="Category Create Successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Edit(int? id) 
        {
            if (id == null || id ==0) 
            { 
              
                return NotFound();  
            }
            Category? categoryFromDb=_applicationDbContext.categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        
        }

        [HttpPost]
        public IActionResult Edit(Category category)
         {
            

            if (ModelState.IsValid)
            {
                _applicationDbContext.Update(category);
                _applicationDbContext.SaveChanges();
                TempData["success"]="Category Update Successfully";

                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id ==0)
            {

                return NotFound();
            }
            Category? categoryFromDb = _applicationDbContext.categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }

        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _applicationDbContext.categories.Find(id);
                if (obj ==null)
                {
                   return NotFound();
                }
               
            
            _applicationDbContext.categories.Remove(obj);
            _applicationDbContext.SaveChanges();
            TempData["success"]="Category Delete Successfully";

            return RedirectToAction("Index");
          
            
        }
    }
}
