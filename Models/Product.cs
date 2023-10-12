using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CRUD.Models
{
    [Bind("FirstName,LastName, Age, country, Image")]

    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Its Required Feild Please Enter Your FirstName. ")]
        [MinLength(5,ErrorMessage = "Must be at least 5 characters long")]
        [MaxLength(25,ErrorMessage ="Max Length of first name is 25 characters.")]
        [DisplayName("الاسم الاول")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "Its Required Feild Please Enter Your LastName. ")]
        [MinLength(5, ErrorMessage = "Must be at least 5 characters long")]
        [MaxLength(25, ErrorMessage = "Max Length of Last Name is 25 characters.")]
        [DisplayName("الاسم الأخير")]

        public string LastName { get; set; }



        [Required(ErrorMessage = "Its Required Feild Please Enter Your Age. ")]

        [Range(18, 100, ErrorMessage = "Age must be between 1 and 100")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Its Required Feild Please Enter Your Country. ")]

        public string country { get; set; }
        [Required(ErrorMessage = "Its Required Feild Please Enter Your fkin image. ")]

  
        [NotMapped]
        [DisplayName("Image")]
        public IFormFile Image { get; set; } // For storing uploaded file temporarily during upload process
        public string? ImagePath { get; set; } // For storing file path in the database



    }
}
