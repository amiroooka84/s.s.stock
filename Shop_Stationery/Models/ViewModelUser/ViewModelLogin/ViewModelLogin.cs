using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelLogin
{
    public class ViewModelLoginCode
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        [DataType(DataType.Text)]
        [Display(Name = "کد تایید")]
        public string ConfirmCode { get; set; }
    }
    public class ViewModelLoginPassword
    {
        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string password { get; set; }
    }
}
