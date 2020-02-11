using System;
using System.Collections.Generic;
using System.Text;
using TestApi.DAL.Enums;

namespace TestApi.BL.DTOs
{
    public class TransactionDTO
    {
        public TypeOperation Operation { get; set; }
        public int Quantity { get; set; }
    }
}
