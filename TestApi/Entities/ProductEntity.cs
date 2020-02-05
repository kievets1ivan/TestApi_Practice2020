using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
