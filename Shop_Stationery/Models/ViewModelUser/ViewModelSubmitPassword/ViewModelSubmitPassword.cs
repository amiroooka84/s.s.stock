using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelSubmitPassword
{
    public class ViewModelSubmitPassword
    {
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        public string PasswordAgain { get; set; }
    }
}
