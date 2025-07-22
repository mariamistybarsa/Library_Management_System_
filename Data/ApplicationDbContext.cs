using Library_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
            public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
       
        public DbSet<BookLoan> BookLoan { get; set; }
        //public DbSet<Book> Books { get; set; }
    }
}

