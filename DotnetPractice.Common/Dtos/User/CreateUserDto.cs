using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.User
{
    public class CreateUserDto
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} را وارد کنید")]
        public required string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "مقدار {0} را وارد کنید")]
        public required string Password { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string? Name { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد کنید")]
        public string? Email { get; set; }
    }
}
