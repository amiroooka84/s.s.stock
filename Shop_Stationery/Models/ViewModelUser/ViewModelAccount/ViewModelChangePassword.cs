using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelAccount
{
    public class ViewModelChangePassword
    {
        [Required(ErrorMessage = "رمز عبور فعلی")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        [MaxLength(100, ErrorMessage = "باید کمتر از 100 حرف باشد")]
        [MinLength(6, ErrorMessage = "باید بیشتر از 6 حرف باشد")]
        public string PasswordNow { get; set; }
        [Required(ErrorMessage = "رمز عبور جدید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        [MaxLength(100, ErrorMessage = "باید کمتر از 100 حرف باشد")]
        [MinLength(6, ErrorMessage = "باید بیشتر از 6 حرف باشد")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تکرار رمز عبور جدید")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [MaxLength(100, ErrorMessage = "باید کمتر از 100 حرف باشد")]
        [MinLength(6, ErrorMessage = "باید بیشتر از 6 حرف باشد")]
        public string PasswordAgain { get; set; }
    }
}
