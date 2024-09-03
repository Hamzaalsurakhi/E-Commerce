using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utiltiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment= webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProdectList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProdectList);
        }
        public IActionResult UpSert(int? id)
        {
           
          
            ProdcutVM prodcutVM = new ProdcutVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u =>
           new SelectListItem
           {
               Text = u.Name,
               Value=u.Id.ToString(),
           }),
                Prodcut =new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(prodcutVM);

            }
            else
            {
                //update
                prodcutVM.Prodcut=_unitOfWork.Product.Get(u => u.Id == id);
                return View(prodcutVM);
            }
           
        }
        [HttpPost]
        public IActionResult UpSert(ProdcutVM product,IFormFile? file)
        {
           
          
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName= Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string prodouctPath = Path.Combine(wwwRootPath, @"images\product");


                    if (!string.IsNullOrEmpty(product.Prodcut.ImageUrl))
                    {
                        var oldImagePath=
                            Path.Combine(wwwRootPath,product.Prodcut.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(prodouctPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.Prodcut.ImageUrl = "/images/product/" + fileName;

                }

                if (product.Prodcut.Id==0)
                {
                    _unitOfWork.Product.Add(product.Prodcut);
                }
                  else
                {
                    _unitOfWork.Product.Update(product.Prodcut);
                }
                _unitOfWork.Save();
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");

            }
            else
            {

                product.CategoryList = _unitOfWork.Category.GetAll().Select(u =>
               new SelectListItem
               {
                   Text = u.Name,
                   Value=u.Id.ToString(),
               });
                return View(product);
            }
            
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {

        //        return NotFound();
        //    }
        //    Product? prodctFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (prodctFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(prodctFromDb);

        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category Update Successfully";

        //        return RedirectToAction("Index");

        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {

        //        return NotFound();
        //    }
        //    Product? prodctFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (prodctFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(prodctFromDb);

        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }


        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category Delete Successfully";

        //    return RedirectToAction("Index");


        //}

        #region APICALL

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProdectList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProdectList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted=_unitOfWork.Product.Get(u =>u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success=false,message = "Error While Deleteing" });
            }
                var oldImagePath =
                Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Delete Successful" });
        }
            
        
        #endregion
    }
}

