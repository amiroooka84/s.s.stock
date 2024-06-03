using bll.User.bl_Category;
using bll.User.bl_Product;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.ViewModelUser.ViewModelSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.User.SearchProduct
{
    public class SearchProductController : Controller
    {
        //////////////////
        #region Search
        //[Route("/SearchProduct/Search")]
        //[HttpGet]
        //public async Task<IActionResult> Search(ViewModelSearchByFilter Search1)
        //{
        //    ViewModelSearchByFilter viewModelSearch = new ViewModelSearchByFilter();
        //    viewModelSearch.Search = Search1.Search;
        //    ViewBag.ViewModelSearch = viewModelSearch;
        //    bl_Product bl_Product = new bl_Product();
        //    bl_Category bl_Category = new bl_Category();
        //    List<Category> categories = new List<Category>();
        //    categories = bl_Category.ReadCategory();
        //    ViewBag.categories = categories;
        //    List<Product> products = new List<Product>();
        //    if (Search1.Search == null)
        //    {
        //        Random rnd = new Random();
        //        long num = rnd.Next(1 , categories.Count);
        //        products = bl_Product.ReadSimilarProduct(num);
        //        return View(products.Take(20).ToList());
        //    }
        //    else
        //    {
        //        products = await bl_Product.SearchProduct(Search1.Search);
        //    }
        //    return View(products);
        //}
        #endregion
        //////////////////

        //////////////////
        #region SearchByFilter
        [Route("/SearchProduct/Search")]
        [HttpGet]
        public async Task<IActionResult> SearchByFilter(ViewModelSearchByFilter Search1)
        {
            ViewModelSearchByFilter viewModelSearch = new ViewModelSearchByFilter();
            viewModelSearch = Search1;
            if (viewModelSearch.Page == 0)
            {
                viewModelSearch.Page = 1;
            }
            ViewBag.ViewModelSearch = viewModelSearch;
            bl_Category bl_Category = new bl_Category();
            List<Category> categories = new List<Category>();
            categories = bl_Category.ReadCategory();
            ViewBag.categories = categories;
            bl_Product bl_Product = new bl_Product();
            List<Product> products = new List<Product>();
            List<Product> ResultSearch = new List<Product>();
            ResultSearch = await bl_Product.SearchProductByFilter(Search1.Search , Search1.Order , Search1.Categories);
            float numpage =  ResultSearch.Count / 20f;
            ViewBag.NumPage = Convert.ToInt32(Math.Ceiling(numpage));//گرد کردن عدد اعشار         
            if (products != null)
            {
                products = ResultSearch.Skip(Search1.Page * 20 - 20).Take(20).ToList();
                if (products.Count == 0)
                {
                    products = ResultSearch.Skip(1 * 20 - 20).Take(20).ToList();
                    viewModelSearch.Page = 1;
                    ViewBag.ViewModelSearch = viewModelSearch;
                }
            }

            return View("Search" , products);
        }
        #endregion
        //////////////////
    }
}
