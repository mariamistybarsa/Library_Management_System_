//Book.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? ISBN { get; set; }

        public string? Publisher { get; set; }

        [Display(Name = "Published Date")]
        public DateTime? PublishedDate { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel? Category { get; set; }

        [Display(Name = "Cover Image")]
        public string? CoverImagePath { get; set; }

        public int? TotalCopies { get; set; }

        public int? AvailableCopies { get; set; }
    }
}
