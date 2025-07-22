//BookLoanViewModel

using Library_Management_System.Models;

namespace Library_Management_System.ViewModel
{
    public class BookLoanViewModel
    {

        public int BookId { get; set; }
        public Book Book { get; set; } // এইটা শুধুই View purposes এর জন্য, তাই validation optional হওয়া উচিত




        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

   

    }
}
