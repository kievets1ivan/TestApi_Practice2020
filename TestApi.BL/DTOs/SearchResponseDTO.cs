using System.Collections.Generic;

namespace TestApi.BL.DTOs
{
    public class SearchResponseDTO
    {
        public int TotalCount { get; set; }
        public IEnumerable<ProductOutcomeDTO> Products { get; set; }
    }
}
