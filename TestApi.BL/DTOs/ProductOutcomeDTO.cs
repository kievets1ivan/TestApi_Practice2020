using System;
using System.Collections.Generic;
using System.Text;

namespace TestApi.BL.DTOs
{
    public class ProductOutcomeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
                
        public decimal TotalPrice { get => Price * Quantity; }

    }
}
