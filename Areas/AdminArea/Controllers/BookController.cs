using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> IndexBook()
        {
            //var books = await _db.Books.Include(b => b.Category).ToListAsync();
            return View();
        }
        public async Task<IActionResult> GetBook()
        {
            var books = await _db.Books.Include(b => b.Category).ToListAsync();

            if (ModelState.IsValid)
            {
                var result = books.Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Author,
                    b.ISBN,
                    b.Publisher,
                    PublishedDate = b.PublishedDate,
                    categoryName = b.Category != null ? b.Category.CategoryName : "",
                    coverImagePath = b.CoverImagePath,
                    b.TotalCopies,
                    b.AvailableCopies
                });

                return Json(result);
            }

            return Json(new { error = "Invalid model state" });
        }




        [HttpGet]
        public IActionResult CreateBook()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(Book d, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string uploadPath = Path.Combine(wwwRootPath, "images/covers");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    d.CoverImagePath = "/images/covers/" + fileName;
                }

                _db.Books.Add(d);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexBook");
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View(d);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return Json(new { success = false });
            }

            _db.Books.Remove(book);
            _db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult EditPartial(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _db.Categories.ToList();
            return PartialView("_EditPartial", book);
        }

        // POST: AdminArea/Book/SaveBook
        [HttpPost]
        public IActionResult SaveBook(Book model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return PartialView("_EditPartial", model);
            }

            var book = _db.Books.FirstOrDefault(b => b.BookId == model.BookId);
            if (book == null)
            {
                return Json(new { success = false });
            }

            book.Title = model.Title;
            book.Author = model.Author;
            book.ISBN = model.ISBN;
            book.Publisher = model.Publisher;
            book.PublishedDate = model.PublishedDate;
            book.CategoryId = model.CategoryId;
            book.TotalCopies = model.TotalCopies;
            book.AvailableCopies = model.AvailableCopies;
            book.CoverImagePath = model.CoverImagePath;

            _db.SaveChanges();

            return Json(new { success = true });
        }
    }
}