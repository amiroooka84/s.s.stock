using bll.User.bl_Basket;
using bll.User.bl_Category;
using bll.User.bl_Comments;
using bll.User.bl_Product;
using Entity;
using Entity.AttributeProduct;
using Entity.Basket;
using Entity.Interests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace Shop_Stationery.Controllers.User.ProductC
{
    public class ProductController : Controller
    {
        private readonly UserManager<UserApp> _userManager;

        public ProductController(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }
        bl_Product bl_Product = new bl_Product();

        ///////////////
        #region product
        [Route("/product/{name}")]
        public async Task<IActionResult> Product(string name)
        {
            ViewBag.title = name;
            Product product = new Product();
            product = await bl_Product.ReadProductByName(name);
            /////////////////////////
            List<Shop> shops = new List<Shop>();
            shops = bl_Product.ReadShopByProductId(product.id);
            ViewBag.Shops = shops;
            ////////////////////////
            List<Product> similarProduct = new List<Product>();
            similarProduct = bl_Product.ReadSimilarProduct(product.Categoryid).TakeLast(20).ToList();
            foreach (var item in similarProduct)
            {
                if (name == item.Name)
                {
                    similarProduct.Remove(item);
                    break;
                }
            }
            ViewBag.SimilarProduct = similarProduct;
            /////////////////////////
            bl_Category bl_Category = new bl_Category();
            string NameCategory = bl_Category.ReadNameCategory(product.Categoryid);
            ViewBag.NameCategory = NameCategory;
            //////////////////////////
            product = await bl_Product.ReadProductByName(name);
            List<ImagePath> imagePaths = new List<ImagePath>();
            imagePaths = await bl_Product.ReadImagesProduct(product.id);
            ViewBag.imagePaths = imagePaths;
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            attributeProducts = await bl_Product.ReadAttributeProduct(product.id);
            ViewBag.AttributeProduct = attributeProducts;
            List<string> NameColors = new List<string>();
            foreach (var item in attributeProducts)
            {
                NameColors.Add(item.Color);
            }
            NameColors = NameColors.Distinct(new CheckAttribute()).ToList();
            ViewBag.NameColors = NameColors.ToList();
            List<string> Size = new List<string>();
            foreach (var item in attributeProducts)
            {
                Size.Add(item.Size);
            }
            Size = Size.Distinct(new CheckAttribute()).ToList();
            ViewBag.Size = Size;
            bl_Comments bl_Comments = new bl_Comments();
            ViewBag.Comments = bl_Comments.ReadComments(product.id);
            bl_Basket bl_Basket = new bl_Basket();
            bool ExistInBasket = false;
            if (User.Identity.IsAuthenticated)
            {
                UserApp userApp = new UserApp();
                userApp = await _userManager.FindByNameAsync(User.Identity.Name);
                Product product1 = new Product();
                product1 = await bl_Product.ReadProductByName(name);
                ExistInBasket = bl_Basket.ExistInBasket(product1.id, userApp.Id);
                ViewBag.ExistInBasket = ExistInBasket;
                List<Interests> interests = new List<Interests>();
                interests = bl_Product.ReadInterests(userApp.Id);
                ViewBag.interest = false;
                foreach (var item in interests)
                {
                    if (product.id == item.Productid)
                    {
                        ViewBag.interest = true;
                    }
                }

            }
            //////////////////////////
            if (product.Name != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }
        class CheckAttribute : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x == y;
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }
        #endregion
        ///////////////

        ///////////////
        #region CalculatePrice
        [Route("/product/CalculatePrice")]
        [HttpGet]
        public async Task<IActionResult> CalculatePrice(long productid, string color, string size)
        {
            if (color == null)
            {
                color = "0";
            }
            if (size == null)
            {
                size = "0";
            }
            bl_Product bl_Product = new bl_Product();
            AttributeProduct attributeProduct = new AttributeProduct();
            attributeProduct = await bl_Product.CalculatePrice(productid, color, size);
            UserApp userApp = new UserApp();
            if (User.Identity.IsAuthenticated)
            {
                userApp = await _userManager.FindByNameAsync(User.Identity.Name);
                Entity.Basket.Basket basket = new Entity.Basket.Basket();
                basket = bl_Product.ReadItemBasket(userApp.Id, attributeProduct, productid);
                if (basket == null)
                {
                    return Json(attributeProduct);
                }
                else
                {
                    return Json("موجود در سبد خرید");
                }
            }
            else
            {
                return Json(attributeProduct);
            }
        }

        #endregion
        ///////////////

        ///////////////
        #region AddBasket
        [Route("/product/AddBasket")]
        [HttpGet]
        public async Task<IActionResult> AddBasket(long productid, string color, string size)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json("login");
            }
            if (color == null)
            {
                color = "0";
            }
            if (size == null)
            {
                size = "0";
            }
            bl_Product bl_Product = new bl_Product();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool ResultAddBasket = await bl_Product.AddBasket(userApp.Id, productid, color, size);
            return Json(ResultAddBasket);
        }

        #endregion
        ///////////////

        ///////////////
        #region AddInterests
        [Route("/product/AddInterests")]
        [HttpGet]
        public async Task<IActionResult> AddInterests(long productid)
        {
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Product.AddInterests(productid, userApp.Id);
            return Json(result);
        }
        #endregion
        ///////////////

        ///////////////
        #region DeleteInterests
        [Route("/product/DeleteInterests")]
        [HttpGet]
        public async Task<IActionResult> DeleteInterests(long productid)
        {
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Product.DeleteInterests(productid, userApp.Id);
            return Json(result);
        }
        #endregion
        ///////////////

    }
}
