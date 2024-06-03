using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelSignup
{
    public class ViewModelExistPhoneNumber
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        [DataType(DataType.Text)]
        [Display(Name = "کد تایید")]
        public string ConfirmCode { get; set; }
    }
}
