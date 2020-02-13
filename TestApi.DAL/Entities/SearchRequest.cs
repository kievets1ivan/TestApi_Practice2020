using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestApi.DAL.Enums;

namespace TestApi.DAL.Entities
{
    public class SearchRequest
    {
        [Required(ErrorMessage = "Fill in field 'Page', please", AllowEmptyStrings = false)]
        public int Page { get; set; }

        [Required(ErrorMessage = "Fill in field 'PerPage', please", AllowEmptyStrings = false)]
        public int PerPage { get; set; }

        [Required(ErrorMessage = "Fill in field 'SortBy', please", AllowEmptyStrings = false)]
        public SortByColumn SortBy { get; set; }
    }
}
