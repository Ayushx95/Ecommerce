using Ecommerce.Data;
using Ecommerce.DataAccess.Repository;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofWork;

        public CategoryController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null) return View(category);
            category = _unitofWork.Category.Get(id.GetValueOrDefault());
            return View(category);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (category == null) return BadRequest();
            if (ModelState.IsValid) return View(category);
            if (category.Id == 0)
                _unitofWork.Category.Add(category);
            else
                _unitofWork.Category.Update(category);
            _unitofWork.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _unitofWork.Category.GetAll();
            return Json(new { data = categories });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CategoryinDb = _unitofWork.Category.Get(id);
            if (CategoryinDb == null)
                return Json(new
                {
                    success = false,
                    message = "Unable to delete data"
                });
            _unitofWork.Category.Remove(CategoryinDb);
            _unitofWork.Save();
            return Json(new
            {
                success = true,
                message = "Data deleted successfully"
            });
        }
    }
}
