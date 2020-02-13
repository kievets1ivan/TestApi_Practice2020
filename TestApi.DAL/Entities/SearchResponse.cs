using System;
using System.Collections.Generic;
using System.Text;

namespace TestApi.DAL.Entities
{
    public class SearchResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
    }
}
