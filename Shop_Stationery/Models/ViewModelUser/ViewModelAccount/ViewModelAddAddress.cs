using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelAccount
{
    public class ViewModelAddAddress
    {
        [Required(ErrorMessage = "آدرس را وارد کنید")]
        [DataType(DataType.Text)]
        [Display(Name = "آدرس")]
        [MaxLength(400, ErrorMessage = "باید کمتر از 400 حرف باشد")]
        public string Address { get; set; }
    }
}
