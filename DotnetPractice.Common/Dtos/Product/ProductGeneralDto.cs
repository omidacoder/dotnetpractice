using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.Product
{
    public class ProductGeneralDto
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int AvailableCount { get; set; }
        public string? photoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
