using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.Product
{
    public class ProductFilterDto
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public double? minPrice { get; set; }
        public double? maxPrice { get; set; }
    }
}
