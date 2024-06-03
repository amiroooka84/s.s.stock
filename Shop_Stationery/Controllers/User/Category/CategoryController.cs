using bll.User.bl_Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.User.ReadCategory
{
    public class CategoryController : Controller
    {
        ////////////////
        #region Categories
        [Route("/Categories")]
        public IActionResult Categories()
        {
            bl_Category bl_Category = new bl_Category();
            ViewBag.Categories = bl_Category.ReadCategory();
            return View();
        }
        #endregion
        ////////////////

    }
}
