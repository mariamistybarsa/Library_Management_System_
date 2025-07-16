using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Library_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult GetCategory()
        {
            var model = _db.Categories.ToList();
            return new JsonResult(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel c)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(c);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var m = _db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (m == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(CategoryModel c)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(c);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int CategoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            if (category == null)
            {
                return Json(new { success = false, message = "Category not found" });
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return Json(new { success = true, message = "Category deleted successfully" });
        } }}
