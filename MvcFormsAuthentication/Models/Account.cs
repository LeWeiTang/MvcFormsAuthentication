using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcFormsAuthentication.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "不得為空白,至少需要3個字元")]
        public string Name { get; set; }
        [Required]
        [StringLength(1280, MinimumLength = 6, ErrorMessage = "不得為空白,至少需要6個字元")]
        public string Password { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "不得為空白,至少需要3個字元")]
        public string Email { get; set; }
        [RegularExpression(@"^09\d{2}\-?\d{3}\-?\d{3}$", ErrorMessage = "需為09xx-xxx-xxx格式")]
        public string Mobile { get; set; }
    }
}