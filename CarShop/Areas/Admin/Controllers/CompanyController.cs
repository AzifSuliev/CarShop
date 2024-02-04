using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }



        public IActionResult Upsert(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == Id);
                return View(companyFromDb);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company? CompanyObj)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Компания была успешно изменена!";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(CompanyObj);
            }
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompaniesList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompaniesList });
        }

        public IActionResult Delete(int? Id)
        {

            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == Id);

            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Ошибка удаления" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Объект удалён" });
        }
        #endregion
    }
}
