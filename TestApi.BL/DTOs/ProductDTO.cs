using System;
using System.ComponentModel.DataAnnotations;

namespace TestApi.BL.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Fill in field 'Name', please", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Name of product can't contain over 50 symbols")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Fill in field 'Price', please", AllowEmptyStrings = false)]
        [Range(1,10000, ErrorMessage = "Price can't be over 10000 and less than 1")]
        public decimal Price { get; set; }
    }
}
