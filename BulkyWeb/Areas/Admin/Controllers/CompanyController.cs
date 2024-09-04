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
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objProdectList = _unitOfWork.Company.GetAll().ToList();
            return View(objProdectList);
        }
        public IActionResult UpSert(int? id)
        {
           
            if (id == null || id == 0)
            {
                //create
                return View(new Company());

            }
            else
            {
                //update
                Company company=_unitOfWork.Company.Get(u => u.id == id);
                return View(company);
            }
           
        }
        [HttpPost]
        public IActionResult UpSert(Company company)
        {
           
          
            if (ModelState.IsValid)
            {
               
               

                if (company.id==0)
                {
                    _unitOfWork.Company.Add(company);
                }
                  else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = "company Create Successfully";
                return RedirectToAction("Index");

            }
            else
            {

               
                return View(company);
            }
            
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {

        //        return NotFound();
        //    }
        //    Company? prodctFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (prodctFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(prodctFromDb);

        //}

        //[HttpPost]
        //public IActionResult Edit(Company Company)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(Company);
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
        //    Company? prodctFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (prodctFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(prodctFromDb);

        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }


        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category Delete Successfully";

        //    return RedirectToAction("Index");


        //}

        #region APICALL

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted=_unitOfWork.Company.Get(u =>u.id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success=false,message = "Error While Deleteing" });
            }
                
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();


            return Json(new { success = true, message = "Delete Successful" });
        }
            
        
        #endregion
    }
}

