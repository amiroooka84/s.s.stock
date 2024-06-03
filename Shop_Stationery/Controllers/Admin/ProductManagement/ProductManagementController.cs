using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bll.Admin.bl_Product;
using bll.Admin.bl_Category;
using Entity;
using Shop_Stationery.Models.ViewModelAdmin.ViewModelProduct;
using bll.Admin.bl_Shop;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Shop_Stationery.Models.ViewModelAdmin.ViewModelShop;
using Entity.ProductAndShop;
using Entity.AttributeProduct;

namespace Shop_Stationery.Controllers.Admin.ProductManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductManagementController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductManagementController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        ////////////////
        #region ProductManagement
        [Route("/ProductManagement")]
        public IActionResult ProductManagement()
        {
            bl_Product bl_Product = new bl_Product();
            bl_Category bl_Category = new bl_Category();
            bl_Shop bl_Shop = new bl_Shop();
            ViewModelProduct viewModelProduct = new ViewModelProduct(new ViewModelAddProduct(), bl_Product.ReadProduct().TakeLast(10).ToList());
            ViewBag.Action = "/ProductManagement";
            ViewBag.Shops = bl_Shop.ReadShop();
            ViewModelAddProduct viewModel = new ViewModelAddProduct();
            List<AttributeProduct> attributes = new List<AttributeProduct>();
            viewModel.Attributes = attributes;
            ViewBag.model = viewModel;
            ViewBag.Categories = bl_Category.ReadCategory();
            ViewBag.Result = TempData["Result"];
            ViewBag.ResultDelete = TempData["ResultDelete"];
            ViewBag.ResultEdit = TempData["ResultEdit"];
            return View(viewModelProduct);
        }
        [Route("/ProductManagement")]
        [HttpPost]
        public IActionResult ProductManagement(ViewModelAddProduct model)
        {
            bl_Product bl_Product = new bl_Product();
            bl_Category bl_Category = new bl_Category();
            bl_Shop bl_Shop = new bl_Shop();
            ViewModelProduct viewModelProduct = new ViewModelProduct(new ViewModelAddProduct(), bl_Product.ReadProduct().TakeLast(20).ToList());
            ViewBag.model = model;
            ViewBag.Shops = bl_Shop.ReadShop();
            ViewBag.Categories = bl_Category.ReadCategory();
            if (model.Name == null || model.Slack == null || model.Code == null || model.Discription == null || model.Specifications == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را وارد کنید");
                return View(viewModelProduct);
            }
            if (model.Discount == null)
            {
                model.Discount = 0.ToString();
            }
            if (!int.TryParse(model.Code, out int Code) ||
                !int.TryParse(model.Price, out int Price) ||
                !int.TryParse(model.Discount, out int Discount))
            {
                ModelState.AddModelError(string.Empty, "اطلاعات وارد شده نادرست است");
                return View(viewModelProduct);
            }
            Product product = new Product()
            {
                Name = model.Name,
                Slack = model.Slack,
                Code = Convert.ToInt32(model.Code),
                Number = model.Attributes.Sum(i=> i.Number),
                Price = Convert.ToInt32(model.Price),
                Discount = Convert.ToInt32(model.Discount),
                Discription = model.Discription,
                Specifications = model.Specifications,
                Categoryid = model.Categoryid
            };
            if (bl_Category.ExistCategoryById(product.Categoryid))
            {
                if (model.Image != null)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Products\\" + model.Image.FileName;
                    using var f = System.IO.File.Create(path);
                    model.Image.CopyTo(f);
                    product.ImagePath = path.Substring(52);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "لطفا عکس اصلی را پر کنید");
                    return View(viewModelProduct);
                }
                List<string> vs = new List<string>();
                if (model.Images != null)
                {
                    foreach (var item in model.Images)
                    {
                        var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Products\\" + item.FileName;
                        using var f = System.IO.File.Create(path);
                        item.CopyTo(f);
                        vs.Add(path.Substring(52));
                    }
                }
                if (bl_Product.ExistProduct(product.Name, product.Code))
                {
                    ModelState.AddModelError(string.Empty, "نام یا کد کالا تکراری است");
                    return View(viewModelProduct);
                }
                else
                {
                    List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
                    foreach (var item in model.Attributes)
                    {
                        if (item.Color == null)
                        {
                            item.Color = "0";
                        }
                        if (item.Size == null)
                        {
                            item.Size = "0";
                        }
                        if (item.Size != null && item.Color != null)
                        {
                            attributeProducts.Add(item);
                        }
                    }
                    var a = bl_Product.AddProduct(product, model.Shops , vs , attributeProducts);
                    if (a)
                    {
                        ViewBag.model = new Product();
                        ViewBag.Result = true;
                        return View(viewModelProduct);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "کالا ثبت نشد");
                        return View(viewModelProduct);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Product.Categoryid", "دسته یافت نشد");
                return View(viewModelProduct);
            }

        }
        #endregion
        ////////////////


        //////////////////////
        #region SearchCategory

        [Route("/ProductManagement/SearchProduct")]
        [HttpPost]
        public IActionResult SearchProduct(ViewModelNameProduct model)
        {
            bl_Category bl_Category = new bl_Category();
            bl_Product bl_Product= new bl_Product();
            List<Product> lp = new List<Product>(); 
            bl_Shop bl_Shop = new bl_Shop();
            ViewBag.Shops = bl_Shop.ReadShop();
            lp = bl_Product.SearchProduct(model.Name);
            ViewBag.model = new ViewModelAddProduct();
            ViewBag.Categories = bl_Category.ReadCategory();
            return View("ProductManagement", new ViewModelProduct(new ViewModelAddProduct(), lp));
        }

        #endregion
        //////////////////////


        //////////////////////
        #region ReadProductById
        [AllowAnonymous]
        [Route("/ProductManagement/ReadProductById")]
        [HttpGet]
        public IActionResult ReadProductById(int id)
        {
            bl_Product bl_Product = new bl_Product();
            bl_Shop shop = new bl_Shop();
            Product product = new Product();
            product = bl_Product.ReadProductById(id);
            List<ProductAndShop> productAndShops = new List<ProductAndShop>();
            foreach (var item in shop.ReadShopAndProduct())
            {
                if (item.Productid == product.id)
                {
                    productAndShops.Add(item);
                }
            }
            List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
            attributeProducts = bl_Product.ReadAttributeProduct(id);
            ViewModelProductAndShop productAndShop = new ViewModelProductAndShop(product, productAndShops , attributeProducts);
            return Json(productAndShop);
        }
        #endregion
        //////////////////////

        /////////////////////
        #region EditProduct
        [Route("/ProductManagement/EditProduct")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(ViewModelAddProduct model , int id)
        {
            
            ViewBag.Action = "/ProductManagement/EditProduct"; 
            bl_Product bl_Product = new bl_Product();
            bl_Category bl_Category = new bl_Category();
            bl_Shop bl_Shop = new bl_Shop();
            ViewModelProduct viewModelProduct = new ViewModelProduct(new ViewModelAddProduct(), bl_Product.ReadProduct().TakeLast(20).ToList());
            ViewBag.model = model;
            ViewBag.Shops = bl_Shop.ReadShop();
            ViewBag.SelectShops = model.Shops;
            ViewBag.Categories = bl_Category.ReadCategory();
            if (model.Name == null || model.Code == null || model.Discription == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را وارد کنید");
                return View("ProductManagement", viewModelProduct);
            }
            if (model.Discount == null)
            {

                model.Discount = 0.ToString();
            }
            if (!int.TryParse(model.Code, out int Code) ||
                !int.TryParse(model.Price, out int Price) ||
                !int.TryParse(model.Discount, out int Discount))
            {
                ModelState.AddModelError(string.Empty, "اطلاعات وارد شده نادرست است");
                return View("ProductManagement", viewModelProduct);
            }
            Product product = new Product()
            {
                Name = model.Name,
                Slack = model.Slack,
                Code = Convert.ToInt32(model.Code),
                Number = model.Attributes.Sum(i => i.Number),
                Price = Convert.ToInt32(model.Price),
                Discount = Convert.ToInt32(model.Discount),
                Discription = model.Discription,
                Specifications = model.Specifications,
                Categoryid = model.Categoryid
            };
            if (bl_Category.ExistCategoryById(product.Categoryid))
            {
                if (model.Image != null)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Products\\" + model.Image.FileName;
                    using var f = System.IO.File.Create(path);
                    model.Image.CopyTo(f);
                    product.ImagePath = path.Substring(52);
                }
                List<string> vs = new List<string>();
                if (model.Images != null)
                {
                    foreach (var item in model.Images)
                    {
                        var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Products\\" + item.FileName;
                        using var f = System.IO.File.Create(path);
                        item.CopyTo(f);
                        vs.Add(path.Substring(52));
                    }
                }
                if (await bl_Product.ExistProductForEdit(product.Name, product.Code , id))
                {
                    ModelState.AddModelError(string.Empty, "نام یا کد کالا تکراری است");
                    return View("ProductManagement", viewModelProduct);
                }
                else
                {
                    List<AttributeProduct> attributeProducts = new List<AttributeProduct>();
                    foreach (var item in model.Attributes)
                    {
                        if (item.Color == null && item.Size != null)
                        {
                            item.Color = "0";
                        }
                        if (item.Size == null && item.Color != null)
                        {
                            item.Size = "0";
                        }
                        if (item.Size != null && item.Color != null)
                        {
                            attributeProducts.Add(item);
                        }
                    }
                    bool a = await bl_Product.EditProduct(product, model.Shops ,vs, attributeProducts , id);
                    if (a)
                    {
                        ViewBag.model = new Product();
                        TempData["ResultEdit"] = true;
                        return RedirectToAction("ProductManagement");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "کالا ثبت نشد");
                        return View("ProductManagement", viewModelProduct);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Product.Categoryid", "دسته یافت نشد");
                return View("ProductManagement", viewModelProduct);
            }
        }
        #endregion
        /////////////////////

        //////////////////////
        #region DeleteProduct

        [Route("/ProductManagement/DeleteProduct")]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            bl_Product bl_Product = new bl_Product();
            bool result = bl_Product.DeleteProduct(id);
            TempData["ResultDelete"] = result;
            return RedirectToAction("ProductManagement");
        }

        #endregion
        //////////////////////

    }
}
