using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Stationery.Models.Classes.ClassesUser.ConfirmCode;
using Shop_Stationery.Models.Classes.ClassesUser.SMS;
using Shop_Stationery.Models.ViewModelUser.ViewModelLogin;
using Microsoft.AspNetCore.Identity;
using static Entity.UserApp.userapp;
using Shop_Stationery.Models.ViewModelUser.ViewModelExistPhoneNumber;

namespace Shop_Stationery.Controllers.User
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _SignInManager;

        public ForgotPasswordController(UserManager<UserApp> userManager, SignInManager<UserApp> SignInManager)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
        }


        ///////////////
        #region ForgotPasswordPhoneNumber
        [Route("/ForgotPasswordPhoneNumber")]
        public IActionResult PhoneNumber()
        {
            return View();
        }
        #endregion
        ///////////////

        ///////////////
        #region ForgotPassword
        [Route("/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ViewModelExistPhoneNumber model)
        {
            TempData["ConfirmCodeForgot"] = TempData["ConfirmCodeForgot"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            if (model.PhoneNumber == null)
            {
                ModelState.AddModelError(string.Empty, "اطلاعات را تکمیل نمایید");
                return View("PhoneNumber");
            }
            if (model.PhoneNumber.Trim().Length != 11)
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل معتبر نمی باشد");
                ViewData["PhoneNumber"] = model.PhoneNumber;
                return View("PhoneNumber");
            }
            else if (!model.PhoneNumber.Trim().StartsWith("09"))
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل معتبر نمی باشد");
                ViewData["PhoneNumber"] = model.PhoneNumber;
                return View("PhoneNumber");
            }
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (userApp == null)
            {
                ModelState.AddModelError(string.Empty, "تلفن در سیستم موجود نمی باشد");
                ViewData["PhoneNumber"] = model.PhoneNumber;
                return View("PhoneNumber");
            }
            ConfirmCode ConfirmCode = new ConfirmCode();
            if (TempData["ConfirmCodeForgot"] != null)
            {
                ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeForgot"]);
                ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
            }
            if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                Random random = new Random();
                int rnd = random.Next(10000, 99999);
                ConfirmCode.PhoneNumber = model.PhoneNumber;
                ViewBag.Code = rnd.ToString();
                ConfirmCode.Code = rnd.ToString();
                ConfirmCode.TimeExpiration = DateTime.Now.AddMinutes(3);
                SMS s = new SMS();
                bool ResultSendCode = s.SendConfirmCode(rnd.ToString(), model.PhoneNumber);
                ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
                TempData["ConfirmCodeForgot"] = JsonConvert.SerializeObject(ConfirmCode);
                TempData.Keep();
            }
            return View();
        }

        [Route("/ForgotPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ViewModelLoginCode Code)
        {
            TempData["ConfirmCodeForgot"] = TempData["ConfirmCodeForgot"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeForgot"]);
            ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
            if (Code.ConfirmCode == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                return View();
            }
            if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "کد نامعتبر است");
                return View();
            }
            if (ConfirmCode.Code == Code.ConfirmCode)
            {
                Random random = new Random();
                int rnd = random.Next(100000, 999999);
                UserApp userApp = new UserApp();
                userApp = await _userManager.FindByNameAsync(ConfirmCode.PhoneNumber);
                var a = _userManager.GeneratePasswordResetTokenAsync(userApp).Result;
                var p = _userManager.ResetPasswordAsync(userApp , a , rnd.ToString()).Result;
                SMS s = new SMS();
                bool ResultSendCode = s.SendConfirmCode(rnd.ToString(), "09224982760");
                return Redirect("/PhoneNumber");
            }

            ModelState.AddModelError(string.Empty, "لطفا بعدا تلاش کنید");
            return View();

        }
        #endregion
        ///////////////
    }
}
