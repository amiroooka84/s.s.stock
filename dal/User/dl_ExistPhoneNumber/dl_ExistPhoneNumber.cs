using Entity;
using Entity.UserApp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace dal.User.dl_ExistPhoneNumber
{
    public class dl_ExistPhoneNumber
    {
        private readonly UserManager<UserApp> _userManager;

        public dl_ExistPhoneNumber(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        ////////////
        #region ExistPhoneNumber
        public async Task<bool> ExistPhoneNumber(string PhoneNumber)
        {
            var result = await _userManager.FindByNameAsync(PhoneNumber);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        ////////////

    }
}
