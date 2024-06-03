using bll.User.bl_Product;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.User.Compare
{
    public class CompareController : Controller
    {
        ////////////////
        #region SelectProduct
        [Route("/Compare/SelectProduct")]
        [HttpGet]
        public async Task<IActionResult> SelectProduct(string Product1, string Product2, string Search)
        {
            if (Product1 == null)
            {
                return Redirect("/");
            }
            bl_Product bl_Product = new bl_Product();
            ViewBag.Product1 = Product1;
            if (Product2 == null)
            {
                Product product = new Product();
                product = await bl_Product.ReadProductByName(Product1);
                if (Search != null)
                {
                    ViewBag.Products = bl_Product.SearchProduct(Search).Result.Take(50);
                }
                else
                {
                    ViewBag.Products = bl_Product.ReadSimilarProduct(product.Categoryid).Take(30);
                }
                return View();
            }
            else
            {
                Product product1 = new Product();
                Product product2 = new Product();
                product1 = await bl_Product.ReadProductByName(Product1);
                product2 = await bl_Product.ReadProductByName(Product2);
                List<Product> products = new List<Product>();
                products.Add(product1);
                products.Add(product2);
                ViewBag.products = products;
                return View("Compare");
            }
        }

        #endregion
        ////////////////

        ////////////////
        #region CompareProducts
        public async Task<IActionResult> CompareProducts(string NameProduct)
        {
            TempData["product"] = TempData["product"];
            ViewBag.products = ViewBag.products;
            bl_Product bl_Product = new bl_Product();
            Product product = new Product();
            product = await bl_Product.ReadProductByName(NameProduct);
            List<Product> products = new List<Product>();
            products.Add((Product)TempData["product"]);
            products.Add(product);
            ViewBag.products = products;
            TempData.Keep();
            return View();
        }
        #endregion
        ////////////////

    }
}
