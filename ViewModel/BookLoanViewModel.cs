using Library_Management_System.Models;

namespace Library_Management_System.ViewModel
{
    public class BookLoanViewModel
    {

        public int BookId { get; set; }

        public Book Book { get; set; } 

        public string UserId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

   

    }
}
