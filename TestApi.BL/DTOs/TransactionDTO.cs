using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestApi.DAL.Enums;

namespace TestApi.BL.DTOs
{
    public class TransactionDTO
    {
        public TypeOperation Operation { get; set; }

        [Required(ErrorMessage = "Fill in field 'Quantity', please", AllowEmptyStrings = false)]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity can't be less than 1")]
        public int Quantity { get; set; }
    }
}
