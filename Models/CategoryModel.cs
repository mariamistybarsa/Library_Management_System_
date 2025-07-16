using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        
        //public List<Book> Books { get; set; }



    }
}
