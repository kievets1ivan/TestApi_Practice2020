using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.DTOs
{
    public class ProductDTO
    {
        //сделать валидацию everywhere
        [Required(ErrorMessage = "Fill in ur name!", AllowEmptyStrings = false)]
        //[StringLength(50, )]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
