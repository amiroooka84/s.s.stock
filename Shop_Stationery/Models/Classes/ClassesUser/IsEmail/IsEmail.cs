using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.Classes.ClassesUser.IsEmail
{
    public class IsEmail
    {
        public bool isEmail(string email)
        {
            Regex regex1 = new Regex("^[a-zA-Z]+[a-zA-Z0-9]+[[a-zA-Z0-9-_.!#$%'*+/=?^]{1,20}@[a-zA-Z0-9]{1,20}.[a-zA-Z]{2,3}$");
            if (regex1.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
