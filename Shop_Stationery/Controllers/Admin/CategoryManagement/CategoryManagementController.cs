using bll.Admin.bl_Category;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.ViewModelAdmin.ViewModelCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.Admin.CategoryManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class CategoryManagementController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CategoryManagementController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //////////////////////
        #region CategoryManagement

        [Route("/CategoryManagement")]
        public IActionResult CategoryManagement()
        {
            ViewBag.Action = "/CategoryManagement";
            bl_Category bl_Category = new bl_Category();
            ViewModelCategory category = new ViewModelCategory(new ViewModelAddCategory(), bl_Category.ReadCategory().TakeLast(20).ToList());
            ViewBag.model = new ViewModelAddCategory();
            ViewBag.Categories = bl_Category.ReadCategory();
            ViewBag.Result = TempData["Result"];
            ViewBag.ResultDelete = TempData["ResultDelete"];
            ViewBag.ResultEdit = TempData["ResultEdit"];
            return View(category);
        }

        [Route("/CategoryManagement")]
        [HttpPost]
        public IActionResult CategoryManagement(ViewModelAddCategory model)
        {
            ViewBag.Action = "/CategoryManagement";
            bl_Category bl_Category = new bl_Category();
            ViewModelCategory viewModelcategory = new ViewModelCategory(new ViewModelAddCategory(), bl_Category.ReadCategory().TakeLast(20).ToList());
            ViewBag.Categories = bl_Category.ReadCategory();
            ViewBag.model = model;
            if (model.Name == null || model.Image == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را وارد کنید");
                return View(viewModelcategory);
            }
            Category category = new Category()
            {
                Name = model.Name,
                Categoryid = model.Category,
            };
            if (model.Category != 0 && !bl_Category.ExistCategoryById(category.Categoryid))
            {
                ModelState.AddModelError("Category.Category", "دسته یافت نشد");
                return View(viewModelcategory);
            }
            else if (bl_Category.ExistCategoryByName(category.Name))
            {
                ModelState.AddModelError("Category.Name", "نام تکراری است");
                return View(viewModelcategory);
            }
            else
            {
                var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Category\\" + model.Image.FileName;
                using var f = System.IO.File.Create(path);
                model.Image.CopyTo(f);
                category.ImagePath = path.ToString().Substring(52);
                var a = bl_Category.AddCategory(category);
                if (a)
                {
                    ViewBag.model = new ViewModelAddCategory();
                    TempData["Result"] = "true";
                    return Redirect("CategoryManagement");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "دسته ثبت نشد");
                    return View(viewModelcategory);
                }
            }
        }

        #endregion
        //////////////////////

        //////////////////////
        #region SearchCategory

        [Route("/CategoryManagement/SearchCategory")]
        [HttpPost]
        public IActionResult SearchCategory(ViewModelNameCategory model)
        {
            bl_Category bl_Category = new bl_Category();
            List<Category> lc = new List<Category>();
            lc = bl_Category.SearchCategory(model.Name);
            ViewBag.model = new ViewModelAddCategory();
            ViewBag.catrgories = bl_Category.ReadCategory();
            return View("CategoryManagement", new ViewModelCategory(new ViewModelAddCategory(), lc));
        }

        #endregion
        //////////////////////

        //////////////////////
        #region DeleteCategory

        [Route("/CategoryManagement/DeleteCategory")]
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            bl_Category bl_Category = new bl_Category();
            bool result = bl_Category.DeleteCategory(id);
            TempData["ResultDelete"] = result.ToString();
            return RedirectToAction("CategoryManagement");
        }

        #endregion
        //////////////////////

        //////////////////////
        #region ReadCategoryById
        [Route("/CategoryManagement/ReadCategoryById")]
        [HttpGet]
        public IActionResult ReadCategoryById(int id)
        {
            bl_Category bl_Category = new bl_Category();
            return Json(bl_Category.ReadCategoryById(id));
        }
        #endregion
        //////////////////////

        /////////////////////
        #region EditCategory
        [Route("/CategoryManagement/EditCategory")]
        [HttpPost]
        public IActionResult EditCategory(ViewModelAddCategory model , int id)
        {
            ViewBag.Action = "/CategoryManagement/EditCategory";
            bl_Category bl_Category = new bl_Category();
            ViewModelCategory viewModelcategory = new ViewModelCategory(new ViewModelAddCategory(), bl_Category.ReadCategory().TakeLast(20).ToList());
            ViewBag.Categories = bl_Category.ReadCategory();
            ViewBag.model = model;
            if (model.Name == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را وارد کنید");
                return View("CategoryManagement", viewModelcategory);
            }
            Category category = new Category()
            {
                Name = model.Name,
                Categoryid = model.Category,
            };
            if (model.Category != 0 && !bl_Category.ExistCategoryById(category.Categoryid))
            {
                ModelState.AddModelError("Category.Category", "دسته یافت نشد");
                return View("CategoryManagement", viewModelcategory);
            }
            else if (bl_Category.ExistCategoryByNameForEdit(category.Name , id))
            {
                ModelState.AddModelError("Category.Name", "نام تکراری است");
                return View("CategoryManagement", viewModelcategory);
            }
            else
            {
                if (model.Image != null)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Category\\" + model.Image.FileName;
                    using var f = System.IO.File.Create(path);
                    model.Image.CopyTo(f);
                    category.ImagePath = path.ToString().Substring(52);
                }
                var a = bl_Category.EditCategory(category , id);
                if (a)
                {
                    ViewBag.model = new ViewModelAddCategory();
                    TempData["ResultEdit"] = "true";
                    return RedirectToAction("CategoryManagement");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "دسته ویرایش نشد");
                    return View("CategoryManagement", viewModelcategory);
                }
            }           
        }
        #endregion
        /////////////////////

        /////////////////////
        #region DiscountForCategory
        [Route("/CategoryManagement/DiscountForCategory")]
        [HttpPost]
        public IActionResult DiscountForCategory(long CategoryId , int Discount)
        {
            bl_Category bl_Category = new bl_Category();
            ViewModelCategory category = new ViewModelCategory(new ViewModelAddCategory(), bl_Category.ReadCategory().TakeLast(20).ToList());
            ViewBag.model = new ViewModelAddCategory();
            ViewBag.Categories = bl_Category.ReadCategory();
            bool Resault = bl_Category.DiscountForCategory(CategoryId, Discount);
            return RedirectToAction(nameof(CategoryManagement));
        }
        #endregion
        /////////////////////

    }
}
