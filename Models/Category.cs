using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROJECT_2024.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "The Category Name field is required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category Name must be between 2 and 50 characters.")]
        public string CategoryName { get; set; }

       
    }
}