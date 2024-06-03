using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop_Stationery.Models.Classes.ClassesUser.ConfirmCode;
using Shop_Stationery.Models.Classes.ClassesUser.SMS;
using Shop_Stationery.Models.ViewModelUser.ViewModelLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace Shop_Stationery.Controllers.User
{
    public class LoginController : Controller
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _SignInManager;

        public LoginController(UserManager<UserApp> userManager, SignInManager<UserApp> SignInManager)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
        }

        //////////////////
        #region Login

        [Route("/Login")]
        public IActionResult Login()
        {
            TempData["ConfirmCodeLogin"] = TempData["ConfirmCodeLogin"];
            TempData["CallBackUrl"] = TempData["CallBackUrl"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            var PhoneNumber = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeLogin"]);
            if (PhoneNumber.PhoneNumber == null)
            {
                return NotFound();
            }
            return View();
        }
        [Route("/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModelLoginPassword p)
        {

            TempData["ConfirmCodeLogin"] = TempData["ConfirmCodeLogin"];
            TempData["CallBackUrl"] = TempData["CallBackUrl"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            if (p.password == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                return View();
            }
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeLogin"]);
            var user = await _userManager.FindByNameAsync(ConfirmCode.PhoneNumber);
            var login = await _SignInManager.PasswordSignInAsync(user, p.password , true , false);
            if (login.Succeeded)
            {
                if (TempData["CallBackUrl"].ToString() != null)
                {
                    return Redirect(TempData["CallBackUrl"].ToString());
                }
                    return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty , login.ToString());
                return View();
            }
        }
        #endregion
        //////////////////


        //////////////////
        #region Logout
        [Route("/Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            _SignInManager.SignOutAsync();
            return Redirect("/");
        }
        #endregion
        //////////////////


        //////////////////
        #region LoginWithCode
        [Route("/LoginWithCode")]
        public IActionResult LoginWithCode()
        {
            TempData["ConfirmCodeLogin"] = TempData["ConfirmCodeLogin"];
            TempData["CallBackUrl"] = TempData["CallBackUrl"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeLogin"]);
            ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
            if (ConfirmCode.TimeExpiration <= DateTime.Now)
            {
                Random random = new Random();
                int rnd = random.Next(10000, 99999);
                ConfirmCode.Code = rnd.ToString();
                ConfirmCode.TimeExpiration = DateTime.Now.AddMinutes(3);
                SMS s = new SMS();
                bool ResultSendCode = s.SendConfirmCode(rnd.ToString(), ConfirmCode.PhoneNumber);
                ViewBag.ConfirmCode = JsonConvert.SerializeObject(ConfirmCode);
                TempData["ConfirmCodeLogin"] = JsonConvert.SerializeObject(ConfirmCode);
            }
            return View();
        }

        [Route("/LoginWithCode")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithCode(ViewModelLoginCode Code)
        {
            TempData["ConfirmCodeLogin"] = TempData["ConfirmCodeLogin"];
            TempData["CallBackUrl"] = TempData["CallBackUrl"];
            TempData.Keep();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "Acccount");
            }
            var ConfirmCode = JsonConvert.DeserializeObject<ConfirmCode>((string)TempData["ConfirmCodeLogin"]);
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
                var User = await _userManager.FindByNameAsync(ConfirmCode.PhoneNumber);
                await _SignInManager.SignInAsync(User, true);
                return Redirect(TempData["CallBackUrl"].ToString());
            }
            
            ModelState.AddModelError(string.Empty, "لطفا بعدا تلاش کنید");
            return View();           
        }
        #endregion
        //////////////////
    }
}
