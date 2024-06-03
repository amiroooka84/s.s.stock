using bll.User.bl_Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;
using bll.User.bl_Product;
using Entity;

namespace Shop_Stationery.Controllers.User.Basket
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly UserManager<UserApp> _userManager;

        public BasketController(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        ////////////////
        #region AddNumberProduct
        [Route("/Basket/AddNumberProduct")]
        [HttpGet]
        public IActionResult AddNumberProduct(string Number, string attrId, string basket)
        {
            bl_Basket bl_Basket = new bl_Basket();
            bool Result = bl_Basket.InventoryNumberProduct(Convert.ToInt64(Number), Convert.ToInt64(attrId), Convert.ToInt64(basket));
            return Json(Result);
        }

        #endregion
        ////////////////

        ////////////////
        #region DeleteBasket
        [Route("/Basket/DeleteBasket")]
        [HttpGet]
        public async Task<IActionResult> DeleteBasket(long id)
        {
            bl_Basket bl_Basket = new bl_Basket();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool Result = await bl_Basket.DeleteBasket(id, userApp.Id);
            return Json(Result);
        }
        #endregion
        ////////////////


        ////////////////
        #region CountBasket
        [Route("/Basket/CountBasket")]
        [HttpGet]
        public async Task<IActionResult> CountBasket(long id)
        {
            bl_Product bl_Product = new bl_Product();
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            UserApp userApp = new UserApp();
            if (User.Identity.IsAuthenticated)
            {
                userApp = await _userManager.FindByNameAsync(User.Identity.Name);
                baskets = bl_Product.ReadBasket(userApp.Id);
                return Json(baskets.Count);
            }
            return Json(0);
        }
        #endregion
        ////////////////

        ////////////////
        #region ReadBasket
        [Route("/Basket/ReadBasket")]
        [HttpGet]
        public async Task<IActionResult> ReadBasket()
        {
            bl_Product bl_Product = new bl_Product();
            List<Entity.Basket.Basket> baskets = new List<Entity.Basket.Basket>();
            UserApp userApp = new UserApp();
            if (User.Identity.IsAuthenticated)
            {
                userApp = await _userManager.FindByNameAsync(User.Identity.Name);
                baskets = bl_Product.ReadBasket(userApp.Id);
                return Json(baskets);
            }
            return Json("login");
        }
        #endregion
        ////////////////

        ////////////////
        #region ReadBasket
        [Route("/Basket/ReadProduct")]
        [HttpGet]
        public IActionResult ReadProduct(long ProductId)
        {
            bl_Product bl_Product = new bl_Product();
            Product product = new Product();
            product = bl_Product.ReadProductById(ProductId);
            return Json(product);
        }
        #endregion
        ////////////////
    }
}
