using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProdectList = _unitOfWork.Product.GetAll().ToList();
            return View(objProdectList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
           

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            Product? prodctFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (prodctFromDb == null)
            {
                return NotFound();
            }
            return View(prodctFromDb);

        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "Category Update Successfully";

                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            Product? prodctFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (prodctFromDb == null)
            {
                return NotFound();
            }
            return View(prodctFromDb);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }


            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Delete Successfully";

            return RedirectToAction("Index");


        }
    }
}

