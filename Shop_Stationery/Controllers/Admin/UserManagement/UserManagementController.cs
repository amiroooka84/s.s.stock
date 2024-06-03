using bll.Admin.bl_User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace Shop_Stationery.Controllers.Admin.UserManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<UserApp> _UserManager;
        public UserManagementController(UserManager<UserApp> UserManager)
        {
            _UserManager = UserManager;
        }
        ////////////////
        #region UserManagement
        [Route("/UserManagement")]
        public IActionResult UserManagement()
        {
            bl_User bl_User = new bl_User();
            List<UserApp> userApps = new List<UserApp>();
            userApps = bl_User.ReadUsers();
            ViewBag.Users = userApps;
            return View();
        }
        #endregion
        ////////////////

        ////////////////
        #region RemoveUser
        public IActionResult RemoveUser()
        {
            bl_User bl_User = new bl_User();
            List<UserApp> userApps = new List<UserApp>();
            userApps = bl_User.ReadUsers();
            ViewBag.Users = userApps;
            return View();
        }
        #endregion
        ////////////////

        ////////////////
        #region InformationUser
        [Route("/UserManagement/InformationUser")]
        public async Task<IActionResult> InformationUser(string NumberPhone)
        {
            UserApp userApp = new UserApp();
            userApp = await _UserManager.FindByNameAsync(NumberPhone);        
            ViewBag.User = userApp;
            return View();
        }
        #endregion
        ////////////////

        ////////////////
        #region ChengeRole
        [Route("/UserManagement/ChengeRole")]
        public async Task<IActionResult> ChengeRole(string NumberPhone , string Role)
        {
            UserApp userApp = new UserApp();
            userApp = await _UserManager.FindByNameAsync(NumberPhone);
            if (Role == "Admin")
            {
                await _UserManager.AddClaimAsync(userApp, new Claim("AdminNumber", "5"));
            }
            else if (Role == "User")
            {
                await _UserManager.RemoveClaimAsync(userApp, new Claim("AdminNumber", "5"));
            }
            ViewBag.User = userApp;
            return View("InformationUser" , NumberPhone);
        }
        #endregion
        ////////////////

    }
}
