using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.Auth
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
