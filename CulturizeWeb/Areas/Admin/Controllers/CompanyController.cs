using Culturize.DataAccess.Repository.IRepository;
using Culturize.Models;
using Culturize.Utility;
using CulturizeWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CulturizeWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompanyController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBlobStorageService _blobStorage;

        public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWOrk, IWebHostEnvironment webHostEnvironment, IBlobStorageService blobStorage)
        {
            _unitOfWork = unitOfWOrk;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _blobStorage = blobStorage;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> companyList = _unitOfWork.CompanyRepo.GetAll();
            return View(companyList);
        }

        public IActionResult Upsert(int? id)
        {
            Company? model = new Company();

            if (id.GetValueOrDefault() > 0)
                model = _unitOfWork.CompanyRepo.Get(p => p.Id == id);

            if (model == null)
                return NotFound();

            if (!String.IsNullOrEmpty(model.LogoBlobFile))
                model.LogoUrl = _blobStorage.GetBlob(model.LogoBlobFile).Uri.ToString();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Company company, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //delete old image
                    if (!String.IsNullOrEmpty(company.LogoBlobFile))
                        await _blobStorage.DeleteBlobAsync(company.LogoBlobFile);

                    //upload to AzureBlob
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    await _blobStorage.UploadImageAsync(file, fileName);

                    company.LogoBlobFile = fileName;
                }

                string msg;

                if (company.Id > 0)
                {
                    _unitOfWork.CompanyRepo.Update(company);
                    msg = "Company updated successfully";
                }
                else
                {
                    _unitOfWork.CompanyRepo.Add(company);
                    msg = "Product created successfully";
                }

                _unitOfWork.Save();

                TempData["success"] = msg;

                return RedirectToAction("Index");
            }

            //invalid model
            return View(company);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.CompanyRepo.GetAll();
            return Json(new
            {
                data = productList
            });
        }

        #region API Endpoints

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return Json(new { success = false, msg = "Company not found." });

            var company = _unitOfWork.CompanyRepo.Get(u => u.Id == id);

            if (company == null)
                return Json(new { success = false, msg = "Company not found." });

            //delete old image
            if (!String.IsNullOrEmpty(company.LogoBlobFile))
                await _blobStorage.DeleteBlobAsync(company.LogoBlobFile);


            _unitOfWork.CompanyRepo.Delete(company);
            _unitOfWork.Save();

            return Json(new { success = true, msg = "Company deleted successfully." });
        }

        #endregion
    }
}
