using bll.Admin.bl_Shop;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.ViewModelAdmin.ViewModelShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.Admin.ShopManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class ShopManagementController : Controller
    {
        //////////////////////
        #region ShopManagement
        [Route("/ShopManagement")]
        public IActionResult ShopManagement()
        {
            ViewBag.model = new ViewModelAddShop();
            bl_Shop bl_Shop = new bl_Shop();
            ViewBag.Shops = bl_Shop.ReadShop();
            ViewBag.Result = TempData["Result"];
            ViewBag.ResultDelete = TempData["ResultDelete"];
            ViewBag.ResultEdit = TempData["ResultEdit"];
            return View();
        }

        [Route("/ShopManagement")]
        [HttpPost]
        public IActionResult ShopManagement(ViewModelAddShop model)
        {
            bl_Shop bl_Shop = new bl_Shop();
            ViewBag.model = model;
            ViewBag.Shops = bl_Shop.ReadShop();
            if (model.Name == null || model.NumberPhone == null || model.Address == null || model.PostingDay == 0.ToString() || model.PostingDay == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را وارد کنید");
                return View();
            }
            else if (model.NumberPhone.Length == 11 || model.NumberPhone.Length == 8)
            {
                Shop shop = new Shop();
                shop.Name = model.Name;
                shop.Address = model.Address;
                shop.NumberPhone = model.NumberPhone;
                shop.PostingDay = Convert.ToInt32(model.PostingDay);
                bool Result = bl_Shop.AddShop(shop);
                TempData["Result"] = Result;
                return View();
            }
            else
            {
                ModelState.AddModelError("NumberPhone", "شماره تماس مغتبر نمی باشد");
                return View();
            }
        }

        #endregion
        //////////////////////

        //////////////////////
        #region DeleteShop

        [Route("/ShopManagement/DeleteShop")]
        [HttpPost]
        public IActionResult DeleteShop(int id)
        {
            bl_Shop bl_Shop = new bl_Shop();
            bool result = bl_Shop.DeleteShop(id);
            TempData["ResultDelete"] = result;
            return RedirectToAction("ShoptManagement");
        }

        #endregion
        //////////////////////
    }
}
