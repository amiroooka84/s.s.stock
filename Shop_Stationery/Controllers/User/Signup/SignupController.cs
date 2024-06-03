using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Stationery.Models.ViewModelUser.ViewModelSignup;
using bll.User.bl_Signup;
using Entity;
using Entity.UserApp;
using static Entity.UserApp.userapp;
using Shop_Stationery.Models.ViewModelUser.ViewModelSubmitPassword;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop_Stationery.Models.Classes.ClassesUser.ConfirmCode;
using Microsoft.AspNetCore.Authorization;
using Shop_Stationery.Models.Classes.ClassesUser.SMS;
using System.Security.Claims;

namespace Shop_Stationery.Controllers.User.Signup
{
    public class SignupController : Controller
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _SignInManager;

        public SignupController(UserManager<UserApp> userManager , SignInManager<UserApp> SignInManager)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
        }

        /////////////////////////
        #region ConfirmCodeAction

        [Route("/Signup/ConfirmCode")]
        public IActionResult ConfirmCode()
        {
            TempData["ConfirmCode"] = TempData["ConfirmCode"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            //var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(HttpContext.Session.GetString("ConfirmCode"));
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCode"]);
            ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
            if (ConfirmCode.PhoneNumber == null)
            {
                return Redirect("/PhoneNumber");
            }
            if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                Random random = new Random();
                int rnd = random.Next(10000, 99999);
                ConfirmCode.Code = rnd.ToString();
                ConfirmCode.TimeExpiration = DateTime.Now.AddMinutes(3);
                SMS s = new SMS();
                ViewBag.Code = rnd.ToString();
                bool ResultSendCode = s.SendConfirmCode(rnd.ToString(), ConfirmCode.PhoneNumber);
                ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
                TempData["ConfirmCode"] = JsonConvert.SerializeObject(ConfirmCode);
                TempData.Keep();

            }
            return View();
        }

        [Route("/Signup/ConfirmCode")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmCode(ViewModelExistPhoneNumber PhoneNumber)
        {
            TempData["ConfirmCode"] = TempData["ConfirmCode"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            //var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(HttpContext.Session.GetString("ConfirmCode"));
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCode"]);
            ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
            if (PhoneNumber.ConfirmCode == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                return View(PhoneNumber);
            }
            if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "کد نامعتبر است");
                return View(PhoneNumber);
            }
            if (PhoneNumber.ConfirmCode != ConfirmCode.Code )
            {
                ModelState.AddModelError(string.Empty, "کد نامعتبر است");
                return View(PhoneNumber);
            }
            else
            {
                ConfirmCode confirm = new ConfirmCode();
                confirm = ConfirmCode;
                confirm.Confirm = true;
                TempData["ConfirmCode"] = JsonConvert.SerializeObject(ConfirmCode);
                //HttpContext.Session.SetString("ConfirmCode", JsonConvert.SerializeObject(confirm));
                return RedirectToAction(nameof(SubmitPassword));
            }
        }
        #endregion
        /////////////////////////
        

        /////////////////////
        #region SubmitPasswordAction
        [Route("/Signup/SubmitPassword")]
        [HttpGet]
        public IActionResult SubmitPassword()
        {
            TempData["ConfirmCode"] = TempData["ConfirmCode"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            //var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(HttpContext.Session.GetString("ConfirmCode"));
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCode"]);
            if (!ConfirmCode.Confirm)
            {
                return NotFound();
            }

            if (ConfirmCode.PhoneNumber == null)
            {
                return NotFound();
            }
            return View();
        }
        [Route("/Signup/SubmitPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitPassword(ViewModelSubmitPassword Passwords)
        {
            TempData["ConfirmCode"] = TempData["ConfirmCode"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            //var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>(HttpContext.Session.GetString("ConfirmCode"));
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCode"]);
            if (!ConfirmCode.Confirm)
            {
                return NotFound();
            }
            if (Passwords.Password == null || Passwords.PasswordAgain == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                return View();
            }
            else if (Passwords.Password.Trim() != Passwords.PasswordAgain.Trim() )
            {
                ModelState.AddModelError(string.Empty, "رمز ها باید یکی باشند");
                return View();
            }
            else if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "کد نامعتبر است");
                return View();
            }
            else
            {
                bl_Signup bl_Signup = new bl_Signup(_userManager);
                UserApp user = new UserApp()
                {
                    UserName = ConfirmCode.PhoneNumber.Trim(),
                    PasswordHash = Passwords.Password.Trim()
                };
                var result = await bl_Signup.SignUp(user);
                if (result == "کاربر ثبت نام شد")
                {
                    if (user.UserName == "09224982760")
                    {
                        await _userManager.AddClaimAsync(user, new Claim("AdminNumber", "1"));
                    }
                    TempData["ConfirmCode"] = null;
                    var login = await _SignInManager.PasswordSignInAsync(ConfirmCode.PhoneNumber , Passwords.Password.Trim() , true , false);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View();
                }
            }

        }
        #endregion
        /////////////////////

    }
}
