using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static System.Net.WebRequestMethods;

namespace PROJECT_2024.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int PId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Product Name field is required!")]
        public string PName { get; set; }
       
        public int Category { get; set; }
        [Required]
        public int Price { get; set; }
        
        public int AgeLemt { get; set; }
        [Required]
        public int Quantity { get; set; }
        [DisplayName("Upload File")]
        public string Image { get; set; }
        [Required]
        public DateTime RelasDate { get; set; }
        //public HttpPostedFileBase ImageFile { get; set; }
    }
}