using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Dtos.Product
{
    public class UpdateProductDto
    {
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string Name { get; set; }
        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند از 200 کاراکتر بیشتر باشد")]
        public string? Description { get; set; }
        [Display(Name = "تعداد موجود در انبار")]
        [Range(0, 100000, ErrorMessage = "{0} باید عددی بزرگتر از 0 و کوچکتر از 100000 باشد")]
        public required int AvailableCount { get; set; }
        //[Display(Name = "آدرس تصویر")]
        //[Url(ErrorMessage = "{0} باید آدرس معتبر باشد")]
        //public string? photoUrl { get; set; }
    }
}
