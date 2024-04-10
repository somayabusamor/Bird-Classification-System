using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJECT_2024.Models
{
    public class Customer
    {
        [Required]
        public string CustomerID { get; set; }
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }
        [Required (ErrorMessage = "This field is required!")]
        [DataType(DataType.Password)]
        public int Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public int ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [RegularExpression("^[0-9]{9}", ErrorMessage = "Customer Phone must contain 9 digit")]
        public int Phone { get; set; }

        public bool IsAdmin { get; set; }

    }
}