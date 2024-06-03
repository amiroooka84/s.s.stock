using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelAccount
{
    public class ViewModelEditProfile
    {

        [DataType(DataType.EmailAddress)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "نام را وارد کنید")]
        [DataType(DataType.Text)]
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "باید کمتر از 100 حرف باشد")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [DataType(DataType.Text)]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(100 , ErrorMessage = "باید کمتر از 100 حرف باشد")]
        public string LastName { get; set; }
    }
}
