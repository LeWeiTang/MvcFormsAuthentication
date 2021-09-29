using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFormsAuthentication.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="不得為空白,至少需要3個字元")]
        [Display(Name ="帳號")]
        public string Name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "不得為空白,至少需要6個字元")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
        [Display(Name = "記得我")]
        public bool Remember { get; set; }
    }
}