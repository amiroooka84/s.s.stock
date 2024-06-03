using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dal;
using Entity;
using Entity.UserApp;
using Microsoft.AspNetCore.Identity;
using static Entity.UserApp.userapp;
using dal.User.dl_Signup;
namespace bll.User.bl_Signup
{
    public class bl_Signup
    {
        private readonly UserManager<UserApp> _userManager;

        public bl_Signup(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }
        

        public async Task<string> SignUp(UserApp user)
        {
            dl_Signup dl_Signup = new dl_Signup(_userManager);
            return await dl_Signup.SignUp(user);
        }
    }
}
