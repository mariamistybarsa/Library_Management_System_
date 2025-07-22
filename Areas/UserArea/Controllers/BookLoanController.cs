using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Data;
using Library_Management_System.ViewModel;
using System.Security.Claims;
using Library_Management_System.Models;

namespace Library_Management_System.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class BookLoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookLoanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index Method
     
        public IActionResult BookList()
        {
            var books = _context.Books
                .Where(b => b.AvailableCopies > 0) // Optional: show only available books
                .ToList();

            return View(books);
        }
        [HttpGet]
        public IActionResult BorrowCreate(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null || book.AvailableCopies <= 0)
                return NotFound();

            var model = new BookLoanViewModel
            {
                BookId = book.BookId,
                Book = book,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7) // optional default
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BorrowCreate(BookLoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var borrow = new BookLoan
                {
                    BookId = model.BookId,
                    BorrowDate = model.BorrowDate,
                    ReturnDate = model.ReturnDate,
                    UserId = userId
                };

                _context.BookLoan.Add(borrow);
                _context.SaveChanges();

                return RedirectToAction("BorrowDetails", new { id = borrow.BookLoanId });
            }

            model.Book = _context.Books.FirstOrDefault(b => b.BookId == model.BookId);
            return View(model);
        }



    }
}
