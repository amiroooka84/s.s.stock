using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.Classes.ClassesUser.ConfirmCode
{
    public class ConfirmCode
    {
        public string Code { get; set; }
        public bool Confirm  { set; get; }
        public string PhoneNumber { get; set; }
        public DateTime TimeExpiration { get; set; }
    }
}
