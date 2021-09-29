using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFormsAuthentication.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "姓名")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "不得為空白,至少需3個字元")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[NotMapped]
        [Required]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "最少需6個位元")]
        [Compare("Password", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "電子郵件")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "不得為空白,至少需3個字元")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "電話")]
        [RegularExpression(@"^09\d{2}\-?\d{3}\-?\d{3}$", ErrorMessage = "需為09xx-xxx-xxx格式")]
        public string Mobile { get; set; }
    }
}