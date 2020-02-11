using System;
using System.Collections.Generic;
using System.Text;
using TestApi.DAL.Enums;

namespace TestApi.BL.DTOs
{
    public class TransactionOutcomeDTO
    {
        public TypeOperation Operation { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public ProductDTO Product { get; set; }
        public string UserName { get; set; }
    }
}
