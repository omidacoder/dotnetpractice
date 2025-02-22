using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.User
{
    public class UserDetailsDto
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "نام کاربری")]
        public required string UserName { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string? Name { get; set; }
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }
        public required string RegisteredAt  { get; set; }



    }
}
