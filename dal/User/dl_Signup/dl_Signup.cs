using Entity;
using Entity.UserApp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace dal.User.dl_Signup
{
    public class dl_Signup
    {
        private readonly UserManager<UserApp> _userManager;

        public dl_Signup(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        /////////////
        #region ExistPhoneNumber
        //متد برای بودن شماره تلفن
        private async Task<bool> ExistPhoneNumber(string PhoneNumber)
        {
            int a = 0;
            db db = new db();
            try
            {
                await foreach (var item in db.Users)
                {
                    if (item.PhoneNumber == PhoneNumber)
                    {
                        a++;
                    }
                }
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }

        }
        #endregion
        /////////////

        /////////////
        #region SignUp
        public async Task<string> SignUp(UserApp user)
        {
            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return "این شماره موجود می باشد";
            }
            else
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    return "کاربر ثبت نام شد";
                }
                else
                {
                    if (result.ToString() == "Failed : PasswordTooShort")
                    {
                        return "رمز عبور باید حداقل 6 کاراکتر باشد";

                    }
                    else
                    {
                        return result.ToString(); ;
                    }
                }
            }
        }

        #endregion
        /////////////
    }
}
