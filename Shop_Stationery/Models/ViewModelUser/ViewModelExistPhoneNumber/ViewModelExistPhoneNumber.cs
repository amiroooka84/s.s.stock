using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelExistPhoneNumber
{
    public class ViewModelExistPhoneNumber
    {
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        [DataType(DataType.Text)]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
    }
}
