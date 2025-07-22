using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class BookLoan
    {
        [Key]
        public int BookLoanId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public DateTime BorrowDate { get; set; }= DateTime.Now;
       

        public DateTime? ReturnDate { get; set; }
        
    }
}
