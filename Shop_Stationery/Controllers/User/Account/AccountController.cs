using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.Classes.ClassesUser.IsEmail;
using Shop_Stationery.Models.ViewModelUser.ViewModelAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;
using bll.User.bl_Address;
using bll.User.bl_Product;
using Entity.Basket;
using Entity;
using Entity.AttributeProduct;
using bll.User.bl_Order;
using Entity.Order;
using System.Text.RegularExpressions;
using Entity.Interests;
using bll.Admin.bl_TrackingCode;

namespace Shop_Stationery.Controllers.User.Account
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _SignInManager;

        public AccountController(UserManager<UserApp> userManager, SignInManager<UserApp> SignInManager)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
        }

        ////////////////////
        #region Account
        [Route("/Account")]
        public IActionResult Account()
        {
            return Redirect("/Account/DashBoard");
        }
        #endregion
        ////////////////////

        ////////////////////
        #region DashBoard
        [Route("/Account/DashBoard")]
        public async Task<IActionResult> DashBoard()
        {
            bl_Address bl_Address = new bl_Address();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Addresses = bl_Address.ReadAddress(user).ToList();
            return View();
        }
        #endregion
        ////////////////////

        ////////////////////
        #region DeleteAddress
        [Route("/Account/DeleteAddress")]
        [HttpPost]
        public async Task<IActionResult> DeleteAddress(long number)
        {
            bl_Address bl_Address = new bl_Address();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Address.DeleteAddress(number, user);
            if (result == true)
            {
                TempData["ResultDeleteAddress"] = "true";
            }
            else
            {
                TempData["ResultDeleteAddress"] = "false";

            }
            return RedirectToAction("DashBoard", "Account");
        }
        #endregion
        ////////////////////

        ////////////////////
        #region AddAddress
        [Route("/Account/AddAddress")]
        [HttpPost]
        public async Task<IActionResult> AddAddress(ViewModelAddAddress model)
        {
            bl_Address bl_Address = new bl_Address();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Address.AddAddress(model.Address , user);
            if (result == true)
            {

                TempData["ResultAddAddress"] = "true";
            }
            else
            {
                TempData["ResultAddAddress"] = "false";

            }

            return RedirectToAction("DashBoard", "Account");
        }
        #endregion
        ////////////////////

        ////////////////////
        #region EditProfile
        [Route("/Account/EditProfile")]
        public IActionResult EditProfile()
        {
            return View();
        }

        [Route("/Account/EditProfile")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(ViewModelEditProfile model)
        {
            if (model.FirstName != null && model.LastName != null)
            {
                if (!Regex.IsMatch(model.FirstName + model.LastName, @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200F ]+$"))
                {
                    ModelState.AddModelError(string.Empty, "لطفا نام و نام خانوادگی را فارسی وارد کنید");
                    ViewData["firstname"] = model.FirstName;
                    ViewData["laststname"] = model.LastName;
                    ViewData["email"] = model.Email;
                    return View();
                }
            }
            if (model.FirstName == null || model.LastName == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                ViewData["firstname"] = model.FirstName;
                ViewData["laststname"] = model.LastName;
                ViewData["email"] = model.Email;
                return View();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.Email != null)
            {
                IsEmail isEmail = new IsEmail();
                if (!isEmail.isEmail(model.Email))
                {
                    ModelState.AddModelError("Email", "لطفا ایمیل وارد کنید");
                    ModelState.AddModelError(string.Empty, "ویرایش انجام نشد");
                    ViewData["firstname"] = model.FirstName;
                    ViewData["laststname"] = model.LastName;
                    ViewData["email"] = model.Email;
                    return View();
                }
                user.Email = model.Email.Trim();
            }

            user.FirstName = model.FirstName.Trim();
            user.LastName = model.LastName.Trim();
            
            if (Regex.IsMatch(model.FirstName + model.LastName, @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200F ]+$"))
            {

            }
            var Up = await _userManager.UpdateAsync(user);
            if (Up.Succeeded)
            {
                TempData["ResultUpdate"] = "true";
                return View();
            }
            else
            {
                TempData["ResultUpdate"] = "false";
                ViewData["firstname"] = model.FirstName;
                ViewData["laststname"] = model.LastName;
                ViewData["email"] = model.Email;
                return View();
            }
        }
        #endregion
        ////////////////////

        ////////////////////
        #region EditAccountSetting
        //[Route("/Account/EditAccountSetting")]
        //public IActionResult EditAccountSetting()
        //{
        //    return View();
        //}
        #endregion
        ////////////////////

        ////////////////////
        #region ChangePassword
        [Route("/Account/ChangePassword")]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [Route("/Account/ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ViewModelChangePassword model)
        {
            if (model.PasswordNow == null || model.Password == null || model.PasswordAgain == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را تکمیل کنید");
                return View();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user , model.PasswordNow , model.Password);
            if (result.Succeeded)
            {
                TempData["ChangePassword"] = "true";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "اطلاعات وارد شده صحیح نیست");
                TempData["ChangePassword"] = "false";
            }
            return View();
        }
        #endregion
        ////////////////////

        ////////////////////
        #region Basket
        [Route("/Account/Basket")]
        public async Task<IActionResult> Basket()
        {
            bl_Product bl_Product = new bl_Product();
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            baskets = bl_Product.ReadBasket(userApp.Id);
            List<Product> products = new List<Product>();
            foreach (Product item in bl_Product.ReadProduct())
            {
                foreach (var item1 in baskets)
                {
                    if (item.id == item1.ProductId)
                    {
                        products.Add(item);
                    }
                }
            }
            List<AttributeProduct> readattributeProducts = new List<AttributeProduct>();
            readattributeProducts = bl_Product.ReadAttribute();
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            foreach (var item in baskets)
            {
                foreach (var item1 in readattributeProducts)
                {
                    if (item.AttributeId == item1.id && item1.Number > 0)
                    {
                        attributeProducts.Add(item1);
                    }
                }
            }
            ViewBag.AttributeProduct = attributeProducts;
            ViewBag.Products = products;
            return View(baskets);
        }
        #endregion
        ////////////////////

        ////////////////////
        #region Order
        [Route("/Account/Order")]
        public async Task<IActionResult> Order()
        {
            ViewBag.ResultOrder = TempData["ResultOrder"];
            if (Request.Headers["Referer"].ToString().Contains("OrderRegistration"))
            {
                if (ViewBag.ResultOrder != "ok")
                {
                    ViewBag.ResultOrder = "nok";
                }
            }

            bl_Order bl_Order = new bl_Order();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Order> orders = new List<Order>();
            orders = bl_Order.ReadOrders(userApp.Id);
            ViewBag.Orders = orders;
            List<ProductOrder> productOrders = new List<ProductOrder>();
            productOrders  = bl_Order.ReadProductOrders(userApp.Id);
            ViewBag.ProductOrders = productOrders;
            return View();
        }
        #endregion
        ////////////////////

        ////////////////////
        #region Interests
        [Route("/Account/Interests")]
        public async Task<IActionResult> Interests()
        {
            bl_Product bl_Product = new bl_Product();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Interests> interests = new List<Interests>();
            interests = bl_Product.ReadInterests(userApp.Id);
            ViewBag.Interests = interests;
            List<Product> products = new List<Product>();
            foreach (var pro in bl_Product.ReadProduct())
            {
                foreach (var inter in interests)
                {
                    if (inter.Productid == pro.id)
                    {
                        products.Add(pro);
                    }
                }
            }
            ViewBag.Products = products;
            return View();
        }

        [Route("/Account/DeleteInterests")]
        [HttpPost]
        public async Task<IActionResult> DeleteInterests(long productid)
        {
            bl_Product bl_Product = new bl_Product();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Product.DeleteInterests(productid, userApp.Id);
            return RedirectToAction(nameof(Interests));
        }
        #endregion
        ////////////////////

        ////////////////////
        #region TrackingCodes
        [Route("/Account/TrackingCodes")]
        public IActionResult TrackingCodes()
        {
            bl_TrackingCode trackingCode = new bl_TrackingCode();
            ViewBag.TrackingCodes = trackingCode.ReadTrackingCodes();
            return View();
        }
        #endregion
        ////////////////////
    }
}
