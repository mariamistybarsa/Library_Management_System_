using Library_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("AdminArea")]
public class AjaxController : Controller
{
    private readonly ApplicationDbContext _db;
    public AjaxController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetBookDetails(int id)
    {
        var book = _db.Books.Include(c => c.Category).FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound();

        return PartialView("_BookDetailsPartial", book);
    }
}
