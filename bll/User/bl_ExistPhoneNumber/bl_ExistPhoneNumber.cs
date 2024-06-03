using Entity;
using Entity.UserApp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;
using dal.User.dl_ExistPhoneNumber;

namespace bll.User.bl_ExistPhoneNumber
{
    public class bl_ExistPhoneNumber
    {
        private readonly UserManager<UserApp> _userManager;

        public bl_ExistPhoneNumber(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> ExistPhoneNumber(string PhoneNumber)
        {
            dl_ExistPhoneNumber _ExistPhoneNumber = new dl_ExistPhoneNumber(_userManager);
            bool result = await _ExistPhoneNumber.ExistPhoneNumber(PhoneNumber);
            return result;
        }
    }
}
