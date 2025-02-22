using DotnetPractice.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos
{
    public class GeneralResponse<T>
    {
        
        public required ResponseStatusEnum status { get; set; }
        public T? data { get; set; }
        public string? message { get; set; }

    }
}
