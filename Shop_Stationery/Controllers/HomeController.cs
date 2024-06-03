using static Entity.UserApp.userapp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop_Stationery.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using bll;
using Entity;
using bll.Admin.bl_Product;
using bll.Admin.bl_Category;
using System.Security.Claims;

namespace Shop_Stationery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<UserApp> _SignInManager;
        private readonly UserManager<UserApp> _UserManager;
        public HomeController(ILogger<HomeController> logger , SignInManager<UserApp> SignInManager , UserManager<UserApp> UserManager)
        {
            _logger = logger;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }

        public async Task<IActionResult> Index()
        {
            bl_Product bl_Product = new bl_Product();
            bl_Category bl_Category= new bl_Category();
            List<Product> BestOffer1 = new List<Product>();
            BestOffer1 = bl_Product.ReadProduct();
            var BestOffer = from i in BestOffer1 orderby i.Discount descending select i;
            ViewBag.BestOffer1 = BestOffer.Take(12).ToList();
            ViewBag.BestOffer2 = BestOffer.Skip(12).Take(12).ToList();
            ////////
            ViewBag.Category = bl_Category.ReadCategory().TakeLast(10).ToList();
            int a = 1234567;
            string b = a.ToString("n0");
            //if (User.Identity.Name == "09224982760")
            //{
            //    UserApp userApp = new UserApp();
            //    userApp = await _UserManager.FindByNameAsync(User.Identity.Name);
            //    await _UserManager.AddClaimAsync(userApp, new Claim("AdminNumber", "1"));
            //}
            return View(); 
        }

        [Route("/درباره ما")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/ارتباط با ما")]
        public IActionResult Relationship()
        {
            return View();
        }

        [Route("/راهنمای سایز")]
        public IActionResult HelpForSize()
        {
            return View();
        }

        [Route("/سوالات متداول")]
        public IActionResult Questions()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
