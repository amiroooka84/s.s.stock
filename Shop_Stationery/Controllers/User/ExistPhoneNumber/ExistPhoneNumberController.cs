using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bll.User.bl_ExistPhoneNumber;
using Microsoft.AspNetCore.Identity;
using Entity;
using Entity.UserApp;
using static Entity.UserApp.userapp;
using Microsoft.Extensions.Logging;
using Shop_Stationery.Models.Classes.ClassesUser.SMS;
using System.Threading;
using Shop_Stationery.Models.ViewModelUser.ViewModelExistPhoneNumber;
using Microsoft.AspNetCore.Http;
using Shop_Stationery.Models.Classes.ClassesUser.ConfirmCode;
using Newtonsoft.Json;

namespace Shop_Stationery.Controllers.User.ExistPhoneNumber
{
    public class ExistPhoneNumberController : Controller
    {
        private  UserManager<UserApp> _userManager;
        public ExistPhoneNumberController(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        ////////////////////
        #region PhoneNumberAction
        [Route("/PhoneNumber")]
        public IActionResult ExistPhoneNumber(string ReturnUrl)
        {
            TempData["CallBackUrl"] = ReturnUrl;
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            return View();
        }
        [Route("/PhoneNumber")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExistPhoneNumber(ViewModelExistPhoneNumber Model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            if (Model.PhoneNumber == null)
            {
                ModelState.AddModelError(string.Empty, "اطلاعات را تکمیل نمایید");
                return View();
            }
            if (Model.PhoneNumber.Trim().Length != 11)
            {
                ModelState.AddModelError(string.Empty , "شماره موبایل معتبر نمی باشد");
                ViewData["PhoneNumber"] = Model.PhoneNumber;
                return View();
            }
            else if(!Model.PhoneNumber.Trim().StartsWith("09"))
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل معتبر نمی باشد");
                ViewData["PhoneNumber"] = Model.PhoneNumber;
                return View();
            }
            else
            {
                bl_ExistPhoneNumber _ExistPhoneNumber = new bl_ExistPhoneNumber(_userManager);
                bool result = await _ExistPhoneNumber.ExistPhoneNumber(Model.PhoneNumber);
                if (!result)
                {
                    ConfirmCode ConfirmCode = new ConfirmCode();
                    ConfirmCode.PhoneNumber = Model.PhoneNumber.Trim();
                    TempData["ConfirmCode"] = JsonConvert.SerializeObject(ConfirmCode);
                    TempData.Keep();
                    //HttpContext.Session.SetString("ConfirmCode", JsonConvert.SerializeObject(ConfirmCode));
                    return RedirectToAction("ConfirmCode", "Signup");
                }
                else
                {
                    ConfirmCode confirmCode = new ConfirmCode();
                    confirmCode.PhoneNumber = Model.PhoneNumber.Trim();
                    TempData["ConfirmCodeLogin"] = JsonConvert.SerializeObject(confirmCode);
                    TempData["CallBackUrl"] = TempData["CallBackUrl"];
                    TempData.Keep();
                    //HttpContext.Session.SetString("ConfirmCodeLogin", JsonConvert.SerializeObject(confirmCode));
                    return RedirectToAction("Login", "Login");
                }
            }
        }
        #endregion
        ////////////////////
    }
}
