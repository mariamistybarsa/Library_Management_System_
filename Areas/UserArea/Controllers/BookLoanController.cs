//BookLoanController
using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Library_Management_System.Areas.UserArea.Controllers
{
    [Area("UserArea")]

    public class BookLoanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookLoanController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexBook()
        {
            var books = await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();
            return View(books);
        }
        public IActionResult BookList()
        {
            var books = _context.Books
                .Where(b => b.AvailableCopies > 0) 
                .ToList();

            return View(books);
        }

        public async Task<IActionResult> LoanBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null || book.TotalCopies <= 0)
            {
                TempData["ErrorMessage"] = "Book is not available!";
                return RedirectToAction("IndexBook");
            }

            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmLoan(int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book == null || book.TotalCopies <= 0)
            {
                TempData["ErrorMessage"] = "Book not available";
                return RedirectToAction("IndexBook");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found. Please log in again.";
                return RedirectToAction("IndexBook");
            }

            var loan = new BookLoan
            {
                BookId = bookId,
                UserId = user.Id,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),  
            };

            _context.BookLoan.Add(loan);

            book.AvailableCopies -= 1;
            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return RedirectToAction("BorrowDetails", new { id = loan.BookLoanId });
        }


        public async Task<IActionResult> BorrowDetails(int id)
        {
            var loan = await _context.BookLoan
                .Include(bl => bl.Book)
                .Include(bl => bl.User)
                .FirstOrDefaultAsync(bl => bl.BookLoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }
        public async Task<IActionResult> BorrowList()
        {
            var allLoans = await _context.BookLoan
                .Include(bl => bl.Book)
                .Include(bl => bl.User)
                .ToListAsync();

            return View(allLoans);
        }

    }
}